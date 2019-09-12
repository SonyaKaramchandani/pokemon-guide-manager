
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	calls place.usp_UpdateGeonames and surveillance.usp_UpdateArticleApi
--				Output: main switchboard, calls other SPs, finishes all updating
-- =============================================

create PROCEDURE surveillance.usp_UpdateSurveillanceApi_main
	@serviceDomainName varchar(128)--'api-prod1.ad.bluedot.global','dw1-ubuntu.ad.bluedot.global:81'
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--0 clean up one-year old articles
		Delete from [surveillance].[ProcessedArticle]
		Where [SystemLastModifiedDate] < DATEADD(year, -1, GETUTCDATE())

		----1. Update Geonames
		--Declare @resultMessage_Geonames varchar(500)
		--EXEC place.usp_UpdateGeonames @resultMessage=@resultMessage_Geonames OUTPUT
		--2. Update Surveillance
		Declare @resultMessage_Articles varchar(500)
		EXEC surveillance.usp_UpdateArticlesApi @serviceDomainName=@serviceDomainName,
			@resultMessage=@resultMessage_Articles OUTPUT

		--3. Update SuggestedEvents
		Declare @resultMessage_SuggestedEvents varchar(500)
		EXEC surveillance.usp_UpdateSuggestedEventsApi @serviceDomainName=@serviceDomainName, 
			@resultMessage=@resultMessage_SuggestedEvents OUTPUT

		--SELECT @resultMessage_Geonames + ' & ' +@resultMessage_Articles as ErrorMessage
		SELECT @resultMessage_Articles  + ' & ' + @resultMessage_SuggestedEvents as ErrorMessage
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update systems in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END