USE Healthmap
GO

/****** Object:  StoredProcedure [bd].[healthmapApi_getDiseaseAlertArticlesArchive_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmapApi_getDiseaseAlertArticlesArchive_sp')
DROP PROCEDURE [bd].[healthmapApi_getDiseaseAlertArticlesArchive_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmapApi_getDiseaseAlertArticlesArchive_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmapApi_getDiseaseAlertArticlesArchive_sp] 
	@LastUpdateTime DATETIME
  AS  
  BEGIN    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ACA.ArticleId, ACA.DiseaseId FROM [bd].[healthmap_DiseaseAlertArticlesArchive] AS ACA INNER JOIN
					  [bd].[healthmap_AlertArticlesArchive] AAA ON ACA.ArticleId = AAA.ArticleId
	WHERE  (AAA.LastUpdateTime >= @LastUpdateTime)
  END

GO


