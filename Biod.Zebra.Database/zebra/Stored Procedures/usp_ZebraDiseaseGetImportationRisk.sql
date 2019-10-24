
-- =============================================
-- Author:		Vivian
-- Create date: 2019-10 
-- Description:	similar as in usp_GetZebraImportationRisk to look for destination spread. 
--				But not check local spread, no need
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraDiseaseGetImportationRisk
	@DiseaseId int, 
	@GeonameIds varchar(256) --user aoi
AS
BEGIN
	SET NOCOUNT ON

	--user locations
	Declare @tbl_userGeonameId table (UserGeonameId int, LocationType int, Latitude Decimal(10, 5), Longitude Decimal(10, 5), CityPoint GEOGRAPHY)
	insert into @tbl_userGeonameId(UserGeonameId, LocationType, Latitude, Longitude)
			Select f2.GeonameId, f2.LocationType, f2.Latitude, f2.Longitude
			From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, place.Geonames as f2
			Where Convert(int, f1.item)=f2.GeonameId
	--
	Update @tbl_userGeonameId
		Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
		Where LocationType=2

	--calculate risk value
	Declare @EventMonth int, @MinPrevelance float, @MaxPrevelance float
	Select @MinPrevelance=MinPrevelance, @MaxPrevelance=MaxPrevelance, @EventMonth=EventMonth
	From zebra.DiseaseEventPrevalence
	Where DiseaseId=@DiseaseId

	If @MinPrevelance IS NOT NULL
	Begin 
		--1. find dest grids
		Declare @tbl_userGrid table (GridId nvarchar(12))
		Insert into @tbl_userGrid (GridId)
			--city
			Select f2.gridId
			From @tbl_userGeonameId as f1, [bd].[HUFFMODEL25KMWORLDHEXAGON] as f2
			Where f1.LocationType=2 and f2.SHAPE.STIntersects(f1.CityPoint) = 1
			union --province
			Select f4.GridId
			From @tbl_userGeonameId as f3, [zebra].[GridProvince] as f4
			Where f3.LocationType=4 and f3.UserGeonameId=f4.Adm1GeonameId
			union --country
			Select f6.GridId
			From @tbl_userGeonameId as f5, [zebra].GridCountry as f6
			Where f5.LocationType=6 and f5.UserGeonameId=f6.CountryGeonameId
		--2. find dest airports 
		Declare @PassengerVolumes int;
		With T1 as (
			Select Distinct f4.DestinationStationId, f4.Volume
			From @tbl_userGrid as f1, [zebra].DiseaseEventDestinationGrid as f2, 
				[zebra].[GridStation] as f3, zebra.DiseaseEventDestinationAirport as f4
			Where f2.DiseaseId=@DiseaseId and f4.DiseaseId=@DiseaseId and MONTH(f3.ValidFromDate)=@EventMonth
				and f1.GridId=f2.GridId and f3.Probability>0.1 
				and f2.GridId=f3.GridId and f3.StationId=f4.DestinationStationId
			)
		Select @PassengerVolumes=sum(Volume) From T1

		--3. Results
		Declare @MinProb decimal(5,4)=1-POWER((1-@MinPrevelance), @PassengerVolumes)
		Declare @MaxProb decimal(5,4)=1-POWER((1-@MaxPrevelance), @PassengerVolumes)
		Declare @MinExpVolume decimal(10,3)=@MinPrevelance*@PassengerVolumes
		Declare @MaxExpVolume decimal(10,3)=@MaxPrevelance*@PassengerVolumes


		Select @MinProb as ImportationMinProbability,  @MaxProb as ImportationMaxProbability, 
			@MinExpVolume as ImportationMinExpTravelers, @MaxExpVolume as ImportationMaxExpTravelers
	End
	Else --@MinPrevelance IS NULL or IsLocalOnly=1
		Select CAST(0 as decimal) as ImportationMinProbability,  CAST(0 as decimal) as ImportationMaxProbability, 
			CAST(0 as decimal) as ImportationMinExpTravelers, CAST(0 as decimal) as ImportationMaxExpTravelers
END