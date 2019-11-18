
-- =============================================
-- Author:		Vivian
-- Create date: 2018-12 
-- Description:	Input: EventId and @UserGridId
-- Modified 2019-04	Output: All destination airport at least 10% from userGrid regardless of the risk values
-- Modified 2019-05: input changed to UserId instead of UserGridId, use AOI; Output added IsLocal; Only show above threshold apts
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEmailGetEventAirportInfo
	@EventId    AS INT,
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;
	--1. user aoi 
	--1.1 geonameIds
	Declare @userAois varchar(256)=(Select AoiGeonameIds From dbo.AspNetUsers Where Id=@UserId)
	--1.2 geonameId in table
	Declare @table_userGeonameIds table (UserGeonameId int, LocationType int, 
								Lat decimal(10,5), Long decimal(10,5), UserCityPoint GEOGRAPHY)
	Insert into @table_userGeonameIds(UserGeonameId, LocationType, Lat, Long)
		Select f2.GeonameId, f2.LocationType, f2.Latitude, f2.Longitude
		From [bd].[ufn_StringSplit](@userAois, ',') as f1, [place].[ActiveGeonames] as f2
		Where f1.item=f2.GeonameId
	--city point
	Update @table_userGeonameIds Set UserCityPoint=geography::Point(Lat, Long, 4326)
		Where LocationType=2
	--1.3 grids for city only (too many from prov/country, saving them hurts performance)
	Declare @tbl_userGrids table (UserGridId nvarchar(12))
	Insert into @tbl_userGrids
		Select f2.gridId
		From @table_userGeonameIds as f1, bd.HUFFMODEL25KMWORLDHEXAGON as f2
		where f1.LocationType=2 and f2.SHAPE.STIntersects(UserCityPoint)=1

	--need end month
	Declare @endMth int
	Select @endMth=EventMonth From zebra.EventPrevalence Where EventId=@EventId;
	--threshold from table
	Declare @DestinationCatchmentThreshold decimal(5,2)=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='DestinationCatchmentThreshold');
	--apts in this grid but not considered as affected
	With T1 as (
		Select f1.GridId -- from city
		From [zebra].[EventDestinationGridV3] as f1, @tbl_userGrids as f2
		Where f1.EventId=@EventId and f1.GridId=f2.UserGridId
		Union -- from prov
		Select f1.GridId
		From [zebra].[EventDestinationGridV3] as f1, @table_userGeonameIds as f2, zebra.GridProvince as f3
		Where f2.LocationType=4 and f1.EventId=@EventId 
			and f2.UserGeonameId=f3.Adm1GeonameId and f1.GridId=f3.GridId
		Union -- from country
		Select f1.GridId
		From [zebra].[EventDestinationGridV3] as f1, @table_userGeonameIds as f2, zebra.GridCountry as f3
		Where f2.LocationType=6 and f1.EventId=@EventId 
			and f2.UserGeonameId=f3.CountryGeonameId and f1.GridId=f3.GridId
		)
	--apts in this grid considered as affected
	SELECT DISTINCT ISNULL([StationName], 'N/A') As AirportName, ISNULL([StationCode], 'N/A') as AirportCode,
	--CONCAT(Case When [MinProb]<0.001 Then '<0.1' 
	--	Else Convert(varchar(10), Cast(Round([MinProb]*100, 1) as decimal(5,1))) End, 
	--	'%-', Cast(Round([MaxProb]*100, 1) as decimal(5,1)), '%') as Probability,
	--CONCAT(Case When [MinExpVolume]<0.1 Then '<0.1' 
	--		Else Convert(varchar(10), Cast(Round([MinExpVolume], 2) as decimal(10,1))) End, 
	--	Case When [MaxExpVolume]<0.1 Then '' Else '-' End, 
	--	Case When [MaxExpVolume]<0.1 Then '' 
	--		Else Convert(varchar(10), Cast(Round([MaxExpVolume], 2) as decimal(10,1))) End
	--	) as ExpectedTravellers,
		[MinProb] ProbabilityMin, 
		[MaxProb] ProbabilityMax,
		[MinExpVolume] InfectedTravellersMin,
		[MaxExpVolume] InfectedTravellersMax
	FROM  [zebra].[EventDestinationAirport] as f1, [zebra].[GridStation] as f2, T1
	Where f1.EventId=@EventId AND f1.DestinationStationId>0  
			AND MONTH(f2.ValidFromDate)=@endMth
			AND f2.Probability>=@DestinationCatchmentThreshold AND f1.DestinationStationId=f2.StationId
			AND f2.GridId=T1.GridId
	Order by [MinExpVolume] Desc, [MinProb] Desc
END