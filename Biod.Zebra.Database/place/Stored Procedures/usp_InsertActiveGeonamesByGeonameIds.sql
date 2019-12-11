
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
	--add their provinces and countries if not already there
	Insert into @tbl_geonameIds
		Select Distinct f2.Admin1GeonameId as GeonameId
		From @tbl_geonameIds as f1, [place].[Geonames] as f2
		Where f1.GeonameId=f2.GeonameId and f2.Admin1GeonameId IS NOT NULL
		Union
		Select Distinct f2.[CountryGeonameId]
		From @tbl_geonameIds as f1, [place].[Geonames] as f2
		Where f1.GeonameId=f2.GeonameId and f2.[CountryGeonameId] IS NOT NULL
		Except
		Select GeonameId
		From [place].[ActiveGeonames]
		Except
		Select GeonameId
		From @tbl_geonameIds
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