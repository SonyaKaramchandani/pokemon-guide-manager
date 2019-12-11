/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--pt-307 vivian
IF NOT EXISTS(SELECT 1 FROM [surveillance].[ArticleFeed] WHERE [ArticleFeedId] = 9)
	Insert into [surveillance].[ArticleFeed]([ArticleFeedId], [ArticleFeedName])
	Values(9, 'RSS')

--print 'rename a column' 
--IF NOT EXISTS(SELECT 1 FROM sys.columns 
--				WHERE Name = N'DiseaseType' AND Object_ID = Object_ID(N'disease.Diseases'))
--	EXEC sp_rename 'disease.Diseases.Type', 'DiseaseType', 'COLUMN'; 
--GO

--print 'preserve disease info'
--If EXISTS(SELECT 1 FROM sys.columns 
--				WHERE Name = N'IncubationAverageDays' AND Object_ID = Object_ID(N'disease.Diseases'))
--Begin
--	Drop Table If Exists [disease].tmp_disease

--	Declare @sqlStr varchar(8000) ='
--	Select DiseaseId, [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays],
--		[SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays]
--	Into [disease].tmp_disease From [disease].[Diseases]'
--	EXEC (@sqlStr)

--	--may need
--	Alter Table [disease].[Diseases] Drop Column If Exists [IncubationAverageDays], 
--		Column If Exists [IncubationMinimumDays], 
--		Column If Exists [IncubationMaximumDays],
--		Column If Exists [SymptomaticAverageDays], 
--		Column If Exists [SymptomaticMinimumDays], 
--		Column If Exists [SymptomaticMaximumDays]
		
--End