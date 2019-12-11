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
--Drop Function IF EXISTS bd.ufn_ValidLocationsOfDisease


--clean db
DROP PROCEDURE IF EXISTS bd.usp_CompareJsonStrings
GO

--vivian: pt-218
UPDATE BiodZebra.[place].[CountryProvinceShapes] 
SET [SimplifiedShape] = [Shape]
WHERE [SimplifiedShape].STNumPoints() > [Shape].STNumPoints() AND [Shape].STNumPoints()>10000

GO

--kevin: PT-179
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

GO

-- kevin: PT-341
UPDATE [BiodZebra].[zebra].[EventOrderByFields] SET [IsDefault] = 0 WHERE [IsDefault] = 1;      -- Remove all existing default settings
UPDATE [BiodZebra].[zebra].[EventOrderByFields] SET [IsDefault] = 1 WHERE [Id] = 7;             -- 7 is Risk of Importation

GO

--pt-307 vivian
IF NOT EXISTS(SELECT 1 FROM [surveillance].[ArticleFeed] WHERE [ArticleFeedId] = 9)
	Insert into [surveillance].[ArticleFeed]([ArticleFeedId], [ArticleFeedName])
	Values(9, 'RSS')