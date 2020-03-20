
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2020-02  
-- Description:	Called by usp_ZebraDataRenderSetImportationRiskByGeonameId, usp_ZebraDataRenderSetImportationRiskByGeonameIdSpreadMd
--				Returns all local active events
-- =============================================

CREATE FUNCTION bd.ufn_GetLocalEventsByGeoname (@GeonameId AS int) 
RETURNS @returnResults TABLE (EventId int)
AS
BEGIN
			--A. event location
			--1. Active events by locs
			Declare @tbl_eventByLoc table (EventId int, EventGeonameId int)
			Insert into @tbl_eventByLoc(EventId, EventGeonameId)
				Select f1.EventId, f1.GeonameId
				From surveillance.Xtbl_Event_Location as f1, surveillance.[Event] as f2
				Where EndDate IS NULL and f1.EventId=f2.EventId;
			--2. eventLoc only
			Declare @tbl_eventLoc table (EventGeonameId int, LocationType int, Admin1GeonameId int, CountryGeonameId int,
										Point GEOGRAPHY, CityBuffer GEOGRAPHY);
			With T1 as (
				Select distinct EventGeonameId From @tbl_eventByLoc
				)
			Insert into @tbl_eventLoc(EventGeonameId, LocationType, Admin1GeonameId, CountryGeonameId, Point)
				Select f1.EventGeonameId, f2.LocationType, f2.Admin1GeonameId, f2.CountryGeonameId,
					f2.Shape
				From T1 as f1, place.Geonames as f2
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
				Insert into @returnResults
					Select distinct f1.EventId
					From @tbl_eventByLoc as f1, @tbl_localSpreadLoc as f2
					Where f1.EventGeonameId=f2.EventGeonameId
				--1.2 clean event by loc
				Delete From @tbl_eventByLoc Where EventId in (Select EventId From @returnResults)
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
					Where LocationType=2 and @intputCityBuffer.STIntersects(Point)=1
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
					Set CityBuffer=(Point).STBuffer(@Distance)
					Where LocationType=2
				Insert into @tbl_localSpreadLoc
					Select EventGeonameId
					From @tbl_eventLoc as f1, [place].[CountryProvinceShapes] as f2
					Where f1.LocationType=2 and f2.GeonameId=@GeonameId
						and f1.CityBuffer.STIntersects(f2.SimplifiedShape)=1
			End --3
			--4. found some and clean
			If Exists (Select 1 From @tbl_localSpreadLoc)
				--1 local spread events
				Insert into @returnResults
					Select distinct f1.EventId
					From @tbl_eventByLoc as f1, @tbl_localSpreadLoc as f2
					Where f1.EventGeonameId=f2.EventGeonameId

	Return
END