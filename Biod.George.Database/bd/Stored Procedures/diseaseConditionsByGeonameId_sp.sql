-- =============================================
-- Author:		Basam
-- Create date: 2019-02 
-- Description:	Get the DiseaseConditionsMaxValues
--				Input: GeonameId
-- Updated 2019-02-05 V6
-- =============================================
/****** Object:  StoredProcedure [bd].[diseaseConditionsByGeonameId_sp]    Script Date: 2019-02-05 1:33:56 PM ******/
CREATE PROCEDURE [bd].[diseaseConditionsByGeonameId_sp] 
    @GeonameId  INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *, 0 AS Distance FROM [bd].[DiseaseConditionsMaxValues] WHERE GeonameId = @GeonameId
END