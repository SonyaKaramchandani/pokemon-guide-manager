
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2020-03 
-- Description:	Input: A json string http://dw1-ubuntu.ad.bluedot.global:81/api/v1/Diseases/Acquisition
--				To update disease.Species
-- =============================================

create PROCEDURE disease.usp_UpdateAcquisitionModes 
	@Json nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. Retrieve data 
		Declare @tbl table ([AcquisitionModeId] int, [DiseaseVectorId] int, 
			[TransferModalityId] int, [Multiplier] int, 
			[AcquisitionModeLabel] nvarchar(100), [AcquisitionModeDefinitionLabel] nvarchar(500))
		--if data type changed, error would be raised here
		INSERT INTO @tbl([AcquisitionModeId], [DiseaseVectorId], [TransferModalityId], [Multiplier],
						[AcquisitionModeLabel], [AcquisitionModeDefinitionLabel])
			SELECT acquisitionModeId, diseaseVectorId, modalityId, multiplier, 
					acquisitionModeLabel, acquisitionModeDefinitionLabel
			FROM OPENJSON(@Json)
				WITH (
					acquisitionModeId int,
					diseaseVectorId int,
					modalityId int,
					multiplier int,
					acquisitionModeLabel nvarchar(100),
					acquisitionModeDefinitionLabel nvarchar(500)
					)
			
		--merge into main table
		MERGE [disease].[AcquisitionModes] AS TARGET
		USING @tbl AS SOURCE
		ON (TARGET.[AcquisitionModeId] = SOURCE.[AcquisitionModeId])
		WHEN MATCHED
			THEN UPDATE SET TARGET.[DiseaseVectorId]=SOURCE.[DiseaseVectorId], 
				TARGET.[TransferModalityId]=SOURCE.[TransferModalityId], 
				TARGET.[Multiplier]=SOURCE.[Multiplier], 
				TARGET.[AcquisitionModeLabel]=SOURCE.[AcquisitionModeLabel], 
				TARGET.[AcquisitionModeDefinitionLabel]=SOURCE.[AcquisitionModeDefinitionLabel]
		WHEN NOT MATCHED BY TARGET 
			THEN INSERT([AcquisitionModeId], [DiseaseVectorId], [TransferModalityId], [Multiplier],
						[AcquisitionModeLabel], [AcquisitionModeDefinitionLabel])
			VALUES (SOURCE.[AcquisitionModeId], SOURCE.[DiseaseVectorId], 
					SOURCE.[TransferModalityId], SOURCE.[Multiplier], 
					SOURCE.[AcquisitionModeLabel], SOURCE.[AcquisitionModeDefinitionLabel])
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