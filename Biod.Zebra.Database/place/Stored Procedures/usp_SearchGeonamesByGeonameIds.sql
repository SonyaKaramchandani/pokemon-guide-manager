
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-05 
-- Description:	Returns geoname by geonameId
-- Notes: if cityNames are duplicated, select 1st only	
-- =============================================

CREATE PROCEDURE place.usp_SearchGeonamesByGeonameIds 
	@GeonameIds varchar(256)
AS
BEGIN
	SET NOCOUNT ON;

	Select f2.GeonameId, f2.DisplayName,  
				Case When f2.LocationType=6 Then 'Country'
					When  f2.LocationType=4 Then 'Province'
					Else 'City' 
				End As LocationType
	From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, place.Geonames as f2
	Where Convert(int, f1.item)=f2.GeonameId;
	
END