
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	query disease info of an event
--				(V3)
-- 2019-07 name changed
-- 2019-09: disease schema change
-- 2019-11: incubation string calls ufn_FormStringFromSeconds
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetDiseaseByEventId
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	Declare @diseaseId int=(Select DiseaseId from [surveillance].[Event] Where EventId=@EventId)

	If @diseaseId IS NOT NULL
	Begin
		Declare @tbl table (Agents varchar(max), AgentTypes varchar(100), TransmissionMode varchar(200), 
						Vaccination varchar(100), Incubation varchar(100), BiosecurityRisk varchar(500));
		--1. Agents
		With ST1 as (
			Select Distinct f2.Agent
			From [disease].[Xtbl_Disease_Agents] as f1, [disease].[Agents] as f2
			Where f1.DiseaseId=@diseaseId and f1.AgentId=f2.AgentId
			)
		Insert into @tbl(Agents)
		Select stuff(
				(	Select ', '+ST1.Agent
					From ST1
					ORDER BY ST1.Agent
					For XML PATH ('')
				), 1,1,'');
		--2. AgentTypes
		With ST1 as (
			Select Distinct f3.AgentType
			From [disease].[Xtbl_Disease_Agents] as f1, [disease].[Agents] as f2, 
				[disease].[AgentTypes] as f3
			Where f1.DiseaseId=@diseaseId and f1.AgentId=f2.AgentId
				and f2.AgentTypeId=f3.AgentTypeId
			)
		Update @tbl set AgentTypes=(
		Select stuff(
				(	Select ', '+ST1.AgentType
					From ST1
					ORDER BY ST1.AgentType
					For XML PATH ('')
				), 1,1,'') );
		--3. transmissions
		With ST2 as (
			Select Distinct f2.DisplayName
			From [disease].[Xtbl_Disease_TransmissionMode] as f1, [disease].[TransmissionModes] as f2
			Where f1.SpeciesId=1 and
				f1.DiseaseId=@diseaseId and f1.TransmissionModeId=f2.TransmissionModeId
			)
		Update @tbl set TransmissionMode=(
		Select stuff(
				(	Select ', '+ST2.DisplayName
					From ST2
					ORDER BY ST2.DisplayName
					For XML PATH ('')
				), 1,1,'') );
		--solve &amp; issue
		Update @tbl set TransmissionMode=REPLACE(transmissionMode, '&amp;', '&')
		--4. incubation
		Declare @incubation varchar(100)
		--get numbers
		Declare @minIncubation bigint, @maxIncubation bigint, @avgIncubation bigint
		Set @minIncubation=
			(Select [IncubationMinimumSeconds] From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		Set @maxIncubation=
			(Select IncubationMaximumSeconds From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		Set @avgIncubation=
			(Select IncubationAverageSeconds From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		--str
		Declare @minStr varchar(10), @maxStr varchar(10), @avgStr varchar(10)
		--min
		Set @minStr=bd.ufn_FormStringFromSeconds(@minIncubation)
		--max
		Set @maxStr=bd.ufn_FormStringFromSeconds(@maxIncubation)
		--avg
		Set @avgStr=bd.ufn_FormStringFromSeconds(@avgIncubation)
		--all
		If  @minStr='-' and @maxStr='-' and @avgStr='-'
			Set @incubation='-'
		Else
			Set @incubation=CONCAT(@minStr, ' to ', @maxStr, ' (',
					@avgStr, ' avg.)');
		Update @tbl set Incubation=@incubation;
		--5. vaccination(intervention)
		With T1 as (
			Select InterventionId 
			From disease.Xtbl_Disease_Interventions 
			Where DiseaseId=@diseaseId and SpeciesId=1
			)
		Update @tbl set Vaccination=(
		select DisplayName from [disease].Interventions as f1, T1 
			Where f1.InterventionId=T1.InterventionId and DisplayName<>'Vaccine'
				and f1.InterventionType='Prevention'
		Union
		select Concat(DisplayName, ' (', convert(varchar(10), CAST(max(RiskReduction)*100 AS INT)), '% eff.)') 
			from [disease].Interventions as f1, [disease].InterventionSpecies as f2, T1
			Where f1.DisplayName='Vaccine' and f2.SpeciesId=1 
				and f1.InterventionId=T1.InterventionId and f2.InterventionId=T1.InterventionId
			Group by DisplayName);
		--if no data
		Update @tbl set Vaccination='Behavioural Only' Where Vaccination is NULL

		--6. biosecurity risk
		Update @tbl set BiosecurityRisk=(Select ISNULL(f2.BiosecurityRiskDesc, '-')
			From [disease].[Diseases] as f1, disease.BiosecurityRisk as f2
			Where DiseaseId=@diseaseId and f1.BiosecurityRisk=f2.BiosecurityRiskCode)

		--disease Id and name, severity level
		Select Agents, AgentTypes, TransmissionMode, 
				Vaccination, Incubation, BiosecurityRisk
		From @tbl
	End
	Else
		Select '-' as Agents, '-' as AgentTypes, '-' as TransmissionMode, 
				'-' as Vaccination, '-' as Incubation, '-' as BiosecurityRisk
		From @tbl

END