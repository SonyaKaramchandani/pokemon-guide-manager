
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-09 
-- Description:	Input: A json string
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
			SELECT speciesId, species
			FROM OPENJSON(@Json)
				WITH (
					speciesId int,
					species varchar(100)
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

		--4. symptomId in old, not in new, delete (will cascade deleted in Xtbl_Disease_Symptom)
		If Exists (Select 1 From [disease].Species Where SpeciesId Not in (Select SpeciesId From @tbl))
		Begin
			Delete from [disease].Species
			Where SpeciesId Not in (Select SpeciesId From @tbl)
		End

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