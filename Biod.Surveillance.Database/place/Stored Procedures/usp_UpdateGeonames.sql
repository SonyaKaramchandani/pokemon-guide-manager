
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	Input: A json string
--				Output: update place.Geonames, not for fist time
-- Modified 25July2018: getting input @Json in stead of download in SP
-- =============================================

CREATE PROCEDURE place.usp_UpdateGeonames   
	@Json nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
	  If @Json = N'[]'
	    SELECT 'Nothing changed' as ErrorMessage
	  Else
	  Begin --1n --A tmp table to hold all imcoming related data 
		
		Declare @tbl table (GeonameId int, Asciiname varchar(200), FeatureCode varchar(50), LocationType int,
						Admin1GeonameId int, Admin1AsciiName varchar(200), CountryGeonameId int, CountryName varchar(200),
						modificationDate date, Alternatenames nvarchar(max), DisplayName nvarchar(500),
						Latitude DECIMAL(10,5), Longitude DECIMAL(10,5), [Population] bigint,
						Arry_Alternatenames nvarchar(max))

		--insert to a tmp table
		INSERT INTO @tbl(GeonameId, Asciiname, FeatureCode, Admin1GeonameId, Admin1AsciiName, 
				CountryGeonameId, CountryName, ModificationDate, Alternatenames, Latitude, Longitude, 
				[population], Arry_Alternatenames)
		SELECT geonameId, asciiname, featureCode, JSON_VALUE(admin1, '$.geonameId'), 
			JSON_VALUE(admin1, '$.asciiName'), JSON_VALUE(country, '$.geonameId'),
			JSON_VALUE(country, '$.countryName'), lastModified, alternatenames,
			Convert(DECIMAL(10,5), latitude), Convert(DECIMAL(10,5), longitude), 
			[population], alternateNamesInfo
		FROM OPENJSON(@Json)
			WITH (
				geonameId int,
				asciiname varchar(200),
				featureCode varchar(50),
				admin1 nvarchar(max) AS JSON,
				country nvarchar(max) AS JSON,
				lastModified date,
				alternatenames nvarchar(max),
				latitude varchar(50),
				longitude varchar(50),
				population bigint,
				alternateNamesInfo nvarchar(max) AS JSON
				) as f1 

		--2. Insert new, and update existing's displayname 
		If Exists (Select 1 From @tbl)
		Begin--2--2.1 add LocationType
			Update @tbl Set LocationType=f2.PlaceTypeId
				From @tbl as f1 left join [place].[GeoNameFeatureCodes] as f2
				On f1.featureCode=f2.FeatureCode;

			--2.2 update DisplayName
			--2.2.1 update non-city/province and TERR DisplayName, others stay NULL
			Update @tbl Set [DisplayName]=Asciiname
				Where LocationType is NULL OR FeatureCode='TERR'
			--2.2.2 update country DisplayName
			Update @tbl Set [DisplayName]= COALESCE(CountryName, Asciiname)
				Where LocationType=6 AND FeatureCode<>'TERR'
			--2.2.3 update province DisplayName
			Update @tbl Set [DisplayName]= CONCAT(Asciiname, ', ', CountryName)
				Where LocationType=4
			--2.2.4 update city DisplayName
			Update @tbl Set [DisplayName]= CONCAT(Asciiname, ', ', Admin1AsciiName, ', ', CountryName)
				Where LocationType=2

			--2.3 English alternate name
			--2.3.1 get en version
			Declare @tbl_name table (GeonameId int, AlternatenameEng varchar(200), LocationType int);
			With T1 as (
				Select distinct GeonameId, LocationType, convert(varchar(600), f2.alternateName) as alternateName
				From @tbl
				CROSS APPLY OPENJSON (Arry_Alternatenames) 
						WITH (alternateName nvarchar(256),
						isoLanguage varchar(10)) as f2
						Where isoLanguage='en' And alternateName is not null
				)
			Insert into @tbl_name(GeonameId, AlternatenameEng, LocationType)
				Select GeonameId, alternateName, LocationType
				From T1


			--2.3 insert new and update existing
			--2.3.1 new to insert
			Declare @tbl_1 table (GeonameId int) -- new, not in existing
			Insert into @tbl_1(GeonameId)
				Select GeonameId From @tbl Except Select GeonameId From place.Geonames
			--insert
			If Exists ( Select 1 from @tbl_1)
			Begin--3 --main table
				Insert into place.Geonames(GeonameId, [Name], LocationType, Admin1GeonameId, CountryGeonameId, 
								DisplayName, ModificationDate, Alternatenames, FeatureCode,
								[CountryName], Latitude, Longitude, [Population])
					Select f1.GeonameId, Asciiname, LocationType, Admin1GeonameId,	CountryGeonameId, 
						DisplayName, modificationDate, Alternatenames, FeatureCode, CountryName,
						Latitude, Longitude, [Population]
					From @tbl as f1, @tbl_1 as f2
					Where f1.geonameId=f2.GeonameId
				--alternate name table
				Insert into place.GeonameAlternatenameEng(GeonameId, AlternatenameEng, LocationType)
					Select f1.GeonameId, AlternatenameEng, LocationType
					From @tbl_name as f1, @tbl_1 as f2
					Where f1.geonameId=f2.GeonameId
			End--3
			--2.3.2 existing to update place.Geonames
			Declare @tbl_2 table (GeonameId int) -- in existing
			Insert into @tbl_2(GeonameId)
				Select GeonameId From @tbl Except Select GeonameId From @tbl_1
			--update
			If Exists ( Select 1 from @tbl_2)
			Begin--4--main table
				Update place.Geonames
				Set [Name]=f2.Asciiname, [LocationType]=f2.LocationType, 
					[Admin1GeonameId]=f2.[Admin1GeonameId],
					CountryGeonameId=F2.CountryGeonameId,
					[DisplayName]=f2.[DisplayName], [Alternatenames]=f2.Alternatenames, 
					[ModificationDate]=f2.modificationDate,
					CountryName=f2.CountryName, FeatureCode=f2.FeatureCode,
					Latitude=f2.Latitude, Longitude=f2.Longitude, 
					[Population]=f2.[Population]
				From place.Geonames as f1, @tbl as f2, @tbl_2 as f3
				Where f1.[GeonameId]=f2.geonameId and f2.geonameId=f3.GeonameId;
				--alternate name table
				--1. delete old ones
				Delete from place.GeonameAlternatenameEng
					Where GeonameId In (Select GeonameId From @tbl_2)
				--2. insert new ones
				Insert into place.GeonameAlternatenameEng(GeonameId, AlternatenameEng, LocationType)
					Select f1.GeonameId, AlternatenameEng, LocationType
					From @tbl_name as f1, @tbl_2 as f2
					Where f1.geonameId=f2.GeonameId
			End--4
			SELECT 'Successfully updated' as ErrorMessage
		End--2
		Else
			SELECT 'Record(s) have not been inserted. Failed to Insert Geonames.' as ErrorMessage
     End--1
	 --action!
	 COMMIT TRAN
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
