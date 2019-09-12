
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Output case counts by location and sum (GeonameId=-1)
--				(V3)
-- 2018-11: V5, total case count
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetCaseCountByEventId
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	--all cases by locations
	Declare @tbl_cases table (GeonameId int, RepCases int, ConfCases int, Deaths int, SuspCases int, 
				LocationType int, LocationName nvarchar(500), LocationTypeName varchar(50));
	Insert into @tbl_cases(GeonameId, RepCases, ConfCases, Deaths, SuspCases, 
					LocationName, LocationType, LocationTypeName)
		Select T1.GeonameId, RepCases, ConfCases, Deaths, SuspCases, f2.DisplayName, f2.LocationType,
			Case When f2.LocationType=6 Then 'Country'
				When f2.LocationType=4 Then 'Province/State' 
				Else 'City/Township'
			End
		From [surveillance].[Xtbl_Event_Location] as T1, place.Geonames as f2
		Where T1.EventId=@EventId and T1.GeonameId=f2.GeonameId 

	--total
	Declare @tbl_cases_final table (RepCases int, ConfCases int, Deaths int, SuspCases int);
	Insert into @tbl_cases_final(RepCases, ConfCases, Deaths, SuspCases)
		Select RepCases, ConfCases, Deaths, SuspCases
		From bd.ufn_TotalCaseCountByEventId(@EventId, 0)
	
	--output
	Select GeonameId, ConfCases, SuspCases, RepCases, Deaths, LocationName, LocationTypeName as LocationType
	From @tbl_cases
	Union
	Select -1, ConfCases, SuspCases, RepCases, Deaths,
		'-' as LocationName, '-' as LocationType
	From @tbl_cases_final

END