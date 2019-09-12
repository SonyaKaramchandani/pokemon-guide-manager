
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10~11 
-- Description:	Returns contents in [disease].[Interventions]
-- Modified: previousely usp_ZebraDashboardGetPreventionMethods
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDashboardGetInterventionMethods
AS
BEGIN
	SET NOCOUNT ON

	Select ROW_NUMBER() OVER(ORDER BY [DisplayName] ASC) AS InterventionDisplayId, P.DisplayName AS InterventionDisplayName From
	(Select Distinct DisplayName From [disease].Interventions) P
    union
    Select 3 as InterventionDisplayId, 'Behavioural Only' as InterventionDisplayName
	order by InterventionDisplayId

END