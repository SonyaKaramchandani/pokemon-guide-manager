
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10~11 
-- Description:	Returns contents in [disease].[Interventions]
-- Modified: previousely usp_ZebraDashboardGetPreventionMethods
-- Modified: 2019-10 use intervention table and include prevention only
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDashboardGetInterventionMethods
AS
BEGIN
	SET NOCOUNT ON

	Declare @tbl table (InterventionDisplayId bigint, InterventionDisplayName varchar(100))
	
	Insert into @tbl(InterventionDisplayId, InterventionDisplayName)
		Select ROW_NUMBER() OVER(ORDER BY [DisplayName] ASC), P.DisplayName
		From (Select Distinct DisplayName From [disease].Interventions
			where [InterventionType]='Prevention') P
    
	Select InterventionDisplayId, InterventionDisplayName From @tbl
	Union
    Select Max(InterventionDisplayId)+1 as InterventionDisplayId, 'Behavioural Only' as InterventionDisplayName
	From @tbl
	order by InterventionDisplayId

END