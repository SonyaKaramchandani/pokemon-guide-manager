﻿
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Returns a country shape text by geonameId
-- =============================================
CREATE PROCEDURE [zebra].[usp_GetZebraCountryShapeByGeonameId]
	@GeonameId    AS INT
AS
BEGIN
	SET NOCOUNT ON
	Select f2.SimplifiedShape.STAsText() as CountryShapText
	From [place].[Geonames] as f1, [place].[CountryProvinceShapes] as f2
	Where f1.GeonameId=@GeonameId and CountryGeonameId=f2.GeonameId

END