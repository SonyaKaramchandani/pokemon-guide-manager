CREATE PROCEDURE [place].[usp_SearchGeonames] 
  @inputTerm NVARCHAR(200)
AS
BEGIN
  SET NOCOUNT ON;
  BEGIN TRY
    DECLARE @allText NVARCHAR(200) = @inputTerm
    
    IF( CHARINDEX( '"', @allText ) > 0 )
      -- The search criteria contains quote (")
      -- assume the entire inputTerm is encompasssed by two quotes, The caller should ensure that
      -- the search will try to match the encompassed text exactly as is
      SET @allText = @allText
    ELSE IF( CHARINDEX( ' ', @allText ) > 0 )
      -- The search criteria contains multiple words separated by space(s)
      -- replace space(s) with a single comma as required by the NEAR clause syntax
      -- the search will try to match all words (ANDed), the words could be in any order
      -- the search will match whole words only, no partial search
      BEGIN
        WHILE @allText LIKE '%  %' SET @allText = replace(@allText, '  ', ' ')
        SET @allText = 'NEAR((' + replace(@allText, ' ', ',') + '))'
      END
    ELSE
      -- The search criteria contains a single word
      -- the search will match any word that begins with the passed value (partial match)
      SET @allText = '"' + @allText + '*"'

    ;with Locations AS
    (
      SELECT GeonameId, DisplayName, LocationType, Latitude, Longitude, Population, Alternatenames,
      ROW_NUMBER() OVER (PARTITION by LocationType ORDER BY Population DESC) AS ranking
      FROM place.[Geonames]
      WHERE LocationType IN (2,4,6) AND GeonameId IN (
        SELECT GeonameId FROM place.uvw_AlternateNames WHERE CONTAINS(Alternatenames, @allText)
        UNION
        SELECT GeonameId FROM place.[Geonames] WHERE CONTAINS(DisplayName, @allText)
      )
    ),
    RankedLocations AS
    (
      SELECT TOP 100 GeonameId, DisplayName, LocationType, Population
      FROM Locations
        WHERE LocationType = 2 AND ranking <= 100
      UNION
      SELECT TOP 100 GeonameId, DisplayName, LocationType, Population
      FROM Locations
        WHERE LocationType = 4 AND ranking <= 100
      UNION
      SELECT TOP 100 GeonameId, DisplayName, LocationType, Population
      FROM Locations
        WHERE LocationType = 6 AND ranking <= 100
   )
   SELECT TOP 300 GeonameId, DisplayName, LocationType
   FROM RankedLocations
   ORDER BY Population DESC

  END TRY

  BEGIN CATCH
    SELECT GeonameId, DisplayName, LocationType
    FROM place.Geonames
      WHERE LocationType = 888
          --SELECT CONCAT('usp_SearchGeonames procedure failed. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
    --              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
    --              ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
    --              ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
    --              ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
    --              ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
  END CATCH;

END