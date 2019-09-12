
-- =============================================
-- Author:		Vivian
-- Create date: 2019-06 
-- Description:	save historial data in five tables
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEmailSetWeeklyData
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
	BEGIN TRAN
		--clean data
		Delete from surveillance.Xtbl_Event_Location_history Where EventDateType=2
		TRUNCATE TABLE zebra.EventPrevalence_history
		TRUNCATE TABLE zebra.EventDestinationAirport_history
		TRUNCATE TABLE zebra.EventDestinationGrid_history
		TRUNCATE TABLE zebra.EventImportationRisksByUser_history
		TRUNCATE TABLE zebra.UserAois_history

		--update data
		Insert into surveillance.Xtbl_Event_Location_history(
				[EventId],[EventDateType],[GeonameId],[EventDate]
				,[SuspCases],[ConfCases],[RepCases],[Deaths])
			Select [EventId], 2, [GeonameId], [EventDate]
				,[SuspCases],[ConfCases],[RepCases],[Deaths]
			From surveillance.Xtbl_Event_Location

		Insert into zebra.EventPrevalence_history(
				[EventId], MinPrevelance, MaxPrevelance, EventMonth)
			Select [EventId], MinPrevelance, MaxPrevelance, EventMonth
			From zebra.EventPrevalence

		Insert into zebra.EventDestinationAirport_history(
				[EventId], DestinationStationId, Volume, MaxProb, MinProb, MaxExpVolume, MinExpVolume)
			Select [EventId], DestinationStationId, Volume, MaxProb, MinProb, MaxExpVolume, MinExpVolume
			From zebra.EventDestinationAirport

		Insert into zebra.EventDestinationGrid_history([EventId], GridId)
			Select [EventId], GridId
			From zebra.EventDestinationGridV3

		Insert into zebra.EventImportationRisksByUser_history(
				UserId, [EventId], LocalSpread, MaxProb, MinProb, MaxVolume, MinVolume)
			Select UserId, [EventId], LocalSpread, MaxProb, MinProb, MaxVolume, MinVolume
			From zebra.EventImportationRisksByUser

		Insert into zebra.UserAois_history(UserId, AoiGeonameIds)
			Select Id, AoiGeonameIds
			From dbo.AspNetUsers
			Where WeeklyOutbreakNotificationEnabled=1

		Select 1 as Result
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;
END