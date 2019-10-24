
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-11  
-- Description:	https://wiki.bluedot.global/pages/viewpage.action?spaceKey=CEN&title=Zebra+Model+Development&preview=/49414858/53811178/CalculatingCumulativeCaseCounts.pptx
--				highest of the same admin level
-- Modified: 2019-06 input @IsHistory=0 get data from Xtbl_Event_Location, @IsHistory=1 get data from Xtbl_Event_Location_history with EventDateType=2
-- Modified: 2019-10 changes based on pptx in https://bluedotglobal.atlassian.net/browse/PT-279
--			For one country only, calculate cases province by province, take the largest number
-- =============================================

CREATE FUNCTION bd.ufn_TotalCaseCountByEventId (@EventId int, @IsHistory bit) 
RETURNS @returnResults TABLE (RepCases int, ConfCases int, Deaths int, SuspCases int)
AS
BEGIN
	--1. all cases
	Declare @tbl_cases table (GeonameId int, RepCases int, ConfCases int, Deaths int, SuspCases int, 
				LocationType int, Admin1GeonameId int);
	If @IsHistory=0 --case count of now
		Insert into @tbl_cases(GeonameId, RepCases, ConfCases, Deaths, SuspCases, LocationType, Admin1GeonameId)
			Select T1.GeonameId, RepCases, ConfCases, Deaths, SuspCases, f2.LocationType, f2.Admin1GeonameId
			From [surveillance].[Xtbl_Event_Location] as T1, place.Geonames as f2
			Where T1.EventId=@EventId and T1.GeonameId=f2.GeonameId 
	Else if @IsHistory=1 --case count of last week
		Insert into @tbl_cases(GeonameId, RepCases, ConfCases, Deaths, SuspCases, LocationType, Admin1GeonameId)
			Select T1.GeonameId, RepCases, ConfCases, Deaths, SuspCases, f2.LocationType, f2.Admin1GeonameId
			From [surveillance].[Xtbl_Event_Location_history] as T1, place.Geonames as f2
			Where T1.EventDateType=2 and T1.EventId=@EventId and T1.GeonameId=f2.GeonameId 
	
	--2. group by LocationType and province
	Declare @tbl_cases_total table (Admin1GeonameId int, LocationType int, 
									RepCases int, ConfCases int, Deaths int, SuspCases int);
	Insert into @tbl_cases_total(Admin1GeonameId, LocationType, RepCases, ConfCases, Deaths, SuspCases)
		Select Admin1GeonameId, LocationType, SUM(RepCases), SUM(ConfCases), SUM(Deaths), SUM(SuspCases)
		From @tbl_cases
		Group by Admin1GeonameId, LocationType;
	--take the max of each category in each province
	With T1 as (
		Select Admin1GeonameId, 
			MAX(RepCases) as RepCases, MAX(ConfCases) as ConfCases, MAX(Deaths) as Deaths, MAX(SuspCases) as SuspCases
		From @tbl_cases_total
		Group by Admin1GeonameId
		),
	T2 as (
		Select SUM(RepCases) as RepCases, SUM(ConfCases) as ConfCases, SUM(Deaths) as Deaths, SUM(SuspCases) as SuspCases
		From T1 
		Where Admin1GeonameId IS NOT NULL
		Union
		Select RepCases, ConfCases, Deaths, SuspCases
		From T1 
		Where Admin1GeonameId IS NULL
		)
	Insert into @returnResults(RepCases, ConfCases, Deaths, SuspCases)
		Select MAX(RepCases) as RepCases, MAX(ConfCases) as ConfCases, MAX(Deaths) as Deaths, MAX(SuspCases) as SuspCases
		From T2
	
	Return
END