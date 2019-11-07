/****** Object:  StoredProcedure [place].[usp_SearchGeonames]    Script Date: 2019-11-05 11:13:10 AM ******/
CREATE PROCEDURE [place].[usp_SearchGeonames] 
  @inputTerm NVARCHAR(200)
AS
BEGIN
  SET NOCOUNT ON;
  BEGIN TRY
    DECLARE @allText NVARCHAR(200) = RTRIM(LTRIM(@inputTerm))
    DECLARE @NumberOfCountriesFound INT = 0
    DECLARE @Countries TABLE
    (
      GeonameId INT,
      DisplayName NVARCHAR(500),
      LocationType INT,
      Population BIGINT,
      Alternatenames NVARCHAR(MAX)
    )
    
    IF (CHARINDEX('"', @allText) > 0)
      -- The search criteria contains quote (")
      -- assume the entire inputTerm is encompasssed by two quotes, The caller should ensure that
      -- the search will try to match the encompassed text exactly as is
      SET @allText = @allText
    ELSE
    BEGIN
      DECLARE @commaIndex INT = CHARINDEX(',', @allText)
      IF (@commaIndex > 0)
      BEGIN
        -- The search criteria contains comma (,)
        -- treat everything before comma as country, the rest as provinces or cities
        -- first search for contries/provinces
        DECLARE @CountryText NVARCHAR(200) = SUBSTRING(@allText, 1, @commaIndex - 1)

        IF (LEN(@CountryText) > 2)
        BEGIN
          IF (CHARINDEX( ' ', @CountryText ) > 0 )
          BEGIN
            -- The search criteria contains multiple words separated by space(s)
            -- replace space(s) with a single comma as required by the NEAR clause syntax
            -- the search will try to match all words (ANDed), the words could be in any order
            -- the search will match whole words only, no partial search
            WHILE @CountryText LIKE '%  %' SET @CountryText = replace(@CountryText, '  ', ' ')
            SET @CountryText = 'NEAR((' + replace(@CountryText, ' ', ',') + '))'
          END
          ELSE
          BEGIN
            -- The search criteria contains a single word
            -- the search will match any word that begins with the passed value (partial match)
            SET @CountryText = '"' + @CountryText + '*"'
          END

          INSERT INTO @Countries
          SELECT GeonameId, DisplayName, LocationType, Population, Alternatenames
          FROM place.[Geonames]
          WHERE LocationType = 6 AND GeonameId IN (
            SELECT GeonameId FROM place.uvw_AlternateNames WHERE CONTAINS(Alternatenames, @CountryText)
            UNION
            SELECT GeonameId FROM place.[Geonames] WHERE CONTAINS(DisplayName, @CountryText)
          )
          SELECT @NumberOfCountriesFound = COUNT(*) FROM @Countries
        END

        SET @allText = RTRIM(LTRIM(SUBSTRING(@allText, @commaIndex + 1, 1000)))
      END

      IF (LEN(@allText) > 2)
      BEGIN
        IF (CHARINDEX( ' ', @allText ) > 0 )
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
      END
    END

    IF (LEN(@allText) < 3)
    BEGIN
      SELECT TOP 100 GeonameId, DisplayName, LocationType
      FROM @Countries
      ORDER BY Population DESC
    END
    ELSE
    BEGIN
      ;WITH Locations AS
      (
        SELECT GeonameId, DisplayName, LocationType, Population, Alternatenames,
        ROW_NUMBER() OVER (PARTITION by LocationType ORDER BY Population DESC) AS Ranking
        FROM place.[Geonames]
        WHERE LocationType IN (2,4,6)
        AND (CountryGeonameId IN (SELECT GeonameId FROM @Countries) OR @NumberOfCountriesFound = 0)
        AND GeonameId IN (

          SELECT GeonameId FROM place.uvw_AlternateNames WHERE CONTAINS(Alternatenames, @allText)
          UNION
          SELECT GeonameId FROM place.[Geonames] WHERE CONTAINS(DisplayName, @allText)
        )
      ),
      RankedLocations AS
      (
        SELECT TOP 100 GeonameId, DisplayName, LocationType, Population
        FROM Locations
          WHERE LocationType = 2 AND Ranking <= 100
        UNION
        SELECT TOP 100 GeonameId, DisplayName, LocationType, Population
        FROM Locations
          WHERE LocationType = 4 AND Ranking <= 100
        UNION
        SELECT TOP 100 GeonameId, DisplayName, LocationType, Population
        FROM Locations
          WHERE LocationType = 6 AND Ranking <= 100
      )
      SELECT TOP 300 GeonameId, DisplayName, LocationType
      FROM RankedLocations
      ORDER BY Population DESC
    END

  END TRY

  BEGIN CATCH
    SELECT GeonameId, DisplayName, LocationType
    FROM place.Geonames
      WHERE LocationType = 888
  END CATCH;

END