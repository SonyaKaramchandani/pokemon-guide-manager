USE [Healthmap]
GO

/****** Object:  UserDefinedFunction [bd].[healthmap_getBdLocation_fn]    Script Date: 2016-01-21 8:21:26 PM ******/
DROP FUNCTION [bd].[healthmap_getBdLocation_fn]
GO

/****** Object:  UserDefinedFunction [bd].[healthmap_getBdLocation_fn]    Script Date: 2016-01-21 8:21:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [bd].[healthmap_getBdLocation_fn](
	@LocationName VARCHAR(250) 
) RETURNS @Tbl TABLE (BdCtryTeryId INT DEFAULT 0, BdProvinceId INT DEFAULT 0, FullLocationName NVARCHAR(350) DEFAULT '')

BEGIN
	DECLARE @location NVARCHAR(350)
	SET @location = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(RTRIM(LTRIM(@LocationName))
						,' ','<>'),'><',''),'<>',' '),' ,', ',')
	                    ,' ', ' AND '), ',', ''),'[',''),']',''),'AND County',''),'AND Province',''),'AND Governorate','')
						,'(',''),')',''),'?',''),'AND State ',''),'AND State ','AND Provincia de')

	INSERT INTO @Tbl
	SELECT TOP 1  BdCtryTeryId, BdProvinceId, FullLocationName FROM [bd].[healthmap_BD_Locations] WHERE CONTAINS(FullLocationName, @location)
	-- Return the result of the function
	RETURN
END
