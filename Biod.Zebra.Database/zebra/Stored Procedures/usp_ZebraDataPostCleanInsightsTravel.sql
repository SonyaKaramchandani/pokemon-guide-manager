
-- =============================================
-- Author:		Vivian
-- Create date: 2020-04 
-- Description:	Restore foreign keys w/o check. Can be deleted after geoname table updated
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDataPostCleanInsightsTravel
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
	BEGIN TRAN
		--add w/o check
		ALTER TABLE [zebra].[Stations] WITH NOCHECK ADD CONSTRAINT [FK_Stations_CityGeoname] FOREIGN KEY([CityGeonameId])
			REFERENCES [place].[Geonames] ([GeonameId])
		ALTER TABLE [zebra].[Stations] WITH NOCHECK ADD CONSTRAINT FK_Stations_Geoname FOREIGN KEY([GeonameId])
			REFERENCES [place].[Geonames] ([GeonameId])
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select Cast (0 as bit) as Result
	END CATCH;
END