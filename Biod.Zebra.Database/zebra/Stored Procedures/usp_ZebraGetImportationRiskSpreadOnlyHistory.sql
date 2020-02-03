
-- =============================================
-- Author:		Vivian
-- Create date: 2019-06 
-- Description:	same as usp_ZebraGetImportationRiskSpreadOnly, except read data from _history tables
-- James confirmed (13June2019, when current week is non-local, last week was non-local)
-- called by usp_GetZebraWeeklyEmail
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraGetImportationRiskSpreadOnlyHistory
	@EventId int, 
	@GeonameIds AS varchar(MAX)
AS
BEGIN
	SET NOCOUNT ON

	--user locations
	Declare @tbl_userGeonameId table (UserGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), CityPoint GEOGRAPHY)
	insert into @tbl_userGeonameId(UserGeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
			From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, [place].[ActiveGeonames] as f2
			Where Convert(int, f1.item)=f2.GeonameId
	Update @tbl_userGeonameId Set CityPoint=(geography::Point(latitude, longitude, 4326)) Where LocationType=2

	/****** no local spread******/
	Declare @EventMonth int, @MinPrevelance float, @MaxPrevelance float
	Select @MinPrevelance=MinPrevelance, @MaxPrevelance=MaxPrevelance, @EventMonth=EventMonth
	From zebra.EventPrevalence_history
	Where EventId=@EventId

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
		Declare @DestinationCatchmentThreshold decimal(5,2)=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='DestinationCatchmentThreshold')
		Declare @PassengerVolumes int;
		With T1 as (
			Select Distinct f4.DestinationStationId, f4.Volume
			From @tbl_userGrid as f1, [zebra].[EventDestinationGrid_history] as f2, 
				[zebra].[GridStation] as f3, zebra.EventDestinationAirport_history as f4
			Where f2.EventId=@EventId and f4.EventId=@EventId and MONTH(f3.ValidFromDate)=@EventMonth
				and f1.GridId=f2.GridId and f3.Probability>=@DestinationCatchmentThreshold 
				and f2.GridId=f3.GridId and f3.StationId=f4.DestinationStationId
			)
		Select @PassengerVolumes=sum(Volume) From T1

		--3. Results
		Declare @MinProb decimal(5,4)=1-POWER((1-@MinPrevelance), @PassengerVolumes)
		Declare @MaxProb decimal(5,4)=1-POWER((1-@MaxPrevelance), @PassengerVolumes)
		Declare @MinExpVolume decimal(10,3)=@MinPrevelance*@PassengerVolumes
		Declare @MaxExpVolume decimal(10,3)=@MaxPrevelance*@PassengerVolumes

		Select ISNULL(@MinProb, CAST(0 as decimal)) as MinProbability,  
		ISNULL(@MaxProb, CAST(0 as decimal)) as MaxProbability, 
		ISNULL(@MinExpVolume, CAST(0 as decimal)) as MinExpTravelers, 
		ISNULL(@MaxExpVolume, CAST(0 as decimal)) as MaxExpTravelers
	End
	Else --@MinPrevelance IS NULL
		Select CAST(0 as decimal) as MinProbability,  CAST(0 as decimal) as MaxProbability, 
		CAST(0 as decimal) as MinExpTravelers, CAST(0 as decimal) as MaxExpTravelers
END