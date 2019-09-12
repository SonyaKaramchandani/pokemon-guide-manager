
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10~11 
-- Description:	Returns a list of diseases
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDashboardGetDiseases
AS
BEGIN
	SET NOCOUNT ON

	SELECT DiseaseId, DiseaseName, [DiseaseType] AS ThreatType, OutbreakPotentialAttributeId
	FROM [disease].Diseases
	ORDER BY DiseaseName

END