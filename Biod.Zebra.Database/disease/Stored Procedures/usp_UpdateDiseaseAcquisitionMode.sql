
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2020-03 
-- Description:	Input: A json string http://dw1-ubuntu.ad.bluedot.global:81/api/v1/Diseases/DiseaseAcquisition
--				To update disease.Species
-- =============================================

create PROCEDURE disease.usp_UpdateDiseaseAcquisitionMode 
	@Json nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. Retrieve data 
		Declare @tbl table ([DiseaseId] int, [SpeciesId] int, [AcquisitionModeId] int, 
							[AcquisitionModeRank] int)
		--if data type changed, error would be raised here
		INSERT INTO @tbl([DiseaseId], [SpeciesId], [AcquisitionModeId], [AcquisitionModeRank])
			SELECT diseaseId, speciesId, acquisitionModeId, acquisitionModeRank
			FROM OPENJSON(@Json)
				WITH (
					diseaseId int,
					speciesId int,
					acquisitionModeId int,
					acquisitionModeRank int
					)
			
		--merge into main table
		MERGE [disease].Xtbl_Disease_AcquisitionMode AS TARGET
		USING @tbl AS SOURCE
		ON (TARGET.[DiseaseId] = SOURCE.[DiseaseId]
			AND TARGET.[SpeciesId] = SOURCE.[SpeciesId]
			AND TARGET.[AcquisitionModeId] = SOURCE.[AcquisitionModeId])
		WHEN MATCHED
			THEN UPDATE SET TARGET.[AcquisitionModeRank]=SOURCE.[AcquisitionModeRank]
		WHEN NOT MATCHED BY TARGET 
			THEN INSERT([DiseaseId], [SpeciesId], [AcquisitionModeId], [AcquisitionModeRank])
			VALUES (SOURCE.[DiseaseId], SOURCE.[SpeciesId], 
					SOURCE.[AcquisitionModeId], SOURCE.[AcquisitionModeRank])
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