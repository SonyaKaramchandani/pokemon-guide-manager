
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-01 
-- Description:	Input: A json string
--				Output: inserted into [disease].[Symptoms]
-- Modified: 2018-07, use lastModifed, not to compare name anymore
-- =============================================

CREATE PROCEDURE disease.usp_UpdateSymptoms 
	@Json nvarchar(max),
	@resultId	INT OUTPUT --1: no deleting, no newly insertion, no major changes
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		Set @resultId=1
		--1. A tmp table to hold all incoming data 
		Declare @tbl table ([SymptomId] int, [Symptom] varchar(100), [SystemId] int, 
					[SymptomDefinition] varchar(500), [DefinitionSource] varchar(500), LastModified datetime)
		--if data type changed, error would be raised here
		INSERT INTO @tbl([SymptomId], [Symptom], [SystemId], [SymptomDefinition], [DefinitionSource], LastModified)
		SELECT symptomId, symptom, systemId, [definition], definitionSource, convert(datetime, lastModified, 126)
			FROM OPENJSON(@Json)
				WITH (
				  symptomId int,
				  symptom varchar(100),
				  systemId int,
				  [definition] varchar(500),
				  definitionSource varchar(500),
				  lastModified char(23)
				  )
			
		--2. LastModified different
		Declare @tbl_id table([SymptomId] int) -- this is to hold questionble Ids
		Insert into @tbl_id
		Select f1.SymptomId from @tbl as f1, [disease].[Symptoms] as f2
			where f1.SymptomId=f2.SymptomId and f1.LastModified>f2.LastModified
			
		If (select count(*) from @tbl_id)>0
		Begin --1
			Update [disease].[Symptoms] 
			Set Symptom=f2.Symptom, 
				SystemId=f2.SystemId, 
				[SymptomDefinition]=f2.[SymptomDefinition], 
				DefinitionSource=f2.DefinitionSource,
				LastModified=f2.LastModified
			From [disease].[Symptoms] as f1, @tbl as f2, @tbl_id as f3
			Where f1.SymptomId=f2.SymptomId and f2.SymptomId=f3.SymptomId
			--
			Set @resultId=0
		End --1
			
	
		--3. symptomId in old, not in new, delete (will cascade deleted in Xtbl_Disease_Symptom)
		If Exists (Select 1 From [disease].[Symptoms] Where SymptomId Not in (Select SymptomId From @tbl))
		Begin
			Delete from [disease].[Symptoms]
			Where SymptomId Not in (Select SymptomId From @tbl)
			--
			Set @resultId=0
		End
			
		--4. New SymptomId: insert into table 
		If Exists (Select 1 From @tbl Where SymptomId Not in (Select SymptomId From [disease].[Symptoms]))
		Begin
			INSERT INTO [disease].[Symptoms]([SymptomId], [Symptom], [SystemId], [SymptomDefinition], [DefinitionSource], LastModified)
			Select [SymptomId], [Symptom], [SystemId], [SymptomDefinition], [DefinitionSource], LastModified
			From @tbl 
			Where [SymptomId] NOT IN (Select [SymptomId] From [disease].[Symptoms])
			--
			Set @resultId=0
		End

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update symptoms in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END