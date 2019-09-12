
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10~11 
-- Description:	Returns contents in [disease].[TransmissionModes]
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDashboardGetDiseaseSeveritys
AS
BEGIN
	SET NOCOUNT ON

  	Select ROW_NUMBER() OVER(ORDER BY SeverityLevel ASC) AS SeverityLevelDisplayId, D.SeverityLevel AS SeverityLevelDisplayName
	From (Select Distinct SeverityLevel	From [disease].[Diseases]) D

END