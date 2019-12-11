
-- =============================================
-- Author:		Vivian
-- Create date: 2019-04 
-- Description:	simplified version of usp_GetZebraImportationRisk, without output UserGeonameId
-- to improve the performance for no need to know each UserGeonameId such as usp_ZebraEventGetEventSummary w/o 
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEventGetImportationRisk
	@EventId int, 
	--@UserGridId nvarchar(12),
	@GeonameIds varchar(256)
AS
BEGIN
	SET NOCOUNT ON

	--user locations
	Declare @tbl_userGeonameId table (UserGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
										CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY)
	insert into @tbl_userGeonameId(UserGeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
			Select f2.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
			From [bd].[ufn_StringSplit](@GeonameIds, ',') as f1, [place].[ActiveGeonames] as f2
			Where Convert(int, f1.item)=f2.GeonameId
	--need number of user aois
	Declare @NumberOfAois int =(Select count(*) From @tbl_userGeonameId)
	--event locations
	Declare @tbl_eventLoc table (GeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
										Latitude Decimal(10, 5), Longitude Decimal(10, 5), 
										CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY);
	Insert into @tbl_eventLoc (GeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
		Select f1.GeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
		From [surveillance].[Xtbl_Event_Location] as f1, [place].[ActiveGeonames] as f2
		Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId

	/******find local spread******/
	Declare @tbl_localSpread table (UserGeonameId int)
	--1.use administrative hierachy
	Insert into @tbl_localSpread
		Select Top 1 f1.UserGeonameId
		From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			--  user's geonameId same as event geonameId
		Where f1.UserGeonameId=f2.GeonameId 
			--user is city, event's a province or country
			or (f1.LocationType=2 and (f2.LocationType=4 and f1.Admin1GeonameId=f2.GeonameId  or f2.LocationType=6 and f1.CountryGeonameId=f2.GeonameId))
			--User is prov, event in a city of that prov or event in that country)
			Or (f1.LocationType=4 and (f2.LocationType=2 and f2.Admin1GeonameId=f1.UserGeonameId or f2.LocationType=6 and f1.CountryGeonameId=f2.GeonameId))
			--User is country, events in that country
			Or (f1.LocationType=6 and f2.LocationType in (2, 4) and f1.UserGeonameId=f2.CountryGeonameId)

	--Return if found any loc is local
	If Exists (Select 1 from @tbl_localSpread) GOTO Branch_1

	--2. use city buffer
	Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
	--user loc is city
	If exists (Select 1 from @tbl_userGeonameId Where LocationType=2)
	Begin --2
		--user city point
		Update @tbl_userGeonameId
			Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
			Where LocationType=2
		--user city buffer
		Update @tbl_userGeonameId
			Set CityBuffer=CityPoint.STBuffer(@Distance) Where CityPoint IS NOT NULL
		--event loc is a city
		Insert into @tbl_localSpread
			Select Top 1 UserGeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2
			Where f1.LocationType=2 and f2.LocationType=2
				 and f1.CityBuffer.STIntersects(geography::Point(f2.Latitude, f2.Longitude, 4326))=1
		--Return if found any loc is local
		If Exists (Select 1 from @tbl_localSpread) GOTO Branch_1

		--event loc is a prov/country
		Insert into @tbl_localSpread
			Select Top 1 UserGeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2, [place].[CountryProvinceShapes] as f3
			Where f1.LocationType=2 and f2.LocationType in (4,6) and f2.GeonameId=f3.GeonameId 
				and f1.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		--Return if found any loc is local
		If Exists (Select 1 from @tbl_localSpread) GOTO Branch_1
	End--2

	--event loc is city
	If exists (Select 1 from @tbl_eventLoc Where LocationType=2)
	Begin --3
		--event city point
		Update @tbl_eventLoc
			Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
			Where LocationType=2
		--event city buffer
		Update @tbl_eventLoc
			Set CityBuffer=CityPoint.STBuffer(@Distance) 
			Where LocationType=2
		--user is prov/country
		Insert into @tbl_localSpread
			Select Top 1 f1.UserGeonameId
			From @tbl_userGeonameId as f1, @tbl_eventLoc as f2, 
				[place].[CountryProvinceShapes] as f3
			Where f1.LocationType in (4,6) and f2.LocationType=2 and f1.UserGeonameId=f3.GeonameId 
				and f2.CityBuffer.STIntersects(f3.SimplifiedShape)=1
	End --3

	Branch_1:
	--3.return local spread
	If Exists (Select 1 from @tbl_localSpread)
	Begin
		Select 1 as localSpread, CAST(0 as decimal) as ImportationMinProbability,  
			CAST(0 as decimal) as ImportationMaxProbability, CAST(0 as decimal) as ImportationMinExpTravelers, 
			CAST(0 as decimal) as ImportationMaxExpTravelers, 
			'-' as ImportationPriorityTitle, '-' as ImportationProbabilityName, 
			@NumberOfAois as NumberOfAois
		from @tbl_localSpread
		--no need to go further
		Return
	End

	/******continue when no local spread******/
	Declare @EventMonth int, @MinPrevelance float, @MaxPrevelance float
	Select @MinPrevelance=MinPrevelance, @MaxPrevelance=MaxPrevelance, @EventMonth=EventMonth
	From zebra.EventPrevalence
	Where EventId=@EventId

	If @MinPrevelance IS NOT NULL 
		AND (Select IsLocalOnly From surveillance.Event Where EventId=@EventId)=0
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
			From @tbl_userGrid as f1, [zebra].[EventDestinationGridV3] as f2, 
				[zebra].[GridStation] as f3, zebra.EventDestinationAirport as f4
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

		Declare @PriorityTitle varchar(20)
		Declare @ProbabilityName varchar(30)
		If @MaxProb IS NULL Or @MaxProb<0.01 
		Begin
			Set @PriorityTitle='negligible'
			Set @ProbabilityName='Negligible'
		End
		Else If @MaxProb<0.2
		Begin
			Set @PriorityTitle='low'
			Set @ProbabilityName='Low probability'
		End
		Else If @MaxProb>0.7
		Begin
			Set @PriorityTitle='high'
			Set @ProbabilityName='High probability'
		End
		Else 
		Begin
			Set @PriorityTitle='medium'
			Set @ProbabilityName='Medium probability'
		End

		Select 0 as localSpread,
			@MinProb as ImportationMinProbability,  @MaxProb as ImportationMaxProbability, 
			@MinExpVolume as ImportationMinExpTravelers, @MaxExpVolume as ImportationMaxExpTravelers,
			@PriorityTitle as ImportationPriorityTitle, @ProbabilityName as ImportationProbabilityName,
			@NumberOfAois as NumberOfAois
	End
	Else --@MinPrevelance IS NULL or IsLocalOnly=1
		Select 0 as localSpread,
			CAST(0 as decimal) as ImportationMinProbability,  CAST(0 as decimal) as ImportationMaxProbability, 
			CAST(0 as decimal) as ImportationMinExpTravelers, CAST(0 as decimal) as ImportationMaxExpTravelers,
			'-' as ImportationPriorityTitle, '-' as ImportationProbabilityName, 0 as NumberOfAois
END