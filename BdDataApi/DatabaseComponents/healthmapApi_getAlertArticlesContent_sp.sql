USE Healthmap
GO

/****** Object:  StoredProcedure [bd].[healthmapApi_getAlertArticlesContent_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmapApi_getAlertArticlesContent_sp')
DROP PROCEDURE [bd].[healthmapApi_getAlertArticlesContent_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmapApi_getAlertArticlesContent_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmapApi_getAlertArticlesContent_sp] 
       @LastUpdateTime DATETIME
  AS  
  BEGIN    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT AC.ArticleId, AC.FullDescription, AC.mainSource, AC.mainSourceUrl
	FROM     [bd].healthmap_AlertArticlesContent AS AC INNER JOIN
					  [bd].healthmap_AlertArticles A ON AC.ArticleId = A.ArticleId
	WHERE  (A.LastUpdateTime >= @LastUpdateTime)
  END

GO


