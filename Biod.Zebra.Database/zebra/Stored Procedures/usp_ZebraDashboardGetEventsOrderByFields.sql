
-- =============================================
-- Author:		Vivian
-- Create date: 2018-12 
-- Description:	for summary sorting
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDashboardGetEventsOrderByFields
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, DisplayName, ColumnName
	FROM zebra.EventOrderByFields
	WHERE IsHidden = 0  
	ORDER BY DisplayOrder
END