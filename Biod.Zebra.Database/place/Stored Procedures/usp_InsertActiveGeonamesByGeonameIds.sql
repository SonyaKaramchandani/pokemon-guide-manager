
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-10 
-- Description:	Insert into [place].[ActiveGeonames] if a geoname is not there
-- =============================================

CREATE PROCEDURE place.usp_InsertActiveGeonamesByGeonameIds 
	@GeonameIds varchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	Declare @tbl_geonameIds table (GeonameId int)
	--new ids
	Insert into @tbl_geonameIds
		Select item
		From [bd].[ufn_StringSplit](@GeonameIds, ',')
		Except
		Select GeonameId
		From [place].[ActiveGeonames]
	--Insert new
	Insert into [place].[ActiveGeonames] ([GeonameId]
		  ,[Name]
		  ,[LocationType]
		  ,[Admin1GeonameId]
		  ,[CountryGeonameId]
		  ,[DisplayName]
		  ,[Alternatenames]
		  ,[ModificationDate]
		  ,[FeatureCode]
		  ,[CountryName]
		  ,[Latitude]
		  ,[Longitude]
		  ,[Population]
		  ,[SearchSeq2]
		  ,[Shape]
		  ,[LatPopWeighted]
		  ,[LongPopWeighted])
	Select f1.[GeonameId]
		  ,[Name]
		  ,[LocationType]
		  ,[Admin1GeonameId]
		  ,[CountryGeonameId]
		  ,[DisplayName]
		  ,[Alternatenames]
		  ,[ModificationDate]
		  ,[FeatureCode]
		  ,[CountryName]
		  ,[Latitude]
		  ,[Longitude]
		  ,[Population]
		  ,[SearchSeq2]
		  ,[Shape]
		  ,[LatPopWeighted]
		  ,[LongPopWeighted]
	From [place].[Geonames] as f1, @tbl_geonameIds as f2
	Where f1.GeonameId=f2.GeonameId
END