
-- =============================================
-- Author:		Vivian
-- Create date: 2019-03 
-- Description:	V7 model https://wiki.bluedot.global/pages/viewpage.action?spaceKey=CEN&title=Insights+Product+Model+Development&preview=/49414858/63013056/aggregation_at_destination_v1.pptx
-- =============================================
CREATE PROCEDURE [zebra].usp_GetZebraImportationRisk
	@EventId int, 
	@UserGridId nvarchar(12), 
	@GeonameIds AS varchar(256)
AS
BEGIN
	SET NOCOUNT ON

	--user locations
	Declare @tbl_userGeonameId table (UserGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
										CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY)
	insert into @tbl_userGeonameId(UserGeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
			From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, place.Geonames as f2
			Where Convert(int, f1.item)=f2.GeonameId

	--event locations
	Declare @tbl_eventLoc table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
										CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY);
	Insert into @tbl_eventLoc (GeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
		Select f1.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
		From [surveillance].[Xtbl_Event_Location] as f1, place.Geonames as f2
		Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId

	/******find local spread******/
	Declare @tbl_localSpread table (UserGeonameId int)
	--1.use administrative hierachy
	Insert into @tbl_localSpread
		Select Distinct f1.UserGeonameId
		From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			--  user's geonameId same as event geonameId
		Where f1.UserGeonameId=f2.GeonameId 
			--user is city, event's a province or country
			or (f1.LocationType=2 and (f2.LocationType=4 and f1.Admin1GeonameId=f2.GeonameId  or f2.LocationType=6 and f1.CountryGeonameId=f2.GeonameId))
			--User is prov, event in a city of that prov or event in that country)
			Or (f1.LocationType=4 and (f2.LocationType=2 and f2.Admin1GeonameId=f1.UserGeonameId or f2.LocationType=6 and f1.CountryGeonameId=f2.GeonameId))
			--User is country, events in that country
			Or (f1.LocationType=6 and f2.LocationType in (2, 4) and f1.UserGeonameId=f2.CountryGeonameId)

	--2. use buffer, need to output each user loc of local spread
	Declare @tbl_userGeonameId_remain table (UserGeonameId int)
	Insert into @tbl_userGeonameId_remain(UserGeonameId)
		Select UserGeonameId From @tbl_userGeonameId
		Except
		Select UserGeonameId From @tbl_localSpread
	--some user loc are not local
	If exists (Select 1 from @tbl_userGeonameId_remain)
	Begin --1
		Declare @Distance int=100000
		--user loc is city
		If exists (Select 1 from @tbl_userGeonameId_remain as f1, @tbl_userGeonameId as f2 
					Where f1.UserGeonameId=f2.UserGeonameId and f2.LocationType=2)
		Begin --2
			--point
			Update @tbl_userGeonameId
				Set CityPoint=(GEOGRAPHY::Point(latitude, longitude, 4326))
				From @tbl_userGeonameId as f1, @tbl_userGeonameId_remain as f2
				Where f1.UserGeonameId=f2.UserGeonameId and f1.LocationType=2
			--buffer
			Update @tbl_userGeonameId
				Set CityBuffer=CityPoint.STBuffer(@Distance) Where CityPoint IS NOT NULL
		End--2
		--event loc is city
		If exists (Select 1 from @tbl_eventLoc Where LocationType=2)
		Begin --3
			--point
			Update @tbl_eventLoc
				Set CityPoint=(GEOGRAPHY::Point(latitude, longitude, 4326))
				Where LocationType=2
			--buffer
			Update @tbl_eventLoc
				Set CityBuffer=CityPoint.STBuffer(@Distance) 
				Where LocationType=2
		End --3
		--intersect
		Insert into @tbl_localSpread
			--user is city
			Select UserGeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2, [place].[CountryProvinceShapes] as f3
			Where f1.CityBuffer is NOT NULL 
				and (f2.LocationType=2 and f1.CityBuffer.STIntersects(f2.CityPoint)=1
					or f2.LocationType in (4,6) and f2.GeonameId=f3.GeonameId 
						and f1.CityBuffer.STIntersects(f3.SimplifiedShape)=1
					)
			--user is prov/country
			Union
			Select f1.UserGeonameId
			From @tbl_userGeonameId as f1, @tbl_userGeonameId_remain as f2,  @tbl_eventLoc as f3, 
				[place].[CountryProvinceShapes] as f4
			Where f1.LocationType in (4,6) and f1.UserGeonameId=f2.UserGeonameId and f3.LocationType=2
				and f2.UserGeonameId=f4.GeonameId and f3.CityBuffer.STIntersects(f4.SimplifiedShape)=1
	End --1
	--3.return local spread
	If Exists (Select 1 from @tbl_localSpread)
	Begin
		Select UserGeonameId, 1 as localSpread, CAST(0 as decimal) as MinProbability,  
			CAST(0 as decimal) as MaxProbability, CAST(0 as decimal) as MinExpTravelers, 
			CAST(0 as decimal) as MaxExpTravelers
		from @tbl_localSpread
		union
		Select UserGeonameId, 0 as localSpread, CAST(0 as decimal) as MinProbability,  
			CAST(0 as decimal) as MaxProbability, CAST(0 as decimal) as MinExpTravelers, 
			CAST(0 as decimal) as MaxExpTravelers
		from @tbl_userGeonameId 
		Where UserGeonameId not in (Select UserGeonameId from @tbl_localSpread)
		--no need to go further
		Return
	End

	/******continue when no local spread******/
	Declare @EventMonth int, @MinPrevelance float, @MaxPrevelance float
	Select @MinPrevelance=MinPrevelance, @MaxPrevelance=MaxPrevelance, @EventMonth=EventMonth
	From Zebra.EventPrevalence
	Where EventId=@EventId

	If @MinPrevelance IS NOT NULL
	Begin 
		--1. find dest grids
		Declare @tbl_userGrid table (GridId nvarchar(12))
		If @UserGridId<>'' --default is user's register location
			Insert into @tbl_userGrid Values(@UserGridId)
		Else --find grid through geonameId
		Begin
			Insert into @tbl_userGrid (GridId)
				--city
				Select f2.gridId
				From @tbl_userGeonameId as f1, [bd].[HUFFMODEL25KMWORLDHEXAGON] as f2
				Where f1.LocationType=2 and f2.SHAPE.STIntersects(f1.CityPoint) = 1
				union --province
				Select f4.GridId
				From @tbl_userGeonameId as f3, [zebra].[GridProvince] as f4
				Where f3.LocationType=4 and f3.UserGeonameId=f4.Adm1GeonameId
				union --country
				Select f6.GridId
				From @tbl_userGeonameId as f5, [zebra].GridCountry as f6
				Where f5.LocationType=6 and f5.UserGeonameId=f6.CountryGeonameId
		End
		--2. find dest airports 
		Declare @tbl_destApts table (DestinationStationId int, Volume int)
		Insert into @tbl_destApts(DestinationStationId, Volume)
		Select Distinct f4.DestinationStationId, f4.Volume
		From @tbl_userGrid as f1, [zebra].[EventDestinationGridV3] as f2, 
			[zebra].[GridStation] as f3, zebra.EventDestinationAirport as f4
		Where f2.EventId=@EventId and f4.EventId=@EventId and MONTH(f3.ValidFromDate)=@EventMonth
			and f1.GridId=f2.GridId and f3.Probability>0.1 
			and f2.GridId=f3.GridId and f3.StationId=f4.DestinationStationId

		--3. Results
		Declare @PassengerVolumes int = (Select sum(Volume) From @tbl_destApts)

		Declare @MinProb decimal(5,4)=1-POWER((1-@MinPrevelance), @PassengerVolumes)
		Declare @MaxProb decimal(5,4)=1-POWER((1-@MaxPrevelance), @PassengerVolumes)
		Declare @MinExpVolume decimal(10,3)=@MinPrevelance*@PassengerVolumes
		Declare @MaxExpVolume decimal(10,3)=@MaxPrevelance*@PassengerVolumes

		Select -1 as UserGeonameId, -1 as localSpread,
			@MinProb as MinProbability,  @MaxProb as MaxProbability, @MinExpVolume as MinExpTravelers, @MaxExpVolume as MaxExpTravelers
	End
	Else --@MinPrevelance IS NULL
		Select -1 as UserGeonameId, -1 as localSpread,
			CAST(0 as decimal) as MinProbability,  CAST(0 as decimal) as MaxProbability, CAST(0 as decimal) as MinExpTravelers, CAST(0 as decimal) as MaxExpTravelers
END