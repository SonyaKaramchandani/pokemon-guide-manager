
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-05 
-- Description:	https://wiki.bluedot.global/display/CEN/Dataset+Report%3A+Geonames
-- Notes: returns cities, if cityNames are duplicated, select 1st only	
-- For Zebra external API, ZebraGeonameCities_Get
-- =============================================

CREATE PROCEDURE place.usp_GetGeonameCities 
	@inputTerm nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY

	    DECLARE @allText NVARCHAR(135) = '"' + @inputTerm + '*"';
		--save geonameId from main table
		Declare @tbl table (GeonameId int)
		Insert into @tbl
        Select GeonameId  
			From place.[Geonames]
			WHERE CONTAINS([DisplayName], @allText)
					AND LocationType = 2
			ORDER BY Population DESC;

		--plus from AlternatenameEng
		With T1 as (
			Select GeonameId From place.GeonameAlternatenameEng
				WHERE CONTAINS([AlternatenameEng], @allText)
					AND LocationType = 2			

			Union
			Select GeonameId 
			From @tbl
			),
		--join all necessary fields
		T2 as (
		select top 100 f2.GeonameId, DisplayName, Longitude, Latitude, f2.Shape.STAsText() ShapeAsText,
			ROW_NUMBER() OVER (PARTITION by DisplayName ORDER BY [Population] desc, SearchSeq2) as SeqSub, 
				ROW_NUMBER() OVER (ORDER BY [Population] desc, SearchSeq2) as Seq
			From T1, place.[Geonames] as f2
			Where T1.GeonameId=f2.GeonameId
			Order by [Population] desc, SearchSeq2
			)
		--output
		Select GeonameId, DisplayName, Longitude, Latitude, ShapeAsText from 
		(
		Select top 10 GeonameId, DisplayName, Seq, Longitude, Latitude, ShapeAsText From T2 Where SeqSub=1
		) as t
		Order by Seq



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