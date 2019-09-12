
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-05 
-- Description:	https://wiki.bluedot.global/display/CEN/Dataset+Report%3A+Geonames
-- Notes: if cityNames are duplicated, select 1st only	
-- 15Aug2018, changed top 3 to top 10, in surveillance only, not in Zebra
-- =============================================

create PROCEDURE place.usp_SearchGeonames 
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
		--province
		T2 as (
			Select GeonameId From place.uvw_ProvinceSubset
				Where AsciiName like @term + '%'
			Union
			Select GeonameId From place.GeonameAlternatenameEng
				Where LocationType=4 and AlternatenameEng like @term + '%'
			),
		--Country
		T3 as (
			Select GeonameId From place.uvw_CountrySubset
				Where AsciiName like @term + '%' OR CountryName like @term + '%'
			Union
			Select GeonameId From place.GeonameAlternatenameEng
				Where LocationType=6 and AlternatenameEng like @term + '%'
			),
		--City
		T4 as (
		select top 30 f2.GeonameId, DisplayName, 2 as LocationType, 
			ROW_NUMBER() OVER (PARTITION by DisplayName ORDER BY [Population] desc, SearchSeq2) as SeqSub, 
				ROW_NUMBER() OVER (ORDER BY [Population] desc, SearchSeq2) as Seq
			From T1, place.uvw_CitySubset as f2
			Where T1.GeonameId=f2.GeonameId
			Order by [Population] desc, SearchSeq2
			),
		--province
		T5 as (
		select top 3 f2.GeonameId, DisplayName, 4 as LocationType, 
				ROW_NUMBER() OVER (ORDER BY [Population] desc) as Seq
			From T2, place.uvw_ProvinceSubset as f2
			Where T2.GeonameId=f2.GeonameId
			Order by [Population] desc
			),
		--Country
		T6 as (
		select top 3 f2.GeonameId, DisplayName, 6 as LocationType, 
				ROW_NUMBER() OVER (ORDER BY [Population] desc, SearchSeq2) as Seq
			From T3, place.uvw_CountrySubset as f2
			Where T3.GeonameId=f2.GeonameId
			Order by [Population] desc, SearchSeq2
		)

		Select GeonameId, DisplayName, LocationType from 
		(
		Select top 10 GeonameId, DisplayName, LocationType, Seq From T4 Where SeqSub=1
		Union
		Select GeonameId, DisplayName, LocationType, Seq From T5
		Union
		Select GeonameId, DisplayName, LocationType, Seq From T6) as t
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