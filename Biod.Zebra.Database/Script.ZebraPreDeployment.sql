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

--vivian PT-92-568
IF OBJECT_ID (N'surveillance.Event', N'U') IS NOT NULL
    ALTER TABLE [surveillance].[Event] DROP COLUMN IF EXISTS HasOutlookReport;
GO

--clean db
DROP PROCEDURE IF EXISTS bd.usp_CompareJsonStrings
GO

--sonya: pt-869: edge case for fiji; double reorient to set geometry to render CCW
IF OBJECT_ID (N'place.CountryProvinceShapes', N'U') IS NOT NULL
    UPDATE [place].[CountryProvinceShapes]
    SET [SimplifiedShape] = [SimplifiedShape].ReorientObject().ReorientObject(),
    [Shape] = [Shape].ReorientObject().ReorientObject()
    WHERE GeonameId in (2205218, 2199295) -- republic of fiji geonameid, northern division (republic of fiji) geonameId
GO

--vivian: pt-218 --> pt-949
IF OBJECT_ID (N'place.CountryProvinceShapes', N'U') IS NOT NULL
UPDATE [place].[CountryProvinceShapes] 
SET [SimplifiedShape] = [Shape]
WHERE [SimplifiedShape].STNumPoints() > [Shape].STNumPoints() AND [SimplifiedShape].STNumPoints()>10000 
       OR [SimplifiedShape].STNumPoints() < [Shape].STNumPoints() AND [Shape].STNumPoints()<10000
GO

--kevin: PT-179
IF OBJECT_ID (N'zebra.EventGroupByFields', N'U') IS NOT NULL
BEGIN
    UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 0 WHERE Id = 1;
    UPDATE [zebra].[EventGroupByFields] SET IsHidden = 1, DisplayOrder = -1 WHERE Id = 2;
    UPDATE [zebra].[EventGroupByFields] SET IsHidden = 1, DisplayOrder = -1 WHERE Id = 3;
    UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 200 WHERE Id = 4;
    UPDATE [zebra].[EventGroupByFields] SET IsHidden = 1, DisplayOrder = -1 WHERE Id = 5;
    UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 300 WHERE Id = 6;
    UPDATE [zebra].[EventGroupByFields] SET DisplayOrder = 400 WHERE Id = 7;

    IF NOT EXISTS(SELECT 1 FROM [zebra].[EventGroupByFields] WHERE Id = 8)
        INSERT INTO [zebra].[EventGroupByFields] (Id, DisplayName, ColumnName, DisplayOrder, IsDefault, IsHidden) VALUES (8, 'Disease', 'DiseaseName', 100, 0, 0)
END

GO

-- kevin: PT-341
IF OBJECT_ID (N'zebra.EventOrderByFields', N'U') IS NOT NULL
BEGIN
    UPDATE [zebra].[EventOrderByFields] SET [IsDefault] = 0 WHERE [IsDefault] = 1;      -- Remove all existing default settings
    UPDATE [zebra].[EventOrderByFields] SET [IsDefault] = 1 WHERE [Id] = 7;             -- 7 is Risk of Importation
END

GO

--pt-307 vivian
IF OBJECT_ID (N'surveillance.ArticleFeed', N'U') IS NOT NULL
BEGIN
    IF NOT EXISTS(SELECT 1 FROM [surveillance].[ArticleFeed] WHERE [ArticleFeedId] = 9)
	    Insert into [surveillance].[ArticleFeed]([ArticleFeedId], [ArticleFeedName])
	    Values(9, 'RSS')
END

--pt-1396
DROP PROCEDURE IF EXISTS zebra.usp_ZebraDataRenderSetSourceDestinationsPart1ByDisease
DROP PROCEDURE IF EXISTS zebra.usp_ZebraDataRenderSetSourceDestinationsPart2ByDisease
DROP PROCEDURE IF EXISTS zebra.usp_ZebraDataRenderSetSourceDestinationsPart3ByDisease
DROP PROCEDURE IF EXISTS zebra.usp_ZebraDiseaseGetImportationRisk
GO

Drop Table If Exists zebra.DiseaseSourceAirport
Drop Table If Exists zebra.DiseaseEventPrevalence
Drop Table If Exists zebra.DiseaseEventDestinationAirport
Drop Table If Exists zebra.DiseaseEventDestinationGrid
GO

