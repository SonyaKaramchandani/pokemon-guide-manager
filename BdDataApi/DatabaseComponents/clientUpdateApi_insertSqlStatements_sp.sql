USE bioDWEB
GO

/****** Object:  StoredProcedure [bd].[clientUpdateApi_insertSqlStatements_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'clientUpdateApi_insertSqlStatements_sp')
DROP PROCEDURE [bd].[clientUpdateApi_insertSqlStatements_sp]
GO


/****** Object:  StoredProcedure [bd].[clientUpdateApi_insertSqlStatements_sp]    Script Date: 2015-06-18 10:27:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [bd].[clientUpdateApi_insertSqlStatements_sp] 
@SchemaName VARCHAR(MAX),
@TableName VARCHAR(Max),
@WhereClause NVARCHAR(Max),
@TopNo INT
AS
 
DECLARE @SQL VARCHAR(max)
DECLARE @nSQL NVARCHAR(Max)
DECLARE @RecordCount INT
DECLARE @FirstColumn NVARCHAR(Max)
 
--SET @TableName=QUOTENAME(@TableName) -- Quote the table name
SET @TableName =@SchemaName + '.' + QUOTENAME(@TableName)
Print @TableName
 
/*We need to find the record count in order to remove Union ALL from the last row*/
SET @nSQL=''
SET @nSQL= N'SELECT @RecordCount=COUNT(*) FROM ' + @TableName + (CASE WHEN ISNULL(@Whereclause,'') <>'' THEN ' Where ' + @Whereclause ELSE '' END)
Print @nSQL
EXEC sp_executesql @query = @nSQL, @params = N'@RecordCount INT OUTPUT', @RecordCount = @RecordCount OUTPUT
/*****************************************************************/
 
/*Need to check either top No of record is less than record count 
in order to remove union all from the last row */
IF @TopNo IS NOT NULL And @TopNo<@RecordCount
    BEGIN
        SET @RecordCount=@TopNo
    END
/***************************************************************/
/*** Here we need to find the first column name to generate a serial number and insert an "Insert into statement" in the first row ***/
SET @nSQL=''
SET @nSQL= N'SELECT @FirstColumn=[name] FROM sys.columns WHERE [Column_id]=1 And object_ID=object_ID('''+ @TableName + ''')'
Print @nSQL
EXEC sp_executesql @query = @nSQL, @params = N'@FirstColumn nvarchar(Max) OUTPUT', @FirstColumn = @FirstColumn OUTPUT
/**************************************************************/
 
DECLARE @FieldName VARCHAR(max) 
SET @FieldName=''
 
If (Select Count(*) FROM sys.columns WHERE object_id=object_id('' + @TableName + '') And is_identity<>0)=1
 
BEGIN
    SET @FieldName = STUFF(
    (
    SELECT ',' + QUOTENAME([Name]) FROM sys.columns WHERE object_id=object_id('' + @TableName + '') Order By [column_id] 
    FOR XML PATH('')), 1, 1, '')
    Set @FieldName ='(' + @FieldName + ')'
    Print @FieldName
    Print len(@FieldName)
END
 
/*******Create list of comma seperated columns *******/
SET @SQL= (SELECT STUFF((SELECT(CASE
WHEN system_type_id In (167,175,189) THEN + ' Cast(ISNULL(LTRIM(RTRIM(''N''''''+Replace(' + QUOTENAME([Name])+ ','''''''','''''''''''')+'''''''''+ ')),''NULL'') as varchar(max)) + '' AS ' + QUOTENAME([Name]) + ''' + '' ,'''+'+ '
WHEN system_type_id In (231,239) THEN + ' Cast(ISNULL(LTRIM(RTRIM(''N''''''+Replace(' + QUOTENAME([Name])+ ','''''''','''''''''''')+'''''''''+ ')),''NULL'') as nvarchar(max)) + '' AS ' + QUOTENAME([Name]) + ''' + '' ,'''+'+ '
WHEN system_type_id In (58,61,36) THEN + ' ISNULL(LTRIM(RTRIM(''N'''''' + Cast(' + QUOTENAME([Name])+ ' as varchar(max))+''''''''' + ')),''NULL'') + '' AS ' + QUOTENAME([Name]) + '''+ '' ,'''+' + '
WHEN system_type_id In (48,52,56,59,60,62,104,106,108,122,127) THEN + ' ISNULL(Cast(' + QUOTENAME([Name])+ ' as varchar(max)),''NULL'')+ '' AS ' + QUOTENAME([Name]) + ''' + '' ,'''+'+ '
END
)
FROM
sys.columns WHERE object_ID=object_ID(''+ @TableName + '') FOR XML PATH('')),1,1,' '))
 
/*******************************************************/
 
/* Here 500 means if the record count is 500 or top no 500 then it will generate "Insert into select ..Union All "
Because more than 500 might reduce its performance. */
IF @TopNo <500 or @RecordCount<500 
    BEGIN
        IF @TopNo IS NULL
            BEGIN
                SET @SQl='SELECT (Case When ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@FirstColumn) + ') =1 THEN '' INSERT INTO ' + @TableName + ' ' + ' '+ @FieldName + ' ''' + ' ELSE '''' END) + ''SELECT ''+' + Left(@SQL,Len(@SQL)-8) + ' + (CASE WHEN ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@FirstColumn) + ') <>' + CONVERT(VARCHAR(10),@RecordCount) + ' THEN '' UNION ALL'' ELSE '''' END) AS [DATA] ' + ' FROM ' + @TableName +(CASE WHEN ISNULL(@Whereclause,'') <>'' THEN ' WHERE ' + @Whereclause ELSE '' END)
            END
    ELSE
            BEGIN
                SET @SQl= 'SELECT TOP ' + CONVERT(VARCHAR(10),@TopNo) + ' (CASE WHEN ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@FirstColumn) + ') =1 THEN '' INSERT INTO ' + @TableName + ' ' + ' '+ @FieldName + '''' + ' ELSE '''' END) + ''SELECT ''+' + LEFT(@SQL,LEN(@SQL)-8) + ' + (CASE WHEN ROW_NUMBER() OVER (ORDER BY ' + QUOTENAME(@FirstColumn) + ') <>' + CONVERT(VARCHAR(10),@RecordCount) + ' THEN '' Union All'' ELSE '''' END) AS [DATA]' + ' FROM ' + @TableName + (CASE WHEN ISNULL(@Whereclause,'') <>'' THEN ' WHERE ' + @Whereclause ELSE '' END)
            END
    END
ELSE
--- Greator then 500 will generate "insert into select *" ... for each record.
    BEGIN
        IF @TopNo IS NULL
            BEGIN
                  SET @SQl='SELECT '' INSERT INTO ' + @TableName + ' ' + ' '+ @FieldName + ' '' + ''SELECT ''+' + Left(@SQL,Len(@SQL)-8) + ' AS [DATA] ' + ' FROM ' + @TableName +(CASE WHEN ISNULL(@Whereclause,'') <>'' THEN ' WHERE ' + @Whereclause ELSE '' END)
            END
    ELSE
            BEGIN
                  SET @SQl='SELECT TOP ' + CONVERT(VARCHAR(10),@TopNo) + ''' INSERT INTO ' + @TableName + ' ' + ' '+ @FieldName + ' '' + ''SELECT ''+' + Left(@SQL,Len(@SQL)-8) + ' AS [DATA] ' + ' FROM ' + @TableName +(CASE WHEN ISNULL(@Whereclause,'') <>'' THEN ' WHERE ' + @Whereclause ELSE '' END)
            END
    END
EXEC (@SQL)
GO

