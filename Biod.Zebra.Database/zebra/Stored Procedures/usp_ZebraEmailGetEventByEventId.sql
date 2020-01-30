
-- =============================================
-- Author:		Vivian
-- Create date: 2018-06 
-- Description:	Input: EventId, buffer size (https://wiki.bluedot.global/pages/viewpage.action?spaceKey=CEN&title=Zebra+Model+Development&preview=/49414858/49423753/ZebraV2_CatchmentProcessFigure.pdf)
--				Output: same as usp_GetZebraEventInfoByEventIdAndGeonameId
--					DiseaseName, locationName, AgentTypes(MicrobeType), transmissions, incubations, intervention(vaccination)
--					Notes, startDate, endDate, reasons, some of counts
--				Version 2
-- 2018-09: added version 3
-- 2018-11: V5
-- 2018-12: V6 local feed, not for email anymore. Remove V2
-- 2019-05: 1-remove case count/userGrid/UserProfileLocationName/EventLocationName in output, 
-- 2-change user profile location to aois and add AoiIds in output
-- UserAoiLocationNames for everyone, add IsLocal in output
-- 2019-07 name changed
-- 2019-08 relevanceType of user's interested disease: 1-always email, 2-default as old, 3-remove from email
-- output IsLocal: 1-local user, 0-destination user, 2-non-local-non-destination but always email
-- 2019-09: disease schema change
-- 2019-11: incubation string calls ufn_FormStringFromSeconds
-- 2020-01: Add IsLocalOnly flag
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEmailGetEventByEventId
	@EventId    AS INT,
	@Distance int=100000 -- 100km
AS
BEGIN
	SET NOCOUNT ON;
	Declare @diseaseId int=(Select DiseaseId from [surveillance].[Event] Where EventId=@EventId)

	If @diseaseId IS NOT NULL
	Begin
		--0 tables
		--for disease
		Declare @tbl table (AgentTypes varchar(100), transmissionMode varchar(200), vaccination varchar(100), 
							reasons varchar(max), incubation varchar(100));
		--for locations
		Declare @tbl_locations table (GeonameId int, LocationType int, LocalOrIntlSpread int);
		Insert into @tbl_locations(GeonameId, LocationType, LocalOrIntlSpread)
			Select GeonameId, LocationType, LocalOrIntlSpread
			From bd.ufn_ValidLocationsOfEvent(@EventId);

		--1. AgentTypes
		With ST1 as (
			Select Distinct f3.AgentType
			From [disease].[Xtbl_Disease_Agents] as f1, [disease].[Agents] as f2, 
				[disease].[AgentTypes] as f3
			Where f1.DiseaseId=@diseaseId and f1.AgentId=f2.AgentId
				and f2.AgentTypeId=f3.AgentTypeId
			)
		Insert into @tbl(AgentTypes)
		Select stuff(
				(	Select ', '+ST1.AgentType
					From ST1
					ORDER BY ST1.AgentType
					For XML PATH ('')
				), 1,2,'') ;
		--2. transmissions
		With ST2 as (
			Select Distinct f2.DisplayName
			From [disease].[Xtbl_Disease_TransmissionMode] as f1, [disease].[TransmissionModes] as f2
			Where f1.SpeciesId=1 and 
				f1.DiseaseId=@diseaseId and f1.TransmissionModeId=f2.TransmissionModeId
			)
		Update @tbl set transmissionMode=(
		Select stuff(
				(	Select ', '+ST2.DisplayName
					From ST2
					ORDER BY ST2.DisplayName
					For XML PATH ('')
				), 1,2,'') );
		--solve &amp; issue
		Update @tbl set transmissionMode=REPLACE(transmissionMode, '&amp;', '&')
		--3. incubation
		Declare @incubation varchar(100)
		--get numbers
		Declare @minIncubation bigint, @maxIncubation bigint, @avgIncubation bigint
		Set @minIncubation=
			(Select [IncubationMinimumSeconds] From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		Set @maxIncubation=
			(Select [IncubationMaximumSeconds] From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		Set @avgIncubation=
			(Select [IncubationAverageSeconds] From [disease].DiseaseSpeciesIncubation Where DiseaseId=@diseaseId and SpeciesId=1)
		--str
		Declare @minStr varchar(10), @maxStr varchar(10), @avgStr varchar(10)
		--min
		Set @minStr=bd.ufn_FormStringFromSeconds(@minIncubation)
		--max
		Set @maxStr=bd.ufn_FormStringFromSeconds(@maxIncubation)
		--avg
		Set @avgStr=bd.ufn_FormStringFromSeconds(@avgIncubation)
		--all
		If  @minStr='-' and @maxStr='-' and @avgStr='-'
			Set @incubation='-'
		Else
			Set @incubation=CONCAT(@minStr, ' to ', @maxStr, ' (',
					@avgStr, ' avg.)');
		Update @tbl set incubation=@incubation;
		--4. vaccination(intervention)
		With T1 as (
			Select InterventionId 
			From disease.Xtbl_Disease_Interventions 
			Where DiseaseId=@diseaseId and SpeciesId=1
			)
		Update @tbl set vaccination=(
		select DisplayName from [disease].Interventions as f1, T1 
			Where f1.InterventionId=T1.InterventionId and DisplayName<>'Vaccine'
				and f1.InterventionType='Prevention'
		Union
		select Concat(DisplayName, ' (', convert(varchar(10), CAST(max(RiskReduction)*100 AS INT)), '% eff.)') 
			from [disease].Interventions as f1, [disease].InterventionSpecies as f2, T1
			Where f1.DisplayName='Vaccine' and f2.SpeciesId=1 
				and f1.InterventionId=T1.InterventionId and f2.InterventionId=T1.InterventionId
			Group by DisplayName);
		--if no data means response
		Update @tbl set Vaccination='Behavioural Only' Where Vaccination is NULL;

		----5. case count, no need anymore

		--6. reason
		With ST3 as (
			Select Distinct f2.ReasonName
			From surveillance.Xtbl_Event_Reason as f1, surveillance.EventCreationReasons as f2
			Where f1.EventId=@EventId and f1.ReasonId=f2.ReasonId
			)
		Update @tbl set reasons=(
		Select stuff(
				(	Select ','+ST3.ReasonName
					From ST3
					ORDER BY ST3.ReasonName
					For XML PATH ('')
				), 1,1,'') );

		--7. users, exclude unsubscribedUser
		--7.1 -user info only (UserAoiLocationNames NULL means local user)
		Declare @tbl_validUsers table (UserId nvarchar(128), UserAoiGeonameIds varchar(max), Email nvarchar(256),
									IsPaidUser bit, DoNotTrackEnabled bit, EmailConfirmed bit, SeqId int, 
									UserAoiLocationNames nvarchar(2000), RelevanceId int);
		--7.2 userId cross AoiGeonameId
		Declare @tbl_validUserCrossGeoname table (UserId nvarchar(128), UserGeonameId int)
		--7.3 user location only
		Declare @tbl_validUsers_locations table (UserGeonameId int, UserAdmin1GeonameId int, UserCountryGeonameId int,
												Lat decimal(10,5), Long decimal(10,5), LocationType int, 
												UserCityPoint GEOGRAPHY, UserCityGrid nvarchar(12));
		--7.1 user info, exclude unsubscribedUser
		Insert into @tbl_validUsers(UserId, UserAoiGeonameIds, Email, IsPaidUser, DoNotTrackEnabled, EmailConfirmed)
			Select f1.Id, f1.AoiGeonameIds, f1.Email, T3.IsPaidUser, f1.DoNotTrackEnabled, f1.EmailConfirmed
			From dbo.AspNetUsers as f1, zebra.ufn_GetUsersPaidStatus() as T3
			Where f1.NewOutbreakNotificationEnabled=1 and f1.Id=T3.UserId;
		--need seq to loop
		With T1 as (
			select UserId, ROW_NUMBER() OVER ( order by UserId) as rankId
			from @tbl_validUsers
			)
		Update @tbl_validUsers Set SeqId=T1.rankId
		From @tbl_validUsers as f1, T1
		Where f1.UserId=T1.UserId
		--7.2 expand to userXgeonames
		Declare @i int=1
		Declare @maxSeqId int =(Select Max(SeqId) From @tbl_validUsers)
		Declare @thisAoi varchar(256)
		While @i<=@maxSeqId
		Begin
			Set @thisAoi=(Select UserAoiGeonameIds From @tbl_validUsers Where SeqId=@i)
			Insert into @tbl_validUserCrossGeoname(UserId, UserGeonameId)
				Select f1.UserId, f2.item
				From @tbl_validUsers as f1, [bd].[ufn_StringSplit](@thisAoi, ',') as f2
				Where SeqId=@i
			Set @i=@i+1
		End
		--7.3 user location info only
		Insert into @tbl_validUsers_locations(UserGeonameId, UserAdmin1GeonameId, UserCountryGeonameId, Lat, Long, LocationType)
			Select Distinct f1.UserGeonameId, f2.Admin1GeonameId, f2.CountryGeonameId,
				f2.Latitude, f2.Longitude, F2.LocationType
			From @tbl_validUserCrossGeoname as f1, [place].[ActiveGeonames] as f2
			Where f1.UserGeonameId=f2.GeonameId;

		--7.4 concate AOI names for all
		With ST1 as (
			Select f1.UserId, f2.DisplayName
			From @tbl_validUserCrossGeoname as f1, [place].[ActiveGeonames] as f2
			Where f1.UserGeonameId=f2.GeonameId
			)
		Update @tbl_validUsers Set UserAoiLocationNames=f2.UserAoiLocationNames
			From @tbl_validUsers as f1, 
				(Select distinct UserId, stuff(
						(	Select '; '+ST1.DisplayName
							From ST1
							Where ST1.UserId=ST2.UserId
							ORDER BY ST1.DisplayName
							For XML PATH ('')
						), 1,2,'') as UserAoiLocationNames
					From ST1 as ST2) as f2
			Where f1.UserId=f2.UserId;

		--8 location of event
		--8.1 local
		Declare @tbl_eventLoc_local table (EventGeonameId int, EventrAdmin1GeonameId int, EventCountryGeonameId int,
											Lat decimal(10,5), Long decimal(10,5), LocationType int, EventCityPoint GEOGRAPHY)
		Insert into @tbl_eventLoc_local(EventGeonameId, Lat, Long, LocationType, EventrAdmin1GeonameId, EventCountryGeonameId)
			Select f1.GeonameId, f2.Latitude, f2.Longitude, f1.LocationType, f2.Admin1GeonameId, f2.CountryGeonameId
			From @tbl_locations as f1, [place].[ActiveGeonames] as f2
			Where f1.LocalOrIntlSpread=1 and f1.GeonameId=f2.GeonameId
		--8.2 spread don't need this table, use pre-processed

		--9 find local users 
		--results local users
		Declare @tbl_userLocal table (UserId nvarchar(128));
		--an tmp table to store user's location found
		Declare @tbl_userLocationFound table (UserGeonameId int);
		--9.1 use admin hierachy
		Insert into @tbl_userLocationFound(UserGeonameId)
			Select Distinct f2.UserGeonameId
			From  @tbl_eventLoc_local as f1, @tbl_validUsers_locations as f2
			--event geonameId same user's geonameId or user's prov/country geonameId
			Where f1.EventGeonameId=f2.UserGeonameId or f1.EventGeonameId=f2.UserCountryGeonameId or f1.EventGeonameId=f2.UserAdmin1GeonameId
				--User is prov, events in this prov
				Or (f2.LocationType=4 and f1.LocationType=2 and f1.EventrAdmin1GeonameId=f2.UserGeonameId)
				--User is country, events in this country
				Or (f2.LocationType=6 and f1.LocationType in (2, 4) and f1.EventCountryGeonameId=f2.UserGeonameId)
		--users found using admin hierachy
		Insert into @tbl_userLocal
			Select distinct f2.UserId
			From @tbl_userLocationFound as f1, @tbl_validUserCrossGeoname as f2
			Where f1.UserGeonameId=f2.UserGeonameId
		--clean up tmp found user's location
		Delete from @tbl_userLocationFound
		--shrink user cross location to look for
		Delete from @tbl_validUserCrossGeoname Where UserId in (Select UserId From @tbl_userLocal)
		
		--9.2 use city buffer
		If Exists (Select 1 From @tbl_validUserCrossGeoname)
		Begin --1
			--shrink user's location to look for
			Delete from @tbl_validUsers_locations Where UserGeonameId not in (Select UserGeonameId From @tbl_validUserCrossGeoname)
			--has city?
			Declare @userHasCity bit=0, @eventHasCity bit=0
			If Exists (Select 1 From @tbl_validUsers_locations Where LocationType=2)
			Begin --2
				Set @userHasCity=1
				Update @tbl_validUsers_locations Set UserCityPoint=geography::Point(Lat, Long, 4326)
			End --2
			If Exists (Select 1 From @tbl_eventLoc_local Where LocationType=2)
			Begin --3
				Set @eventHasCity=1
				Update @tbl_eventLoc_local Set EventCityPoint=geography::Point(Lat, Long, 4326)
			End -- 3

			--9.2.1 check city in city buffer
			If @userHasCity=1 and @eventHasCity=1
			Begin --4
				Insert into @tbl_userLocationFound
					Select Distinct f2.UserGeonameId
					From @tbl_eventLoc_local as f1, @tbl_validUsers_locations as f2
					Where f1.LocationType=2 and f2.LocationType=2
						And EventCityPoint.STBuffer(@Distance).STIntersects(UserCityPoint)=1
				--when found
				If Exists (Select 1 From @tbl_userLocationFound)
				Begin --5
					--userIds found using city in city buffer
					Insert into @tbl_userLocal
						Select distinct f2.UserId
						From @tbl_userLocationFound as f1, @tbl_validUserCrossGeoname as f2
						Where f1.UserGeonameId=f2.UserGeonameId
					--clean up tmp found user's location
					Delete from @tbl_userLocationFound
					--shrink user cross location to look for
					Delete from @tbl_validUserCrossGeoname Where UserId in (Select UserId From @tbl_userLocal)
				End --5
			End --4

			--9.2.2 check user city buffer intersects event prov/country shape
			If Exists (Select 1 From @tbl_validUserCrossGeoname)
			Begin --6
				--shrink user's location to look for
				Delete from @tbl_validUsers_locations 
					Where UserGeonameId not in (Select UserGeonameId From @tbl_validUserCrossGeoname)
				--Users have city, event has prov/country (@userHasCity not valid anymore)
				If Exists (Select 1 From @tbl_validUsers_locations Where LocationType=2) 
					And Exists (Select 1 From @tbl_eventLoc_local Where LocationType in (4,6))
				Begin --7
					Insert into @tbl_userLocationFound
						Select Distinct f2.UserGeonameId
						From @tbl_eventLoc_local as f1, @tbl_validUsers_locations as f2, 
							[place].[CountryProvinceShapes] as f3
						Where f1.LocationType in (4,6) and f2.LocationType=2 and f1.EventGeonameId=f3.GeonameId
							and f2.UserCityPoint.STBuffer(@Distance).STIntersects(f3.SimplifiedShape)=1
					--when found
					If Exists (Select 1 From @tbl_userLocationFound)
					Begin --8
						--userIds found 
						Insert into @tbl_userLocal
							Select distinct f2.UserId
							From @tbl_userLocationFound as f1, @tbl_validUserCrossGeoname as f2
							Where f1.UserGeonameId=f2.UserGeonameId
						--clean up tmp found user's location
						Delete from @tbl_userLocationFound
						--shrink user cross location to look for
						Delete from @tbl_validUserCrossGeoname Where UserId in (Select UserId From @tbl_userLocal)
					End --8
				End --7
			End --6

			--9.2.3 check event city buffer intersects user prov/country shape
			If Exists (Select 1 From @tbl_validUserCrossGeoname)
			Begin --9
				--shrink user's location to look for
				Delete from @tbl_validUsers_locations 
					Where UserGeonameId not in (Select UserGeonameId From @tbl_validUserCrossGeoname)
				--Users have prov/country, event has city (@eventHasCity not valid anymore)
				If Exists (Select 1 From @tbl_validUsers_locations Where LocationType in (4,6)) 
					And @eventHasCity=1
				Begin --10
					Insert into @tbl_userLocationFound
						Select Distinct f2.UserGeonameId
						From @tbl_eventLoc_local as f1, @tbl_validUsers_locations as f2, 
							[place].[CountryProvinceShapes] as f3
						Where f1.LocationType=2 and f2.LocationType in (4,6) and f2.UserGeonameId=f3.GeonameId
							and f1.EventCityPoint.STBuffer(@Distance).STIntersects(f3.SimplifiedShape)=1
					--when found
					If Exists (Select 1 From @tbl_userLocationFound)
					Begin --11
						--userIds found 
						Insert into @tbl_userLocal
							Select distinct f2.UserId
							From @tbl_userLocationFound as f1, @tbl_validUserCrossGeoname as f2
							Where f1.UserGeonameId=f2.UserGeonameId
						--clean up tmp found user's location
						Delete from @tbl_userLocationFound
						--shrink user cross location to look for
						Delete from @tbl_validUserCrossGeoname Where UserId in (Select UserId From @tbl_userLocal)
					End --11
				End --10
			End --9

		End --1

		--10 destination users
		If Exists (Select 1 From @tbl_validUserCrossGeoname)
		Begin --10
			--1. User locations
			--shrink user's location to look for
			Delete from @tbl_validUsers_locations 
				Where UserGeonameId not in (Select UserGeonameId From @tbl_validUserCrossGeoname)
			--find user grids for cities only
			Update @tbl_validUsers_locations Set UserCityGrid=f2.gridId
				From @tbl_validUsers_locations as f1, bd.HUFFMODEL25KMWORLDHEXAGON as f2
				Where f1.LocationType=2 and f2.SHAPE.STIntersects(UserCityPoint)=1

			----users remaining for destinations, potential users from spread
			--Declare @tbl_validUsersDest table(userId nvarchar(128), Email nvarchar(256),  
			--								IsPaidUser bit, UserAoiLocationNames nvarchar(2000))

			--Dates
			Declare @startDate Date, @endDate Date
			Select @startDate=StartDate, @endDate=EndDate from surveillance.[Event] Where EventId=@EventId;
			--local event?
			Declare @isLocalOnly bit=(Select [IsLocalOnly] from [surveillance].[Event] where EventId=@EventId)
		
			--10.2 destination users 
			Declare @tbl_userDest table (UserId nvarchar(128));
			If @isLocalOnly=0
			Begin --10.2
				--1 find user geonameid from dest grid
				Insert into @tbl_userLocationFound(UserGeonameId)
					--use user city's grid
					Select f1.UserGeonameId
					From @tbl_validUsers_locations as f1, [zebra].[EventDestinationGridV3] as f2
					Where f1.LocationType=2 and f2.EventId=@EventId and f1.UserCityGrid=f2.GridId
					Union --use user prov's grid
					Select f1.UserGeonameId
					From @tbl_validUsers_locations as f1, [zebra].[EventDestinationGridV3] as f2, zebra.GridProvince as f3
					Where f1.LocationType=4 and f2.EventId=@EventId and f1.UserGeonameId=f3.Adm1GeonameId and f2.GridId=f3.GridId
					Union --use user country's grid
					Select f1.UserGeonameId
					From @tbl_validUsers_locations as f1, [zebra].[EventDestinationGridV3] as f2, zebra.GridCountry as f3
					Where f1.LocationType=6 and f2.EventId=@EventId and f1.UserGeonameId=f3.CountryGeonameId and f2.GridId=f3.GridId;
				--2 find userId
				Insert into @tbl_userDest
					Select Distinct f2.UserId
					From @tbl_userLocationFound as f1, @tbl_validUserCrossGeoname as f2
					Where f1.UserGeonameId=f2.UserGeonameId
				--3 shrink user cross location for AOI names
				Delete from @tbl_validUserCrossGeoname Where UserId NOT IN (Select UserId From @tbl_userDest);
	
			End --10.2		

			--11.ProbabilityMax
			Declare @ProbabilityMax decimal(10,3)
			Set @ProbabilityMax=(Select [MaxExportationProbabilityViaAirports] From [zebra].[EventExtension]
					                   Where EventId=@EventId)
			Declare @PriorityTitle varchar(20)
			Declare @ProbabilityName varchar(30)
			If @ProbabilityMax IS NULL Or @ProbabilityMax<0.01 
			Begin
				Set @PriorityTitle='negligible'
				Set @ProbabilityName='Negligible'
			End
			Else If @ProbabilityMax<0.2
			Begin
				Set @PriorityTitle='low'
				Set @ProbabilityName='Low probability'
			End
			Else If @ProbabilityMax>0.7
			Begin
				Set @PriorityTitle='high'
				Set @ProbabilityName='High probability'
			End
			Else 
			Begin
				Set @PriorityTitle='medium'
				Set @ProbabilityName='Medium probability'
			End

		End --10

		--11 Relevance from user settings
		--from user settings
		Update @tbl_validUsers Set RelevanceId=f2.RelevanceId
			From @tbl_validUsers as f1, [zebra].[Xtbl_User_Disease_Relevance] as f2
			Where f1.UserId=f2.UserId and f2.DiseaseId=@diseaseId
		--from role settings
		Update @tbl_validUsers Set RelevanceId=f3.RelevanceId
			From @tbl_validUsers as f1, [dbo].[AspNetUserRoles] as f2, [zebra].[Xtbl_Role_Disease_Relevance] as f3
			Where f1.RelevanceId is NULL and f1.UserId=f2.UserId and f2.RoleId=f3.RoleId and f3.DiseaseId=@diseaseId
		--11.1 RelevanceId=1, always email
		Declare @tbl_userAlways table (UserId nvarchar(128))
		Insert into @tbl_userAlways
			Select UserId 
			From @tbl_validUsers
			Where RelevanceId=1
			Except
			(Select UserId From @tbl_userLocal
			Union
			Select UserId From @tbl_userDest
			)
		--11.2 RelevanceId=3, remove from email
		Delete from @tbl_userLocal Where UserId in 
			(Select UserId From @tbl_validUsers Where RelevanceId=3)
		Delete from @tbl_userDest Where UserId in 
			(Select UserId From @tbl_validUsers Where RelevanceId=3)

		--12.final		
		If @isLocalOnly=1 --local only
			--local users
			Select f1.DiseaseId, f1.DiseaseName, 
				ISNULL(f3.AgentTypes, '-') as MicrobeType, ISNULL(transmissionMode, '-') as TransmittedBy, 
				@incubation as IncubationPeriod, ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate], 
				ISNULL(@startDate, GETUTCDATE()) as StartDate, ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				f3.reasons as Reasons, @PriorityTitle as ExportationPriorityTitle, f2.Email, f4.EventTitle, 
				@EventId as EventId, f1.OutbreakPotentialAttributeId, @ProbabilityName as ExportationProbabilityName,  
				f2.IsPaidUser, f2.DoNotTrackEnabled, f2.EmailConfirmed, f2.UserAoiLocationNames as UserAoiLocationNames, 
				f5.UserId, f2.UserAoiGeonameIds as AoiGeonameIds, 1 as IsLocal, ISNULL(f2.RelevanceId, 2) as RelevanceId, @isLocalOnly as IsLocalOnly
			From disease.Diseases as f1, @tbl_validUsers as f2, @tbl as f3, 
				surveillance.[Event] as f4,
				@tbl_userLocal as f5, [surveillance].[EventPriorities] as f6
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId and f2.UserId=f5.UserId
				and f4.PriorityId=f6.PriorityId
			Union --always email users
			Select f1.DiseaseId, f1.DiseaseName, 
				ISNULL(f3.AgentTypes, '-') as MicrobeType, ISNULL(transmissionMode, '-') as TransmittedBy, 
				@incubation as IncubationPeriod, ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate], 
				ISNULL(@startDate, GETUTCDATE()) as StartDate, ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				f3.reasons as Reasons, @PriorityTitle as ExportationPriorityTitle, f2.Email, f4.EventTitle, 
				@EventId as EventId, f1.OutbreakPotentialAttributeId, @ProbabilityName as ExportationProbabilityName,  
				f2.IsPaidUser, f2.DoNotTrackEnabled, f2.EmailConfirmed, f2.UserAoiLocationNames as UserAoiLocationNames, 
				f5.UserId, f2.UserAoiGeonameIds as AoiGeonameIds, 2 as IsLocal, 1 as RelevanceId, @isLocalOnly as IsLocalOnly
			From disease.Diseases as f1, @tbl_validUsers as f2, @tbl as f3, 
				surveillance.[Event] as f4,
				@tbl_userAlways as f5, [surveillance].[EventPriorities] as f6
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId and f2.UserId=f5.UserId
				and f4.PriorityId=f6.PriorityId
		Else --local and global
			--local users
			Select f1.DiseaseId, f1.DiseaseName,  
				ISNULL(f3.AgentTypes, '-') as MicrobeType, ISNULL(transmissionMode, '-') as TransmittedBy, 
				@incubation as IncubationPeriod, ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate],
				ISNULL(@startDate, GETUTCDATE()) as StartDate, ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				f3.reasons as Reasons, @PriorityTitle as ExportationPriorityTitle, f2.Email, f4.EventTitle, 
				@EventId as EventId, f1.OutbreakPotentialAttributeId, @ProbabilityName as ExportationProbabilityName, 
				f2.IsPaidUser, f2.DoNotTrackEnabled, f2.EmailConfirmed, f2.UserAoiLocationNames as UserAoiLocationNames, f5.UserId as UserId, 
				f2.UserAoiGeonameIds as AoiGeonameIds, 1 as IsLocal, ISNULL(f2.RelevanceId, 2) as RelevanceId, @isLocalOnly as IsLocalOnly
			From disease.Diseases as f1, @tbl_validUsers as f2,
				@tbl as f3, surveillance.[Event] as f4,
				@tbl_userLocal as f5
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId and f2.UserId=f5.UserId
			Union -- destination users
			Select f1.DiseaseId, f1.DiseaseName, 
				ISNULL(f3.AgentTypes, '-') as MicrobeType, ISNULL(transmissionMode, '-') as TransmittedBy, 
				@incubation as IncubationPeriod, ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate],
				ISNULL(@startDate, GETUTCDATE()) as StartDate, ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				f3.reasons as Reasons, @PriorityTitle as ExportationPriorityTitle, f2.Email, f4.EventTitle, 
				@EventId as EventId, f1.OutbreakPotentialAttributeId, @ProbabilityName as ExportationProbabilityName, 
				f2.IsPaidUser, f2.DoNotTrackEnabled, f2.EmailConfirmed, f2.UserAoiLocationNames, f2.UserId, 
				f2.UserAoiGeonameIds as AoiGeonameIds, 0 as IsLocal, ISNULL(f2.RelevanceId, 2) as RelevanceId, @isLocalOnly as IsLocalOnly
			From disease.Diseases as f1, @tbl_validUsers as f2, @tbl as f3, 
				surveillance.[Event] as f4, @tbl_userDest as f5
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId and f2.UserId=f5.UserId
			Union --always email users
			Select f1.DiseaseId, f1.DiseaseName,  
				ISNULL(f3.AgentTypes, '-') as MicrobeType, ISNULL(transmissionMode, '-') as TransmittedBy, 
				@incubation as IncubationPeriod, ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate],
				ISNULL(@startDate, GETUTCDATE()) as StartDate, ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				f3.reasons as Reasons, @PriorityTitle as ExportationPriorityTitle, f2.Email, f4.EventTitle, 
				@EventId as EventId, f1.OutbreakPotentialAttributeId, @ProbabilityName as ExportationProbabilityName, 
				f2.IsPaidUser, f2.DoNotTrackEnabled, f2.EmailConfirmed, f2.UserAoiLocationNames as UserAoiLocationNames, f5.UserId as UserId, 
				f2.UserAoiGeonameIds as AoiGeonameIds, 2 as IsLocal, 1 as RelevanceId, @isLocalOnly as IsLocalOnly
			From disease.Diseases as f1, @tbl_validUsers as f2,
				@tbl as f3, surveillance.[Event] as f4,
				@tbl_userAlways as f5
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId and f2.UserId=f5.UserId
	End
	Else
	Begin
		Select top 0 0 as DiseaseId, '-' DiseaseName, '-' as MicrobeType, '-' as TransmittedBy, 
			'-' as IncubationPeriod, '-' as Vaccination, '-' as Brief, GETDATE() as LastUpdatedDate, 
			GETDATE() as StartDate, GETDATE() as EndDate, '-' as Reasons, 'negligible' as ExportationPriorityTitle, 
			'-' as Email, '-' as EventTitle, @EventId as EventId, 0 as OutbreakPotentialAttributeId, 
			'Negligible' as ExportationProbabilityName, CAST(0 AS BIT) as IsPaidUser, 
			CAST(0 AS BIT) as DoNotTrackEnabled, CAST(0 AS BIT) as EmailConfirmed,
			'-' as UserAoiLocationNames, '-' as UserId, '-' as AoiGeonameIds, 0 as IsLocal, 0 as RelevanceId, 0 as IsLocalOnly
	End
END