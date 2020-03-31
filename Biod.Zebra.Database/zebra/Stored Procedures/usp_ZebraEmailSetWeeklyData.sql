
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
		TRUNCATE TABLE zebra.UserAois_history

		--update data
		Insert into surveillance.Xtbl_Event_Location_history(
				[EventId],[EventDateType],[GeonameId],[EventDate]
				,[SuspCases],[ConfCases],[RepCases],[Deaths])
			Select [EventId], 2, [GeonameId], [EventDate]
				,[SuspCases],[ConfCases],[RepCases],[Deaths]
			From surveillance.Xtbl_Event_Location

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