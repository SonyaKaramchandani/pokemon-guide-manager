
-- =============================================
-- Author:		Vivian
-- Create date: 2019-06 
-- Description:	Input - EventId
-- Output: Disease name
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetDiseaseNameByEventId
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	Select f2.DiseaseName
		From surveillance.Event as f1, disease.Diseases as f2
		Where f1.EventId=@EventId and f1.DiseaseId=f2.DiseaseId

END