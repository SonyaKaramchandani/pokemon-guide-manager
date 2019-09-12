USE Healthmap
GO
--Build server test
/****** Object:  StoredProcedure [bd].[healthmapApi_updateHealthmapTables_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmapApi_updateHealthmapTables_sp')
DROP PROCEDURE [bd].[healthmapApi_updateHealthmapTables_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmapApi_updateHealthmapTables_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmapApi_updateHealthmapTables_sp] 
       @CleanupDate DATETIME
  AS  
  BEGIN    
		BEGIN TRAN healthmap_insert
			-- Copy the updated records to the main tables
			-- update bd.healthmap_AlertArticles
			UPDATE bd.healthmap_AlertArticles SET
			  ArticleId = UAA.ArticleId
			, Title = UAA.Title
			, [Description] = UAA.[Description]
			, PublicationDate = UAA.PublicationDate
			, SourceUrl = UAA.SourceUrl
			, Author = UAA.Author
			, LocationName = UAA.LocationName
			, LocationTypeId = UAA.LocationTypeId
			, SourceTypeId = UAA.SourceTypeId
			, LastUpdateTime = UAA.LastUpdateTime
			FROM     bd.healthmap_AlertArticles AS AA INNER JOIN
							  bd.healthmap_AlertArticlesUpdated AS UAA ON AA.ArticleId = UAA.ArticleId
			WHERE  (UAA.CleanupDate <= @CleanupDate)

			-- update [bd].[healthmap_AlertArticlesContent]
			UPDATE [bd].[healthmap_AlertArticlesContent] SET
               [ArticleId] = UAAC.[ArticleId]
			  ,[FullDescription] = UAAC.[FullDescription]
			  ,[mainSource] = UAAC.[mainSource]
			  ,[mainSourceUrl] = UAAC.[mainSourceUrl]
			FROM     [bd].[healthmap_AlertArticlesContent] AS AAC INNER JOIN
							  [bd].[healthmap_AlertArticlesContentUpdated] AS UAAC ON AAC.ArticleId = UAAC.ArticleId
			WHERE  (UAAC.CleanupDate <= @CleanupDate)

			-- update [bd].[healthmap_AlertLocationType]
			UPDATE [bd].[healthmap_AlertLocationType] SET
			   [Description] = UA.[Description]
			  ,[Alias] = UA.[Alias]
			FROM     [bd].[healthmap_AlertLocationType] AS A INNER JOIN
							 [bd].[healthmap_AlertLocationTypeUpdated] AS UA ON A.Id = UA.Id
			WHERE  (UA.CleanupDate <= @CleanupDate)

			-- update [bd].[healthmap_AlertSourceType]
			UPDATE [bd].healthmap_AlertSourceType SET
			   [Description] = UA.[Description]
			  ,[Alias] = UA.[Alias]
			FROM     [bd].[healthmap_AlertSourceType] AS A INNER JOIN
							 [bd].[healthmap_AlertSourceTypeUpdated] AS UA ON A.Id = UA.Id
			WHERE  (UA.CleanupDate <= @CleanupDate)

			-- update [bd].[healthmap_Disease]
			UPDATE [bd].[healthmap_Disease] SET
			   [Name] = UA.[Name]
			  ,[Description] = UA.[Description]
			  ,[IsOfInterest] = UA.[IsOfInterest]
			FROM     [bd].[healthmap_Disease] AS A INNER JOIN
							 [bd].[healthmap_DiseaseUpdated] AS UA ON A.DiseaseId = UA.DiseaseId
			WHERE  (UA.CleanupDate <= @CleanupDate)

			-- update [bd].[healthmap_DiseaseAlertArticles]
			DELETE [bd].[healthmap_DiseaseAlertArticles] WHERE [ArticleId] IN (SELECT DISTINCT ArticleId FROM [bd].[healthmap_DiseaseAlertArticlesUpdated])
			INSERT INTO [bd].[healthmap_DiseaseAlertArticles] 
			     SELECT [DiseaseId], [ArticleId] FROM [bd].[healthmap_DiseaseAlertArticlesUpdated] WHERE (CleanupDate <= @CleanupDate)

			--Finally, clean up the old records
			DELETE FROM [bd].[healthmap_AlertArticlesUpdated] WHERE CleanupDate <= @CleanupDate	
			DELETE FROM [bd].[healthmap_AlertArticlesContentUpdated] WHERE CleanupDate <= @CleanupDate	
			DELETE FROM [bd].[healthmap_AlertLocationTypeUpdated] WHERE CleanupDate <= @CleanupDate	
			DELETE FROM [bd].[healthmap_AlertSourceTypeUpdated] WHERE CleanupDate <= @CleanupDate	
			DELETE FROM [bd].[healthmap_DiseaseUpdated] WHERE CleanupDate <= @CleanupDate	
			DELETE FROM [bd].[healthmap_DiseaseAlertArticlesUpdated] WHERE CleanupDate <= @CleanupDate	
		COMMIT TRAN healthmap_insert 
  END

