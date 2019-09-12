
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-01 
-- Description:	Input: A json string
--				Output: do QA and update [disease].[Systems]
-- Modified: 2018-07, use lastModifed, not to compare name anymore
-- =============================================

CREATE PROCEDURE disease.usp_UpdateSystems 
	@Json nvarchar(max),
	@resultId	INT OUTPUT --0: there's deleting, or newly insertion, or updated
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		Set @resultId=1
		--1. A tmp table to hold all imcoming data 
		Declare @tbl table (SystemId int, [System] varchar(100), [Notes] varchar(500), LastModified datetime)
		--if data type changed, error would be raised here
		INSERT INTO @tbl(SystemId, [System], [Notes], LastModified)
		SELECT systemId, [system], notes, LastModified
		FROM OPENJSON(@Json)
			WITH (
				systemId int,
				notes varchar(500),
				[system] varchar(100),
				lastModified char(23)
				)
			
		--2. LastModified different
		Declare @tbl_id table(SystemId int) -- this is to hold questionble Ids
		Insert into @tbl_id
		Select f1.SystemId from @tbl as f1, [disease].[Systems] as f2
			where f1.SystemId=f2.SystemId and f1.LastModified>f2.LastModified
			
		If (select count(*) from @tbl_id)>0
		Begin --1
			Set @resultId=0
			--
			Update [disease].[Systems] 
				Set [System]=f2.[System], [Notes]=f2.[Notes],LastModified=f2.LastModified
			From [disease].[Systems] as f1, @tbl as f2, @tbl_id as f3
			Where f1.SystemId=f2.SystemId and f1.SystemId=f3.SystemId
		End --1
			
		--3. SystemId in old, not in new, delete (will set null in [disease].[Symptoms])
		If Exists (Select 1 From [disease].[Systems] Where SystemId Not in (Select SystemId From @tbl))
		Begin
			Delete from [disease].[Systems]
			Where SystemId Not in (Select SystemId From @tbl)
			--
			Set @resultId=0
		End
			
		--4. New SystemId: insert into table 
		If Exists (Select 1 From @tbl Where SystemId Not in (Select SystemId From [disease].[Systems]))
		Begin
			INSERT INTO [disease].[Systems](SystemId, [System], [Notes], LastModified)
			Select SystemId, [System], [Notes], LastModified
			From @tbl 
			Where SystemId NOT IN (Select SystemId From [disease].[Systems])
			--
			Set @resultId=0
		End

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update systems in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END