-- =============================================
-- Author:		Vivian
-- Create date: 2019-09 
-- Description:	find out if source data from api modified by comparing [bd].[DiseaseMapUpdateDate], then update the table
-- Input: DS api string base part, mapId
-- Output: 1-Source data from api modified, 0-not modified or error
-- =============================================
CREATE PROCEDURE [bd].usp_GeorgeGetDiseaseMapUpdateInfo 
	@serviceBaseUrlProd varchar(128),
	@mapId int --111:MaxValue, 107:GCS
AS
BEGIN
    SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
		Declare @Response NVARCHAR(max)
		Declare @json NVARCHAR(max)=''
		Declare @inputJson NVARCHAR(max)
		Declare @urlStr varchar(500) -- dev and prod api urls are different
		Set @urlStr=@serviceBaseUrlProd

		--1. get lastUpdate date from table
		Declare @BlueDotAPI varchar(200)
		If CHARINDEX('dw1-ubuntu', @serviceBaseUrlProd)>0
			Set @BlueDotAPI='staging'
		Else
			Set @BlueDotAPI='prod'
		--date from table
		Declare @LastModifiedDateFromTable date=
			(Select LastModifiedDate From [bd].[DiseaseMapUpdateDate]
				Where MapId=@mapId and BlueDotAPI=@BlueDotAPI
			)

		--2. retrieve from API
		Set @inputJson= @urlStr + 'Models/ModelOutputs?modelOutputId=' 
						+ convert(varchar(20), @mapId)
		
		EXECUTE bd.InvokeService @inputJson, @Response OUT
		--date from API
		Declare @LastModifiedDateFromAPI date=
			(select lastModified From OPENJSON(@Response)
				WITH (lastModified date)
			)

		--3. compare
		If @LastModifiedDateFromAPI>@LastModifiedDateFromTable
		Begin
			--update lastModified by DS
			Update [bd].[DiseaseMapUpdateDate] Set LastModifiedDate=@LastModifiedDateFromAPI
				Where MapId=@mapId and BlueDotAPI=@BlueDotAPI
			--update when devOp updated
			Update [bd].[DiseaseMapUpdateDate] Set LastModifiedDate=GETDATE()
				Where MapId=@mapId and BlueDotAPI=CONCAT(@BlueDotAPI,'DevOp')
			--return true
			Select CONVERT(bit, 1) as IsUpdated
		End
		Else
			Select CONVERT(bit, 0) as IsUpdated
		COMMIT TRAN

	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select CONVERT(bit, 0) as IsUpdated
	END CATCH;
END

GO

