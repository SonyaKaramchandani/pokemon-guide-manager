
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	Input: A json string, compare with that in database
--				Output: exists-'1'-exists, missing-'XXX is missing from XXX'
-- =============================================

CREATE FUNCTION bd.ufn_CheckMissingColumn (@JsonStr nvarchar(max), @ColumnName varchar(100), @JsonId int) 
RETURNS varchar(200)
AS
BEGIN

	DECLARE @returnResluts varchar(200)='1'

	Declare @pos int= (Select CHARINDEX(CONCAT('"', @ColumnName, '"'), @JsonStr))

	If @pos<=0
		Set @returnResluts=@ColumnName + ' is missing from ' 
			+ (Select [Description] From [bd].[LastJsonStrs] Where Id=@JsonId)

	RETURN @returnResluts

END