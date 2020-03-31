
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Returns a country shape text by geonameId
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDataPreCleanInsightsTravel
	@EventId    AS INT
	--,@EventLocationCases nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
	BEGIN TRAN
		--discontinued
		IF (OBJECT_ID('zebra.DiseaseSourceAirport', 'U') IS NOT NULL) 
			TRUNCATE TABLE zebra.DiseaseSourceAirport
		IF (OBJECT_ID('zebra.DiseaseEventDestinationAirport', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.DiseaseEventDestinationAirport
		--utility
		IF (OBJECT_ID('zebra.AirportRanking', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.AirportRanking
		IF (OBJECT_ID('zebra.StationDestinationAirport', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.StationDestinationAirport
		IF (OBJECT_ID('zebra.GridStation', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.GridStation

		--old model
		IF (OBJECT_ID('zebra.EventDestinationAirport', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.EventDestinationAirport
		IF (OBJECT_ID('zebra.EventDestinationAirport_history', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.EventDestinationAirport_history
		IF (OBJECT_ID('zebra.EventSourceAirport', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.EventSourceAirport
		--spread model
		IF (OBJECT_ID('zebra.EventDestinationAirportSpreadMd', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.EventDestinationAirportSpreadMd
		IF (OBJECT_ID('zebra.EventSourceAirportSpreadMd', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.EventSourceAirportSpreadMd
		IF (OBJECT_ID('zebra.EventSourceDestinationRisk', 'U') IS NOT NULL)
			TRUNCATE TABLE zebra.EventSourceDestinationRisk
		--[Stations]
		Delete From zebra.[Stations]
		--clean foreign keys because some geonames are not in Geoname table
		IF (OBJECT_ID('zebra.FK_Stations_CityGeoname', 'F') IS NOT NULL)
			ALTER TABLE [zebra].[Stations] DROP CONSTRAINT FK_Stations_CityGeoname
		IF (OBJECT_ID('zebra.FK_Stations_Geoname', 'F') IS NOT NULL)
			ALTER TABLE [zebra].[Stations] DROP CONSTRAINT FK_Stations_Geoname	
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select Cast (0 as bit) as Result
	END CATCH;
END