
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Returns a country shape text by geonameId
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDataPostCleanInsightsTravel
	@EventId    AS INT
	--,@EventLocationCases nvarchar(max)
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