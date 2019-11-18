
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-10  
-- Description:	Called by usp_ZebraEventGetCustomEventSummaryByEventId, usp_ZebraEventGetEventSummaryByEventId
--				find out if an event is local or not in terms of @GeonameIds
-- =============================================

CREATE FUNCTION zebra.ufn_GetEventStatusByGeonames (@EventId AS INT, @GeonameIds AS VARCHAR(2000)) 
RETURNS BIT --1:local, 0:not local
AS
BEGIN
	Declare @IsLocal bit=0
	Declare @Distance int = (Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')

	--1. user geonames
	Declare @tbl_UserGeonameIds table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
									Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
									MyPoint GEOGRAPHY, MyBuffer GEOGRAPHY)
	Insert into @tbl_UserGeonameIds(GeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
		Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
		From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, [place].[ActiveGeonames] as f2
		Where Convert(int, f1.item)=f2.GeonameId;
	--2. event geonames
	Declare @tbl_EventLocations table (GeonameId int, LocationType int, CountryGeonameId int, Admin1GeonameId int,
									Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
									EventPoint GEOGRAPHY, EventBuffer GEOGRAPHY)
	Insert into @tbl_EventLocations(GeonameId, LocationType, CountryGeonameId, Admin1GeonameId, Latitude, Longitude)
		Select f2.GeonameId, f2.LocationType, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude
		From [surveillance].[Xtbl_Event_Location] as f1, [place].[ActiveGeonames] as f2
		Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId;

	--3. Use admin level 
	If Exists ( 
			Select 1
			From @tbl_EventLocations as f1, @tbl_UserGeonameIds as f2
				--event geonameId same user's geonameId or user's prov/country geonameId
			Where f1.GeonameId=f2.GeonameId or f1.GeonameId=f2.CountryGeonameId or f1.GeonameId=f2.Admin1GeonameId
				--User is prov, events in this prov
				Or (f2.LocationType=4 and f1.LocationType=2 and f1.Admin1GeonameId=f2.GeonameId)
				--User is country, events in this country
				Or (f2.LocationType=6 and f1.LocationType in (2, 4) and f1.CountryGeonameId=f2.GeonameId)
			)
		--found by admin level
		Set @IsLocal=1
	--4. Use user's city buffer
	Else If Exists (Select 1 From @tbl_UserGeonameIds Where LocationType=2)
	Begin --1
		--1 for local (city only)
		--user's buffer
		Update @tbl_UserGeonameIds Set MyPoint=(geography::Point(latitude, longitude, 4326)) Where LocationType=2
		Update @tbl_UserGeonameIds Set MyBuffer=MyPoint.STBuffer(@Distance) Where LocationType=2
		--event city point
		Update @tbl_EventLocations Set EventPoint=(geography::Point(latitude, longitude, 4326)) Where LocationType=2

		--event's city in my buffer
		If Exists (
				Select 1
				From @tbl_UserGeonameIds as f1, @tbl_EventLocations as f2
				Where f1.LocationType=2 and f2.LocationType=2
					and MyBuffer.STIntersects(EventPoint) = 1
				)
			--
			Set @IsLocal=1
		--event's prov/country intersects my buffer
		Else If Exists (
				Select 1
				From @tbl_UserGeonameIds as f1, @tbl_EventLocations as f2, 
					[place].[CountryProvinceShapes] as f3
				Where f1.LocationType=2 and f2.LocationType in (4,6) and f2.GeonameId=f3.GeonameId
					and MyBuffer.STIntersects(f3.SimplifiedShape) = 1
				)
			--
			Set @IsLocal=1
	End --1
	--User's province/country intersects event's buffer
	Else If Exists (Select 1 From @tbl_UserGeonameIds Where LocationType in (4,6))
		And Exists (Select 1 From @tbl_EventLocations Where LocationType=2)
	Begin --2
		--event city point
		Update @tbl_EventLocations Set EventPoint=(geography::Point(latitude, longitude, 4326)) Where LocationType=2
		--event city buffer2
		Update @tbl_EventLocations Set EventBuffer=EventPoint.STBuffer(@Distance) Where LocationType=2
		--User's prov/country intersects event city buffer
		If Exists (
				Select 1
				From @tbl_UserGeonameIds as f1, @tbl_EventLocations as f2, 
					[place].[CountryProvinceShapes] as f3
				Where f1.LocationType in (4,6) and f2.LocationType=2 and f1.GeonameId=f3.GeonameId
					and EventBuffer.STIntersects(f3.SimplifiedShape) = 1
				)
			--
			Set @IsLocal=1
	End --2

	Return @IsLocal
END