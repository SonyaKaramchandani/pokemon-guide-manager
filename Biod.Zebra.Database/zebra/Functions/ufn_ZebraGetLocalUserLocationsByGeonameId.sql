
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-07  
-- Description:	Input: GeonameId, LookForPaidUser, NewCaseNotificationEnabled
--				Output: A list of UserIds which is local in terms of input GeonameId
--				called by usp_ZebraEmailGetProximalEmailData to find proximal users of a geonameId
-- =============================================

CREATE FUNCTION bd.ufn_ZebraGetLocalUserLocationsByGeonameId (
	@GeonameId int, --event's one aoi
	@LookForPaidUser bit, --looking for paid user? 1-paid user only, 0-all users(don't filter)
	@NewCaseNotificationEnabled bit, --1-NewCaseNotificationEnabled user only, 0-all users(don't filter)
	@EmailConfirmed bit, --1-EmailConfirmed user only, 0-all users(don't filter)
	@DiseaseId int --don't include relevance=3 users; -1 means don't filter
	) 
RETURNS @returnResults TABLE (UserId nvarchar(128))
AS
BEGIN
	
	--1. prepare users
	--1.1 with paid and newCaseNotificationEnabled and EmailConfirmed info
	Declare @tbl_Users table(UserId nvarchar(128), AoiGeonameIds varchar(256))
	Insert into @tbl_Users(UserId, AoiGeonameIds)
		Select f1.UserId, f2.AoiGeonameIds
		From zebra.ufn_GetUsersPaidStatus() as f1, dbo.AspNetUsers as f2
		Where (@LookForPaidUser=0 or f1.IsPaidUser=@LookForPaidUser) 
			and (@NewCaseNotificationEnabled=0 or f2.NewCaseNotificationEnabled=@NewCaseNotificationEnabled)
			and (@EmailConfirmed=0 or f2.NewCaseNotificationEnabled=@EmailConfirmed)
			and f1.UserId=f2.Id
	--1.2 need to consider relevance?
	If @DiseaseId>0
	Begin
		--remove from user setting
		Delete from @tbl_Users Where UserId in 
			(Select UserId From [zebra].[Xtbl_User_Disease_Relevance]
				Where DiseaseId=@diseaseId and RelevanceId=3);
		--remove from role setting
		With T1 as (
			Select UserId From @tbl_Users
			Except
			Select UserId From [zebra].[Xtbl_User_Disease_Relevance]
				Where DiseaseId=@diseaseId
			),
		T2 as (
			Select T1.UserId
			From T1, [dbo].[AspNetUserRoles] as f2, [zebra].[Xtbl_Role_Disease_Relevance] as f3
			Where T1.UserId=f2.UserId and f2.RoleId=f3.RoleId and f3.DiseaseId=@diseaseId and f3.RelevanceId=3
			)
		Delete from @tbl_Users Where UserId in (Select UserId From T2)
	End
	--1.2 expand to userXgeonames
	Declare @tbl_userCrossGeoname table (UserId nvarchar(128), UserGeonameId int)
	Declare @thisUser nvarchar(128), @thisAoi varchar(256)
	Declare MyCursor CURSOR FAST_FORWARD 
	FOR Select UserId, AoiGeonameIds
		From @tbl_Users
	
	OPEN MyCursor
	FETCH NEXT FROM MyCursor
	INTO @thisUser, @thisAoi

	WHILE @@FETCH_STATUS = 0
	Begin
		Insert into @tbl_userCrossGeoname(UserId, UserGeonameId)
			Select @thisUser, f1.item
			From [bd].[ufn_StringSplit](@thisAoi, ',') as f1
		FETCH NEXT FROM MyCursor
		INTO @thisUser, @thisAoi
	End

	CLOSE MyCursor
	DEALLOCATE MyCursor

	--1.3 User's location only
	Declare @tbl_userGeonameId table (UserGeonameId int, CountryGeonameId int, Admin1GeonameId int, LocationType int,
									Latitude Decimal(10, 5), Longitude Decimal(10, 5),
									CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY);
	insert into @tbl_userGeonameId(UserGeonameId, CountryGeonameId, Admin1GeonameId, Latitude, Longitude, LocationType)
		Select distinct f1.UserGeonameId, f2.CountryGeonameId, f2.Admin1GeonameId, f2.Latitude, f2.Longitude, f2.LocationType
		From @tbl_userCrossGeoname as f1, [place].[ActiveGeonames] as f2
		Where f1.UserGeonameId=f2.GeonameId

	Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
	
	--2. prepare event location
	Declare @intputLocType int, @intputCityPoint GEOGRAPHY, @intputCityBuffer GEOGRAPHY
	Declare @admin1GeonameId int, @countryGeonameId int
	Select @intputLocType=LocationType, @intputCityPoint=Shape,
			@admin1GeonameId=Admin1GeonameId, @countryGeonameId=CountryGeonameId
		From [place].[ActiveGeonames] 
		Where GeonameId=@GeonameId
	If @intputLocType=2 --city
		Set @intputCityBuffer=@intputCityPoint.STBuffer(@Distance)
			
	--3. find local users 
	Declare @tbl_localSpreadUserGeonameId table (UserGeonameId int)
	--3.1 use administrative hierachy
	Insert into @tbl_localSpreadUserGeonameId(UserGeonameId)
		Select Distinct UserGeonameId
		From @tbl_userGeonameId
			--  user's geonameId same as event geonameId
		Where UserGeonameId=@GeonameId 
			--user is city, event's a province or country
			or (LocationType=2 and (@intputLocType=4 and Admin1GeonameId=@GeonameId  or @intputLocType=6 and CountryGeonameId=@GeonameId))
			--User is prov, event in a city of that prov or event in that country)
			Or (LocationType=4 and (@intputLocType=2 and UserGeonameId=@admin1GeonameId or @intputLocType=6 and CountryGeonameId=@GeonameId))
			--User is country, events in that country
			Or (LocationType=6 and @intputLocType in (2, 4) and UserGeonameId=@countryGeonameId)
	--3.1.1 save results
	Insert into @returnResults
		Select Distinct f1.UserId
		From @tbl_userCrossGeoname as f1, @tbl_localSpreadUserGeonameId as f2
		Where f1.UserGeonameId=f2.UserGeonameId
	--3.1.2 clean up
	--3.1.2.1 GeonameId of users in not found
	Declare @tbl table (UserGeonameId int)
	Insert into @tbl
		Select distinct UserGeonameId
		From @tbl_userCrossGeoname 
		Where UserId Not in (Select UserId From @returnResults)
	--3.1.2.2 remaining UserGeonameId to look for
	Delete from @tbl_userGeonameId
		Where UserGeonameId Not in (Select UserGeonameId From @tbl)
	Delete from @tbl_localSpreadUserGeonameId

	--3.2 use non-administrative hierachy
	If Exists (Select 1 From @tbl_userGeonameId)
	Begin --3
		--user aoi city buffer
		If exists (Select 1 from @tbl_userGeonameId Where LocationType=2)
		Begin --4 
			--user city points
			Update @tbl_userGeonameId
				Set CityPoint=(geography::Point(Latitude, Longitude, 4326))
				Where LocationType=2
			--user city buffer
			Update @tbl_userGeonameId
				Set CityBuffer=CityPoint.STBuffer(@Distance) Where CityPoint IS NOT NULL
		End --4
		
		--A. Input loc is a city
		If @intputLocType=2
		Begin --5
			--user city intersects input city
			Insert into @tbl_localSpreadUserGeonameId
				Select distinct UserGeonameId
				From @tbl_userGeonameId
				Where LocationType=2 and CityBuffer.STIntersects(@intputCityPoint)=1
			--user prov/country intersects input city
			Insert into @tbl_localSpreadUserGeonameId
				Select distinct f1.UserGeonameId
				From @tbl_userGeonameId as f1, [place].[CountryProvinceShapes] as f2
				Where f1.LocationType in (4,6) and f1.UserGeonameId=f2.GeonameId 
					and @intputCityBuffer.STIntersects(f2.SimplifiedShape)=1
		End --5
		Else --B. Input loc is prov/country
			--user city intersects input prov/country
			Insert into @tbl_localSpreadUserGeonameId
				Select distinct f1.UserGeonameId
				From @tbl_userGeonameId as f1, [place].[CountryProvinceShapes] as f2
				Where f1.LocationType=2 and f2.GeonameId=@GeonameId
					and f1.CityBuffer.STIntersects(f2.SimplifiedShape)=1
	
		--save results
		Insert into @returnResults
			Select Distinct f1.UserId
			From @tbl_userCrossGeoname as f1, @tbl_localSpreadUserGeonameId as f2
			Where f1.UserGeonameId=f2.UserGeonameId
	End --3

	Return
END