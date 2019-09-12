﻿
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-09 
-- Description:	Input: A json string http://api-dev1.ad.bluedot.global:83/api/v1/Diseases/Species
--				To update disease.Species
-- =============================================

create PROCEDURE disease.usp_UpdateSpecies 
	@Json nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. A tmp table to hold all imcoming data 
		Declare @tbl table (SpeciesId int, SpeciesName varchar(128))
		--if data type changed, error would be raised here
		INSERT INTO @tbl(SpeciesId, SpeciesName)
			SELECT speciesId, speciesName
			FROM OPENJSON(@Json)
				WITH (
					speciesId int,
					speciesName varchar(100)
					)
			
		--2. Update all existing with same speciesId
		Update disease.Species Set SpeciesName=f2.SpeciesName
			From disease.Species as f1, @tbl as f2
			Where f1.SpeciesId=f2.SpeciesId
			
		--3. New SpeciesId: insert into table 
		INSERT INTO disease.Species(SpeciesId, SpeciesName)
			Select SpeciesId, SpeciesName
			From @tbl 
			Where SpeciesId NOT IN (Select SpeciesId From disease.Species)

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