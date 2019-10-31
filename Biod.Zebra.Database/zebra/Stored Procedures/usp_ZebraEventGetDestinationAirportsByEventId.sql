
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	query destination airports from zebra.EventDestinationAirport
--				(V3)
-- Modified: 2019-04 added @GeonameIds, Show Risk of Importation airports relevant to GeonameIds
-- Modification 21Jun2019: change end of event length from Enddate to date_of_last_reported_case
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetDestinationAirportsByEventId
	@EventId INT,
	@GeonameIds varchar(256)
AS
BEGIN
	SET NOCOUNT ON;
	--total vol
	Declare @totalVol int=(Select Volume 
		From zebra.EventDestinationAirport
		Where EventId=@EventId and DestinationStationId=-1)
	--dep apts exists?
	Declare @hasApts bit=0
	If exists (Select 1 From zebra.EventDestinationAirport
			Where EventId=@EventId and DestinationStationId<>-1)
		Set @hasApts=1

	--@totalVol>0 does guarantee @hasApts=1
	If @totalVol>0 and @hasApts=1
	Begin --1 no AOI
		If @GeonameIds=N''
			Select Top 5 CityDisplayName, StationName, StationCode, CAST(Volume as int) as Volume, 
				CONVERT(varchar(10), convert(int, round(100.0*Volume/@totalVol, 0))) + '%' as Pct, 
				Longitude, Latitude, [MinProb] as ProbabilityMin,
				[MaxProb] as ProbabilityMax, [MinExpVolume] as InfectedTravellersMin,
				[MaxExpVolume] as InfectedTravellersMax
			From zebra.EventDestinationAirport
			Where EventId=@EventId AND DestinationStationId>0
			Order By ProbabilityMax Desc
		Else If @GeonameIds=N'-1'
			Select CityDisplayName, StationName, StationCode, CAST(Volume as int) as Volume, 
				CONVERT(varchar(10), convert(int, round(100.0*Volume/@totalVol, 0))) + '%' as Pct, 
				Longitude, Latitude, [MinProb] as ProbabilityMin,
				[MaxProb] as ProbabilityMax, [MinExpVolume] as InfectedTravellersMin,
				[MaxExpVolume] as InfectedTravellersMax
			From zebra.EventDestinationAirport
			Where EventId=@EventId AND DestinationStationId>0
		Else
		Begin --2 Has AOI
			--1.User locations
			Declare @tbl_userGrids table (GridId nvarchar(12))
			Declare @tbl_userLocations table (GeonameId int, Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
									GridId nvarchar(12), LocationType int)
			Insert into @tbl_userLocations(GeonameId, Latitude, Longitude, LocationType)
				Select f2.GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
				From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, [place].[ActiveGeonames] as f2
			Where Convert(int, f1.item)=f2.GeonameId
			--1.2 grids for cities
			Update @tbl_userLocations Set GridId=f2.gridId
				From @tbl_userLocations as f1, bd.HUFFMODEL25KMWORLDHEXAGON as f2
				Where f1.LocationType=2 and
				f2.SHAPE.STIntersects(geography::Point(f1.Latitude, f1.Longitude, 4326)) = 1
			--1.3. add grids from prov and country
			Insert into @tbl_userGrids
				Select GridId From @tbl_userLocations Where LocationType=2
				Union
				Select f2.GridId
					From @tbl_userLocations as f1, [zebra].[GridProvince] as f2
					Where f1.LocationType=4 and f1.GeonameId=f2.Adm1GeonameId
				Union
				Select f2.GridId
					From @tbl_userLocations as f1, [zebra].GridCountry as f2
					Where f1.LocationType=6 and f1.GeonameId=f2.CountryGeonameId
			--2. need timeline
			Declare @endMth int
			Set @endMth=(Select MONTH(MAX(EventDate)) From surveillance.Xtbl_Event_Location Where EventId=@EventId);

			--Results
			Select Distinct f1.CityDisplayName, f1.StationName, f1.StationCode, CAST(f1.Volume as int) as Volume, 
				CONVERT(varchar(10), convert(int, round(100.0*Volume/@totalVol, 0))) + '%' as Pct, 
				f1.Longitude, f1.Latitude, f1.[MinProb] as ProbabilityMin,
				f1.[MaxProb] as ProbabilityMax, f1.[MinExpVolume] as InfectedTravellersMin,
				f1.[MaxExpVolume] as InfectedTravellersMax
			From zebra.EventDestinationAirport as f1, @tbl_userGrids as f2, zebra.EventDestinationGridV3 as f3,
				[zebra].[GridStation] as f4
			Where f1.EventId=@EventId AND f1.DestinationStationId>0 AND f3.[EventId]=@EventId  
				AND MONTH(f4.ValidFromDate)=@endMth AND f2.GridId=f3.GridId
				AND f4.Probability>0.1 AND f1.DestinationStationId=f4.StationId
				AND f3.GridId=f4.GridId
			Order By InfectedTravellersMin Desc, ProbabilityMin Desc
		End --2
	End --1
	Else 
		Select '-' as CityDisplayName, '-' as StationName, '-' as StationCode,  
			CAST(0 as int) as Volume, '-' as Pct, CAST(0 as decimal) as Longitude, 
			CAST(0 as decimal) as Latitude,
			-1.0 as ProbabilityMin, -1.0 as ProbabilityMax,
			-1.0 as InfectedTravellersMin, -1.0 as InfectedTravellersMax

END