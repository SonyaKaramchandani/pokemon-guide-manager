
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10~11 
-- Description:	Returns contents in [disease].[TransmissionModes]
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDashboardGetTransmissionModes
AS
BEGIN
	SET NOCOUNT ON

	Select TransmissionModeId, TransmissionMode, DisplayName AS TransmissionModeDisplayName
	From [disease].[TransmissionModes]
	Order by DisplayName

END