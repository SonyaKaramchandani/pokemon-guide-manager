
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-01 
-- Description:	Input: A json string
--				Output: do QA and update [disease].[Systems]
-- =============================================

CREATE PROCEDURE bd.usp_CompareJsonStrings 
	@JsonStr nvarchar(max), 
	@Id varchar(5),
	@resultId	INT OUTPUT --1-same, 0-different
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		Set @resultId=1
		--first time, insert
		If not exists (Select 1 from [bd].[LastJsonStrs] Where Id=@Id)
		Begin --1
			Declare @desc varchar(100)
			If @Id=1 Set @desc='disease.Diseases'
			Else if @Id=2 Set @desc='disease.Symptoms'
			Else if @Id=3 Set @desc='disease.Systems'
			Else if @Id=4 Set @desc='place.Geonames'
			Else if @Id=5 Set @desc='surveillance.Articles'

			Insert into [bd].[LastJsonStrs](Id, [Description], JsonStr, LastUpdatedDate)
			Select @Id, @desc, @JsonStr, GETDATE()
	
			Set @resultId=0
		End --1
		--not first time
		Else
		Begin --2
			DECLARE @existingStr nvarchar(max)=(Select JsonStr From [bd].[LastJsonStrs] Where Id=@Id)
			--different, update
			If @existingStr is Null or @JsonStr COLLATE Latin1_General_CS_AS <> @existingStr COLLATE Latin1_General_CS_AS
			Begin
				Update [bd].[LastJsonStrs] Set JsonStr=@JsonStr, LastUpdatedDate=GETDATE() Where Id=@Id
				Set @resultId=0
			End
		End --2

		RETURN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to compare json in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END
