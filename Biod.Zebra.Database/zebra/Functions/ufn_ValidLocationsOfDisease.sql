
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-09  
-- Description:	logic similar as in ufn_ValidLocationsOfEvent in destination, different in local
--				Input: One disease (several events)
--				Output: LocalOrIntlSpread (broader than event's LocalOrIntlSpread), plus case count
--				1-for local: output all admin levels
--				2-for destination spread: country not spread
-- =============================================

CREATE FUNCTION bd.ufn_ValidLocationsOfDisease (@DiseaseId int) 
RETURNS @returnResults TABLE 
	(GeonameId int, LocationType int, LocalOrIntlSpread int, RepCases int, ConfCases int, SuspCases int, Deaths int)
AS
BEGIN
	--A. Active events from disease
	Declare @tbl_events table (EventId int, IsLocalOnly bit)
	Insert into @tbl_events(EventId, IsLocalOnly)
		Select EventId, IsLocalOnly
		From [surveillance].[Event]
		Where DiseaseId=@DiseaseId and EndDate IS NULL and [SpeciesId]=1

	--B. prepare location data
	Declare @tbl_eventLoc table (GeonameId int, LocationType int, IsLocalOnly bit, RepCases int, ConfCases int, SuspCases int, Deaths int);
	Insert into @tbl_eventLoc (GeonameId, LocationType, IsLocalOnly, RepCases, ConfCases, SuspCases, Deaths)
		Select f1.GeonameId, f2.LocationType, f3.IsLocalOnly, SUM(f1.RepCases), SUM(f1.ConfCases), SUM(f1.SuspCases), SUM(f1.Deaths)
		From [surveillance].[Xtbl_Event_Location] as f1, [place].[ActiveGeonames] as f2, @tbl_events as f3
		Where f1.EventId=f3.EventId and f1.GeonameId=f2.GeonameId
		Group by f1.GeonameId, f2.LocationType, f3.IsLocalOnly;
	--B.2 when a loc is both IsLocalOnly=1/0, dump =1 ones
	With T1 as (
		Select f1.GeonameId
		From @tbl_eventLoc as f1, @tbl_eventLoc as f2
		Where f1.GeonameId=f2.GeonameId and f1.IsLocalOnly<>f2.IsLocalOnly
		)
	Delete From @tbl_eventLoc 
		Where IsLocalOnly=1 And GeonameId in (Select GeonameId From T1);

	--C. process
	Insert into @returnResults(GeonameId, LocationType, LocalOrIntlSpread, RepCases, ConfCases, SuspCases, Deaths)
		Select GeonameId, LocationType, 1, RepCases, ConfCases, SuspCases, Deaths
		From @tbl_eventLoc

	--intl spread, no country
	Insert into @returnResults(GeonameId, LocationType, LocalOrIntlSpread, RepCases, ConfCases, SuspCases, Deaths)
		Select GeonameId, LocationType, 2, RepCases, ConfCases, SuspCases, Deaths
		From @tbl_eventLoc
		Where LocationType<>6 and IsLocalOnly=0
	
	Return
END