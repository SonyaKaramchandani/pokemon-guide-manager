
-- =============================================
-- Author:		Vivian
-- Create date: 2019-10 
-- Description:	calculate case count in aois local area
-- https://wiki.bluedot.global/display/CEN/Insights+Product+Model+Development
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDiseaseGetLocalCaseCount
	@DiseaseId int, 
	@GeonameIds varchar(256), --user aoi
	@Distance int
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

	--event locations for local
	Declare @tbl_eventLoc table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										RepCases int, ConfCases int, SuspCases int, Deaths int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
										CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY);
	Insert into @tbl_eventLoc (GeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, 
								LocationType, RepCases, ConfCases, SuspCases, Deaths)
		Select f1.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, 
			f2.LocationType, f1.RepCases, f1.ConfCases, f1.SuspCases, f1.Deaths
		From bd.ufn_ValidLocationsOfDisease(@DiseaseId) as f1, place.Geonames as f2
		Where f1.LocalOrIntlSpread=1 and f1.GeonameId=f2.GeonameId
	--need to save a full version (@tbl_eventLoc keeps deleting records)
	Declare @tbl_eventLoc_saved table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int)
	Insert into @tbl_eventLoc_saved(GeonameId, CountryGeonameId, Admin1GeonameId, LocationType)
		Select GeonameId, CountryGeonameId, Admin1GeonameId, LocationType
		From @tbl_eventLoc

	/**target is to find if each event loc is local or not**/
	Declare @tbl_localSpread table (EventGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
									RepCases int, ConfCases int, SuspCases int, Deaths int)
	--1.use administrative hierachy
	Insert into @tbl_localSpread(EventGeonameId)
		Select f2.GeonameId
		From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			--  user's geonameId same as event geonameId
		Where f1.UserGeonameId=f2.GeonameId 
			--user is city, event's a province or country
			or (f1.LocationType=2 and (f2.LocationType=4 and f1.Admin1GeonameId=f2.GeonameId  or f2.LocationType=6 and f1.CountryGeonameId=f2.GeonameId))
			--User is prov, event in a city of that prov or event in that country)
			Or (f1.LocationType=4 and (f2.LocationType=2 and f2.Admin1GeonameId=f1.UserGeonameId or f2.LocationType=6 and f1.CountryGeonameId=f2.GeonameId))
			--User is country, events in that country
			Or (f1.LocationType=6 and f2.LocationType in (2, 4) and f1.UserGeonameId=f2.CountryGeonameId)
	--other info needed in @tbl_localSpread
	Update @tbl_localSpread 
		Set CountryGeonameId=f2.CountryGeonameId, Admin1GeonameId=f2.Admin1GeonameId, LocationType=f2.LocationType,
			RepCases=f2.RepCases, ConfCases=f2.ConfCases, SuspCases=f2.SuspCases, Deaths=f2.Deaths
		From @tbl_localSpread as f1, @tbl_eventLoc as f2
		Where f1.EventGeonameId=f2.GeonameId;
	--clean @tbl_eventLoc
	Delete from @tbl_eventLoc Where GeonameId In (Select EventGeonameId From @tbl_localSpread)
	
	--2. remaining use city buffer of user's
	--user loc is city
	If exists (Select 1 from @tbl_eventLoc) 
		and exists (Select 1 From @tbl_userGeonameId Where LocationType=2)
	Begin --2
		--user city point
		Update @tbl_userGeonameId
			Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
			Where LocationType=2
		--user city buffer
		Update @tbl_userGeonameId
			Set CityBuffer=CityPoint.STBuffer(@Distance) Where CityPoint IS NOT NULL
		--event loc is a city
		Insert into @tbl_localSpread(EventGeonameId)
			Select f2.GeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			Where f1.LocationType=2 and f2.LocationType=2
				 and f1.CityBuffer.STIntersects(geography::Point(f2.Latitude, f2.Longitude, 4326))=1

		--event loc is a prov/country
		Insert into @tbl_localSpread(EventGeonameId)
			Select f2.GeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2, [place].[CountryProvinceShapes] as f3
			Where f1.LocationType=2 and f2.LocationType in (4,6) and f2.GeonameId=f3.GeonameId 
				and f1.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		
		--other info needed in @tbl_localSpread
		Update @tbl_localSpread 
			Set CountryGeonameId=f2.CountryGeonameId, Admin1GeonameId=f2.Admin1GeonameId, LocationType=f2.LocationType,
				RepCases=f2.RepCases, ConfCases=f2.ConfCases, SuspCases=f2.SuspCases, Deaths=f2.Deaths
			From @tbl_localSpread as f1, @tbl_eventLoc as f2
			Where f1.EventGeonameId=f2.GeonameId;

		--clean @tbl_eventLoc
		Delete from @tbl_eventLoc Where GeonameId In (Select EventGeonameId From @tbl_localSpread)
	End--2

	--event loc is city
	If exists (Select 1 from @tbl_eventLoc Where LocationType=2)
	Begin --3
		--event city point
		Update @tbl_eventLoc
			Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
			Where LocationType=2
		--event city buffer
		Update @tbl_eventLoc
			Set CityBuffer=CityPoint.STBuffer(@Distance) 
			Where LocationType=2
		--user is prov/country
		Insert into @tbl_localSpread(EventGeonameId)
			Select Top 1 f1.UserGeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2, 
				[place].[CountryProvinceShapes] as f3
			Where f1.LocationType in (4,6) and f2.LocationType=2 and f1.UserGeonameId=f3.GeonameId 
				and f2.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		--other info needed in @tbl_localSpread
		Update @tbl_localSpread 
			Set CountryGeonameId=f2.CountryGeonameId, Admin1GeonameId=f2.Admin1GeonameId, LocationType=f2.LocationType,
				RepCases=f2.RepCases, ConfCases=f2.ConfCases, SuspCases=f2.SuspCases, Deaths=f2.Deaths
			From @tbl_localSpread as f1, @tbl_eventLoc as f2
			Where f1.EventGeonameId=f2.GeonameId;
	End --3

	--3. calculate cases
	If Exists (Select 1 from @tbl_localSpread)
	Begin
		--group by country and locType
		Declare @tbl_cases_total table (CountryGeonameId int, LocationType int, RepCases int, ConfCases int, Deaths int, SuspCases int, RankId int);
		Insert into @tbl_cases_total(CountryGeonameId, LocationType, RepCases, ConfCases, Deaths, SuspCases)
			Select CountryGeonameId, LocationType, SUM(RepCases), SUM(ConfCases), SUM(Deaths), SUM(SuspCases)
			From @tbl_localSpread
			Group by CountryGeonameId, LocationType;
		--use the case count of highest of the same admin level
		With T1 as (
			Select CountryGeonameId, LocationType, 
				ROW_NUMBER() OVER (PARTITION BY CountryGeonameId, LocationType 
								ORDER BY RepCases DESC, ConfCases DESC, Deaths DESC, SuspCases DESC) as Rid
			From @tbl_cases_total
			)
		Update @tbl_cases_total Set RankId=T1.Rid
			From @tbl_cases_total as f1, T1
			Where f1.CountryGeonameId=T1.CountryGeonameId and f1.LocationType=T1.LocationType
			
		--sum up of non-0 highest numbers
		Select SUM (CASE
						WHEN RepCases=0 
						THEN
							CASE
								WHEN ConfCases=0 
								THEN
									CASE 
										WHEN SuspCases=0
										THEN Deaths
										ELSE SuspCases
									END
								ELSE
									ConfCases
							END
						ELSE 
							RepCases 
					END
					) as CaseCount
			From @tbl_cases_total
			Where RankId=1
	End
	Else
		Select 0 as CaseCount

END