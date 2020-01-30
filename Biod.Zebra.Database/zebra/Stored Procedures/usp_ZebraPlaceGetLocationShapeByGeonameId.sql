
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2019-03 
-- Description:	Output: Location information
-- Modification: Input: Use AoiGeonameIds
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraPlaceGetLocationShapeByGeonameId
	@AoiGeonameIds AS VARCHAR(max)
AS
BEGIN
	With T1 as (
		Select Convert(int, item) as GeonameId
		From [bd].[ufn_StringSplit](@AoiGeonameIds, ',')
		)
	SELECT  
		CASE WHEN G.LocationType = 2 THEN 'City'
			 WHEN G.LocationType = 4 THEN 'Province'
			 WHEN G.LocationType = 6 THEN 'Country'
		END LocationType,
		G.GeonameId, G.DisplayName AS LocationDisplayName, G.CountryGeonameId, G.CountryName,
		G.Shape.ToString() ShapeCentroidAsText,
		CASE WHEN G.LocationType = 2 THEN G.Shape.ToString()
			 WHEN G.LocationType = 4 THEN GS.SimplifiedShape.ToString()
			 WHEN G.LocationType = 6 THEN GS.SimplifiedShape.ToString()
		END ShapeAsText, G.Admin1GeonameId as ProvinceGeonameId
	FROM T1 inner join [place].[ActiveGeonames] AS G on T1.GeonameId=G.GeonameId
		LEFT JOIN [place].[CountryProvinceShapes] GS ON GS.GeonameId = G.GeonameId
END