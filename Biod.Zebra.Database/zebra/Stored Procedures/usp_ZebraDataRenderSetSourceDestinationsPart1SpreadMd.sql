
-- =============================================
-- Author:		Vivian
-- Create date: 2019-12-19 
-- based on revised_prevalence_calculations
-- Description:	1st part of pre-calculations, output source gridId and number of cases
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	--clean old data
	Delete from zebra.[EventSourceAirportSpreadMd] Where EventId=@EventId;
	Delete from zebra.EventDestinationAirportSpreadMd Where EventId=@EventId;
	Delete from zebra.EventDestinationGridSpreadMd Where EventId=@EventId
	Delete from zebra.EventExtensionSpreadMd Where EventId=@EventId
	Delete from zebra.[EventSourceDestinationRisk] Where EventId=@EventId
	Delete from zebra.EventSourceGridSpreadMd Where EventId=@EventId

	If (Select IsLocalOnly from [surveillance].[Event] Where EventId=@EventId)=0
		AND EXISTS (Select 1 from [surveillance].[Xtbl_Event_Location] Where EventId=@EventId)
	BEGIN --event location exists
		Declare @tbl_spreadLocs table (GeonameId int, LocationType int, Admin1GeonameId int, 
									Cases int, lat decimal(10,5), long decimal(10,5))
		--1. Event location w/ highest case count
		Insert into @tbl_spreadLocs(GeonameId, LocationType, Admin1GeonameId, Cases, lat, long)
			Select f1.GeonameId, f2.LocationType, f2.Admin1GeonameId, 
				(Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v)),
				COALESCE(f2.LatPopWeighted, f2.Latitude), COALESCE(f2.LongPopWeighted, f2.Longitude)
			From [surveillance].[Xtbl_Event_Location] as f1, [place].[ActiveGeonames] as f2
			Where f2.LocationType<>6 and f1.EventId=@EventId and f1.GeonameId=f2.GeonameId;
		

		If exists (Select 1 from @tbl_spreadLocs)
		BEGIN --1
			--2. calculate adjusted case count for province which has un-accounted cities
			Declare @tbl_eventProvince table (GeonameId int, Cases int);
			With T1 as (
				Select Admin1GeonameId, LocationType, SUM(Cases) as Cases
				From @tbl_spreadLocs
				Group by Admin1GeonameId, LocationType
			)
			Insert into @tbl_eventProvince(GeonameId, Cases)
				Select f1.Admin1GeonameId, (f1.Cases-f2.Cases)
				From T1 as f1, T1 as f2
				Where f1.LocationType=4 and f2.LocationType=2
					And f1.Admin1GeonameId=f2.Admin1GeonameId and f1.Cases>f2.Cases;
			--3. update province case
			With T1 as (--only has prov
				Select GeonameId From @tbl_spreadLocs Where LocationType=4
				Except
				Select Admin1GeonameId From @tbl_spreadLocs Where LocationType=2
				)
			Delete From @tbl_spreadLocs
				Where LocationType=4 and GeonameId Not in 
					(Select GeonameId From @tbl_eventProvince --prov has un-accounted cities
					Union
					Select GeonameId From T1) --location is only prov
			Update @tbl_spreadLocs Set Cases=f2.Cases
				From @tbl_spreadLocs as f1, @tbl_eventProvince as f2
				Where f1.GeonameId=f2.GeonameId
				
			--4 geoname-grid
			Declare @tbl_loc_grid table (GeonameId int, GridId nvarchar(12))
			Insert into @tbl_loc_grid(GeonameId, GridId)
				Select f2.GeonameId, f1.gridId
				From bd.HUFFMODEL25KMWORLDHEXAGON as f1, @tbl_spreadLocs as f2
				Where f1.SHAPE.STIntersects(geography::Point(f2.lat, f2.long, 4326)) = 1

			--5 output grids of event 
			Select f1.GridId, sum(f2.Cases) as Cases
			From @tbl_loc_grid as f1, @tbl_spreadLocs as f2
			Where f1.GeonameId=f2.GeonameId
			Group by f1.GridId
		END --1
		Else --no valid spread locations
			--output
			Select '-1' as GridId, 0 as Cases
	END --event location exists
	ELSE --event location not exists
		--output
		Select '-1' as GridId, 0 as Cases

END