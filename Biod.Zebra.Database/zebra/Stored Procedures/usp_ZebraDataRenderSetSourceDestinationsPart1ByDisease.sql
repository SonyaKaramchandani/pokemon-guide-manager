
-- =============================================
-- Author:		Vivian
-- Create date: 2019-09-30 
-- Description:	1st part of pre-calculations, output source gridId and number of cases
-- By disease instead of by event
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart1ByDisease
	@DiseaseId INT
AS
BEGIN
	SET NOCOUNT ON;
	--clean existing old pre-calculated data
	Delete from zebra.DiseaseSourceAirport Where DiseaseId=@DiseaseId;
	Delete from zebra.DiseaseEventPrevalence Where DiseaseId=@DiseaseId;
	Delete from zebra.DiseaseEventDestinationAirport Where DiseaseId=@DiseaseId;
	Delete from zebra.DiseaseEventDestinationGrid Where DiseaseId=@DiseaseId;

	--get loc info with case count
	Declare @tbl_allEventLoc table (GeonameId int, RepCases int, ConfCases int, SuspCases int, Deaths int)
	Insert into @tbl_allEventLoc(GeonameId, RepCases, ConfCases, SuspCases, Deaths)
		Select GeonameId, RepCases, ConfCases, SuspCases, Deaths
		From bd.ufn_ValidLocationsOfDisease(@DiseaseId)
		Where LocalOrIntlSpread=2
	If exists (Select 1 from @tbl_allEventLoc)
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
			COALESCE(f2.LatPopWeighted, f2.Latitude),
			COALESCE(f2.LongPopWeighted, f2.Longitude)
		From @tbl_allEventLoc as f1, place.Geonames as f2
		Where f1.GeonameId=f2.GeonameId
				
		--1.2 geoname-grid
		Declare @tbl_loc_grid table (GeonameId int, GridId nvarchar(12))
		Insert into @tbl_loc_grid(GeonameId, GridId)
			Select f2.GeonameId, f1.gridId
			From bd.HUFFMODEL25KMWORLDHEXAGON as f1, @tbl_eventLoc as f2
			Where f1.SHAPE.STIntersects(geography::Point(f2.lat, f2.long, 4326)) = 1
		--1.3 grids of event
		--Insert into @tbl_eventGrids(GridId, Cases)
		Select f1.GridId, sum(f2.Cases) as Cases
		From @tbl_loc_grid as f1, @tbl_eventLoc as f2
		Where f1.GeonameId=f2.GeonameId
		Group by f1.GridId
	END --1
	Else --no valid spread locations
		--output
		Select '-1' as GridId, 0 as Cases

END