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
print 'rename a column' 
IF NOT EXISTS(SELECT 1 FROM sys.columns 
				WHERE Name = N'DiseaseType' AND Object_ID = Object_ID(N'disease.Diseases'))
	EXEC sp_rename 'disease.Diseases.Type', 'DiseaseType', 'COLUMN'; 
GO

print 'preserve disease info'
If EXISTS(SELECT 1 FROM sys.columns 
				WHERE Name = N'IncubationAverageDays' AND Object_ID = Object_ID(N'disease.Diseases'))
Begin
	Drop Table If Exists [disease].tmp_disease

	Declare @sqlStr varchar(8000) ='
	Select DiseaseId, [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays],
		[SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays]
	Into [disease].tmp_disease From [disease].[Diseases]'
	EXEC @sqlStr

	--may need
	--Alter Table [disease].[Diseases] Drop Column [IncubationAverageDays], [IncubationMinimumDays], [IncubationMaximumDays],
	--	[SymptomaticAverageDays], [SymptomaticMinimumDays], [SymptomaticMaximumDays]
		
End

