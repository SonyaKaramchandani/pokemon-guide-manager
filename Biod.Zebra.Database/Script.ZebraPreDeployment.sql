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
--clean db
DROP PROCEDURE IF EXISTS bd.usp_CompareJsonStrings
GO

--vivian: pt-218
UPDATE BiodZebra.[place].[CountryProvinceShapes] 
SET [SimplifiedShape] = [Shape]
WHERE [SimplifiedShape].STNumPoints() > [Shape].STNumPoints() AND [Shape].STNumPoints()>10000

GO

--author?
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

--pt-307 vivian
IF NOT EXISTS(SELECT 1 FROM [surveillance].[ArticleFeed] WHERE [ArticleFeedId] = 9)
	Insert into [surveillance].[ArticleFeed]([ArticleFeedId], [ArticleFeedName])
	Values(9, 'RSS')