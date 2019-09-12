

-- =============================================
-- Author:		Anson Liang
-- Create date: 2018-09 
-- Description:	Input: geometry
--				Output: geometry
-- 14Jan2019: Change input and output to geography
-- =============================================

CREATE FUNCTION [bd].ufn_RemoveArtefacts(@g geography) 
RETURNS geography 
AS 
BEGIN

  DECLARE @h geography = geography::STGeomFromText('POINT EMPTY', @g.STSrid)
  DECLARE @i int = 1
  WHILE @i <= @g.STNumGeometries() BEGIN
    IF(@g.STGeometryN(@i).STDimension() = 2) BEGIN
      SELECT @h = @h.STUnion(@g.STGeometryN(@i))
    END
    SET @i = @i + 1
  END
  RETURN @h

END

