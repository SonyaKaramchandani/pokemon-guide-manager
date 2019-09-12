USE Healthmap
GO

/****** Object:  StoredProcedure [bd].[healthmapApi_getAlertArticlesContentArchive_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmapApi_getAlertArticlesContentArchive_sp')
DROP PROCEDURE [bd].[healthmapApi_getAlertArticlesContentArchive_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmapApi_getAlertArticlesContentArchive_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmapApi_getAlertArticlesContentArchive_sp] 
       @LastUpdateTime DATETIME
  AS  
  BEGIN    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ACA.ArticleId, ACA.FullDescription, ACA.mainSource, ACA.mainSourceUrl
	FROM     [bd].healthmap_AlertArticlesContentArchive AS ACA INNER JOIN
					  [bd].healthmap_AlertArticlesArchive AA ON ACA.ArticleId = AA.ArticleId
	WHERE  (AA.LastUpdateTime >= @LastUpdateTime)
  END

GO


