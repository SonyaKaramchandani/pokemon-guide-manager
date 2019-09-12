USE bioDWEB
GO

/****** Object:  StoredProcedure [bd].[clientUpdateApi_getSqlStatements_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'clientUpdateApi_getSqlStatements_sp')
DROP PROCEDURE [bd].[clientUpdateApi_getSqlStatements_sp]
GO


/****** Object:  StoredProcedure [bd].[clientUpdateApi_getSqlStatements_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[clientUpdateApi_getSqlStatements_sp] 
	@Client VARCHAR(10) = 'cdc',
	@Top INT = 0,
	@ExecutedByClientStatus BIT = 0,
	@ClientUpdateStartDate Date = '1900/01/01',
	@ClientUpdateEndDate Date = '1900/01/01'
  AS  
  BEGIN    
	-- SET NOCOUNT ON added to prevent extra result sets from
	BEGIN TRY
		SET NOCOUNT ON;
		IF (@Client = 'cdc')
		BEGIN
			SELECT TOP(@Top) * FROM [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByCdcStatus = @ExecutedByClientStatus) 
			ORDER BY Id;
			
			WITH T AS
			(
			SELECT  TOP(@Top) *
			FROM    [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByCdcStatus = @ExecutedByClientStatus) 
			ORDER BY Id ASC
			)
			UPDATE T
			SET ExecutedByCdcStatus = 1, CdcUpdateDate = GETDATE();
		END
		ELSE IF (@Client = 'asean')
		BEGIN
			SELECT TOP(@Top) * FROM [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByAseanStatus = @ExecutedByClientStatus) 
			ORDER BY Id;

			WITH T AS
			(
			SELECT  TOP(@Top) *
			FROM    [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByAseanStatus = @ExecutedByClientStatus) 
			ORDER BY Id ASC
			)
			UPDATE T
			SET ExecutedByAseanStatus = 1, AseanUpdateDate = GETDATE();
		END
		ELSE IF (@Client = 'db1')
		BEGIN
			SELECT TOP(@Top) * FROM [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByDb1Status = @ExecutedByClientStatus)
			ORDER BY Id;

			WITH T AS
			(
			SELECT  TOP(@Top) *
			FROM    [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByDb1Status = @ExecutedByClientStatus) 
			ORDER BY Id ASC
			)
			UPDATE T
			SET ExecutedByDb1Status = 1, Db1UpdateDate = GETDATE();
		END
		ELSE IF (@Client = 'db1demo')
		BEGIN
			SELECT TOP(@Top) * FROM [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByDb1DemoStatus = @ExecutedByClientStatus)
			ORDER BY Id;
		
			WITH T AS
			(
			SELECT  TOP(@Top) *
			FROM    [bd].[ClientDatabaseUpdate] 
			WHERE (ExecutedByDb1demoStatus = @ExecutedByClientStatus) 
			ORDER BY Id ASC
			)
			UPDATE T
			SET ExecutedByDb1demoStatus = 1, Db1demoUpdateDate = GETDATE();
		END
	END TRY
	BEGIN CATCH
		RETURN -999
	END CATCH
  END

GO


