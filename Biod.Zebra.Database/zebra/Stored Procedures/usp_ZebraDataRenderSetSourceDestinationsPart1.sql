
-- =============================================
-- Author:		Vivian
-- Create date: 2018-11-30 
-- Description:	1st part of pre-calculations, output source gridId and number of cases
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart1
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	If EXISTS (Select 1 from [surveillance].[Xtbl_Event_Location] Where EventId=@EventId)
	BEGIN --event location exists
		Declare @tbl_spreadLocs table (GeonameId int)
		--1. Event location exists?
		Insert into @tbl_spreadLocs
			Select GeonameId
			From bd.ufn_ValidLocationsOfEvent(@EventId)
			Where LocalOrIntlSpread=2

		If exists (Select 1 from @tbl_spreadLocs)
		BEGIN --1
			--1 locations of event
			Declare @tbl_eventLoc table (GeonameId int, Cases int, lat decimal(10,5), long decimal(10,5));
			--1.1 points
			Insert into @tbl_eventLoc(GeonameId, Cases, lat, long)
				Select f1.[GeonameId], 
					CASE
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
				END AS Cases,
				COALESCE(f3.LatPopWeighted, f3.Latitude),
				COALESCE(f3.LongPopWeighted, f3.Longitude)
			From [surveillance].[Xtbl_Event_Location] as f1, @tbl_spreadLocs as f2, place.Geonames as f3
			Where EventId=@EventId and f1.GeonameId=f2.GeonameId and f2.GeonameId=f3.GeonameId
				
			--1.2 geoname-grid
			Declare @tbl_loc_grid table (GeonameId int, GridId nvarchar(12))
			Insert into @tbl_loc_grid(GeonameId, GridId)
				Select f2.GeonameId, f1.gridId
				From bd.HUFFMODEL25KMWORLDHEXAGON as f1, @tbl_eventLoc as f2
				Where f1.SHAPE.STIntersects(geography::Point(f2.lat, f2.long, 4326)) = 1
			--1.3 grids of event
			--Declare @tbl_eventGrids table (GridId nvarchar(12), Cases int, minCase int, maxCase int);
			--Insert into @tbl_eventGrids(GridId, Cases)
			Select f1.GridId, sum(f2.Cases) as Cases
			From @tbl_loc_grid as f1, @tbl_eventLoc as f2
			Where f1.GeonameId=f2.GeonameId
			Group by f1.GridId
		END --1
		Else --no valid spread locations
		Begin
			--clean old data
			Delete from zebra.[EventSourceAirport] Where EventId=@EventId;
			Delete from zebra.EventDestinationAirport Where EventId=@EventId;
			Delete from zebra.EventDestinationGridV3 Where EventId=@EventId
			Delete from zebra.EventPrevalence Where EventId=@EventId
			--output
			Select '-1' as GridId, 0 as Cases
		End --no valid spread locations
	END --event location exists
	ELSE --event location not exists
	Begin
		--clean old data
		Delete from zebra.[EventSourceAirport] Where EventId=@EventId;
		Delete from zebra.EventDestinationAirport Where EventId=@EventId;
		Delete from zebra.EventDestinationGridV3 Where EventId=@EventId
		Delete from zebra.EventPrevalence Where EventId=@EventId
		--output
		Select '-1' as GridId, 0 as Cases
	End

END