
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2020-03 
-- Description:	Input: A json string http://dw1-ubuntu.ad.bluedot.global:81/api/v1/Diseases/Acquisition
--				To update disease.Species
-- =============================================

create PROCEDURE disease.usp_UpdateDiseaseVectors 
	@Json nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. Retrieve data 
		Declare @tbl table ([DiseaseVectorId] int, [DiseaseVector] nvarchar(100), [DiseaseVectorCategory] nvarchar(200))
		--if data type changed, error would be raised here
		INSERT INTO @tbl([DiseaseVectorId], [DiseaseVector], [DiseaseVectorCategory])
			SELECT diseaseVectorId, diseaseVector, diseaseVectorCategory
			FROM OPENJSON(@Json)
				WITH (
					diseaseVectorId int,
					diseaseVector nvarchar(100),
					diseaseVectorCategory nvarchar(200)
					)
			
		--merge into main table
		MERGE [disease].DiseaseVectors AS TARGET
		USING @tbl AS SOURCE
		ON (TARGET.[DiseaseVectorId] = SOURCE.[DiseaseVectorId])
		WHEN MATCHED
			THEN UPDATE SET TARGET.[DiseaseVector]=SOURCE.[DiseaseVector],
						TARGET.[DiseaseVectorCategory]=SOURCE.[DiseaseVectorCategory]
		WHEN NOT MATCHED BY TARGET 
			THEN INSERT([DiseaseVectorId], [DiseaseVector], [DiseaseVectorCategory])
			VALUES (SOURCE.[DiseaseVectorId], SOURCE.[DiseaseVector], SOURCE.[DiseaseVectorCategory])
		WHEN NOT MATCHED BY SOURCE
		THEN DELETE;

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update Speciess in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END