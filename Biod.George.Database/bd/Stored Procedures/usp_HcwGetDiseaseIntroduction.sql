-- =============================================
-- Author:		Vivian
-- Create date: 2019-02 
-- Description:	Get disease message where sectionId=1
--				Input: DiseaseId

-- =============================================

CREATE PROCEDURE [bd].[usp_HcwGetDiseaseIntroduction] 
    @DiseaseId  INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [message] AS Introduction 
	FROM [bd].[DiseaseMobileMessage] 
	WHERE DiseaseId = @DiseaseId and [sectionId]=1
END