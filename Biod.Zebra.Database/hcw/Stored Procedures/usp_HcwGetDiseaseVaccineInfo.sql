
-- =============================================
-- Author:		Vivian
-- Create date: 2019-02 
-- Description:	Take diseaseIds, and user travel/onset info
--				Using incubation time to filter diseaseIds
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE [hcw].usp_HcwGetDiseaseVaccineInfo
	@DiseaseIds varchar(2000) 
AS
BEGIN
	SET NOCOUNT ON

	If @DiseaseIds<>''
	BEGIN
		--1. Disease
		Declare @tbl_disease table (DiseaseId int)
		Insert into @tbl_disease
			Select item From [bd].[ufn_StringSplit](@DiseaseIds, ',');

		Declare @myBoolean bit=0;
		-- Add info
		With T1 as (
			Select distinct f1.DiseaseId, 1 as HasVaccine
			From [disease].Xtbl_Disease_Interventions as f1, [disease].InterventionSpecies as f2
			Where f1.SpeciesId=1 and f2.SpeciesId=1
				and f1.InterventionId=f2.InterventionId and f2.RiskReduction>=0.95
		)
		Select f1.DiseaseId, f2.DiseaseName, Convert(bit, ISNULL(T1.HasVaccine, 0)) as HasVaccine
		From @tbl_disease as f1 
			inner join [disease].Diseases as f2 on f1.DiseaseId=f2.DiseaseId
			left join T1 on f1.DiseaseId=T1.DiseaseId
	END --2 end
	ELSE
		Select -1 as DiseaseId, '' as DiseaseName, @myBoolean as HasVaccine

END