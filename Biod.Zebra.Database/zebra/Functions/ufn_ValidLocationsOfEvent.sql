
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-11  
-- Description:	Input: One event one country
--				Output: LocalOrIntlSpread
--				1-for local notification: always notify the highest adm level user plus in cities' buffer
--				2-for intl spread: country not spread
-- =============================================

CREATE FUNCTION bd.ufn_ValidLocationsOfEvent (@EventId int) 
RETURNS @returnResults TABLE (GeonameId int, LocationType int, LocalOrIntlSpread int)
AS
BEGIN
	
	--A. prepare location data
	Declare @tbl_eventLoc table (GeonameId int, LocationType int);
	Insert into @tbl_eventLoc (GeonameId, LocationType)
		Select f1.GeonameId, f2.LocationType
		From [surveillance].[Xtbl_Event_Location] as f1, place.Geonames as f2
		Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId
	--A.2 IsLocalOnly
	Declare @IsLocalOnly bit=(Select IsLocalOnly from [surveillance].[Event] Where EventId=@EventId)

	--B. process
	--Has country level
	If Exists (Select 1 from @tbl_eventLoc Where LocationType=6) 
		--local, country plus city (it's buffer may incl. foreign cities)
		Insert into @returnResults(GeonameId, LocationType, LocalOrIntlSpread)
		Select GeonameId, LocationType, 1
		From @tbl_eventLoc
		Where LocationType in (2, 6)
	Else --no country
		--local, province plus city
		Insert into @returnResults(GeonameId, LocationType, LocalOrIntlSpread)
		Select GeonameId, LocationType, 1
		From @tbl_eventLoc

	--intl spread, no country
	If @IsLocalOnly=0
		Insert into @returnResults(GeonameId, LocationType, LocalOrIntlSpread)
		Select GeonameId, LocationType, 2
		From @tbl_eventLoc
		Where LocationType<>6
	
	Return
END