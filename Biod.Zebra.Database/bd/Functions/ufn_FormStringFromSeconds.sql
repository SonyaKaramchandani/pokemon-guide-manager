
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-11 
-- Description:	Input: seconds
--				Output: a string of time as described in PT-270
-- =============================================

CREATE FUNCTION bd.ufn_FormStringFromSeconds (@TimeInSeconds bigint) 
RETURNS varchar(100)
AS
BEGIN
	Declare @returnResluts varchar(100)

	If @TimeInSeconds IS NULL Or @TimeInSeconds<0 Set @returnResluts='-'
	Else if @TimeInSeconds=0 Set @returnResluts='0 second'
	Else if @TimeInSeconds=1 Set @returnResluts='1 second'
	Else if @TimeInSeconds<60 Set @returnResluts=CONCAT(@TimeInSeconds, ' seconds')
	Else if @TimeInSeconds<90 Set @returnResluts='1 minute'
	Else if @TimeInSeconds<3600 Set @returnResluts=CONCAT(CONVERT(INT, ROUND(@TimeInSeconds/60.0, 0)), ' minutes')
	Else if @TimeInSeconds<5400 Set @returnResluts='1 hour'
	Else if @TimeInSeconds<86400 Set @returnResluts=CONCAT(CONVERT(INT, ROUND(@TimeInSeconds/3600.0, 0)), ' hours')
	Else if @TimeInSeconds<129600 Set @returnResluts='1 day'
	Else if @TimeInSeconds<31556952 Set @returnResluts=CONCAT(CONVERT(INT, ROUND(@TimeInSeconds/86400.0, 0)), ' days')
	Else if @TimeInSeconds<47335428 Set @returnResluts='1 year'
	Else Set @returnResluts=CONCAT(CONVERT(INT, ROUND(@TimeInSeconds/31556952.0, 0)), ' years')

	RETURN @returnResluts

END