-- =============================================
-- Author:		Vivian
-- Create date: 2018-11 
-- Description:	Update George DiseaseAPI tables
--				Input: Json strings
-- Updated 2019-02 
-- Updated 2019-09: new schema, only pull speciesId=1 and 'Infectious Disease' only
-- =============================================
CREATE PROCEDURE [bd].usp_PullRegularTables 
	@JsonSymptoms nvarchar(max),
	@JsonDiseases nvarchar(max),
	@JsonGeorgeMessaging nvarchar(max),
	@JsonGeorgeModifiers nvarchar(max),
	@JsonInterventionCategory nvarchar(max)
AS
BEGIN
    SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN

			--I. Clean tables
			--1st round
			Delete from bd.DiseaseActivityMod --4
			Delete from bd.DiseaseIncubation --5
			Delete from bd.DiseaseMobileMessage --6
			Delete from bd.DiseasePreventionMod --8
			Delete from bd.DiseaseSeasonality --9
			Delete from bd.DiseaseSeverity --10
			Delete from bd.DiseaseSeverityMod --11
			Delete from bd.DiseaseSymptom --12
			Delete from bd.DiseaseTransmission --13
			--2nd round
			Delete from bd.DiseasePrevention --7
			Delete from bd.Symptom --17
			--3rd round
			Delete from bd.PreventionType --used to be unchanged, part of PreventionTypeInterventionCategory
			Delete from bd.PreventionTypeInterventionCategory --no need directly for George, helps PreventionType
			Delete from bd.Disease --3 

			--II. new data
			/*Symptom*/
			--insert into bd.Symptom (altNames N/A)
			SET IDENTITY_INSERT bd.Symptom ON
			Insert into bd.Symptom(symptomId, [name], symptomCategoryId, [definition], definitionSource)
				Select symptomId, symptom, systemId, [definition], definitionSource
				From OPENJSON(@JsonSymptoms)
				WITH (symptomId int, 
					symptom varchar(256),
					systemId int,
					[definition] varchar(max),
					definitionSource varchar(256)
					)
			SET IDENTITY_INSERT bd.Symptom OFF

			/*disease related*/
			--A. useful tables
			--1. Diseases used in George: "only contains george diseases for which there is a featureclass for"
			Declare @tbl_validDisease table (diseaseId int)
			Insert into @tbl_validDisease
				SELECT SUBSTRING(c.name, 2, CHARINDEX('_', c.name)-2)
				FROM sys.columns c
					JOIN sys.tables t ON c.object_id = t.object_id
				WHERE t.name = 'DiseaseConditions_GCS' and c.name like '%_prev'
					and SCHEMA_NAME(t.schema_id)='map';

			--2. main diseases table--from 2 APIs
			Declare @tbl_updateDiseases table (
				diseaseId int, 
				[name] nvarchar(64),
				[colloquialNames] nvarchar(256),
				[searchTerms] nvarchar(2000),
				pronunciation nvarchar(64),
				[diseaseType] varchar(32), --pathogenType->agentType
				microbe varchar(64),
				[environmentalFactors] varchar(64),
				lastModified datetime,
				extentVetted bit,
				prevalenceVetted bit,
				presenceBitmask int,
				preventability float,
				modelWeight float,
				notes varchar(max)
				)

			--B. API
			--1. GET /Diseases/GeorgeMessaging
			Declare @tbl_GeorgeMessage table (diseaseId int, sectionId int, [message] varchar(max))
			-- insert into table
			INSERT INTO @tbl_GeorgeMessage(diseaseId, sectionId, [message])
			Select diseaseId, sectionId, [message]
			From OPENJSON(@JsonGeorgeMessaging)
			WITH (diseaseId int, 
				sectionId int, 
				[message] varchar(max)
				)
			Where diseaseId in (Select diseaseId From @tbl_validDisease)

			--2. GET /Diseases/Diseases
			--2.1 tbl to store info
			Declare @tbl_DiseasesAPI table (
				diseaseId int, 
				[name] nvarchar(64),
				alternateDiseaseNames nvarchar(max),
				pronunciation nvarchar(64),
				severityLevel varchar(50),
				treatmentAvailable varchar(10),
				isChronic bit,
				transmissionModes nvarchar(max),
				agents nvarchar(max),
				environmentalFactors nvarchar(max),
				diseaseSymptoms nvarchar(max),
				interventions nvarchar(max),
				incubation nvarchar(max),
				lastModified datetime
				)
			--2.2 inserts into DiseasesAPI table
			Insert into @tbl_DiseasesAPI(diseaseId, [name], alternateDiseaseNames, pronunciation,
				severityLevel, treatmentAvailable, isChronic, transmissionModes, agents, environmentalFactors, 
				diseaseSymptoms, interventions, incubation, lastModified)
			Select diseaseId, disease, alternateDiseaseNames, pronunciation, severityLevel, 
				treatmentAvailable, isChronic, transmissionModes, agents, environmentalFactors, diseaseSymptoms,
				interventions, incubation, lastModified
			From OPENJSON(@JsonDiseases)
			WITH (diseaseId int, 
				disease nvarchar(64),
				alternateDiseaseNames nvarchar(max) AS JSON,
				pronunciation nvarchar(64),
				severityLevel varchar(50),
				treatmentAvailable varchar(10),
				isChronic bit,
				transmissionModes nvarchar(max) AS JSON,
				agents nvarchar(max) AS JSON,
				environmentalFactors nvarchar(max) AS JSON,
				diseaseSymptoms nvarchar(max) AS JSON,
				interventions nvarchar(max) AS JSON,
				incubation nvarchar(max) AS JSON,
				lastModified char(19),
				diseaseType varchar(200)
				)
			Where diseaseId in (select diseaseId from @tbl_validDisease)
				and diseaseType='Infectious Disease';
			--2.3 insert into main disease table directly from DiseasesAPI
			Insert into @tbl_updateDiseases(diseaseId, [name], pronunciation, lastModified)
				Select diseaseId, [name], pronunciation, lastModified
				From @tbl_DiseasesAPI

			--2.4 alternateName for colloquialNames/searchTerms
			--2.4.1 extract from json
			Declare @tbl_alternateName table (diseaseId int, alternateName nvarchar(256), isColloquial bit, isSearchTerm bit )
			Insert into @tbl_alternateName(diseaseId, alternateName, isColloquial, isSearchTerm)
				Select diseaseId, alternateName, isColloquial, isSearchTerm
				From @tbl_DiseasesAPI
				CROSS APPLY OPENJSON (alternateDiseaseNames)
					With (alternateName nvarchar(256),
						isColloquial bit,
						isSearchTerm bit) as f2;
			--2.4.2 inserts into main disease table
			If Exists (Select 1 from @tbl_alternateName)
			Begin
				--colloquialNames
				With T1 as (
					Select distinct ST2.diseaseId, 
						substring(
							(	Select ','+ST1.alternateName  AS [text()]
								From @tbl_alternateName ST1
								Where isColloquial=1 and ST1.diseaseId = ST2.diseaseId
								For XML PATH ('')
							), 2, 5000) colloquialNames
					from @tbl_alternateName ST2
				)
				Update @tbl_updateDiseases Set colloquialNames=T1.colloquialNames
				From @tbl_updateDiseases as f1, T1
				Where f1.diseaseId=T1.diseaseId;
				--searchTerms
				With T2 as (
					Select distinct ST2.diseaseId, 
						substring(
							(	Select ','+ST1.alternateName  AS [text()]
								From @tbl_alternateName ST1
								Where isSearchTerm=1 and ST1.diseaseId = ST2.diseaseId
								For XML PATH ('')
							), 2, 5000) searchTerms
					from @tbl_alternateName ST2
				)
				Update @tbl_updateDiseases Set searchTerms=T2.searchTerms
				From @tbl_updateDiseases as f1, T2
				Where f1.diseaseId=T2.diseaseId
			End

			--2.5 agents (diseaseType/microbe)
			--2.5.1 extract from json
			Declare @tbl_agents table (diseaseId int, diseaseType varchar(32), microbe varchar(64), Seq int)
			Insert into @tbl_agents(diseaseId, diseaseType, microbe, Seq)
				Select diseaseId, agentType, agent, ROW_NUMBER() OVER (PARTITION by diseaseId, agentType Order by agentType)
				From @tbl_DiseasesAPI
				CROSS APPLY OPENJSON (agents)
					With (agentType varchar(32),
						agent varchar(64)) as f2;
			--2.5.2 inserts into main disease table according to diseaseType freq 
			With T1 as (
				Select diseaseId, diseaseType, count(*) as numMicrobe
				From @tbl_agents
				Group by diseaseId, diseaseType
			),
			T2 as (
			Select *, ROW_NUMBER() OVER (PARTITION by diseaseId, diseaseType ORDER BY numMicrobe desc, diseaseType) as rankId
			From T1
			)
			Update @tbl_updateDiseases Set diseaseType=f3.diseaseType, microbe=f3.microbe
			From @tbl_updateDiseases as f1, T2, @tbl_agents as f3
			Where T2.rankId=1 and f3.Seq=1 and f1.diseaseId=T2.diseaseId 
				and T2.diseaseId=f3.diseaseId and T2.diseaseType=f3.diseaseType

			--2.6 environmentalFactor
			--2.6.1 extract from json
			Declare @tbl_environmentalFactors table (diseaseId int, environmentalFactor varchar(64))
			Insert into @tbl_environmentalFactors(diseaseId, environmentalFactor)
				Select diseaseId, environmentalFactor
				From @tbl_DiseasesAPI
				CROSS APPLY OPENJSON (environmentalFactors)
					With (environmentalFactor varchar(64)) as f2;
			--2.6.2 inserts into main disease table
			If Exists (Select 1 from @tbl_environmentalFactors)
			Begin
				--environmentalFactor
				With T1 as (
					Select distinct ST2.diseaseId, 
						substring(
							(	Select ','+ ST1.environmentalFactor  AS [text()]
								From @tbl_environmentalFactors ST1
								Where ST1.diseaseId = ST2.diseaseId
								For XML PATH ('')
							), 2, 5000) environmentalFactors
					from @tbl_environmentalFactors ST2
				)
				Update @tbl_updateDiseases Set environmentalFactors=T1.environmentalFactors
				From @tbl_updateDiseases as f1, T1
				Where f1.diseaseId=T1.diseaseId;
			End

			--3. GET /Diseases/GeorgeModifiers
			--3.1 tbl to store API info
			Declare @tbl_GeorgeModifiers table (
				diseaseId int, 
				vettedForProduct bit,
				presenceBitmask int,
				preventability float,
				modelWeight float,
				notes varchar(max),
				interventionModifiers nvarchar(max),
				severityModifiers nvarchar(max),
				activityModifiers nvarchar(max),
				seasonalityModifiers nvarchar(max)
				)
			--3.2 insert into GeorgeModifiers table
			Insert into @tbl_GeorgeModifiers(diseaseId, vettedForProduct, presenceBitmask, 
					preventability, modelWeight, notes, interventionModifiers, 
					severityModifiers, activityModifiers, seasonalityModifiers)
			Select diseaseId, vettedForProduct, presenceBitmask, 
					preventability, modelWeight, notes, interventionModifiers, 
					severityModifiers, activityModifiers, seasonalityModifiers
			From OPENJSON(@JsonGeorgeModifiers)
			WITH (diseaseId int, 
				vettedForProduct bit,
				presenceBitmask int,
				preventability float,
				modelWeight float,
				notes varchar(max),
				interventionModifiers nvarchar(max) AS JSON,
				severityModifiers nvarchar(max) AS JSON,
				activityModifiers nvarchar(max) AS JSON,
				seasonalityModifiers nvarchar(max) AS JSON
				)
			Where diseaseId in (select diseaseId from @tbl_validDisease)
			--3.3 save in disease table
			Update @tbl_updateDiseases
			Set extentVetted=f2.vettedForProduct, prevalenceVetted=f2.vettedForProduct, 
				presenceBitmask=f2.presenceBitmask, preventability=f2.preventability, 
				modelWeight=f2.modelWeight, notes=f2.notes
			From @tbl_updateDiseases as f1, @tbl_GeorgeModifiers as f2
			Where f1.diseaseId=f2.diseaseId

			--4. GET /Diseases/InterventionCategory
			Declare @tbl_InterventionCategory table (interventionCategoryId	NVARCHAR (128), [type] VARCHAR (64), interventionTypeId INT)
			Insert into @tbl_InterventionCategory(interventionCategoryId, [type], interventionTypeId)
			Select interventionCategoryId, interventionCategory, interventionTypeId
			From OPENJSON(@JsonInterventionCategory)
			WITH (interventionCategoryId NVARCHAR (128), 
				interventionCategory VARCHAR (64),
				interventionTypeId INT
				)

			--C. Insert new data 
			--1. Disease(#3)
			SET IDENTITY_INSERT bd.Disease ON
			Insert into bd.Disease([diseaseId], [name], [colloquialNames], [searchTerms], [pronunciation]
				  ,[diseaseType],[microbe], [extentVetted], [prevalenceVetted]
				  ,[canUseForAnalytics], [presenceBitmask], [preventability], [modelWeight]
				  ,[environmentalFactors], [notes], [lastModified])
				Select [diseaseId], [name], ISNULL([colloquialNames], ''), 
					ISNULL([searchTerms], ''), [pronunciation]
				  ,ISNULL([diseaseType], ''),[microbe], [extentVetted], [prevalenceVetted]
				  ,1, [presenceBitmask], [preventability], [modelWeight]
				  ,ISNULL([environmentalFactors], ''), [notes], [lastModified]
				From @tbl_updateDiseases
			SET IDENTITY_INSERT bd.Disease OFF

			--1.5 PreventionTypeInterventionCategory and 
			DBCC CHECKIDENT ('bd.PreventionTypeInterventionCategory', RESEED, 0)
			Insert into bd.PreventionTypeInterventionCategory(InterventionCategoryId, [type], InterventionTypeId)
			Select interventionCategoryId, [type], interventionTypeId
			From @tbl_InterventionCategory
			-- PreventionType
			SET IDENTITY_INSERT bd.PreventionType ON
			Insert into bd.PreventionType([id], [type])
			Select [id], [type]
			From bd.PreventionTypeInterventionCategory
 			SET IDENTITY_INSERT bd.PreventionType OFF

			--2. DiseasePrevention(#7) from interventions (interventionType=type in bd.PreventionType)
			SET IDENTITY_INSERT bd.DiseasePrevention ON
			Insert into bd.DiseasePrevention([id], [diseaseId], 
					[type], [riskReduction], [availability], [category], 
					[travel], [oral], [duration])
				Select interventionId, diseaseId, f3.id, [riskReduction], 
					interventionAccessibility, categoryId, [travel], [oral], [duration]
				From @tbl_DiseasesAPI as f1
				CROSS APPLY OPENJSON (interventions)
					With (interventionId int,
						interventionCategory varchar(64),
						interventionCategoryId NVARCHAR (128),
						riskReduction float,
						interventionAccessibility varchar(64),
						categoryId int,
						travel bit,
						oral bit,
						duration varchar(64),
						speciesId int
						) as f2, bd.PreventionTypeInterventionCategory as f3
				Where f2.speciesId=1 and f2.interventionCategoryId=f3.InterventionCategoryId;
 			SET IDENTITY_INSERT bd.DiseasePrevention OFF

			--3. DiseasePreventionMod(#8) from preventionModifiers of Gmo
			Insert into bd.DiseasePreventionMod(prevention, conditionId, messageId)
				Select distinct interventionId, conditionId, messageId
				From @tbl_GeorgeModifiers
					CROSS APPLY OPENJSON (interventionModifiers)
						With (interventionId int,
							conditionId int,
							messageId int) as f2
	
			--4. DiseaseTransmission(#13) from transmissionModes of D 
			Insert into bd.DiseaseTransmission([diseaseId], [mode], [rank]
						,[agents], [contact], [actions])
				Select diseaseId, transmissionModeId, transmissionRank, reservoirVector, contact, actions
				From @tbl_DiseasesAPI
				CROSS APPLY OPENJSON (transmissionModes)
					With (transmissionModeId int,
						transmissionRank int,
						reservoirVector varchar(64),
						contact varchar(64),
						actions varchar(64),
						speciesId int
						) as f2
					Where speciesId=1;

			--5. DiseaseSymptom(#12) from diseaseSymptoms of D (verify), notes not to expose
			Insert into bd.DiseaseSymptom([diseaseId], [symptomId], [associationScore])
				Select diseaseId, [symptomId], [associationScore]
				From @tbl_DiseasesAPI
				CROSS APPLY OPENJSON (diseaseSymptoms)
					With ([symptomId] int,
						[associationScore] float,
						speciesId int
						) as f2
					Where speciesId=1;

			--6. DiseaseSeverityMod(#11) from severityModifiers of Gmo
			Insert into bd.DiseaseSeverityMod(diseaseId, conditionId, addend, conditionParameter)
			Select diseaseId, conditionId, addend, conditionParameter
			From @tbl_GeorgeModifiers
				CROSS APPLY OPENJSON (severityModifiers)
					With (conditionId int,
						addend int,
						conditionParameter float) as f2

			--7. DiseaseSeverity(#10) from diseaseSymptoms of D (verify), notes not to expose
			Insert into bd.DiseaseSeverity([diseaseId], [chronic], [level], [treatmentAvailable])
				Select diseaseId, isChronic,
					Case severityLevel When 'low' then 1 
						When 'medium' then 2
						else 3 End, 
					Case When treatmentAvailable='no' then 0 else 1 End 
				From @tbl_DiseasesAPI

			--8. DiseaseSeasonality(#9) from seasonalityModifiers of Gmo
			Insert into bd.DiseaseSeasonality(diseaseId, [zone], fromMonth, toMonth, offSeasonWeight)
			Select diseaseId, zoneId, fromMonth, toMonth, offSeasonWeight
			From @tbl_GeorgeModifiers
				CROSS APPLY OPENJSON (seasonalityModifiers)
					With (zoneId int,
						fromMonth int,
						toMonth int,
						offSeasonWeight float) as f2

			--9. DiseaseMobileMessage(#6) from Gme
			Insert into bd.DiseaseMobileMessage(diseaseId, sectionId, [message])
				Select diseaseId, sectionId, [message]
				From @tbl_GeorgeMessage

			--10. DiseaseIncubation(#9) from incubation of D
			--need a tmp table to remove nulls
			Declare @tbl_tmp table (diseaseId int, [minimumDays] float, [maximumDays] float, [averageDays] float)
			Insert into @tbl_tmp(diseaseId, [minimumDays], [maximumDays], [averageDays])
			Select diseaseId, minimumDays, maximumDays, averageDays
			From @tbl_DiseasesAPI
				CROSS APPLY OPENJSON (incubation) 
				WITH (speciesId int,
					averageDays float,
					minimumDays float,
					maximumDays float
				) as f2
				Where f2.speciesId=1
			--insert into main table
			Insert into bd.DiseaseIncubation(diseaseId, [minimumDays], [maximumDays], [averageDays])
			Select diseaseId, [minimumDays], [maximumDays], [averageDays]
			From @tbl_tmp
			Where [minimumDays] is not NULL and [maximumDays] is not NULL and [averageDays] is not NULL

			--11. DiseaseActivityMod(#4) from activityModifiers of Gmo
			Insert into bd.DiseaseActivityMod(diseaseId, activity, scale)
			Select diseaseId, activityId, scale
			From @tbl_GeorgeModifiers
				CROSS APPLY OPENJSON (activityModifiers)
					With (activityId int,
						scale float) as f2

			SELECT 'Successfully updated' as ErrorMessage
		COMMIT TRAN

	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
END

GO

