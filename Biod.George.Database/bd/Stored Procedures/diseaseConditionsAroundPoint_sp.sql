/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4457)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

/****** Object:  StoredProcedure [bd].[diseaseConditionsAroundPoint_sp]    Script Date: 2/3/2019 11:20:46 AM ******/

CREATE PROCEDURE [bd].[diseaseConditionsAroundPoint_sp] 
    @latitude FLOAT,
    @longitude FLOAT,
    @radius FLOAT = 50000.0          -- in meters
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @srid INT;
    DECLARE @p geography, @c geography;
    --DECLARE @latradius FLOAT, @longradius FLOAT;
    DECLARE @longStr VARCHAR(12), @latStr VARCHAR(12)
	--, @leftLongStr VARCHAR(12), @rightLongStr VARCHAR(12), @topLatStr VARCHAR(12), @bottomLatStr VARCHAR(12);

    --SET @latradius = @radius / 110574.0;
    --SET @longradius = @radius / (111320.0 * cos(@latitude));

    --SET @longStr = STR(@longitude, 12, 8);
    --SET @latStr = STR(@latitude, 12, 8);
    --SET @leftLongStr = STR(@longitude - @longradius, 12, 8);
    --SET @rightLongStr = STR(@longitude + @longradius, 12, 8);
    --SET @topLatStr = STR(@latitude - @latradius, 12, 8);
    --SET @bottomLatStr = STR(@latitude + @latradius, 12, 8);

    SET @srid = 4326;     -- 4326 is the Spatial Resolution ID (SRID) for the GCS_WGS_1984 projection
    --SET @p = geography::STPointFromText('POINT (' + @latStr + ' ' + @longStr + ')', @srid);
	SET @p = geography::Point(@latitude, @longitude, @srid);
    SET @c =@p.STBuffer(@radius);
    --SET @c = geography::STGeomFromText('CURVEPOLYGON ((' + @leftLongStr + ' ' + @latStr + ', ' + @longStr + ' ' + @topLatStr + ', ' + @rightLongStr + ' ' + @latStr + ', ' + @longStr + ' ' + @bottomLatStr + ', ' + @leftLongStr + ' ' + @latStr + '))', @srid);

    -- Distance returned in degrees
	-- Must use Shape.STIntersects(@c) = 1 to engage spatial index
    SELECT *, (Shape.STDistance(@p) * 110574.0) as Distance  FROM [map].[DiseaseConditions_GCS] WHERE Shape.STIntersects(@c) = 1 ORDER BY Distance;
END
