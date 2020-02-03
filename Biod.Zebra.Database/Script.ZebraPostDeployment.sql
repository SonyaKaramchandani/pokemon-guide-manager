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


--print 'populate [disease].[DiseaseSpeciesSymptomatic]' 
--If Not Exists (Select 1 From [disease].[DiseaseSpeciesSymptomatic])
--	insert into [disease].[DiseaseSpeciesSymptomatic]([DiseaseId], [SpeciesId], [SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays])
--		Select [DiseaseId], 1, [SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays]
--		From [disease].tmp_disease 
--		where  [SymptomaticAverageDays] is not null
--GO

--print 'populate [disease].[DiseaseSpeciesIncubation]' 
--If Not Exists (Select 1 From [disease].[DiseaseSpeciesIncubation])
--	insert into [disease].[DiseaseSpeciesIncubation]([DiseaseId], [SpeciesId], [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays])
--		Select [DiseaseId], 1, [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays]
--		From [disease].tmp_disease 
--		where  [IncubationAverageDays] is not null
--GO

print 'clean database'
Drop Table If Exists [disease].Xtbl_Disease_Pathogens
Drop Table If Exists [disease].Pathogens
Drop Table If Exists [disease].PathogenTypes

Drop Table If Exists [disease].Xtbl_Disease_Preventions
Drop Table If Exists [disease].[Preventions]

Drop Table If Exists [disease].tmp_disease
--PT-836
Drop Function If Exists zebra.ufn_GetSubscribedUsers

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

--PT-459-PT-553 populate [bd].[ConfigurationVariables]
If Not Exists (Select 1 From [bd].[ConfigurationVariables] Where [Name]='Distance')
	Insert into [bd].[ConfigurationVariables]([ConfigurationVariableId], [Name], [Value], [ValueType], [Description], [ApplicationName])
	Values(NEWID(), 'DestinationCatchmentThreshold', '0.1', 'Double', 'Probability to use arrive in a catchmeant area of an airport, >=', 'Biod.Zebra.Database')
	,(NEWID(), 'SourceCatchmentThreshold', '0.01', 'Double', 'Probability to use an airport in a catchmeant area, >=', 'Biod.Zebra.Database')
	,(NEWID(), 'Distance', '100000', 'Int', 'Meter, buffer size', 'Biod.Zebra.Database')
GO

--PT-711-717
If Not Exists (Select 1 From [bd].[ConfigurationVariables] Where [Name]='NotificationThreshold')
	Insert into [bd].[ConfigurationVariables]([ConfigurationVariableId], [Name], [Value], [ValueType], [Description], [ApplicationName])
	Values(NEWID(), 'NotificationThreshold', '0.01', 'Double', 'Maximum importation probability to send notification, >=', 'Biod.Zebra.Database')
GO

--PT-742-770 populate [bd].[ConfigurationVariables]
If Not Exists (Select 1 From [bd].[ConfigurationVariables] Where [Name]='IsEventImportationRisksByGeonamePopulated')
	Insert into [bd].[ConfigurationVariables]([ConfigurationVariableId], [Name], [Value], [ValueType], [Description], [ApplicationName])
	Values(NEWID(), 'IsEventImportationRisksByGeonamePopulated', 'false', 'Boolean', 'Whether zebra.EventImportationRisksByGeoname already populated', 'Biod.Zebra.Database')
GO

--Vivian: add existing user goenameId to ActiveGeonames
Declare @tbl_Users table (UserId nvarchar(128), AoiGeonameIds varchar(max), SeqId int);
With T1 as (
	select Id, ROW_NUMBER() OVER ( order by Id) as rankId
	from dbo.AspNetUsers
	)
Insert into @tbl_Users(UserId, AoiGeonameIds, SeqId)
Select T1.Id, f1.AoiGeonameIds, T1.rankId
From [dbo].[AspNetUsers] as f1, T1
Where f1.Id=T1.Id
--locs from aoi
Declare @tbl_geonameIds table (GeonameId int)

Declare @i int=1
Declare @maxSeqId int =(Select Max(SeqId) From @tbl_Users)
Declare @thisAoi varchar(256)
While @i<=@maxSeqId
Begin
	Set @thisAoi=(Select AoiGeonameIds From @tbl_Users Where SeqId=@i)
	Insert into @tbl_geonameIds
		Select item
		From [bd].[ufn_StringSplit](@thisAoi, ',') as f2
	Set @i=@i+1
End
/*task 1, populate [place].[ActiveGeonames]*/
--all user geonames not in existing activeGeonames
Declare @tbl_UserLoc table (GeonameId int)
Insert into @tbl_UserLoc
select [GeonameId] From [dbo].[AspNetUsers]
Union
Select [GeonameId] from @tbl_geonameIds
Except
Select [GeonameId] from place.ActiveGeonames
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
	From [place].[Geonames] as f1, @tbl_UserLoc as f2
	Where f1.GeonameId=f2.GeonameId
/*task 2, PT-742-770 populate EventImportationRisksByGeoname*/
Declare @isUpdated varchar(20)=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='IsEventImportationRisksByGeonamePopulated')
If @isUpdated='false'
Begin
	--adds Canada, China, USA
	Insert into @tbl_geonameIds values(1814991), (6251999), (6252001)
	--unique
	Declare @tbl_geonameIdsUni table (GeonameId int)
	Insert into @tbl_geonameIdsUni 
		Select distinct GeonameId From @tbl_geonameIds
	--inserts with new aoi
	Declare  @thisAoiGeonameId int
	Declare MyCursor CURSOR FAST_FORWARD 
	FOR Select GeonameId
		From @tbl_geonameIdsUni
	
	OPEN MyCursor
	FETCH NEXT FROM MyCursor
	INTO @thisAoiGeonameId

	WHILE @@FETCH_STATUS = 0
	Begin
		EXEC zebra.usp_ZebraDataRenderSetImportationRiskByGeonameId  @thisAoiGeonameId

		FETCH NEXT FROM MyCursor
		INTO @thisAoiGeonameId
	End
	CLOSE MyCursor
	DEALLOCATE MyCursor
	--update status
	Update [bd].[ConfigurationVariables] set [Value]='true' Where [Name]='IsEventImportationRisksByGeonamePopulated'
End

GO
--PT-92-568-647
:r .\PostDeploymentData\populateStationLatLong.sql
GO

--PT-92-568
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'tmp_StationLatLong')
	Update [zebra].[Stations] 
		Set [Latitude]=f2.[Latitude], [Longitude]=f2.[Longitude]
		From [zebra].[Stations] as f1, bd.tmp_StationLatLong as f2
		Where f1.StationId=f2.StationId
GO

--vivian: pt-630 populate city geoname from Stations to ActiveGeonames
Declare @tbl_cityGeonameIds table (GeonameId int)
Insert @tbl_cityGeonameIds
	Select [CityGeonameId] From [zebra].[Stations] Where [CityGeonameId] IS NOT NULL
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
	From [place].[Geonames] as f1, @tbl_cityGeonameIds as f2
	Where f1.GeonameId=f2.GeonameId
GO

--vivian: pt-674 populate province geoname
Declare @tbl_provGeonameIds table (GeonameId int)
Insert @tbl_provGeonameIds
	Select [GeonameId] From [place].[Geonames] Where LocationType=4
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
	From [place].[Geonames] as f1, @tbl_provGeonameIds as f2
	Where f1.GeonameId=f2.GeonameId
GO