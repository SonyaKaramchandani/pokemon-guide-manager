
-- =============================================
-- Author:		Vivian
-- Create date: 2019-02 
-- Description:	Take diseaseIds, and user travel/onset info
--				Using incubation time to filter diseaseIds
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE [hcw].usp_HcwGetDiseaseByIncubation
	@DiseaseIds varchar(2000),
	@UserReturnDate date='1900-01-01', --date user left the location, default means don't know
	@LengthOfStay int=-1, --until @UserReturnDate, default means don't know
	@OnsetOfSymptomsDays int=-1 --from onset of symptom to today, default means don't know 
AS
BEGIN
	SET NOCOUNT ON

	If @DiseaseIds<>''
	BEGIN
		--1. Disease
		--1.1 input
		Declare @tbl_disease table (DiseaseId int)
		Insert into @tbl_disease
			Select item From [bd].[ufn_StringSplit](@DiseaseIds, ',')
		--1.2 result
		Declare @tbl_diseaseResult table (DiseaseId int)

		--Return date or onset don't know, return full list of input diseaseIds
		If @UserReturnDate='1900-01-01' OR @OnsetOfSymptomsDays=-1
			Insert into @tbl_diseaseResult
				Select DiseaseId From @tbl_disease
		--Known date information
		Else
		Begin --2
			--2. incubation
			Declare @OnsetSymptomDate date --date of onset of symptoms
			Set @OnsetSymptomDate=DATEADD(D, -@OnsetOfSymptomsDays, GETDATE())
	
			Declare @X1 int --days from arrived in the location to onset of symptom
			If @LengthOfStay<>-1
				Set @X1=DATEDIFF(D, @UserReturnDate, @OnsetSymptomDate) + @LengthOfStay

			--Onset after return
			If @OnsetSymptomDate>=@UserReturnDate
			Begin
				Declare @X2 int --days from left the location to onset of symptom
				Set @X2=DATEDIFF(D, @UserReturnDate, @OnsetSymptomDate)
				Insert into @tbl_diseaseResult
					Select DiseaseId From @tbl_disease
					Except --Shortest possible infected days can be longer than max_incub. 
					Select f1.DiseaseId
						From @tbl_disease as f1, disease.DiseaseSpeciesIncubation as f2
						Where f2.SpeciesId=1 and f1.DiseaseId=f2.DiseaseId and @X2>ROUND(f2.IncubationMaximumSeconds/86400.0, 0)
				If @LengthOfStay<>-1
					--longest possible infected days can be longer than min_incub.
					Delete from @tbl_diseaseResult
					Where DiseaseId In (
					Select f1.DiseaseId
						From @tbl_disease as f1, disease.DiseaseSpeciesIncubation as f2
						Where f2.SpeciesId=1 and f1.DiseaseId=f2.DiseaseId and @X1<ROUND(f2.IncubationMinimumSeconds/86400.0, 0))
			End
			--Onset before return
			Else 
			Begin
				If @LengthOfStay<>-1
					Insert into @tbl_diseaseResult
						Select f1.DiseaseId
						From @tbl_disease as f1, disease.DiseaseSpeciesIncubation as f2
						Where f2.SpeciesId=1 and @X1>=ROUND(f2.IncubationMinimumSeconds/86400.0, 0) 
							and @X1<=ROUND(f2.IncubationMaximumSeconds/86400.0, 0) and f1.DiseaseId=f2.DiseaseId
				Else -- when don't know @LengthOfStay, return full list
					Insert into @tbl_diseaseResult
						Select DiseaseId From @tbl_disease;
			End
		End;
		-- return
		Select f1.DiseaseId, f3.SymptomId, f4.Symptom, f3.AssociationScore
		From @tbl_diseaseResult as f1 
			left join [disease].[Xtbl_Disease_Symptom] as f3 on f3.DiseaseId=f1.DiseaseId
			left join [disease].Symptoms as f4 on f3.SymptomId=f4.SymptomId
		Where f3.SpeciesId=1
	END --2 end
	ELSE
		Select NULL as DiseaseId, NULL as SymptomId, NULL as Symptom

END