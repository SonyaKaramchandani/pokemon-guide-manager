
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2018-05 
-- Description:	Output: List of event information
--				For Zebra external API, ZebraEventsInfo_Get
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraApiGetEventLocationShapesByEventid
	@EventId AS INT
AS
BEGIN
	SELECT  DISTINCT
		E.EventId, 
		CASE WHEN G.LocationType = 2 THEN 'City'
			 WHEN G.LocationType = 4 THEN 'Province'
			 WHEN G.LocationType = 6 THEN 'Country'
		END LocationType,
		EL.GeonameId, G.DisplayName AS LocationDisplayName, G.CountryGeonameId, G.CountryName,
		G.Shape.ToString() ShapeCentroidAsText,
		CASE WHEN G.LocationType = 2 THEN G.Shape.ToString()
			 WHEN G.LocationType = 4 THEN GS.SimplifiedShape.ToString()
			 WHEN G.LocationType = 6 THEN GS.SimplifiedShape.ToString()
		END ShapeAsText, G.Admin1GeonameId as ProvinceGeonameId
	FROM surveillance.Event AS E INNER JOIN
		surveillance.Xtbl_Event_Location AS EL ON E.EventId = EL.EventId INNER JOIN
		place.Geonames AS G ON EL.GeonameId = G.GeonameId 
		--INNER JOIN place.Geonames AS G2 ON G.CountryGeonameId = G2.GeonameId  
		LEFT JOIN [place].[CountryProvinceShapes] GS ON GS.GeonameId = G.GeonameId
	WHERE E.EventId = @EventId --186
END