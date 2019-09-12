
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	calls place.usp_UpdateGeonames and surveillance.usp_UpdateArticleApi
--				Output: main switchboard, calls other SPs, finishes all updating
-- =============================================

create PROCEDURE surveillance.usp_UpdateSuggestedEventsApi
	--'api-dev1.ad.bluedot.global:83'
	@serviceDomainName varchar(128),--'api-prod1.ad.bluedot.global', 'dw1-ubuntu.ad.bluedot.global:81'
	@resultMessage	varchar(500) OUTPUT --@Response not loop
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		Set @resultMessage='UpdateSuggestedEvents: '
		Declare @Response NVARCHAR(max)
		Declare @json NVARCHAR(max)=''
		Declare @inputJson NVARCHAR(max)
		Declare @urlStr varchar(500) -- dev and prod api urls are different
		Set @urlStr=@serviceDomainName

		--1. meta
		--get last pulling time
		Declare @startDate char(19)
		--not first time
		If Exists (Select 1 From surveillance.[LastSuggestedEventPullDate])
			Set @startDate=convert(varchar, 
				(Select Top 1 DATEADD(SECOND, 0.1, SystemLastModifiedDate) From surveillance.[LastSuggestedEventPullDate]), 126)

		--2. retrieve from API
		--2.1 API input string
		--first time
		If @startDate IS NULL
			Set @inputJson='http://' + @urlStr + '/api/v1/Surveillance/SuggestedEvents?countryAggregate=true&suggestedEventType=3&userAction=null'
		--not first time
		Else
			Set @inputJson='http://' + @urlStr + '/api/v1/Surveillance/SuggestedEvents?systemLastModifiedDate='
				+ @startDate + '&countryAggregate=true&suggestedEventType=3&userAction=null'
		
		EXECUTE bd.InvokeService @inputJson, @Response OUT

		--2.2 tmp table to store data
		Declare @tbl_raw table (suggestedEventId varchar(30), EventTitle varchar(200), SystemLastModifiedDate datetime,
			DiseaseId int, Highlights varchar(max), json_locations nvarchar(max), json_articles nvarchar(max), 
			json_reasons nvarchar(max))
		--populate tmp table
		INSERT INTO @tbl_raw(suggestedEventId, EventTitle, SystemLastModifiedDate, DiseaseId, Highlights,
			json_locations, json_articles, json_reasons)
		Select suggestedEventId, title, systemLastModifiedDate, JSON_VALUE(disease, '$.diseaseId'), highlights,
			locations, articles, reason
		FROM OPENJSON(@Response)
			WITH (
				suggestedEventId varchar(30),
				title varchar(200),
				systemLastModifiedDate char(19),
				highlights varchar(max),
				disease nvarchar(max) AS JSON,
				locations nvarchar(max) AS JSON,
				articles nvarchar(max) AS JSON,
				reason nvarchar(max) AS JSON
				) as f1

		--3. prepare
		--already exists
		Declare @tbl_oldEvents table (SuggestedEventId varchar(30))
		Insert into @tbl_oldEvents
			Select f1.SuggestedEventId
			From [surveillance].[SuggestedEvent] as f1, @tbl_raw as f2
			Where f1.SuggestedEventId=f2.suggestedEventId
		--new events
		Declare @tbl_newEvents table (SuggestedEventId varchar(30))
		Insert into @tbl_newEvents
			Select SuggestedEventId From @tbl_raw
			Except
			Select SuggestedEventId From @tbl_oldEvents

		--4. process
		--4.1 [SuggestedEvent] 
		--new to insert
		Insert into [surveillance].[SuggestedEvent](SuggestedEventId, EventTitle, 
			SystemLastModifiedDate, DiseaseId, Highlights)
		Select f1.suggestedEventId, EventTitle, SystemLastModifiedDate, DiseaseId, Highlights
		From @tbl_raw as f1, @tbl_newEvents as f2
		Where f1.suggestedEventId=f2.SuggestedEventId
		--existing to update 
		Update [surveillance].[SuggestedEvent]
		Set EventTitle=f2.EventTitle, SystemLastModifiedDate=f2.SystemLastModifiedDate, 
			DiseaseId=f2.DiseaseId, Highlights=f2.Highlights
		From [surveillance].[SuggestedEvent] as f1, @tbl_raw as f2
		Where f1.SuggestedEventId=f2.suggestedEventId

		--4.2 LastSuggestedEventPullDate
		--first time
		If @startDate IS NULL
			Insert into [surveillance].[LastSuggestedEventPullDate]
				Select MAX([SystemLastModifiedDate]) From [surveillance].[SuggestedEvent]
		Else --not first time
		Begin
			Declare @maxModifiedDate datetime=(Select MAX([SystemLastModifiedDate]) From [surveillance].[SuggestedEvent])
			If @maxModifiedDate IS NOT NULL and @maxModifiedDate>@startDate
				Update [surveillance].[LastSuggestedEventPullDate]
				Set [SystemLastModifiedDate]=@maxModifiedDate
		End

		--4.3 [Xtbl_SuggestedEvent_Reason] 111
		--delete old ones
		Delete from [surveillance].Xtbl_SuggestedEvent_Reason
		Where SuggestedEventId in 
			(Select SuggestedEventId From @tbl_oldEvents)
		--insert
		Insert into [surveillance].Xtbl_SuggestedEvent_Reason(SuggestedEventId, Reason)
			Select suggestedEventId, value
			From @tbl_raw
			CROSS APPLY OPENJSON (json_reasons) 

		--[Xtbl_SuggestedEvent_Article] 553
		--delete old ones
		Delete from [surveillance].Xtbl_SuggestedEvent_Article
		Where SuggestedEventId in 
			(Select SuggestedEventId From @tbl_oldEvents)
		--insert
		Insert into [surveillance].Xtbl_SuggestedEvent_Article(SuggestedEventId, ArticleId)
			Select suggestedEventId, _id
			From @tbl_raw
			CROSS APPLY OPENJSON (json_articles)
				WITH (_id varchar(128)) as f2

		--[Xtbl_SuggestedEvent_Location] 143
		--delete old ones
		Delete from [surveillance].Xtbl_SuggestedEvent_Location
		Where SuggestedEventId in 
			(Select SuggestedEventId From @tbl_oldEvents)
		--insert
		Insert into [surveillance].Xtbl_SuggestedEvent_Location(SuggestedEventId, GeonameId)
			Select suggestedEventId, bdGeonameId
			From @tbl_raw
			CROSS APPLY OPENJSON (json_locations)
				WITH (bdGeonameId int) as f2	
	--action!
		If CHARINDEX('[',@Response)<>1
			Set @resultMessage = @resultMessage + @Response
		Else
			Set @resultMessage = @resultMessage + 'Success'
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update systems in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END