
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-01 
-- Description:	Input: A json string
-- Output: inserted into diseases related tables
-- Modified 2018-07: use lastModifed, not to compare name anymore
-- Modified 2018-09: added species, Pathogens->Agents, etc.
-- =============================================

CREATE PROCEDURE disease.usp_UpdateDiseases 
	@Json nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. table Agents (previously Pathogens & PathogenTypes)
		Declare @tbl_Agents table (AgentId int, Agent varchar(2000), AgentTypeId int, AgentType varchar(256))
		--A tmp table to hold imcoming Agents
		INSERT INTO @tbl_Agents(AgentId, Agent, AgentTypeId, AgentType)
			Select distinct agentId, agent, agentTypeId, agentType
			FROM OPENJSON(@Json)
				WITH (
				agents nvarchar(max) AS JSON
				)
			CROSS APPLY OPENJSON (Agents) 
				WITH 
				(agentId int,
				agent varchar(2000),
				agentTypeId int,
				agentType varchar(256)) as f2
		--1.1 table AgentTypes
		Declare @tbl_AgentTypes table (AgentTypeId int, AgentType varchar(100))
		Insert into @tbl_AgentTypes(AgentTypeId, AgentType)
			Select distinct AgentTypeId, AgentType
			From @tbl_Agents
		--1.1.1 delete old ones
		Delete from [disease].AgentTypes
			Where AgentTypeId not in (Select AgentTypeId From @tbl_AgentTypes)
		--1.1.2 add new ones
		Insert into [disease].AgentTypes(AgentTypeId, AgentType)
			Select AgentTypeId, AgentType
			From @tbl_AgentTypes
			Where AgentTypeId not in (Select AgentTypeId From [disease].AgentTypes)
		--1.1.3 update existing ones
		Update [disease].AgentTypes Set AgentType=f2.AgentType
			From [disease].AgentTypes as f1, @tbl_AgentTypes as f2
			Where f1.AgentTypeId=f2.AgentTypeId
		--1.2 table Agents
		--1.2.1 delete old ones
		Delete from [disease].Agents
			Where AgentId not in (Select AgentId From @tbl_Agents)
		--1.2.2 update existing ones
		Update [disease].Agents
			Set Agent=f2.Agent, AgentTypeId=f2.AgentTypeId
			From [disease].Agents as f1, @tbl_Agents as f2
			Where f1.AgentId=f2.AgentId
		--1.2.3 add new ones
		Insert into [disease].Agents(AgentId, Agent, AgentTypeId)
			Select AgentId, Agent, AgentTypeId
			From @tbl_Agents
			Where AgentId not in (Select AgentId From [disease].Agents)

		--2. table TransmissionModes 
		Declare @tbl_TransmissionModes table (TransmissionModeId int, TransmissionMode varchar(100), DisplayName varchar(100))
		--A tmp table to hold imcoming TransmissionModes
		INSERT INTO @tbl_TransmissionModes(TransmissionModeId, TransmissionMode, DisplayName)
			Select distinct transmissionModeId, transmissionMode, transmissionModeDisplayName
			FROM OPENJSON(@Json)
				WITH (
				transmissionModes nvarchar(max) AS JSON
				)
				CROSS APPLY OPENJSON (transmissionModes) 
					WITH 
					(transmissionModeId int,
					transmissionMode varchar(100),
					transmissionModeDisplayName varchar(100)
					)
		--2.1 delete old ones
		Delete from [disease].TransmissionModes
			Where TransmissionModeId not in (Select TransmissionModeId From @tbl_TransmissionModes)
		--2.2 update existing ones
		Update [disease].TransmissionModes
			Set TransmissionMode=f2.TransmissionMode, DisplayName=f2.DisplayName
			From [disease].TransmissionModes as f1, @tbl_TransmissionModes as f2
			Where f1.TransmissionModeId=f2.TransmissionModeId
		--2.3 add new ones
		Insert into [disease].TransmissionModes(TransmissionModeId, TransmissionMode, DisplayName)
			Select TransmissionModeId, TransmissionMode, DisplayName
			From @tbl_TransmissionModes
			Where TransmissionModeId not in (Select TransmissionModeId From [disease].TransmissionModes)

		--3. table Interventions (previously Preventions)
		Declare @tbl_InterventionSpecies table (InterventionId int, InterventionType varchar(100), Oral bit,
									RiskReduction decimal (4,2), Duration varchar(100), SpeciesId int, DisplayName varchar(100))
		--A tmp table to hold imcoming Interventions
		INSERT INTO @tbl_InterventionSpecies(InterventionId, InterventionType, Oral, RiskReduction, Duration, SpeciesId, DisplayName)
			Select distinct interventionId, interventionType, oral, riskReduction, duration, speciesId, interventionCategory
			FROM OPENJSON(@Json)
				WITH (
				interventions nvarchar(max) AS JSON
				)
			CROSS APPLY OPENJSON (interventions) 
				WITH (
				interventionId int, 
				interventionType varchar(100), 
				oral bit,
				riskReduction decimal (4,2), 
				duration varchar(100),
				speciesId int,
				interventionCategory varchar(100)
				)
		--3.1 table Interventions
		Declare @tbl_Interventions table (InterventionId int, InterventionType varchar(100), Oral bit, DisplayName varchar(100))
		Insert into @tbl_Interventions
			Select distinct InterventionId, InterventionType, Oral, DisplayName
			From @tbl_InterventionSpecies
		--3.1.1 delete old ones
		Delete from [disease].Interventions
			Where InterventionId not in (Select InterventionId From @tbl_Interventions)
		--3.1.2 update existing ones
		Update [disease].Interventions 
			Set InterventionType=f2.InterventionType, Oral=f2.Oral, DisplayName=f2.DisplayName
			From [disease].Interventions as f1, @tbl_Interventions as f2
			Where f1.InterventionId=f2.InterventionId
		--3.1.3 add new ones 
		Insert into [disease].Interventions(InterventionId, InterventionType, Oral, DisplayName)
			Select InterventionId, InterventionType, Oral, DisplayName
			From @tbl_InterventionSpecies
			Where InterventionId not in (Select InterventionId From [disease].Interventions);
		--3.2 table InterventionSpecies resume 
		--3.2.1 delete old ones
		With T1 as
			(Select InterventionId, SpeciesId from [disease].[InterventionSpecies]
			Except 
			Select InterventionId, SpeciesId From @tbl_InterventionSpecies)
		Delete [disease].[InterventionSpecies] 
			From [disease].[InterventionSpecies] as f1, T1
			Where f1.InterventionId=T1.InterventionId and f1.SpeciesId=T1.SpeciesId;
		--3.2.2 update existing ones
		Update [disease].InterventionSpecies 
			Set RiskReduction=f2.RiskReduction, Duration=f2.Duration
			From [disease].InterventionSpecies as f1, @tbl_InterventionSpecies as f2
			Where f1.InterventionId=f2.InterventionId and f1.SpeciesId=f2.SpeciesId;
		--3.3 add new ones
		With T1 as
			(Select InterventionId, SpeciesId from @tbl_InterventionSpecies
			Except 
			Select InterventionId, SpeciesId From [disease].[InterventionSpecies])
		Insert into [disease].[InterventionSpecies](InterventionId, SpeciesId, RiskReduction, Duration)
			Select f1.InterventionId, f1.SpeciesId, f1.RiskReduction, f1.Duration
			From @tbl_InterventionSpecies as f1, T1
			Where f1.InterventionId=T1.InterventionId and f1.SpeciesId=T1.SpeciesId
		--handle null Duration
		Update [disease].[InterventionSpecies] Set Duration='' Where Duration IS NULL

		--4.[Diseases]-as others, always update
		Declare @tbl_Diseases table ([DiseaseId] int, [DiseaseName] nvarchar(100), Pronunciation varchar(100), 
			[LastModified] datetime, ParentDiseaseId int, SeverityLevel varchar(100),
			IsChronic bit, TreatmentAvailable varchar(100), BiosecurityRisk	VARCHAR(100),
			OutbreakPotentialAttributeId int, [DiseaseType] varchar(200), IsZoonotic bit);
		--A tmp table to hold incoming disease data 
		INSERT INTO @tbl_Diseases ([DiseaseId], [DiseaseName], Pronunciation, [LastModified], 
				ParentDiseaseId, SeverityLevel, IsChronic, TreatmentAvailable, BiosecurityRisk,
				OutbreakPotentialAttributeId, [DiseaseType], IsZoonotic)
		SELECT diseaseId, disease, pronunciation, convert(datetime, lastModified, 126) as lastModified, 
			diseaseParentId, severityLevel, isChronic, treatmentAvailable, biosecurityRisk,
			ISNULL(outbreakPotential,5), diseaseType, isZoonotic
		FROM OPENJSON(@Json)
			WITH (diseaseId int,
				disease varchar(100),
				diseaseType varchar(200),
				pronunciation varchar(100),
				lastModified char(23),
				diseaseParentId int,
				severityLevel varchar(100),
				isChronic bit,
				treatmentAvailable varchar(100),
				biosecurityRisk varchar(100),
				outbreakPotential int,
				isZoonotic bit
			) 
		--4.1 DiseaseId in both old and new: 
		Update [disease].Diseases 
		Set [DiseaseName]=f2.[DiseaseName], Pronunciation=f2.Pronunciation, [LastModified]=f2.[LastModified], 
				ParentDiseaseId=f2.ParentDiseaseId,
				SeverityLevel=f2.SeverityLevel, IsChronic=f2.IsChronic, TreatmentAvailable=f2.TreatmentAvailable,
				BiosecurityRisk=f2.BiosecurityRisk,
				OutbreakPotentialAttributeId=f2.OutbreakPotentialAttributeId,
				[DiseaseType]=f2.[DiseaseType],
				IsZoonotic=f2.IsZoonotic
		From [disease].Diseases as f1, @tbl_Diseases as f2
		Where f1.DiseaseId=f2.DiseaseId 
		--4.2 DiseaseId in old, not in new, delete (will cascade deleted in 3 Xtbls)
		Delete from [disease].Diseases
			Where DiseaseId Not in (Select DiseaseId From @tbl_Diseases)
		--4.3 New DiseaseId: insert into table 
		INSERT INTO [disease].Diseases ([DiseaseId], [DiseaseName], Pronunciation, [LastModified], 
				ParentDiseaseId, SeverityLevel, IsChronic, TreatmentAvailable, BiosecurityRisk,
				OutbreakPotentialAttributeId, [DiseaseType], IsZoonotic)
		Select [DiseaseId], [DiseaseName], Pronunciation, [LastModified],  ParentDiseaseId,
				SeverityLevel, IsChronic, TreatmentAvailable, BiosecurityRisk,
				OutbreakPotentialAttributeId, [DiseaseType], IsZoonotic
		From @tbl_Diseases 
		Where [DiseaseId] NOT IN (Select [DiseaseId] From [disease].Diseases)

		--5.[Xtbl_Disease_Symptom]
		Declare @tbl_Xtbl_Disease_Symptom table (DiseaseId int, SpeciesId int, SymptomId int, 
							Frequency varchar(50), AssociationScore int)
		--A tmp table to hold imcoming Disease_Symptom
		INSERT INTO @tbl_Xtbl_Disease_Symptom(DiseaseId, SpeciesId, SymptomId, Frequency, AssociationScore)
			SELECT distinct diseaseId, f2.speciesId, f2.symptomId, f2.frequency, f2.associationScore
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					diseaseSymptoms nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (diseaseSymptoms) 
					WITH 
					(symptomId int,
					speciesId int,
					frequency varchar(50),
					associationScore int) as f2;
		--5.1 in old not in new, delete
		With T1 as
			(Select DiseaseId, SpeciesId, SymptomId From [disease].[Xtbl_Disease_Symptom]
			Except 
			Select DiseaseId, SpeciesId, SymptomId From @tbl_Xtbl_Disease_Symptom)
		Delete [disease].[Xtbl_Disease_Symptom] 
			From [disease].[Xtbl_Disease_Symptom] as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.SymptomId=T1.SymptomId and f1.SpeciesId=T1.SpeciesId;
		--5.2 update existing ones
		--update Frequency
		Update [disease].[Xtbl_Disease_Symptom]
			Set Frequency=f2.Frequency, AssociationScore=f2.AssociationScore
			From [disease].[Xtbl_Disease_Symptom] as f1, @tbl_Xtbl_Disease_Symptom as f2
			Where f1.DiseaseId=f2.DiseaseId and f1.SymptomId=f2.SymptomId and f1.SpeciesId=f2.SpeciesId;
		--5.3 in new not in old, insert
		With T1 as
			(Select DiseaseId, SpeciesId, SymptomId From @tbl_Xtbl_Disease_Symptom
			Except 
			Select DiseaseId, SpeciesId, SymptomId From [disease].[Xtbl_Disease_Symptom])
		INSERT INTO [disease].[Xtbl_Disease_Symptom](DiseaseId, SpeciesId, SymptomId, Frequency, AssociationScore)
			Select f2. DiseaseId, f2.SpeciesId, f2.SymptomId, f2.Frequency, f2.AssociationScore
			From T1, @tbl_Xtbl_Disease_Symptom as f2
			Where T1.DiseaseId=f2.DiseaseId and T1.SymptomId=f2.SymptomId and T1.SpeciesId=f2.SpeciesId

		--6. [Xtbl_Disease_AlternateName]
		Declare @tbl_Xtbl_Disease_AlternateName table (DiseaseId int, AlternateName nvarchar(200), IsColloquial bit)
		--A tmp table to hold imcoming AlternateNames
		INSERT INTO @tbl_Xtbl_Disease_AlternateName(DiseaseId, AlternateName, IsColloquial)
			SELECT distinct diseaseId, f2.alternateName, f2.isColloquial
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					alternateDiseaseNames nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (alternateDiseaseNames) 
					WITH 
					(alternateName nvarchar(200),
					isColloquial bit) as f2;
		--6.1 in old not in new, delete
		With T1 as
		(Select DiseaseId, AlternateName
			From [disease].Xtbl_Disease_AlternateName
		Except Select DiseaseId, AlternateName 
			From @tbl_Xtbl_Disease_AlternateName)
		Delete [disease].Xtbl_Disease_AlternateName 
			From [disease].Xtbl_Disease_AlternateName as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.AlternateName=T1.AlternateName;
		--6.2 update existing ones
		Update [disease].Xtbl_Disease_AlternateName
		Set IsColloquial=f2.IsColloquial
		From [disease].Xtbl_Disease_AlternateName as f1, @tbl_Xtbl_Disease_AlternateName as f2
		Where f1.DiseaseId=f2.DiseaseId and f1.AlternateName=f2.AlternateName 
			and f1.IsColloquial <> f2.IsColloquial;
		--6.3 in new not in old, insert
		With T1 as
		(Select DiseaseId, AlternateName
			From @tbl_Xtbl_Disease_AlternateName
		Except Select DiseaseId, AlternateName
			From [disease].Xtbl_Disease_AlternateName)
		INSERT INTO [disease].Xtbl_Disease_AlternateName(DiseaseId, AlternateName, IsColloquial)
			Select f2.DiseaseId, f2.AlternateName, f2.IsColloquial 
			From T1, @tbl_Xtbl_Disease_AlternateName as f2
			Where T1.DiseaseId=f2.DiseaseId and T1.AlternateName=f2.AlternateName

		--7. [Xtbl_Disease_TransmissionMode]
		Declare @tbl_Xtbl_Disease_TransmissionMode table (DiseaseId int, TransmissionModeId int, SpeciesId int)
		--A tmp table to hold imcoming TransmissionModes
		INSERT INTO @tbl_Xtbl_Disease_TransmissionMode(DiseaseId, TransmissionModeId, SpeciesId)
			SELECT distinct diseaseId, f2.transmissionModeId, ISNULL(speciesId, 1)
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					transmissionModes nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (transmissionModes) 
					WITH 
					(transmissionModeId int,
					 speciesId int
					 ) as f2;
		--7.1 in old not in new, delete
		With T1 as
		(Select DiseaseId, SpeciesId, TransmissionModeId From [disease].Xtbl_Disease_TransmissionMode
		Except 
		Select DiseaseId, SpeciesId, TransmissionModeId From @tbl_Xtbl_Disease_TransmissionMode)
		Delete [disease].Xtbl_Disease_TransmissionMode 
			From [disease].Xtbl_Disease_TransmissionMode as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.SpeciesId=T1.SpeciesId
				and f1.TransmissionModeId=T1.TransmissionModeId;
		--7.2 in new not in old, insert
		With T1 as
		(Select DiseaseId, SpeciesId, TransmissionModeId From @tbl_Xtbl_Disease_TransmissionMode
		Except 
		Select DiseaseId, SpeciesId, TransmissionModeId From [disease].Xtbl_Disease_TransmissionMode)
		INSERT INTO [disease].Xtbl_Disease_TransmissionMode(DiseaseId, SpeciesId, TransmissionModeId)
			Select DiseaseId, SpeciesId, TransmissionModeId From T1

		--8. [Xtbl_Disease_Agents]
		Declare @tbl_Xtbl_Disease_Agents table (DiseaseId int, AgentId int)
		--A tmp table to hold imcoming Agentss
		INSERT INTO @tbl_Xtbl_Disease_Agents(DiseaseId, AgentId)
			SELECT distinct diseaseId, f2.agentId
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					agents nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (agents) 
					WITH 
					(agentId int) as f2;
		--8.1 in old not in new, delete
		With T1 as
		(Select DiseaseId, AgentId From [disease].Xtbl_Disease_Agents
		Except 
		Select DiseaseId, AgentId From @tbl_Xtbl_Disease_Agents)
		Delete [disease].Xtbl_Disease_Agents 
			From [disease].Xtbl_Disease_Agents as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.AgentId=T1.AgentId;
		--8.2 in new not in old, insert
		With T1 as
		(Select DiseaseId, AgentId From @tbl_Xtbl_Disease_Agents
		Except 
		Select DiseaseId, AgentId From [disease].Xtbl_Disease_Agents)
		INSERT INTO [disease].Xtbl_Disease_Agents(DiseaseId, AgentId)
			Select DiseaseId, AgentId From T1

		--9. [Xtbl_Disease_Interventions]
		Declare @tbl_Xtbl_Disease_Interventions table (DiseaseId int, SpeciesId int, InterventionId int)
		--A tmp table to hold imcoming Interventions
		INSERT INTO @tbl_Xtbl_Disease_Interventions(DiseaseId, SpeciesId, InterventionId)
			SELECT distinct diseaseId, f2.speciesId, f2.interventionId
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					interventions nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (interventions) 
				WITH (interventionId int,
					speciesId int
				) as f2;
		--9.1 in old not in new, delete
		With T1 as
		(Select DiseaseId, SpeciesId, InterventionId From [disease].Xtbl_Disease_Interventions
		Except 
		Select DiseaseId, SpeciesId, InterventionId From @tbl_Xtbl_Disease_Interventions)
		Delete [disease].Xtbl_Disease_Interventions 
			From [disease].Xtbl_Disease_Interventions as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.InterventionId=T1.InterventionId
				and f1.SpeciesId=T1.SpeciesId;
		--9.2 in new not in old, insert
		With T1 as
		(Select DiseaseId, SpeciesId, InterventionId From @tbl_Xtbl_Disease_Interventions
		Except 
		Select DiseaseId, SpeciesId, InterventionId From [disease].Xtbl_Disease_Interventions)
		INSERT INTO [disease].Xtbl_Disease_Interventions(DiseaseId, SpeciesId, InterventionId)
			Select DiseaseId, SpeciesId, InterventionId From T1

		--10. DiseaseSpeciesIncubation
		Declare @tbl_DiseaseSpeciesIncubation table (DiseaseId int, SpeciesId int, 
			[IncubationAverageDays] DECIMAL (10, 2), [IncubationMinimumDays] DECIMAL (10, 2), [IncubationMaximumDays] DECIMAL (10, 2))
		--A tmp table to hold imcoming Interventions
		INSERT INTO @tbl_DiseaseSpeciesIncubation(DiseaseId, SpeciesId, [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays])
			SELECT distinct diseaseId, f2.speciesId, f2.averageDays, f2.minimumDays, f2.maximumDays
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					incubation nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (incubation) 
				WITH (speciesId int,
					averageDays float,
					minimumDays float,
					maximumDays float
				) as f2;
		--10.1 in old not in new, delete
		With T1 as
		(Select DiseaseId, SpeciesId From [disease].[DiseaseSpeciesIncubation]
		Except 
		Select DiseaseId, SpeciesId From @tbl_DiseaseSpeciesIncubation)
		Delete [disease].[DiseaseSpeciesIncubation] 
			From [disease].[DiseaseSpeciesIncubation] as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.SpeciesId=T1.SpeciesId;
		--10.2 update existing ones
		Update [disease].[DiseaseSpeciesIncubation]
			Set [IncubationAverageDays]=f2.[IncubationAverageDays], 
				[IncubationMinimumDays]=f2.[IncubationMinimumDays], 
				[IncubationMaximumDays]=f2.[IncubationMaximumDays]
			From [disease].[DiseaseSpeciesIncubation] as f1, @tbl_DiseaseSpeciesIncubation as f2
			Where f1.DiseaseId=f2.DiseaseId and f1.SpeciesId=f2.SpeciesId;
		--10.3 in new not in old, insert
		With T1 as
		(Select DiseaseId, SpeciesId From @tbl_DiseaseSpeciesIncubation
		Except 
		Select DiseaseId, SpeciesId From [disease].[DiseaseSpeciesIncubation])
		INSERT INTO [disease].[DiseaseSpeciesIncubation](DiseaseId, SpeciesId,
						[IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays])
			Select f1.DiseaseId, f1.SpeciesId, [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays] 
			From T1, @tbl_DiseaseSpeciesIncubation as f1
			Where T1.DiseaseId=f1.DiseaseId and T1.SpeciesId=f1.SpeciesId

		--11. DiseaseSpeciesSymptomatic
		Declare @tbl_DiseaseSpeciesSymptomatic table (DiseaseId int, SpeciesId int, 
			[SymptomaticAverageDays] DECIMAL (10, 2), [SymptomaticMinimumDays] DECIMAL (10, 2), [SymptomaticMaximumDays] DECIMAL (10, 2))
		--A tmp table to hold imcoming Interventions
		INSERT INTO @tbl_DiseaseSpeciesSymptomatic(DiseaseId, SpeciesId, [SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays])
			SELECT distinct diseaseId, f2.speciesId, f2.averageDays, f2.minimumDays, f2.maximumDays
			FROM OPENJSON(@Json)
				WITH (diseaseId int,
					symptomaticPeriod nvarchar(max) AS JSON
				) as f1
				CROSS APPLY OPENJSON (symptomaticPeriod) 
				WITH (speciesId int,
					averageDays float,
					minimumDays float,
					maximumDays float
				) as f2;
		--11.1 in old not in new, delete
		With T1 as
		(Select DiseaseId, SpeciesId From [disease].[DiseaseSpeciesSymptomatic]
		Except 
		Select DiseaseId, SpeciesId From @tbl_DiseaseSpeciesSymptomatic)
		Delete [disease].[DiseaseSpeciesSymptomatic] 
			From [disease].[DiseaseSpeciesSymptomatic] as f1, T1
			Where f1.DiseaseId=T1.DiseaseId and f1.SpeciesId=T1.SpeciesId;
		--11.2 update existing ones
		Update [disease].[DiseaseSpeciesSymptomatic]
			Set [SymptomaticAverageDays]=f2.[SymptomaticAverageDays], 
				[SymptomaticMinimumDays]=f2.[SymptomaticMinimumDays], 
				[SymptomaticMaximumDays]=f2.[SymptomaticMaximumDays]
			From [disease].[DiseaseSpeciesSymptomatic] as f1, @tbl_DiseaseSpeciesSymptomatic as f2
			Where f1.DiseaseId=f2.DiseaseId and f1.SpeciesId=f2.SpeciesId;
		--11.3 in new not in old, insert
		With T1 as
		(Select DiseaseId, SpeciesId From @tbl_DiseaseSpeciesSymptomatic
		Except 
		Select DiseaseId, SpeciesId From [disease].[DiseaseSpeciesSymptomatic])
		INSERT INTO [disease].[DiseaseSpeciesSymptomatic](DiseaseId, SpeciesId,
						[SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays])
			Select f1.DiseaseId, f1.SpeciesId, [SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays] 
			From T1, @tbl_DiseaseSpeciesSymptomatic as f1
			Where T1.DiseaseId=f1.DiseaseId and T1.SpeciesId=f1.SpeciesId

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update diseases in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END