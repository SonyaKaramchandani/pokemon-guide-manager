
-- =============================================
-- Author:		Vivian
-- Create date: 2018-12
-- Description:	Returns contents in [disease].[OutbreakPotentialCategory]
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDashboardGetOutbreakPotentialCategories
AS
BEGIN
	SET NOCOUNT ON

	Select [AttributeId]
      ,[Rule]
      ,[NeedsMap]
      ,[MapThreshold]
      ,[EffectiveMessage]
	  ,[EffectiveMessageDescription]
	  ,[IsLocalTransmissionPossible]
  FROM [disease].[OutbreakPotentialCategory]

END