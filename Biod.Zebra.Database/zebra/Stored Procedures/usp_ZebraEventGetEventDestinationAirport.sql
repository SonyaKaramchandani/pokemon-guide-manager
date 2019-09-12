
-- =============================================
-- Author:		Basam
-- Create date: 2019-04
-- Description:	Returns event destination airports
-- 2019-07-03 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEventGetEventDestinationAirport
  @EventId AS INT = 0
AS
BEGIN
  SET NOCOUNT ON
  IF (@EventId = 0)
	  SELECT [EventId]
		   ,[DestinationStationId]
		   ,[StationName]
		   ,[StationCode]
		   ,[CityDisplayName]
		   ,[Volume]
		   ,[Latitude]
		   ,[Longitude]
		   ,[MinProb]
		   ,[MaxProb]
		   ,[MinExpVolume]
		   ,[MaxExpVolume]
	  FROM [zebra].[EventDestinationAirport]
   ELSE
	  SELECT [EventId]
		   ,[DestinationStationId]
		   ,[StationName]
		   ,[StationCode]
		   ,[CityDisplayName]
		   ,[Volume]
		   ,[Latitude]
		   ,[Longitude]
		   ,[MinProb]
		   ,[MaxProb]
		   ,[MinExpVolume]
		   ,[MaxExpVolume]
	  FROM [zebra].[EventDestinationAirport]
	  WHERE [EventId] = @EventId
END