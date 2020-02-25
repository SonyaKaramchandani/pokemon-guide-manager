
-- =============================================
-- Author:		Vivian
-- Create date: 2020-02 
-- Description:	Insert risk values into EventImportationRisksByGeonameSpreadMd if @GeonameId not exists
--				regardless if events are local spread or not
-- Output: 1-success, 0-@GeonameId already in table, -1-failed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetImportationRiskByGeonameIdSpreadMd
	@GeonameId as int --user aoi
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		If Not exists (Select 1 From zebra.EventImportationRisksByGeoname Where GeonameId=@GeonameId)
		BEGIN
			--A. event location
			--1. Valid events
			Declare @tbl_events table (EventId int)
			Insert into @tbl_events
				Select EventId From surveillance.[Event] Where EndDate IS NULL
			--2. Local events
			Declare @tbl_localSpreadEvent table (EventId int)
			Insert into @tbl_localSpreadEvent
				Select EventId 
				From bd.ufn_GetLocalEventsByGeoname (@GeonameId)

			--B. aoi information
			Declare @locType int,@point GEOGRAPHY
			Select @locType=LocationType, @point=Shape
				From place.Geonames
				Where GeonameId=@GeonameId

			--C. Destination risks
			--1. find dest grids
			Declare @tbl_userGrid table (GridId nvarchar(12))
			If @locType=2	--city
				Insert into @tbl_userGrid (GridId)
					Select gridId
					From [bd].[HUFFMODEL25KMWORLDHEXAGON]
					Where SHAPE.STIntersects(@point) = 1
			Else If @locType=4	--province
				Insert into @tbl_userGrid (GridId)
					Select GridId
					From [zebra].[GridProvince] as f4
					Where @GeonameId=Adm1GeonameId
			Else If @locType=6	--country
				Insert into @tbl_userGrid (GridId)
					Select GridId
					From [zebra].GridCountry as f6
					Where @GeonameId=f6.CountryGeonameId
			--2. calculate risk values based on dest airports (similar as 10(a)12(a), but derived from 10(b)12(b))
			Declare @DestinationCatchmentThreshold decimal(5,2)
				=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='DestinationCatchmentThreshold');
			With T1 as (
				Select Distinct f0.EventId, f4.DestinationStationId, f4.MinProb, f4.MaxProb, f4.MinExpVolume, f4.MaxExpVolume
				From @tbl_events as f0, @tbl_userGrid as f1, [zebra].[EventDestinationGridSpreadMd] as f2, 
					[zebra].[GridStation] as f3, zebra.EventDestinationAirportSpreadMd as f4
				Where MONTH(f3.ValidFromDate)=MONTH(GETUTCDATE()) and f3.Probability>=@DestinationCatchmentThreshold
					and f0.EventId=f2.EventId and f0.EventId=f4.EventId and f1.GridId=f2.GridId  
					and f2.GridId=f3.GridId and f3.StationId=f4.DestinationStationId
				)
			Insert into zebra.EventImportationRisksByGeonameSpreadMd(GeonameId, EventId, MinProb, MaxProb, MinVolume, MaxVolume, LocalSpread)
				Select @GeonameId, EventId, 1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(MinProb, 1)),0))), 
					1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(MaxProb, 1)),0))), SUM(MinExpVolume), SUM(MaxExpVolume), 0
				From T1
				Group by EventId;
			--3. no risk value events
			With T1 as (
				Select EventId From @tbl_events
				Except
				Select EventId From zebra.EventImportationRisksByGeonameSpreadMd
				Where GeonameId=@GeonameId
				)
			Insert into zebra.EventImportationRisksByGeonameSpreadMd(GeonameId, EventId, MinProb, MaxProb, MinVolume, MaxVolume, LocalSpread)
				Select @GeonameId, EventId, 0, 0, 0, 0, 0
				From T1
			--4. local spread yes
			Update zebra.EventImportationRisksByGeonameSpreadMd
				Set LocalSpread=1
				From zebra.EventImportationRisksByGeonameSpreadMd as f1, @tbl_localSpreadEvent as f2
				Where f1.GeonameId=@GeonameId and f1.EventId=f2.EventId

			-- success
			Select 1 as Result
		END 
		ELSE
			--already exists, not updated
			Select 0 as Result
	--action!
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;
END