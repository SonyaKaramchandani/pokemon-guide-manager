USE Healthmap
GO

/****** Object:  StoredProcedure [bd].[healthmapApi_getDiseaseAlertArticlesUpdated_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmapApi_getDiseaseAlertArticlesUpdated_sp')
DROP PROCEDURE [bd].[healthmapApi_getDiseaseAlertArticlesUpdated_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmapApi_getDiseaseAlertArticlesUpdated_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmapApi_getDiseaseAlertArticlesUpdated_sp] 
  AS  
  BEGIN    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [bd].[healthmap_DiseaseAlertArticlesUpdated]
  END

GO


