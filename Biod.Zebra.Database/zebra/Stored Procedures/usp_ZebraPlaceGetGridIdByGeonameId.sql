
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2018-05 
-- Description:	Input: GeonameId
--				Output: Get the GridId by GeonameId
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraPlaceGetGridIdByGeonameId
	@GeonameId    AS INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Longitude AS FLOAT
	DECLARE @Latitude AS FLOAT
	SELECT @Longitude = Longitude, @Latitude = Latitude FROM [place].[Geonames] WHERE GeonameId = @GeonameId
	
	SELECT H.gridId AS GridId FROM bd.HUFFMODEL25KMWORLDHEXAGON AS H WHERE H.SHAPE.STIntersects(geography::Point(@Latitude, @Longitude, 4326)) = 1 
END