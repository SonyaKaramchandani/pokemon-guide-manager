
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-05 
-- Description:	https://wiki.bluedot.global/display/CEN/Dataset+Report%3A+Geonames
-- Notes: returns cities, if cityNames are duplicated, select 1st only	
-- For Zebra external API, ZebraGeonameCities_Get
-- =============================================

create PROCEDURE place.usp_GetGeonameCities 
	@inputTerm nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		declare @term nvarchar(200)
		Set @term=REPLACE(@inputTerm, ',', '');
		--City
		With T1 as (
			Select GeonameId From place.uvw_CitySubset
				Where AsciiName like @term + '%'
			Union
			Select GeonameId From place.GeonameAlternatenameEng
				Where LocationType=2 and AlternatenameEng like @term + '%'
			),
		--City
		T2 as (
		select top 100 f2.GeonameId, DisplayName, 2 as LocationType, 
			ROW_NUMBER() OVER (PARTITION by DisplayName ORDER BY [Population] desc, SearchSeq2) as SeqSub, 
				ROW_NUMBER() OVER (ORDER BY [Population] desc, SearchSeq2) as Seq
			From T1, place.uvw_CitySubset as f2
			Where T1.GeonameId=f2.GeonameId
			Order by [Population] desc, SearchSeq2
			)

		Select GeonameId, DisplayName, LocationType from 
		(
		Select top 10 GeonameId, DisplayName, LocationType, Seq From T2 Where SeqSub=1
		) as t
		Order by LocationType, Seq
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