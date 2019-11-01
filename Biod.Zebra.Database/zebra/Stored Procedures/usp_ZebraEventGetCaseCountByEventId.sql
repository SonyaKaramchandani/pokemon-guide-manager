
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Output case counts by location and sum (GeonameId=-1)
--				(V3)
-- 2018-11: V5, total case count
-- 2019-07 name changed
-- 2019-11 add 4 output columns to show whether case counts are raw or calculated
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetCaseCountByEventId
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	--all cases by locations
	Declare @tbl_cases table (GeonameId int, RepCases int, ConfCases int, Deaths int, SuspCases int, 
				LocationType int, LocationName nvarchar(500), LocationTypeName varchar(50), Admin1GeonameId int,
				RepCasesIsRaw int, ConfCasesIsRaw int, DeathsIsRaw int, SuspCasesIsRaw int);
	Insert into @tbl_cases(GeonameId, RepCases, ConfCases, Deaths, SuspCases, 
					LocationName, LocationType, LocationTypeName, Admin1GeonameId)
		Select T1.GeonameId, RepCases, ConfCases, Deaths, SuspCases, f2.DisplayName, f2.LocationType,
			Case When f2.LocationType=6 Then 'Country'
				When f2.LocationType=4 Then 'Province/State' 
				Else 'City/Township'
			End,
			f2.Admin1GeonameId
		From [surveillance].[Xtbl_Event_Location] as T1, [place].[ActiveGeonames] as f2
		Where T1.EventId=@EventId and T1.GeonameId=f2.GeonameId 
	--data at what level?
	Declare @hasCity bit=0, @hasProvince bit=0, @hasCountry bit=0
	If Exists (Select 1 From @tbl_cases Where LocationType=2)
		Set @hasCity=1
	If Exists (Select 1 From @tbl_cases Where LocationType=4)
		Set @hasProvince=1
	If Exists (Select 1 From @tbl_cases Where LocationType=6)
		Set @hasCountry=1
	--A. only has one locType
	If (Select count(Distinct LocationType) From @tbl_cases)=1
		Update  @tbl_cases 
		Set RepCasesIsRaw=1, ConfCasesIsRaw=1, DeathsIsRaw=1, SuspCasesIsRaw=1
	--B. at least two locTypes
	Else
	Begin --1
		--1. cities are raw
		If @hasCity=1
			Update @tbl_cases 
			Set RepCasesIsRaw=1, ConfCasesIsRaw=1, DeathsIsRaw=1, SuspCasesIsRaw=1
			Where LocationType=2
		--2. city plus province
		If @hasCity=1 and @hasProvince=1
		Begin --2
			--calculate city total
			Declare @tbl_calulatedProv table (Admin1GeonameId int, RepCases int, ConfCases int, Deaths int, SuspCases int)
			Insert into @tbl_calulatedProv(Admin1GeonameId, RepCases, ConfCases, Deaths, SuspCases)
				Select Admin1GeonameId, SUM(RepCases), SUM(ConfCases), SUM(Deaths), SUM(SuspCases)
				From @tbl_cases
				Where LocationType=2
				Group by Admin1GeonameId;
			--city total vs province raw
			If @hasProvince=1
				With T1 as (
					Select f1.GeonameId, 
						Case When f1.RepCases>=f2.RepCases Then f1.RepCases Else f2.RepCases End as RepCases, 
						Case When f1.ConfCases>=f2.ConfCases Then f1.ConfCases Else f2.ConfCases End as ConfCases, 
						Case When f1.Deaths>=f2.Deaths Then f1.Deaths Else f2.Deaths End as Deaths, 
						Case When f1.SuspCases>=f2.SuspCases Then f1.SuspCases Else f2.SuspCases End as SuspCases, 
						Case When f1.RepCases>=f2.RepCases Then 1 Else 0 End as RepCasesIsRaw, 
						Case When f1.ConfCases>=f2.ConfCases Then 1 Else 0 End as ConfCasesIsRaw, 
						Case When f1.Deaths>=f2.Deaths Then 1 Else 0 End as DeathsIsRaw, 
						Case When f1.SuspCases>=f2.SuspCases Then 1 Else 0 End as SuspCasesIsRaw
					From @tbl_cases as f1, @tbl_calulatedProv as f2
					Where f1.LocationType=4 and f1.GeonameId=f2.Admin1GeonameId
					)
				Update @tbl_cases
					Set RepCases=T1.RepCases, ConfCases=T1.ConfCases, 
						Deaths=T1.Deaths, SuspCases=T1.SuspCases,
						RepCasesIsRaw=T1.RepCasesIsRaw, ConfCasesIsRaw=T1.ConfCasesIsRaw, 
						DeathsIsRaw=T1.DeathsIsRaw, SuspCasesIsRaw=T1.SuspCasesIsRaw
				From @tbl_cases as f1, T1
				Where f1.GeonameId=T1.GeonameId;
			--city plus province plus country
			If @hasCountry=1
			Begin --2.1
				With T1 as (
					Select Admin1GeonameId, RepCases, ConfCases, Deaths, SuspCases,
						RepCasesIsRaw, ConfCasesIsRaw, DeathsIsRaw, SuspCasesIsRaw
					From @tbl_cases
					Where LocationType=4
					Union
					Select Admin1GeonameId, RepCases, ConfCases, Deaths, SuspCases, 1, 1, 1, 1
					From @tbl_calulatedProv
					Where Admin1GeonameId not in (Select GeonameId From @tbl_cases Where LocationType=4)
					),
				T2 as (
					Select SUM(RepCases) as RepCases, SUM(ConfCases) as ConfCases, 
						SUM(Deaths) as Deaths, SUM(SuspCases) as SuspCases, 
						MIN(RepCasesIsRaw) as RepCasesIsRaw, MIN(ConfCasesIsRaw) as ConfCasesIsRaw, 
						MIN(DeathsIsRaw) as DeathsIsRaw, MIN(SuspCasesIsRaw) as SuspCasesIsRaw
					From T1
					),
				T3 as (
					Select  
						Case When f1.RepCases>=f2.RepCases Then f1.RepCases Else f2.RepCases End as RepCases, 
						Case When f1.ConfCases>=f2.ConfCases Then f1.ConfCases Else f2.ConfCases End as ConfCases, 
						Case When f1.Deaths>=f2.Deaths Then f1.Deaths Else f2.Deaths End as Deaths, 
						Case When f1.SuspCases>=f2.SuspCases Then f1.SuspCases Else f2.SuspCases End as SuspCases, 
						Case When f1.RepCases>=f2.RepCases Then 1 Else f2.RepCasesIsRaw End as RepCasesIsRaw, 
						Case When f1.ConfCases>=f2.ConfCases Then 1 Else f2.ConfCasesIsRaw End as ConfCasesIsRaw, 
						Case When f1.Deaths>=f2.Deaths Then 1 Else f2.DeathsIsRaw End as DeathsIsRaw, 
						Case When f1.SuspCases>=f2.SuspCases Then 1 Else f2.SuspCasesIsRaw End as SuspCasesIsRaw
					From @tbl_cases as f1, T2 as f2
					Where f1.LocationType=6
					)
				Update @tbl_cases
					Set RepCases=T3.RepCases, ConfCases=T3.ConfCases, 
						Deaths=T3.Deaths, SuspCases=T3.SuspCases,
						RepCasesIsRaw=T3.RepCasesIsRaw, ConfCasesIsRaw=T3.ConfCasesIsRaw, 
						DeathsIsRaw=T3.DeathsIsRaw, SuspCasesIsRaw=T3.SuspCasesIsRaw
				From @tbl_cases as f1, T3
				Where f1.LocationType=6;
			End --2.1
		End --2
		Else --city plus country or province plus Country
		Begin --3
			With T1 as (
					Select SUM(RepCases) as RepCases, SUM(ConfCases) as ConfCases, 
						SUM(Deaths) as Deaths, SUM(SuspCases) as SuspCases
					From @tbl_cases 
					Where LocationType<>6
					),
				T2 as (
					Select  
						Case When f1.RepCases>=f2.RepCases Then f1.RepCases Else f2.RepCases End as RepCases, 
						Case When f1.ConfCases>=f2.ConfCases Then f1.ConfCases Else f2.ConfCases End as ConfCases, 
						Case When f1.Deaths>=f2.Deaths Then f1.Deaths Else f2.Deaths End as Deaths, 
						Case When f1.SuspCases>=f2.SuspCases Then f1.SuspCases Else f2.SuspCases End as SuspCases, 
						Case When f1.RepCases>=f2.RepCases Then 1 Else 0 End as RepCasesIsRaw, 
						Case When f1.ConfCases>=f2.ConfCases Then 1 Else 0 End as ConfCasesIsRaw, 
						Case When f1.Deaths>=f2.Deaths Then 1 Else 0 End as DeathsIsRaw, 
						Case When f1.SuspCases>=f2.SuspCases Then 1 Else 0 End as SuspCasesIsRaw
					From @tbl_cases as f1, T1 as f2
					Where f1.LocationType=6
					)
			Update @tbl_cases
				Set RepCases=T2.RepCases, ConfCases=T2.ConfCases, 
					Deaths=T2.Deaths, SuspCases=T2.SuspCases,
					RepCasesIsRaw=T2.RepCasesIsRaw, ConfCasesIsRaw=T2.ConfCasesIsRaw, 
					DeathsIsRaw=T2.DeathsIsRaw, SuspCasesIsRaw=T2.SuspCasesIsRaw
			From @tbl_cases as f1, T2
			Where f1.LocationType=6;
		End --3
	End --1
	--total
	Declare @tbl_cases_final table (RepCases int, ConfCases int, Deaths int, SuspCases int,
		RepCasesIsRaw int, ConfCasesIsRaw int, DeathsIsRaw int, SuspCasesIsRaw int);
	If @hasCountry=1
		Insert into @tbl_cases_final(RepCases, ConfCases, Deaths, SuspCases, 
									RepCasesIsRaw, ConfCasesIsRaw, DeathsIsRaw, SuspCasesIsRaw)
			Select RepCases, ConfCases, Deaths, SuspCases, RepCasesIsRaw, ConfCasesIsRaw, DeathsIsRaw, SuspCasesIsRaw
			From @tbl_cases
			Where LocationType=6
	Else 
		Insert into @tbl_cases_final(RepCases, ConfCases, Deaths, SuspCases, 
									RepCasesIsRaw, ConfCasesIsRaw, DeathsIsRaw, SuspCasesIsRaw)
			Select SUM(RepCases) as RepCases, SUM(ConfCases) as ConfCases, 
				SUM(Deaths) as Deaths, SUM(SuspCases) as SuspCases, 
				MIN(RepCasesIsRaw) as RepCasesIsRaw, MIN(ConfCasesIsRaw) as ConfCasesIsRaw, 
				MIN(DeathsIsRaw) as DeathsIsRaw, MIN(SuspCasesIsRaw) as SuspCasesIsRaw
			From @tbl_cases 
			Where @hasProvince=0 and LocationType=4 Or LocationType=2
	
	--output
	Select GeonameId, ConfCases, SuspCases, RepCases, Deaths, LocationName, LocationTypeName as LocationType,
			Convert(bit, RepCasesIsRaw) as RepCasesIsRaw, Convert(bit, ConfCasesIsRaw) as ConfCasesIsRaw,
			Convert(bit, DeathsIsRaw) as DeathsIsRaw, Convert(bit, SuspCasesIsRaw) as SuspCasesIsRaw
	From @tbl_cases
	Union
	Select -1, ConfCases, SuspCases, RepCases, Deaths, '-' as LocationName, '-' as LocationType,
			Convert(bit, RepCasesIsRaw) as RepCasesIsRaw, Convert(bit, ConfCasesIsRaw) as ConfCasesIsRaw,
			Convert(bit, DeathsIsRaw) as DeathsIsRaw, Convert(bit, SuspCasesIsRaw) as SuspCasesIsRaw
	From @tbl_cases_final

END