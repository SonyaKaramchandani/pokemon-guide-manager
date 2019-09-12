
-- =============================================
-- Author:		Vivian
-- Create date: 2019-02 
-- Description:	Take a diseaseId, returns last page: Desease Details
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE [hcw].usp_HcwGetDiseaseDetailInfo
	@DiseaseId int
AS
BEGIN
	SET NOCOUNT ON
		--1. Introduction, from George, do it on server side
		

		--2. Use zebra sp to get most info
		--2.1 eventId from diseaseId
		Declare @myEventId int = (Select top 1 EventId From [surveillance].[Event] Where DiseaseId=@DiseaseId)
		--2.2 a table to store info
		Declare @tbl table (Agents varchar(max), AgentTypes varchar(100), TransmissionMode varchar(200), 
						Vaccination varchar(100), Incubation varchar(100), BiosecurityRisk varchar(500))

		Insert into @tbl
		exec hcw.usp_HcwGetDiseaseInfoByDiseaseId @DiseaseId=@DiseaseId

		--3. symptoms and disease name
		Select f2.DiseaseId, f2.DiseaseName, f1.Agents, f1.AgentTypes, f1.TransmissionMode,
			f1.Incubation, f1.Vaccination, f3.SymptomId, f4.Symptom, f3.AssociationScore
		From @tbl as f1 inner join [disease].Diseases as f2 on @DiseaseId=f2.DiseaseId 
			left join [disease].[Xtbl_Disease_Symptom] as f3 on f3.DiseaseId=@DiseaseId
			left join [disease].Symptoms as f4 on f3.SymptomId=f4.SymptomId
		Where f3.SpeciesId=1


END