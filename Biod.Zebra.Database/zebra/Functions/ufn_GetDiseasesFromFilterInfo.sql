
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	Input: Disease attributes
--				Output: A list of DiseasesId
-- 2019-09: disease schema change
-- =============================================

CREATE FUNCTION zebra.ufn_GetDiseasesFromFilterInfo (
	@DiseasesIds AS VARCHAR(2000),           --Ids CSV
    @TransmissionModesIds AS VARCHAR(256),  --Ids CSV
    @InterventionMethods AS VARCHAR(256),     --InterventionDisplayName's CSV
    @SeverityRisks AS VARCHAR(256),         --SeverityLevelDisplayName's CSV high/low
    @BiosecurityRisks AS VARCHAR(256)       --BiosecurityRiskCode's CSV 
	) 
RETURNS @returnResults TABLE (DiseasesId int)
AS
BEGIN
	--all empty, return empty table
	If @DiseasesIds='' AND @TransmissionModesIds='' AND @InterventionMethods=''
		AND @SeverityRisks='' AND @BiosecurityRisks=''
		Return
	--A. prepare data
	--1. TransmissionModes
	Declare @tbl_DiseaseIds table (DiseaseId int)
	Insert into @tbl_DiseaseIds
		Select item From [bd].[ufn_StringSplit](@DiseasesIds, ',')
	--2. TransmissionModes
	Declare @tbl_TransmissionModes table (TransmissionModeId int)
	Insert into @tbl_TransmissionModes
		Select item From [bd].[ufn_StringSplit](@TransmissionModesIds, ',')
	--3. Intervention 
	--3.1 save input in a table
	Declare @tbl_Interventions table (Intervention varchar(100))
	Insert into @tbl_Interventions
		Select item From [bd].[ufn_StringSplit](@InterventionMethods, ',')
	--3.2 convert to ids
	Declare @tbl_InterventionIds table (InterventionId int)
	Insert into @tbl_InterventionIds
		Select Distinct f1.InterventionId
		From [disease].[Interventions] as f1, @tbl_Interventions as f2
		Where f1.DisplayName=f2.Intervention;
	--3.3 Behavioural is what not in Xtbl_Disease_Interventions
	Declare @tbl_DiseaseFromIntervention table (DiseaseId int)
	--no Intervention diseases
	If CHARINDEX('Behavioural', @InterventionMethods)>0
		Insert into @tbl_DiseaseFromIntervention
			Select DiseaseId
			From [disease].[Diseases]
			Where DiseaseId not in (Select DiseaseId from [disease].Xtbl_Disease_Interventions where SpeciesId=1)
	--3.4 combined with disease with Interventions
	Insert into @tbl_DiseaseFromIntervention
		Select Distinct f1.DiseaseId 
		From [disease].Xtbl_Disease_Interventions as f1, @tbl_InterventionIds as f2
		Where f1.InterventionId=f2.InterventionId 
			and f1.DiseaseId not in (Select DiseaseId from @tbl_DiseaseFromIntervention where SpeciesId=1)
	--4. SeverityRisk
	Declare @tbl_SeverityLevels table (SeverityLevel varchar(100))
	Insert into @tbl_SeverityLevels
		Select item From [bd].[ufn_StringSplit](@SeverityRisks, ',')
	--4. SeverityRisk
	Declare @tbl_BiosecurityRisks table (BiosecurityRisk varchar(100))
	Insert into @tbl_BiosecurityRisks
		Select item From [bd].[ufn_StringSplit](@BiosecurityRisks, ',');

	--B. results
	With T1 as (
		Select Distinct DiseaseId 
		From [disease].[Xtbl_Disease_TransmissionMode] as f1, @tbl_TransmissionModes as f2
		Where f1.SpeciesId=1 and f1.TransmissionModeId=f2.TransmissionModeId
		)
	Insert into @returnResults
		Select DiseaseId from [disease].[Diseases]
		Where (@DiseasesIds='' OR DiseaseId in (Select DiseaseId From @tbl_DiseaseIds))
			AND (@TransmissionModesIds='' OR DiseaseId IN (Select DiseaseId From T1))
			AND (@InterventionMethods='' OR DiseaseId IN (Select DiseaseId From @tbl_DiseaseFromIntervention))
			AND (@SeverityRisks='' OR SeverityLevel in (Select SeverityLevel From @tbl_SeverityLevels))
			AND (@BiosecurityRisks='' OR BiosecurityRisk in (Select BiosecurityRisk From @tbl_BiosecurityRisks))

	Return
END