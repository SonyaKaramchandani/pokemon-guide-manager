
-- =============================================
-- Author:		Vivian
-- Create date: 2019-11 
-- Description:	calculate case count in aois local area
-- https://wiki.bluedot.global/display/CEN/Insights+Product+Model+Development
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDiseaseGetLocalCaseCount
	@DiseaseId int, 
	@GeonameIds varchar(MAX),
	@EventId int = NULL
AS
BEGIN
	SET NOCOUNT ON

	Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')

	--1. user locations
	Declare @tbl_userGeonameId table (UserGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
										CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY)
	insert into @tbl_userGeonameId(UserGeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
			From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, place.ActiveGeonames as f2
			Where Convert(int, f1.item)=f2.GeonameId

	--2. event locations 
	--2.1 caseCount of all locs
	Declare @tbl_eventLoc table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
								CaseCount int, Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
								CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY);
	Insert into @tbl_eventLoc (GeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, 
								LocationType, CaseCount)
		Select f1.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, 
			f2.LocationType,
      (SELECT Max(v) FROM (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) AS value(v))
		From [surveillance].[Xtbl_Event_Location] as f1, place.ActiveGeonames as f2, [surveillance].[Event] as f3
		Where (@EventId is null or f1.EventId = @EventId) and f3.DiseaseId=@DiseaseId and f3.EndDate IS NULL and [SpeciesId]=1
			and f1.EventId=f3.EventId and f1.GeonameId=f2.GeonameId
	--2.2 adjusted total caseCount on province level when any locType of this province in event
	--don't use this to track province, only track province delta 
	--IsDelta=true means un-counted caseCount in that province not in any cities of that province
	--IsDelta=0 means the caseCount is total
	Declare @tbl_province table (CountryGeonameId int, Admin1GeonameId int, CaseCount int, IsDelta bit);
	With T1 as (
		Select CountryGeonameId, Admin1GeonameId, LocationType, SUM(CaseCount) as CaseCount
		From @tbl_eventLoc 
		Where LocationType<>6
		Group by CountryGeonameId, Admin1GeonameId, LocationType
		)
	Insert into @tbl_province(CountryGeonameId, Admin1GeonameId, CaseCount, IsDelta)
		Select CountryGeonameId, Admin1GeonameId, MAX(CaseCount), 0
		From T1
		Group by CountryGeonameId, Admin1GeonameId
		Union
		Select f1.CountryGeonameId, f1.Admin1GeonameId, f1.CaseCount-f2.CaseCount, 1
		From T1 as f1, T1 as f2
		Where f1.LocationType=4 and f2.LocationType=2
			and f1.Admin1GeonameId=f2.Admin1GeonameId 
			and f1.CaseCount>f2.CaseCount;
	--When a province has itself (no cities) in event loc, 
	--need to add delta which caseCount is the same
	With T1 as (
		Select GeonameId From @tbl_eventLoc Where LocationType=4 
		Except
		Select Admin1GeonameId From @tbl_eventLoc Where LocationType=2
		)
	--add delta to @tbl_province
	Insert into @tbl_province(CountryGeonameId, Admin1GeonameId, CaseCount, IsDelta)
		Select f1.CountryGeonameId, f1.Admin1GeonameId, f1.CaseCount, 1
		From @tbl_province as f1, T1
		Where f1.Admin1GeonameId=T1.GeonameId
		
	--2.3 adjusted total caseCount on country level
	--don't use this to track country, only track country delta 
	--IsDelta=true means un-counted caseCount in that country not in any province 
	--IsDelta=0 means the caseCount is total
	Declare @tbl_country table (CountryGeonameId int, CaseCount int, IsDelta bit);
	With T1 as (
		Select CountryGeonameId, SUM(CaseCount) as CaseCount, 4 as LocType
		From @tbl_province 
		Where IsDelta=0
		Group by CountryGeonameId
		Union
		Select CountryGeonameId, CaseCount, 6 as LocType
		From @tbl_eventLoc
		Where LocationType=6
		)
	Insert into @tbl_country( CountryGeonameId,  CaseCount, IsDelta)
		Select CountryGeonameId, MAX(CaseCount), 0
		From T1
		Group by CountryGeonameId
		Union
		Select f1.CountryGeonameId, f1.CaseCount-f2.CaseCount, 1
		From T1 as f1, T1 as f2
		Where f1.LocType=6 and f2.LocType=4
			and f1.CountryGeonameId=f2.CountryGeonameId 
			and f1.CaseCount>f2.CaseCount;
	--When a country has itself (no cities/provinces) in event loc, 
	--need to add delta which caseCount is the same
	With T1 as (
		Select GeonameId From @tbl_eventLoc Where LocationType=6 
		Except
		Select CountryGeonameId From @tbl_eventLoc Where LocationType<>6
		)
	--add delta to @tbl_province
	Insert into @tbl_country(CountryGeonameId, CaseCount, IsDelta)
		Select f1.CountryGeonameId, f1.CaseCount, 1
		From @tbl_country as f1, T1
		Where f1.CountryGeonameId=T1.GeonameId

	/**target is to find combined caseCount of all local locs**/
	--Delta=true means un-counted caseCount in that country not in any province 
	-- or in that province not in any cities of that province
	Declare @tbl_localSpread table (EventGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
							CaseCount int, Delta bit)
	--A. user admin levels
	Declare @tbl_tmp  table (EventGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int)
	--1. user aoi has country 
	If Exists (Select 1 from @tbl_userGeonameId Where LocationType=6)
	Begin --1
		--all event country/prov/city in user country's territory
		Insert into @tbl_tmp(EventGeonameId, CountryGeonameId, Admin1GeonameId, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.LocationType
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			Where f1.LocationType=6 and f1.UserGeonameId=f2.CountryGeonameId
		--event loc has country
		If Exists (Select 1 from @tbl_tmp Where LocationType=6)
		Begin--1.1
			Insert into @tbl_localSpread(EventGeonameId, CaseCount)
				Select f1.EventGeonameId, f2.CaseCount
				From @tbl_tmp as f1, @tbl_country as f2
				Where f1.LocationType=6 and IsDelta=0 and f1.EventGeonameId=f2.CountryGeonameId
			--remove all locs in event country
			Delete From @tbl_eventLoc
				Where CountryGeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=6)
			Delete From @tbl_tmp
				Where CountryGeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=6)
		End--1.1
		--event loc has province
		If Exists (Select 1 from @tbl_tmp Where LocationType=4)
		Begin --1.2
			Insert into @tbl_localSpread(EventGeonameId, CaseCount)
				Select f1.EventGeonameId, f2.CaseCount
				From @tbl_tmp as f1, @tbl_province as f2
				Where f1.LocationType=4 and IsDelta=0 and f1.EventGeonameId=f2.Admin1GeonameId
			--remove all locs in event province
			Delete From @tbl_eventLoc
				Where Admin1GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=4)
			Delete From @tbl_tmp
				Where Admin1GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=4)
		End --1.2
		--remaining event locs are cities
		If Exists (Select 1 from @tbl_tmp Where LocationType=2)
		Begin --1.3
			Insert into @tbl_localSpread(EventGeonameId, CaseCount)
				Select f1.EventGeonameId, f2.CaseCount
				From @tbl_tmp as f1, @tbl_eventLoc as f2
				Where f1.LocationType=2 and f1.EventGeonameId=f2.GeonameId
			--remove all locs in event province
			Delete From @tbl_eventLoc
				Where GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=2)
			Delete From @tbl_tmp
				Where LocationType=2
		End --1.3 @tbl_tmp should be empty now
	End --1
	--2. user aoi has provinces
	If Exists (Select 1 from @tbl_userGeonameId Where LocationType=4)
	Begin --2
		--all event country/prov/city in user provin's territory or is uer province's country
		Insert into @tbl_tmp(EventGeonameId, CountryGeonameId, Admin1GeonameId, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.LocationType
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			Where f1.LocationType=4 and (f1.UserGeonameId=f2.Admin1GeonameId or f1.CountryGeonameId=f2.GeonameId)
		--event loc has province
		If Exists (Select 1 from @tbl_tmp Where LocationType=4)
		Begin--2.1
			--cases from province itself
			Insert into @tbl_localSpread(EventGeonameId, CaseCount)
				Select f1.EventGeonameId, f2.CaseCount
				From @tbl_tmp as f1, @tbl_province as f2
				Where f1.LocationType=4 and IsDelta=0
					and f1.EventGeonameId=f2.Admin1GeonameId;
			--remove all locs in event country
			Delete From @tbl_eventLoc
				Where Admin1GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=4)
			Delete From @tbl_tmp
				Where Admin1GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=4)
		End--2.1
		--event loc has cities
		If Exists (Select 1 from @tbl_tmp Where LocationType=2)
		Begin--2.2
			--cases from province itself
			Insert into @tbl_localSpread(EventGeonameId, CaseCount)
				Select f1.EventGeonameId, f2.CaseCount
				From @tbl_tmp as f1, @tbl_eventLoc as f2
				Where f1.LocationType=2 and f1.EventGeonameId=f2.GeonameId;
			--remove all locs in event country
			Delete From @tbl_eventLoc
				Where GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=2)
		End--2.2
		--when country appears in @tbl_tmp, need to add delta(cases in country but not in any provinces)
		If Exists (Select 1 from @tbl_tmp Where LocationType=6)
		Begin --2.3
			Insert into @tbl_localSpread(EventGeonameId, CaseCount, LocationType, Delta)
				Select Distinct f1.EventGeonameId, f2.CaseCount, 6, 1
				From @tbl_tmp as f1, @tbl_country as f2
				Where f1.LocationType=6 and f2.IsDelta=1 and f1.EventGeonameId=f2.CountryGeonameId
			--un-counted country cases done
			Delete From @tbl_country
				Where IsDelta=1 and CountryGeonameId in (Select EventGeonameId From @tbl_tmp)
		End --2.3
		--clean up @tbl_tmp
		Delete From @tbl_tmp
	End --2
	--3. user aoi has cities 
	If Exists (Select 1 from @tbl_userGeonameId Where LocationType=2)
	Begin --3
		--all event country/prov/city in user provin's territory or is uer province's country
		Insert into @tbl_tmp(EventGeonameId, CountryGeonameId, Admin1GeonameId, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.LocationType
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			Where f1.LocationType=2 
				and (f1.UserGeonameId=f2.GeonameId
					or f1.Admin1GeonameId=f2.GeonameId
					or f1.CountryGeonameId=f2.GeonameId)
		--event loc has cities
		If Exists (Select 1 from @tbl_tmp Where LocationType=2)
		Begin--3.1
			--cases from city itself
			Insert into @tbl_localSpread(EventGeonameId, CaseCount)
				Select f1.EventGeonameId, f2.CaseCount
				From @tbl_tmp as f1, @tbl_eventLoc as f2
				Where f1.LocationType=2 and f1.EventGeonameId=f2.GeonameId;
			--remove all locs in event country
			Delete From @tbl_eventLoc
				Where GeonameId in (Select EventGeonameId From @tbl_tmp Where LocationType=2)
		End--3.1
		--when province appears in @tbl_tmp, need to add delta (cases in province but not in any cities)
		If Exists (Select 1 from @tbl_tmp Where LocationType=4)
		Begin --3.2
			Insert into @tbl_localSpread(EventGeonameId, CaseCount, LocationType, Delta)
				Select Distinct f1.EventGeonameId, f2.CaseCount, 4, 1
				From @tbl_tmp as f1, @tbl_province as f2
				Where f1.LocationType=4 and f2.IsDelta=1 and f1.EventGeonameId=f2.Admin1GeonameId
			--un-counted province cases done
			Delete From @tbl_province
				Where IsDelta=1 and Admin1GeonameId in (Select EventGeonameId From @tbl_tmp)
		End --3.2
		--when country appears in @tbl_tmp, need to add delta(cases in country but not in any provinces)
		If Exists (Select 1 from @tbl_tmp Where LocationType=6) 
		Begin --3.3
			Insert into @tbl_localSpread(EventGeonameId, CaseCount, LocationType, Delta)
				Select Distinct f1.EventGeonameId, f2.CaseCount, 6, 1
				From @tbl_tmp as f1, @tbl_country as f2
				Where f1.LocationType=6 and f2.IsDelta=1 and f1.EventGeonameId=f2.CountryGeonameId
			--un-counted country cases done
			Delete From @tbl_country
				Where IsDelta=1 and CountryGeonameId in (Select EventGeonameId From @tbl_tmp)
		End --3.3
		--clean up @tbl_tmp
		Delete From @tbl_tmp
	End --3

	--B. remaining use city buffer of user's
	--1. user loc is city
	If exists (Select 1 from @tbl_eventLoc) 
		and exists (Select 1 From @tbl_userGeonameId Where LocationType=2)
	Begin --1
		--user city point
		Update @tbl_userGeonameId
			Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
			Where LocationType=2
		--user city buffer
		Update @tbl_userGeonameId
			Set CityBuffer=CityPoint.STBuffer(@Distance) Where CityPoint IS NOT NULL
		--event loc is a city
		Insert into @tbl_localSpread(EventGeonameId, CaseCount)
			Select f2.GeonameId, f2.CaseCount
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			Where f1.LocationType=2 and f2.LocationType=2
				 and f1.CityBuffer.STIntersects(geography::Point(f2.Latitude, f2.Longitude, 4326))=1;

		--event loc is a prov/country, only include delta caseCount
		With T1 as (
			Select Distinct CountryGeonameId as GeonameId, CaseCount
			From @tbl_country
			Where IsDelta=1
			Union
			Select Distinct Admin1GeonameId as GeonameId, CaseCount
			From @tbl_province
			Where IsDelta=1
			)
		Insert into @tbl_localSpread(EventGeonameId, CaseCount, Delta)
			Select f2.GeonameId, f2.CaseCount, 1
			From @tbl_userGeonameId as f1, T1 as f2, [place].[CountryProvinceShapes] as f3
			Where f1.LocationType=2 and f2.GeonameId=f3.GeonameId 
				and f1.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		
		--clean up
		Delete from @tbl_eventLoc 
			Where GeonameId In (Select EventGeonameId From @tbl_localSpread Where Delta IS NULL)
		Delete from @tbl_country 
			Where IsDelta=1
				and CountryGeonameId In (Select EventGeonameId From @tbl_localSpread Where Delta=1)
		Delete from @tbl_province 
			Where IsDelta=1
				and Admin1GeonameId In (Select EventGeonameId From @tbl_localSpread Where Delta=1)
	End--1

	--2. event loc is city
	If exists (Select 1 from @tbl_eventLoc Where LocationType=2)
	Begin --2
		--event city point
		Update @tbl_eventLoc
			Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
			Where LocationType=2
		--event city buffer
		Update @tbl_eventLoc
			Set CityBuffer=CityPoint.STBuffer(@Distance) 
			Where LocationType=2
		--user is prov/country
		Insert into @tbl_localSpread(EventGeonameId, CaseCount)
			Select f1.UserGeonameId, f2.CaseCount
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2, 
				[place].[CountryProvinceShapes] as f3
			Where f1.LocationType in (4,6) and f2.LocationType=2 and f1.UserGeonameId=f3.GeonameId 
				and f2.CityBuffer.STIntersects(f3.SimplifiedShape)=1
	End --2

	--C. return total case count
	If Exists (Select 1 from @tbl_localSpread)
		Select Sum(CaseCount) as CaseCount From @tbl_localSpread
	Else
		Select 0 as CaseCount

END