
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2020-02  
-- Description:	Called by usp_ZebraDataRenderSetImportationRiskByGeonameId, usp_ZebraDataRenderSetImportationRiskByGeonameIdSpreadMd
--				Returns all local active events
-- Update: call ufn_GetProximalEventLocations
-- =============================================

CREATE FUNCTION bd.ufn_GetLocalEventsByGeoname (@GeonameId AS int) 
RETURNS @returnResults TABLE (EventId int)
AS
BEGIN
	Insert into @returnResults
		Select distinct EventId
		From zebra.ufn_GetProximalEventLocations(@GeonameId, NULL, NULL)
        Where IsWithinLocation=1
	
    Return
END