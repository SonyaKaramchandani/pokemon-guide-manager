
-- =============================================
-- Author:		Vivian
-- Create date: 2020-01 
-- Description:	Insert risk values into EventImportationRisksByGeoname if @GeonameId not exists
--				regardless if events are local spread or not
-- Output: 1-success, 0-@GeonameId already in table, -1-failed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetImportationRiskByGeonameId
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
			--2. Events by locs
			Declare @tbl_eventByLoc table (EventId int, EventGeonameId int)
			Insert into @tbl_eventByLoc(EventId, EventGeonameId)
				Select f1.EventId, f1.GeonameId
				From surveillance.Xtbl_Event_Location as f1, @tbl_events as f2
				Where f1.EventId=f2.EventId
			--3. eventLoc only
			Declare @tbl_eventLoc table (EventGeonameId int, LocationType int, Admin1GeonameId int, CountryGeonameId int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), CityBuffer GEOGRAPHY)
			Insert into @tbl_eventLoc(EventGeonameId, LocationType, Admin1GeonameId, CountryGeonameId, Latitude, Longitude)
				Select distinct f1.EventGeonameId, f2.LocationType, f2.Admin1GeonameId, f2.CountryGeonameId,
					f2.Latitude, f2.Latitude
				From @tbl_eventByLoc as f1, place.Geonames as f2
				Where f1.EventGeonameId=f2.GeonameId

			--B. aoi information
			Declare @locType int, @admin1GeonameId int, @countyGeonameId int, @Latitude Decimal(10, 5), @Longitude Decimal(10, 5)
			Select @locType=LocationType, @admin1GeonameId=Admin1GeonameId, @countyGeonameId=CountryGeonameId, 
					@Latitude=Latitude, @Longitude=Longitude
				From place.Geonames
				Where GeonameId=@GeonameId
			--city point
			Declare @intputCityPoint GEOGRAPHY
			If @locType=2
				Set @intputCityPoint=(geography::Point(@Latitude, @Longitude, 4326))
			
			--C. local spread
			Declare @tbl_localSpreadLoc table (EventGeonameId int)
			Declare @tbl_localSpreadEvent table (EventId int)
			--1. using admin level
			Insert into @tbl_localSpreadLoc
				Select EventGeonameId
				From @tbl_eventLoc
				Where EventGeonameId=@GeonameId --same
				or @locType=2 and LocationType=4 and EventGeonameId=@admin1GeonameId --aoi's city, eventLoc's it's province
				or @locType in (2,4) and LocationType=6 and EventGeonameId=@countyGeonameId --aoi's city/prov, eventLoc's it's country
				or @locType=4 and LocationType=2 and Admin1GeonameId=@GeonameId --aoi's province, eventLoc's it's city
				or @locType=6 and LocationType<6 and CountryGeonameId=@GeonameId --aoi's country, eventLoc's it's city/province
			--found some
			If Exists (Select 1 From @tbl_localSpreadLoc)
			Begin --1
				--1.1 local spread events
				Insert into @tbl_localSpreadEvent
					Select distinct f1.EventId
					From @tbl_eventByLoc as f1, @tbl_localSpreadLoc as f2
					Where f1.EventGeonameId=f2.EventGeonameId
				--1.2 clean event by loc
				Delete From @tbl_eventByLoc Where EventId in (Select EventId From @tbl_localSpreadEvent)
				--1.3 clean eventLoc
				Delete From @tbl_eventLoc Where EventGeonameId Not in (Select EventGeonameId From @tbl_eventByLoc)
				--1.4 clean @tbl_localSpreadLoc
				Delete from @tbl_localSpreadLoc
			End --1
			--2. using aoi city buffer
			Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
			If @locType=2 And Exists (Select 1 From @tbl_eventLoc)
			Begin --2
				Declare @intputCityBuffer GEOGRAPHY
				Set @intputCityBuffer=@intputCityPoint.STBuffer(@Distance)
				-- buffer to intersect
				Insert into @tbl_localSpreadLoc
					Select EventGeonameId
					From @tbl_eventLoc
					Where LocationType=2 and @intputCityBuffer.STIntersects(geography::Point(Latitude, Longitude, 4326))=1
					Union
					Select f1.EventGeonameId
					From @tbl_eventLoc as f1, [place].[CountryProvinceShapes] as f2
					Where f1.LocationType in (4,6) and f1.EventGeonameId=f2.GeonameId 
						and @intputCityBuffer.STIntersects(f2.SimplifiedShape)=1
			End --2
			--3. using event city buffer
			Else If @locType>2 And Exists (Select 1 From @tbl_eventLoc Where LocationType=2)
			Begin --3
				Update @tbl_eventLoc 
					Set CityBuffer=(geography::Point(Latitude, Longitude, 4326)).STBuffer(@Distance)
					Where LocationType=2
				Insert into @tbl_localSpreadLoc
					Select EventGeonameId
					From @tbl_eventLoc as f1, [place].[CountryProvinceShapes] as f2
					Where f1.LocationType=2 and f2.GeonameId=@GeonameId
						and f1.CityBuffer.STIntersects(f2.SimplifiedShape)=1
			End --3
			--4. found some and clean
			If Exists (Select 1 From @tbl_localSpreadLoc)
			Begin --4
				--1 local spread events
				Insert into @tbl_localSpreadEvent
					Select distinct f1.EventId
					From @tbl_eventByLoc as f1, @tbl_localSpreadLoc as f2
					Where f1.EventGeonameId=f2.EventGeonameId
				--2 clean event by loc
				Delete From @tbl_eventByLoc Where EventId in (Select EventId From @tbl_localSpreadEvent)
				--3 clean eventLoc
				Delete From @tbl_eventLoc Where EventGeonameId Not in (Select EventGeonameId From @tbl_eventByLoc)
			End --4

			--D. Destination risks
			If Exists (Select 1 From @tbl_eventLoc)
			Begin --D
				--1. remaining events
				Declare @tbl_eventsToFind table (EventId int, EventMonth int, PassengerVolumes int,
						MinPrevelance float, MaxPrevelance float,
						MinProb decimal(5,4), MaxProb decimal(5,4), MinVolume decimal(10,3), MaxVolume decimal(10,3), IsLocal bit);
				Insert into @tbl_eventsToFind(EventId, EventMonth, MinPrevelance, MaxPrevelance, IsLocal)
					Select f1.EventId, f1.EventMonth, f1.MinPrevelance, f1.MaxPrevelance, case when le.EventId is null then 0 else 1 end
					From @tbl_events e
          join [zebra].[EventPrevalence] as f1 on f1.EventId = e.EventId --won't insert if not in prevelance table
          left join @tbl_localSpreadEvent le on le.EventId = e.EventId
					Where e.EventId=f1.EventId

				--2. find dest grids
				Declare @tbl_userGrid table (GridId nvarchar(12))
				If @locType=2	--city
					Insert into @tbl_userGrid (GridId)
						Select gridId
						From [bd].[HUFFMODEL25KMWORLDHEXAGON]
						Where SHAPE.STIntersects(@intputCityPoint) = 1
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
				--3. find dest airports to calculate passenger volumes for each event
				Declare @DestinationCatchmentThreshold decimal(5,2)
					=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='DestinationCatchmentThreshold');
				With T1 as (
					Select Distinct f0.EventId, f4.DestinationStationId, f4.Volume
					From @tbl_eventsToFind as f0, @tbl_userGrid as f1, [zebra].[EventDestinationGridV3] as f2, 
						[zebra].[GridStation] as f3, zebra.EventDestinationAirport as f4
					Where f0.EventId=f2.EventId and f0.EventId=f4.EventId and f0.EventMonth=MONTH(f3.ValidFromDate)
						and f1.GridId=f2.GridId and f3.Probability>=@DestinationCatchmentThreshold 
						and f2.GridId=f3.GridId and f3.StationId=f4.DestinationStationId
					)
				, T2 as (
					Select EventId, SUM(T1.Volume) as Volume
					From T1
					Group by EventId
					)
				Update @tbl_eventsToFind
					Set PassengerVolumes=T2.Volume
					From @tbl_eventsToFind as f1, T2
					Where f1.EventId=T2.EventId
				--4. prob
				Update @tbl_eventsToFind 
					Set MinProb=1-POWER((1-MinPrevelance), PassengerVolumes),
						MaxProb=1-POWER((1-MaxPrevelance), PassengerVolumes),
						MinVolume=MinPrevelance*PassengerVolumes,
						MaxVolume=MaxPrevelance*PassengerVolumes
					Where MinPrevelance IS NOT NULL and PassengerVolumes IS NOT NULL and PassengerVolumes>0
			End --D

			--E. results
			--1. events with precalculated prevalence (local and remote)
			If Exists (Select 1 From @tbl_eventsToFind)
				Insert into zebra.EventImportationRisksByGeoname(GeonameId, LocalSpread, EventId, MinProb, MaxProb, MinVolume, MaxVolume)
					Select @GeonameId, IsLocal, EventId, ISNULL(MinProb, 0), ISNULL(MaxProb, 0), ISNULL(MinVolume, 0), ISNULL(MaxVolume, 0)
					From @tbl_eventsToFind;

			--1. events without precalculated prevalence
			With T1 as (
				Select EventId From @tbl_events
				Except
				Select EventId From @tbl_eventsToFind
				)
			Insert into zebra.EventImportationRisksByGeoname(GeonameId, LocalSpread, EventId, MinProb, MaxProb, MinVolume, MaxVolume)
				Select @GeonameId, 0, EventId, 0, 0, 0, 0
				From T1
			--4. success
			Select 1 as Result
		END 
		ELSE
			Select 0 as Result
	--action!
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;
END