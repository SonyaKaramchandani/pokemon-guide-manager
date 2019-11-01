/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--Update [dbo].[AspNetRoles] Set [IsPublic]=0 Where [IsPublic] is NULL
--GO

--Alter table [dbo].[AspNetRoles] Alter Column IsPublic BIT NOT NULL 
--GO

--print 'Modifying public status in [dbo].[AspNetRoles]'
--update [dbo].[AspNetRoles]
--set [IsPublic] = 1
--where [Name] in ('Doctor', 'IPAC', 'Infection Control', 'Nurse', 'Professor', 'Other', 'Student');
--GO

--give "Other" role to users don't have a public role, always needed
With T1 as (
	Select Id From [dbo].[AspNetUsers]
	Except
	Select UserId From [dbo].[AspNetUserRoles]
		Where RoleId in (Select RoleId From [dbo].[AspNetRoles] Where [IsPublic]=1)
	)
Insert into [dbo].[AspNetUserRoles](UserId, RoleId)
	Select T1.Id, f2.Id
	From T1, [dbo].[AspNetRoles] as f2
	Where f2.Name='Other'
GO

print 'populate [disease].[Species]' 
If Not Exists (Select 1 From [disease].[Species])
	insert into [disease].[Species](SpeciesId, SpeciesName)
		values(1, 'Human')
GO

print 'populate [disease].[Interventions]' 
If Not Exists (Select 1 From [disease].[Interventions])
	insert into [disease].[Interventions]([InterventionId], [InterventionType], [Oral], DisplayName)
		select PreventionId, PreventionType, Oral, DisplayName 
		from [disease].[Preventions]
GO

print 'populate [disease].[InterventionSpecies]' 
If Not Exists (Select 1 From [disease].[InterventionSpecies])
	insert into [disease].InterventionSpecies(InterventionId, SpeciesId, RiskReduction, Duration)
		select PreventionId, 1, RiskReduction, Duration
		from [disease].[Preventions]
GO

print 'populate [disease].[AgentTypes]' 
If Not Exists (Select 1 From [disease].[AgentTypes])
	insert into [disease].[AgentTypes]([AgentTypeId], [AgentType])
		select [PathogenTypeId], [PathogenType] from [disease].[PathogenTypes]
GO

print 'populate [disease].[Agents]' 
If Not Exists (Select 1 From [disease].[Agents])
	insert into [disease].[Agents]([AgentId], [Agent], [AgentTypeId])
		select PathogenId, [Pathogen], [PathogenTypeId] from [disease].Pathogens
GO

print 'populate [disease].[Xtbl_Disease_Interventions]' 
If Not Exists (Select 1 From [disease].[Xtbl_Disease_Interventions])
	insert into [disease].[Xtbl_Disease_Interventions] ([DiseaseId], [SpeciesId], [InterventionId])
		select [DiseaseId], 1, PreventionId from [disease].Xtbl_Disease_Preventions
GO


print 'populate [disease].[Xtbl_Disease_Agents]' 
If Not Exists (Select 1 From [disease].[Xtbl_Disease_Agents])
	insert into [disease].[Xtbl_Disease_Agents] ([DiseaseId], [AgentId])
		select [DiseaseId], PathogenId from [disease].[Xtbl_Disease_Pathogens]
GO


print 'populate [disease].[DiseaseSpeciesSymptomatic]' 
If Not Exists (Select 1 From [disease].[DiseaseSpeciesSymptomatic])
	insert into [disease].[DiseaseSpeciesSymptomatic]([DiseaseId], [SpeciesId], [SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays])
		Select [DiseaseId], 1, [SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays]
		From [disease].tmp_disease 
		where  [SymptomaticAverageDays] is not null
GO

print 'populate [disease].[DiseaseSpeciesIncubation]' 
If Not Exists (Select 1 From [disease].[DiseaseSpeciesIncubation])
	insert into [disease].[DiseaseSpeciesIncubation]([DiseaseId], [SpeciesId], [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays])
		Select [DiseaseId], 1, [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays]
		From [disease].tmp_disease 
		where  [IncubationAverageDays] is not null
GO

print 'clean database'
--Drop Table If Exists [disease].Xtbl_Disease_Pathogens
--Drop Table If Exists [disease].Pathogens
--Drop Table If Exists [disease].PathogenTypes

--Drop Table If Exists [disease].Xtbl_Disease_Preventions
--Drop Table If Exists [disease].[Preventions]

Drop Table If Exists [disease].tmp_disease

--vivian: pt-376 populate country geoname
Declare @tbl_geonameIds table (GeonameId int)
Insert @tbl_geonameIds
	Select [GeonameId] From [place].[Geonames] Where LocationType=6
	Except
	Select [GeonameId] From [place].[ActiveGeonames]
--populate
Insert into [place].[ActiveGeonames] ([GeonameId]
		  ,[Name]
		  ,[LocationType]
		  ,[Admin1GeonameId]
		  ,[CountryGeonameId]
		  ,[DisplayName]
		  ,[Alternatenames]
		  ,[ModificationDate]
		  ,[FeatureCode]
		  ,[CountryName]
		  ,[Latitude]
		  ,[Longitude]
		  ,[Population]
		  ,[SearchSeq2]
		  ,[Shape]
		  ,[LatPopWeighted]
		  ,[LongPopWeighted])
Select f1.[GeonameId]
		  ,[Name]
		  ,[LocationType]
		  ,[Admin1GeonameId]
		  ,[CountryGeonameId]
		  ,[DisplayName]
		  ,[Alternatenames]
		  ,[ModificationDate]
		  ,[FeatureCode]
		  ,[CountryName]
		  ,[Latitude]
		  ,[Longitude]
		  ,[Population]
		  ,[SearchSeq2]
		  ,[Shape]
		  ,[LatPopWeighted]
		  ,[LongPopWeighted]
	From [place].[Geonames] as f1, @tbl_geonameIds as f2
	Where f1.GeonameId=f2.GeonameId
GO

