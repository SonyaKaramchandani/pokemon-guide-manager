
-- =============================================
-- Author:		Vivian
-- Create date: 2019-06 
-- Description:	Returns increased cases only within a week
-- Input: EventId
-- Output: Return local spread paid users list of UserId, UserEmail, LocationName, LocationDisplayName, DeltaRptCases, TotalCases
-- 7-day range is between eventDate and Now
-- Local is related to increased locations only
-- 2019-07 name changed	
-- 2019-07 calls bd.ufn_ZebraGetLocalUserLocationsByGeonameId
-- 2019-08 relevanceType of user's interested disease: 3-remove from email
-- 2019-11 adds location type in output
-- 2019-11 kind of replaced by usp_ZebraEventGetProximalUsersByEventId
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEmailGetProximalEmailData
	@EventId    AS INT
AS
BEGIN
	SET NOCOUNT ON;

	If (Select EndDate From [surveillance].[Event] Where EventId=@EventId) IS NULL
		And Exists (Select 1 from surveillance.Xtbl_Event_Location_history Where EventId=@EventId)
	BEGIN --1 active event, and not new
		
		Declare @diseaseId int=(Select DiseaseId from [surveillance].[Event] Where EventId=@EventId)
		--1. event locations
		--1.1 location case info
		Declare @tbl_eventLocations table (GeonameId int, [Name] nvarchar(200), DisplayName nvarchar(500),
											EventDate Date, RepCases int, RepCasesOld int, DeltaRepCases int, 
											SeqId int, LocationType int)
		Insert into @tbl_eventLocations(GeonameId, RepCases, RepCasesOld, EventDate)
			Select f1.GeonameId, f1.RepCases, f2.RepCases, f2.EventDate
			From surveillance.Xtbl_Event_Location as f1 
				left join surveillance.Xtbl_Event_Location_history as f2 on f1.GeonameId=f2.GeonameId
			Where f1.EventId=@EventId and f2.EventId=@EventId and f2.EventDateType=1
		
		--1.2 delete cases no-change and decrease locations or out of 7-day changes
		Delete from @tbl_eventLocations Where RepCases<=RepCasesOld OR DATEDIFF(d, EventDate, GETUTCDATE())>6
		
		If exists (Select 1 from @tbl_eventLocations)
		Begin --2
			--User X EventLoc
			Declare @tbl_UserIdEventLocation table (UserId nvarchar(128), EventGeonameId int);

			--1.3 add geo info and delta
			With T1 as (
				select GeonameId, ROW_NUMBER() OVER ( order by GeonameId) as SeqId
				from @tbl_eventLocations
				)
			Update @tbl_eventLocations 
				Set [Name]=f2.[Name], DisplayName=f2.DisplayName, DeltaRepCases=RepCases-ISNULL(RepCasesOld, 0),
					SeqId=T1.SeqId, LocationType=f2.LocationType
				From @tbl_eventLocations as f1, [place].[ActiveGeonames] as f2, T1
				Where f1.GeonameId=f2.GeonameId and f1.GeonameId=T1.GeonameId
			
			--2. find local users
			Declare @i int=1
			Declare @maxSeqId int =(Select Max(SeqId) From @tbl_eventLocations)
			Declare @thisLocation int
			While @i<=@maxSeqId
			Begin
				Set @thisLocation=(Select GeonameId From @tbl_eventLocations Where SeqId=@i)
				Insert into @tbl_UserIdEventLocation(UserId, EventGeonameId)
					Select UserId, @thisLocation
					From bd.ufn_ZebraGetLocalUserLocationsByGeonameId(@thisLocation, 1, 1, 0, @diseaseId)
				
				Set @i=@i+1
			End

			--3. Total cases is the total increased cases
			Declare @totalCases int =(Select RepCases From bd.ufn_TotalCaseCountByEventId(@EventId, 0))

			--4. Output
			Select distinct f1.UserId, f2.Email, f2.DoNotTrackEnabled, f2.EmailConfirmed, f3.[Name] as LocationName, f3.DisplayName as LocationDisplayName, 
					f3.DeltaRepCases, @totalCases as TotalCases, f3.EventDate, f3.LocationType
				From @tbl_UserIdEventLocation as f1, dbo.AspNetUsers as f2, @tbl_eventLocations as f3
				Where f1.UserId=f2.Id and f1.EventGeonameId=f3.GeonameId
		End --2
		Else
			Select TOP (0) '-' as UserId, '-' as Email, '-' as DoNotTrackEnabled, convert(bit, 0) as EmailConfirmed, '-' as LocationName, '-' as LocationDisplayName, 
					0 as DeltaRepCases, 0 as TotalCases, '1900-01-01' as EventDate, 0 as LocationType
	END --1 active event, and not new
	ELSE
		Select TOP (0) '-' as UserId, '-' as Email, '-' as DoNotTrackEnabled, convert(bit, 0) as EmailConfirmed, '-' as LocationName, '-' as LocationDisplayName, 
				0 as DeltaRepCases, 0 as TotalCases, '1900-01-01' as EventDate, 0 as LocationType
END