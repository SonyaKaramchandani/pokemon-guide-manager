
-- =============================================
-- Author:		Basam
-- Create date: 2019-04 
-- Description:	Returns events destination grids
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDataServicesGetEventDestinationGrid
  @EventId AS INT = 0,
  @GridId AS NVARCHAR(12) = ''
AS
BEGIN
	SET NOCOUNT ON
	IF (@EventId = 0 AND @GridId = '')
		SELECT [EventId]
			  ,[GridId]
		FROM [zebra].[EventDestinationGridV3]
	ELSE IF (@EventId <> 0 AND @GridId = '')
		SELECT [EventId]
			  ,[GridId]
		FROM [zebra].[EventDestinationGridV3]
		WHERE [EventId] = @EventId
	ELSE IF (@EventId = 0 AND @GridId <> '')
		SELECT [EventId]
			  ,[GridId]
		FROM [zebra].[EventDestinationGridV3]
		WHERE [GridId] = @GridId
	ELSE IF (@EventId <> 0 AND @GridId <> '')
		SELECT [EventId]
			  ,[GridId]
		FROM [zebra].[EventDestinationGridV3]
		WHERE [EventId] = @EventId AND [GridId] = @GridId
END