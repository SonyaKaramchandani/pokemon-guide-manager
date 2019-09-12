
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	Input: A json string, compare with that in database
--				Output: exists-'1'-exists, missing-'XXX is missing from XXX'
-- =============================================

CREATE FUNCTION [bd].[ufn_StringSplit](
	@sInputList VARCHAR(8000) -- List of delimited items
	,@sDelimiter VARCHAR(20) = ',' -- delimiter that separates items
) RETURNS @List TABLE (item VARCHAR(8000))

BEGIN
	DECLARE @sItem VARCHAR(8000)
	WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
		 BEGIN
			 SELECT
				  @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),
				  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
 
			 IF LEN(@sItem) > 0
				INSERT INTO @List SELECT @sItem
			 END

			IF LEN(@sInputList) > 0
				INSERT INTO @List SELECT @sInputList -- Put the last item in
	RETURN
END
