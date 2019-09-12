
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10~11 
-- Description:	Returns contents in [disease].[BiosecurityRisk]
-- =============================================
CREATE PROCEDURE [zebra].[usp_ZebraDashboardGetBiosecurityRisks]
AS
BEGIN
	SET NOCOUNT ON

	Select ROW_NUMBER() OVER(ORDER BY [BiosecurityRiskCode] ASC) AS [BiosecurityRiskDisplayId], B.[BiosecurityRiskCode], B.[BiosecurityRiskDesc] From 
	(SELECT [BiosecurityRiskCode], [BiosecurityRiskDesc]  FROM [disease].[BiosecurityRisk]) B

END