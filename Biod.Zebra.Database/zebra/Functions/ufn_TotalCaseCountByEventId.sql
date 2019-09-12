
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-11  
-- Description:	https://wiki.bluedot.global/pages/viewpage.action?spaceKey=CEN&title=Zebra+Model+Development&preview=/49414858/53811178/CalculatingCumulativeCaseCounts.pptx
--				highest of the same admin level
-- Modified: 2019-06 input @IsHistory=0 get data from Xtbl_Event_Location, @IsHistory=1 get data from Xtbl_Event_Location_history with EventDateType=2
-- =============================================

CREATE FUNCTION bd.ufn_TotalCaseCountByEventId (@EventId int, @IsHistory bit) 
RETURNS @returnResults TABLE (RepCases int, ConfCases int, Deaths int, SuspCases int)
AS
BEGIN
	--1. all cases
	Declare @tbl_cases table (GeonameId int, RepCases int, ConfCases int, Deaths int, SuspCases int, 
				LocationType int);
	If @IsHistory=0 --case count of now
		Insert into @tbl_cases(GeonameId, RepCases, ConfCases, Deaths, SuspCases, LocationType)
			Select T1.GeonameId, RepCases, ConfCases, Deaths, SuspCases, f2.LocationType
			From [surveillance].[Xtbl_Event_Location] as T1, place.Geonames as f2
			Where T1.EventId=@EventId and T1.GeonameId=f2.GeonameId 
	Else if @IsHistory=1 --case count of last week
		Insert into @tbl_cases(GeonameId, RepCases, ConfCases, Deaths, SuspCases, LocationType)
			Select T1.GeonameId, RepCases, ConfCases, Deaths, SuspCases, f2.LocationType
			From [surveillance].[Xtbl_Event_Location_history] as T1, place.Geonames as f2
			Where T1.EventDateType=2 and T1.EventId=@EventId and T1.GeonameId=f2.GeonameId 
	
	--2. group by LocationType
	Declare @tbl_cases_total table (LocationType int, RepCases int, ConfCases int, Deaths int, SuspCases int);
	Insert into @tbl_cases_total(LocationType, RepCases, ConfCases, Deaths, SuspCases)
		Select LocationType, SUM(RepCases), SUM(ConfCases), SUM(Deaths), SUM(SuspCases)
		From @tbl_cases
		Group by LocationType
	
	--3. find the admin level with highest case count
	Declare @locLevel int
	--Use reported
	If Exists (Select 1 from @tbl_cases_total Where RepCases<>0)
		Set @locLevel=(Select Top 1 LocationType From @tbl_cases_total Order by RepCases Desc, LocationType Desc) 
	--Use confirmed
	Else If Exists (Select 1 from @tbl_cases_total Where ConfCases<>0)
		Set @locLevel=(Select Top 1 LocationType From @tbl_cases_total Order by ConfCases Desc, LocationType Desc) 
	--Use suspected
	Else If Exists (Select 1 from @tbl_cases_total Where SuspCases<>0)
		Set @locLevel=(Select Top 1 LocationType From @tbl_cases_total Order by SuspCases Desc, LocationType Desc) 
	--Use deaths
	Else
		Set @locLevel=(Select Top 1 LocationType From @tbl_cases_total Order by Deaths Desc, LocationType Desc) 
	--fill in final total cases
	If @locLevel=6
		Insert into @returnResults(RepCases, ConfCases, Deaths, SuspCases)
		Select RepCases, ConfCases, Deaths, SuspCases
		From @tbl_cases_total Where LocationType=6
	Else If @locLevel=4
		Insert into @returnResults(RepCases, ConfCases, Deaths, SuspCases)
		Select RepCases, ConfCases, Deaths, SuspCases
		From @tbl_cases_total Where LocationType=4
	Else 
		Insert into @returnResults(RepCases, ConfCases, Deaths, SuspCases)
		Select RepCases, ConfCases, Deaths, SuspCases
		From @tbl_cases_total Where LocationType=2
	
	Return
END