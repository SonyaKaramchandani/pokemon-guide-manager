
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-01 
-- Description:	Input: All 3 disease API json strings
--				Output: main switchboard, calls other SPs, finishes all updating
-- =============================================

CREATE PROCEDURE disease.usp_UpdateDiseaseApi_main 
	@Json_1 nvarchar(max), -- Diseases
	@Json_2 nvarchar(max), -- Symptoms
	@Json_3 nvarchar(max), -- Systems
	@Json_4 nvarchar(max), -- Species
	@Json_5 nvarchar(max), -- DiseaseVectors
	@Json_6 nvarchar(max), -- TransferModality
	@Json_7 nvarchar(max), -- AcquisitionModes
	@Json_8 nvarchar(max) -- DiseaseAcquisitionMode
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN

		Declare @resultId_System INT, @resultId_Symptom INT
		--1 process Systems first
		EXEC disease.usp_UpdateSystems @Json_3, @resultId=@resultId_System OUTPUT
		--2 process Symptoms second
		EXEC disease.usp_UpdateSymptoms @Json_2, @resultId=@resultId_Symptom OUTPUT
		--3 process Species 
		EXEC disease.usp_UpdateSpecies @Json_4
		--4 process Diseases 
		EXEC disease.usp_UpdateDiseases @Json_1
		--5 process DiseaseVectors
		EXEC disease.usp_UpdateDiseaseVectors @Json_5
		--6 process TransferModality
		EXEC disease.usp_UpdateTransferModality @Json_6
		--7 process AcquisitionModes
		EXEC disease.usp_UpdateAcquisitionModes @Json_7
		--8 process DiseaseAcquisitionMode
		EXEC disease.usp_UpdateDiseaseAcquisitionMode @Json_8

		SELECT 'Successfully updated' as ErrorMessage

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update main in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END