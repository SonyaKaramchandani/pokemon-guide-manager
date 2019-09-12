-- =============================================
-- Author:		BASAM Ahmad
-- Create date: 2019-06 
-- Description:	https://wiki.bluedot.global/display/CEN/Dataset+Report%3A+Geonames
-- Notes: if cityNames are duplicated, select 1st only	
-- =============================================

CREATE PROCEDURE [place].[usp_SearchGeonames] 
	@inputTerm nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @allText NVARCHAR(135) 
	Declare @inputTerm_internal nvarchar(200)=@inputTerm
	--Added chi to search for exact 3 letters for tuning as 'chi' is existed in around 724000 location
	IF (LOWER(@inputTerm_internal) = 'chi') OR (LOWER(@inputTerm_internal) = 'china') 
		OR (LOWER(@inputTerm_internal) = 'shang') OR (LOWER(@inputTerm_internal) = 'sheng')
		OR (LOWER(@inputTerm_internal) = 'ind') 
		SET @allText = '''' + @inputTerm_internal + '''';
	ELSE
		SET @allText = '"' + @inputTerm_internal + '*"';
		
	WITH City AS
	(
	SELECT TOP(3) G.GeonameId, G.DisplayName, G.Latitude, G.Longitude, G.Population,
	ROW_NUMBER() OVER (PARTITION by G.DisplayName ORDER BY G.Population DESC) as ranking
	FROM place.[Geonames] G
	   WHERE CONTAINS(G.DisplayName, @allText) AND G.LocationType = 2
	ORDER BY G.Population DESC
	),
	Province AS
	(
	SELECT TOP(3) G.GeonameId, G.DisplayName, G.Latitude, G.Longitude, G.Population,
	ROW_NUMBER() OVER (PARTITION by G.DisplayName ORDER BY G.Population DESC) as ranking
	FROM place.[Geonames] G
	   WHERE CONTAINS(G.DisplayName, @allText) AND G.LocationType = 4
	ORDER BY G.Population DESC
	),
	Country AS
	(
	SELECT TOP(3) G.GeonameId, G.DisplayName, G.Latitude, G.Longitude, G.Population,
	ROW_NUMBER() OVER (PARTITION by G.DisplayName ORDER BY G.Population DESC) as ranking
	FROM place.[Geonames] G
	   WHERE CONTAINS(G.DisplayName, @allText) AND G.LocationType = 6
	ORDER BY G.Population DESC
	)

	SELECT * FROM (
		SELECT City.GeonameId, City.DisplayName, 'City' LocationType, 
					City.Latitude, City.Longitude
		FROM City WHERE ranking=1 

		UNION

		SELECT Province.GeonameId, Province.DisplayName, 'Province' LocationType, 
					Province.Latitude, Province.Longitude
		FROM Province WHERE ranking=1 

		UNION

		SELECT Country.GeonameId, Country.DisplayName, 'Country' LocationType, 
					Country.Latitude, Country.Longitude
		FROM Country WHERE ranking=1 
	) G
END