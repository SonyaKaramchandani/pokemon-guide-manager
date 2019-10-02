
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Similar as usp_GetZebraEventInfoByEventId but w/o user info
--				Simple version
-- 2019-07 name changed
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraAnalyticsGetEventByEventId
	@EventId    AS INT
AS
BEGIN
	SET NOCOUNT ON;
	Declare @diseaseId int=(Select DiseaseId from [surveillance].[Event] Where EventId=@EventId)

	If @diseaseId IS NOT NULL
	Begin
		Declare @tbl table (AgentTypes varchar(100), transmissionMode varchar(200), vaccination varchar(100), 
							reasons varchar(max), incubation varchar(100));
		--1. AgentTypes
		With ST1 as (
			Select Distinct f3.AgentType
			From [disease].[Xtbl_Disease_Agents] as f1, [disease].[Agents] as f2, 
				[disease].[AgentTypes] as f3
			Where f1.DiseaseId=@diseaseId and f1.AgentId=f2.AgentId
				and f2.AgentTypeId=f3.AgentTypeId
			)
		Insert into @tbl(AgentTypes)
		Select stuff(
				(	Select ','+ST1.AgentType
					From ST1
					ORDER BY ST1.AgentType
					For XML PATH ('')
				), 1,1,'') ;
		--2. transmissions
		With ST2 as (
			Select Distinct f2.DisplayName
			From [disease].[Xtbl_Disease_TransmissionMode] as f1, [disease].[TransmissionModes] as f2
			Where f1.DiseaseId=@diseaseId and f1.SpeciesId=1
				and f1.TransmissionModeId=f2.TransmissionModeId
			)
		Update @tbl set transmissionMode=(
		Select stuff(
				(	Select ','+ST2.DisplayName
					From ST2
					ORDER BY ST2.DisplayName
					For XML PATH ('')
				), 1,1,'') );
		--solve &amp; issue
		Update @tbl set transmissionMode=REPLACE(transmissionMode, '&amp;', '&')
		--3. incubation
		Declare @incubation varchar(100)
		--get numbers
		Declare @minIncubasion decimal(10,2), @maxIncubasion decimal(10,2), @avgIncubasion decimal(10,2)
		Set @minIncubasion=(Select [IncubationMinimumDays] From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		Set @maxIncubasion=(Select IncubationMaximumDays From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		Set @avgIncubasion=(Select IncubationAverageDays From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		--set unit
		declare @d char(1)='d'
		declare @h char(1)='h'
		--str
		Declare @minStr varchar(10), @maxStr varchar(10), @avgStr varchar(10)
		--min
		If @minIncubasion IS NULL Set @minStr='-'
		Else if @minIncubasion<1 Set @minStr=CONCAT(CONVERT(INT, ROUND(@minIncubasion*24, 0)), @h)
		Else Set @minStr=CONCAT(CONVERT(INT, ROUND(@minIncubasion, 0)), @d)
		--max
		If @maxIncubasion IS NULL Set @maxStr='-'
		Else if @maxIncubasion<1 Set @maxStr=CONCAT(CONVERT(INT, ROUND(@maxIncubasion*24, 0)), @h)
		Else Set @maxStr=CONCAT(CONVERT(INT, ROUND(@maxIncubasion, 0)), @d)
		--avg
		If @avgIncubasion IS NULL Set @avgStr='-'
		Else if @avgIncubasion<1 Set @avgStr=CONCAT(CONVERT(INT, ROUND(@avgIncubasion*24, 0)), @h)
		Else Set @avgStr=CONCAT(CONVERT(INT, ROUND(@avgIncubasion, 0)), @d)
		--all
		If  @minStr='-' and @maxStr='-' and @avgStr='-'
			Set @incubation='-'
		Else
			Set @incubation=CONCAT(@minStr, ' to ', @maxStr, ' (',
					@avgStr, ' avg.)');
		Update @tbl set incubation=@incubation;
		--4. vaccination(intervention)
		With T1 as 
			(Select InterventionId 
			From disease.Xtbl_Disease_Interventions 
			Where DiseaseId=@diseaseId and SpeciesId=1
			)
		Update @tbl set vaccination=(
		select DisplayName 
			from [disease].Interventions as f1, T1 
			Where f1.InterventionId=T1.InterventionId and DisplayName='Prophylaxis'
		Union
		select Concat(DisplayName, ' (', convert(varchar(10), CAST(max(RiskReduction)*100 AS INT)), '% eff.)') 
			from [disease].Interventions as f1, [disease].InterventionSpecies as f2, T1
			Where f1.DisplayName='Vaccine' and f2.SpeciesId=1 
				and f1.InterventionId=T1.InterventionId and f2.InterventionId=T1.InterventionId
			Group by DisplayName);

		--5. case count
		Declare @tbl_case Table(RepCases int, SuspCases int, ConfCases int, Deaths int);
		Insert into @tbl_case(RepCases, SuspCases, ConfCases, Deaths)
			Select Sum(RepCases), Sum(SuspCases), Sum(ConfCases), Sum(Deaths)
			from  [surveillance].[Xtbl_Event_Location] where EventId=@EventId;		

		--6. reason
		With ST3 as (
			Select Distinct f2.ReasonName
			From surveillance.Xtbl_Event_Reason as f1, surveillance.EventCreationReasons as f2
			Where f1.EventId=@EventId and f1.ReasonId=f2.ReasonId
			)
		Update @tbl set reasons=(
		Select stuff(
				(	Select ','+ST3.ReasonName
					From ST3
					ORDER BY ST3.ReasonName
					For XML PATH ('')
				), 1,1,'') );

		--7.ProbabilityMax
		Declare @ProbabilityMax decimal(10,3)
		Set @ProbabilityMax=(Select MaxProb From [zebra].[EventDestinationAirport]
				Where EventId=@EventId And DestinationStationId=-1)

		Select 
			f4.EventTitle, 
			Case When @ProbabilityMax IS NULL Or @ProbabilityMax<0.01 Then 'negligible'
				When @ProbabilityMax<0.2 And @ProbabilityMax>=0.01 Then 'low'
				When @ProbabilityMax>0.7 Then 'high'
			Else 'medium'
			End as PriorityTitle, 
			f4.LastUpdatedDate, f4.StartDate, 
			EndDate, f1.DiseaseName, ISNULL(f3.AgentTypes, '-') as MicrobeType, 
			ISNULL(f3.transmissionMode, '-') as TransmittedBy, @incubation as IncubationPeriod,
			ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, f4.Summary as Brief, 
			f2.ConfCases as CaseConf, f2.SuspCases as CaseSusp, f2.RepCases as CasesRpted, f2.Deaths as Deaths,
			f3.reasons as Reasons, @EventId as EventId, 
			Case When @ProbabilityMax IS NULL Or @ProbabilityMax<0.01 Then 'Negligible'
				When @ProbabilityMax<0.2 And @ProbabilityMax>=0.01 Then 'Low probability'
				When @ProbabilityMax>0.7 Then 'High probability'
				Else 'Medium probability'
			End as ProbabilityName
		From disease.Diseases as f1, @tbl_case as f2, @tbl as f3, 
			surveillance.[Event] as f4
		Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId
	End
	Else
	Begin
		Select '-' as EventTitle, 'negligible' as PriorityTitle, 
			NULL as LastUpdatedDate, 
			NULL as StartDate, NULL as EndDate, '-' as DiseaseName, 
			'-' as MicrobeType, '-' as TransmittedBy, '-' as IncubationPeriod, 
			'-' as Vaccination, '-' as Brief, 0 as CaseConf, 0 as CaseSusp, 0 as CasesRpted,
			0 as Deaths, '-' as Reasons, @EventId as EventId, 
			'Negligible' as ProbabilityName
	End
END