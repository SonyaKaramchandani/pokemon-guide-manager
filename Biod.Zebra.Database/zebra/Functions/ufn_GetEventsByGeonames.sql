
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-11  
-- Description:	Called by usp_ZebraApiGetEventByGeonameId, usp_ZebraEventGetEventSummary
--				Returns all local and spread(dest) events
-- Modified 2018-12: input @GeonameIds(of AOI) can be provs/countries and mix
-- Modified 2019-04: expanded definiton of local: city buffer intersects prov/country shape
-- =============================================

CREATE FUNCTION bd.ufn_GetEventsByGeonames (@GeonameIds AS VARCHAR(2000), @Distance int, @EndDateIsNull bit) 
RETURNS @returnResults TABLE (EventId int, IsLocal bit)
AS
BEGIN
	--0 filter by @EndDateIsNull
	Declare @tbl_Xtbl_Event_Location Table (EventId int, GeonameId int)
	If @EndDateIsNull=1
		Insert into @tbl_Xtbl_Event_Location(EventId, GeonameId)
			Select f1.EventId, f1.GeonameId
			From [surveillance].[Xtbl_Event_Location] as f1, [surveillance].[Event] as f2
			Where f2.EndDate IS NULL And f1.EventId=f2.EventId
	Else
		Insert into @tbl_Xtbl_Event_Location(EventId, GeonameId)
			Select EventId, GeonameId
			From [surveillance].[Xtbl_Event_Location]

	--Store
	--local events through admin levels
	Declare @tbl_foundEvents table (EventId int);
	--local events through city
	Declare @tbl_localEvents table (EventId int);
	--Dest events
	Declare @tbl_DestEvents table (EventId int);

	--A. Find local events without buffer
	--1. user geonames
	Declare @tbl_UserGeonameIds table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
									Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
									MyPoint GEOGRAPHY, MyBuffer GEOGRAPHY)
	Insert into @tbl_UserGeonameIds(GeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
		Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
		From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, [place].[ActiveGeonames] as f2
		Where Convert(int, f1.item)=f2.GeonameId;
	--2. event geonames
	Declare @tbl_allEventLocations table (GeonameId int, LocationType int, CountryGeonameId int, Admin1GeonameId int,
									Latitude Decimal(10, 5), Longitude Decimal(10, 5))
	Insert into @tbl_allEventLocations(GeonameId, LocationType, CountryGeonameId, Admin1GeonameId, Latitude, Longitude)
		Select Distinct f2.GeonameId, f2.LocationType, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude
		From @tbl_Xtbl_Event_Location as f1, [place].[ActiveGeonames] as f2
		Where f1.GeonameId=f2.GeonameId;
	--3. Use admin level to find events
	With T1 as (
		Select Distinct f1.GeonameId
		From @tbl_allEventLocations as f1, @tbl_UserGeonameIds as f2
			--event geonameId same user's geonameId or user's prov/country geonameId
		Where f1.GeonameId=f2.GeonameId or f1.GeonameId=f2.CountryGeonameId or f1.GeonameId=f2.Admin1GeonameId
			--User is prov, events in this prov
			Or (f2.LocationType=4 and f1.LocationType=2 and f1.Admin1GeonameId=f2.GeonameId)
			--User is country, events in this country
			Or (f2.LocationType=6 and f1.LocationType in (2, 4) and f1.CountryGeonameId=f2.GeonameId)
		)
	Insert into @tbl_foundEvents
		Select Distinct f1.EventId
		From @tbl_Xtbl_Event_Location as f1, T1
		Where f1.GeonameId=T1.GeonameId 

	--B.look for remaining using buffers
	Declare @tbl_EventsToCheck table (EventId int)
	Insert into @tbl_EventsToCheck
		Select EventId from @tbl_Xtbl_Event_Location
		Except Select EventId from @tbl_foundEvents

	--B1. remaining after admin level search
	If Exists (Select 1 from @tbl_EventsToCheck)
	BEGIN --1
		--1 for local (city only)
		--user's buffer
		Update @tbl_UserGeonameIds Set MyPoint=(geography::Point(latitude, longitude, 4326)) Where LocationType=2
		Update @tbl_UserGeonameIds Set MyBuffer=MyPoint.STBuffer(@Distance) Where LocationType=2
		--remaining events' locations
		Declare @tbl_EventsToCheckLocations table (GeonameId int, LocationType int, Latitude Decimal(10, 5), 
									Longitude Decimal(10, 5), EventPoint GEOGRAPHY, EventBuffer GEOGRAPHY)
		Insert into @tbl_EventsToCheckLocations(GeonameId, LocationType, Latitude, Longitude)
			Select Distinct f1.GeonameId, f1.LocationType, f1.Latitude, f1.Longitude
			From @tbl_allEventLocations as f1, @tbl_Xtbl_Event_Location as f2, @tbl_EventsToCheck as f3
			Where f1.GeonameId=f2.GeonameId and f2.EventId=f3.EventId
		--event city buffer
		Update @tbl_EventsToCheckLocations Set EventPoint=(geography::Point(latitude, longitude, 4326)) Where LocationType=2

		--user my buffer to intersect event loc (city only)
		Declare @tbl_foundLocalEventLocations table (GeonameId int);
		--events' loc in my buffer
		Insert into @tbl_foundLocalEventLocations(GeonameId)
			Select Distinct f2.GeonameId
			From @tbl_UserGeonameIds as f1, @tbl_EventsToCheckLocations as f2
			Where f1.LocationType=2 and f2.LocationType=2
				and MyBuffer.STIntersects(EventPoint) = 1
		--found local events through city buffer contains city
		Insert into @tbl_localEvents
			Select Distinct f2.EventId
			From @tbl_foundLocalEventLocations as f1, @tbl_Xtbl_Event_Location as f2,
				@tbl_EventsToCheck as f3
			Where f1.GeonameId=f2.GeonameId and f2.EventId=f3.EventId;
		--Events remaining after city buffer contains city
		Delete from @tbl_EventsToCheck Where EventId in (Select EventId from @tbl_localEvents)

		--B2. remaining after user city buffer contains event cities
		If Exists (Select 1 from @tbl_EventsToCheck)
		Begin --2
			--tmp save remaining event's locations
			Declare @tmp_geonameId table (GeonameId int)
			Insert into @tmp_geonameId
				Select Distinct f2.GeonameId
				From @tbl_EventsToCheck as f1, @tbl_Xtbl_Event_Location as f2
				Where f1.EventId=f2.EventId
			--Events location remaining after city buffer contains city
			Delete from @tbl_EventsToCheckLocations Where GeonameId Not in (Select GeonameId from @tmp_geonameId)
			--look for user city buffer intersects prov/country shapes
			Delete from @tbl_foundLocalEventLocations;
			Declare @tbl_localEvents2 table (EventId int);
			--events' loc in my buffer
			Insert into @tbl_foundLocalEventLocations(GeonameId)
				Select Distinct f2.GeonameId
				From @tbl_UserGeonameIds as f1, @tbl_EventsToCheckLocations as f2, [place].[CountryProvinceShapes] as f3
				Where f1.LocationType=2 and f2.LocationType in (4,6) and f2.GeonameId=f3.GeonameId
					and f1.MyBuffer.STIntersects(f3.SimplifiedShape)=1
			--found local events through user city buffer intersects events prov/country shape
			Insert into @tbl_localEvents2
				Select Distinct f2.EventId
				From @tbl_foundLocalEventLocations as f1, @tbl_Xtbl_Event_Location as f2,
					@tbl_EventsToCheck as f3
				Where f1.GeonameId=f2.GeonameId and f2.EventId=f3.EventId;
			--Events remaining after user city buffer intersects events prov/country shapes
			Delete from @tbl_EventsToCheck Where EventId in (Select EventId from @tbl_localEvents2)

			--B3. remaining after user city buffer intersects events prov/country shapes
			If Exists (Select 1 from @tbl_EventsToCheck)
			Begin --3
				--remaining event's locations
				Delete from @tmp_geonameId
				Insert into @tmp_geonameId
					Select Distinct f2.GeonameId
					From @tbl_EventsToCheck as f1, @tbl_Xtbl_Event_Location as f2
					Where f1.EventId=f2.EventId
				--Events location remaining after user city buffer intersects events prov/country shapes
				Delete from @tbl_EventsToCheckLocations Where GeonameId Not in (Select GeonameId from @tmp_geonameId)
				--look for event city buffer intersects user prov/country shapes
				Update @tbl_EventsToCheckLocations Set EventBuffer=EventPoint.STBuffer(@Distance) Where LocationType=2
				Delete from @tbl_foundLocalEventLocations;
				Declare @tbl_localEvents3 table (EventId int);
				--event city buffer intersects user prov/country shapes
				Insert into @tbl_foundLocalEventLocations(GeonameId)
					Select Distinct f2.GeonameId
					From @tbl_UserGeonameIds as f1, @tbl_EventsToCheckLocations as f2, [place].[CountryProvinceShapes] as f3
					Where f1.LocationType in (4,6) and f2.LocationType=2 and f1.GeonameId=f3.GeonameId
						and f2.EventBuffer.STIntersects(f3.SimplifiedShape)=1
				--found local events through user city buffer intersects events prov/country shape
				Insert into @tbl_localEvents3
					Select Distinct f2.EventId
					From @tbl_foundLocalEventLocations as f1, @tbl_Xtbl_Event_Location as f2,
						@tbl_EventsToCheck as f3
					Where f1.GeonameId=f2.GeonameId and f2.EventId=f3.EventId;
				--Events remaining after user city buffer intersects events prov/country shapes
				Delete from @tbl_EventsToCheck Where EventId in (Select EventId from @tbl_localEvents3)


				--C. for dest 
				Delete from @tbl_EventsToCheck
					Where EventId in 
						(Select EventId from [surveillance].[Event] Where IsLocalOnly=1)
				If Exists (Select 1 from @tbl_EventsToCheck)
				Begin --4
					--remove events with only country level locations
					--events with only country level
					Declare @tbl_tmp table (EventId int);
					With T1 as (
						Select distinct f1.EventId, f3.LocationType
						From @tbl_Xtbl_Event_Location as f1, 
							@tbl_EventsToCheck as f2, [place].[ActiveGeonames] as f3
						Where f1.EventId=f2.EventId and f1.GeonameId=f3.GeonameId
					)
					Insert into @tbl_tmp
						Select EventId From T1 Where LocationType=6
						Except
						Select EventId From T1 Where LocationType<>6
					--remove
					Delete from @tbl_EventsToCheck
						Where EventId in (Select EventId From @tbl_tmp)

					--faster when look for existing grid first w/o considering events
					Declare @tbl_tmpGrid table (GridId nvarchar(12))
					Insert into @tbl_tmpGrid
						Select Distinct f1.gridId
						From bd.HUFFMODEL25KMWORLDHEXAGON as f1, @tbl_UserGeonameIds as f2
						Where f2.LocationType=2 And f2.MyPoint.STIntersects(f1.SHAPE) = 1;
					--events with grid contains myPoint or intersects with prov/country shapes
					Insert into @tbl_DestEvents
						Select f2.EventId 
						From @tbl_tmpGrid as f1, [zebra].EventDestinationGridV3 as f2, @tbl_EventsToCheck as f3
						Where f2.EventId=f3.EventId and f1.GridId=f2.GridId  
						Union
						Select f4.EventId 
						From @tbl_EventsToCheck as f4, @tbl_UserGeonameIds as f5, [zebra].EventDestinationGridV3 as f6, [zebra].GridProvince as f7
						Where f5.LocationType=4 and f4.EventId=f6.EventId and f5.GeonameId=f7.Adm1GeonameId And f6.GridId=f7.GridId 
						Union
						Select f8.EventId 
						From  @tbl_EventsToCheck as f8, @tbl_UserGeonameIds as f9, [zebra].EventDestinationGridV3 as f10, [zebra].[GridCountry] as f11
						Where f9.LocationType=6 and f8.EventId=f10.EventId and f9.GeonameId=f11.CountryGeonameId And f10.GridId=f11.GridId
				End --4
			End --3
		End --2
	END --1

	--results
	Insert into @returnResults (EventId, IsLocal)
		Select EventId, 1 From @tbl_foundEvents --from admin level
		Union
		Select EventId, 1 From @tbl_localEvents --city intersects city
		Union
		Select EventId, 1 From @tbl_localEvents2 --user city buffer intersects events prov/country shape
		Union
		Select EventId, 1 From @tbl_localEvents3 --event city buffer intersects user prov/country shapes
		Union
		Select EventId, 0 From @tbl_DestEvents;
	Return
END