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
Drop Function IF EXISTS bd.ufn_ValidLocationsOfDisease

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

GO
UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 0 WHERE Id = 1;
UPDATE [zebra].[EventGroupByFields] SET IsHidden = 1, DisplayOrder = -1 WHERE Id = 2;
UPDATE [zebra].[EventGroupByFields] SET IsHidden = 1, DisplayOrder = -1 WHERE Id = 3;
UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 200 WHERE Id = 4;
UPDATE [zebra].[EventGroupByFields] SET IsHidden = 1, DisplayOrder = -1 WHERE Id = 5;
UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 300 WHERE Id = 6;
UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 400 WHERE Id = 7;

IF NOT EXISTS(SELECT 1 FROM [zebra].[EventGroupByFields] WHERE Id = 8)
BEGIN 
    INSERT INTO [zebra].[EventGroupByFields] (Id, DisplayName, ColumnName, DisplayOrder, IsDefault, IsHidden) VALUES (8, 'Disease', 'DiseaseName', 100, 0, 0)
END