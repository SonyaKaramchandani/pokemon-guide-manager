
-- =============================================
-- Author:		Vivian
-- Create date: 2018-12 
-- Description:	for summary grouping
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDashboardGetEventsGroupByFields
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id,  
	CASE IsDefault
	   WHEN 1 THEN DisplayName + ' (Default)'
	   ELSE DisplayName
	END DisplayName
	FROM zebra.EventGroupByFields
	WHERE IsHidden = 0  
	ORDER BY DisplayOrder
END