
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2018-05 
-- Description:	Input: GeonameId
--				Output: Get the location display name by GeonameId
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraPlaceGetLocationNameByGeonameId
	@GeonameId    AS INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DisplayName LocationName FROM [place].[ActiveGeonames] WHERE GeonameId = @GeonameId
END