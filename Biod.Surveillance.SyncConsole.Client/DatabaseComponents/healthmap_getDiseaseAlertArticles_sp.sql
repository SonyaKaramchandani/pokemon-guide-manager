USE bioDWEB
GO

/****** Object:  StoredProcedure [bd].[healthmap_getDiseaseAlertArticles_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'healthmap_getDiseaseAlertArticles_sp')
DROP PROCEDURE [bd].[healthmap_getDiseaseAlertArticles_sp]
GO


/****** Object:  StoredProcedure [bd].[healthmap_getDiseaseAlertArticles_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[healthmap_getDiseaseAlertArticles_sp] 
  AS  
  BEGIN    
		SELECT * FROM [bd].[healthmap_DiseaseAlertArticles] 
  END

GO


