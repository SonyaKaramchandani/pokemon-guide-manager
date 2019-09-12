USE bioDWEB
GO

/****** Object:  StoredProcedure [bd].[healthmap_insertDiseaseAlertArticles_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmap_insertDiseaseAlertArticles_sp')
DROP PROCEDURE [bd].[healthmap_insertDiseaseAlertArticles_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmap_insertDiseaseAlertArticles_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmap_insertDiseaseAlertArticles_sp]  
  @DiseaseId INT,
  @ArticleId NVARCHAR(50)
  AS  
  BEGIN    
		INSERT INTO [bd].[healthmap_DiseaseAlertArticles] 
		VALUES (@DiseaseId, @ArticleId)
  END

GO


