
-- =============================================
-- Author:		Vivian
-- Create date: 2019-02 
-- Description:	Take diseaseIds, returns with their symptomId and associationScore 
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE [hcw].usp_HcwGetDiseaseSymptomScore
	@DiseaseIds varchar(2000)
AS
BEGIN
	SET NOCOUNT ON

	Declare @tbl_disease table (DiseaseId int)
	Insert into @tbl_disease
		Select item From [bd].[ufn_StringSplit](@DiseaseIds, ',')

	Select f2.DiseaseId, f2.SymptomId, f3.Symptom, f2.AssociationScore
	From @tbl_disease as f1, [disease].[Xtbl_Disease_Symptom] as f2, [disease].[Symptoms] as f3
	Where f2.SpeciesId=1 and f1.DiseaseId=f2.DiseaseId and f2.SymptomId=f3.SymptomId

END