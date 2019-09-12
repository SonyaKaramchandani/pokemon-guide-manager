
-- =============================================
-- Author:		Vivian
-- Create date: 2018-06 
-- Description:	Input: EventId, buffer size (https://wiki.bluedot.global/pages/viewpage.action?spaceKey=CEN&title=Zebra+Model+Development&preview=/49414858/49423753/ZebraV2_CatchmentProcessFigure.pdf)
--				Output: same as usp_GetZebraEventInfoByEventIdAndGeonameId
--					DiseaseName, locationName, pathogenTypes(MicrobeType), transmissions, incubations, prevention(vaccination)
--					Notes, startDate, endDate, reasons, some of counts
--				Version 2
-- 2018-09: added version 3
-- 2018-11: V5
-- 2018-12: V6 local feed, not for email anymore. Remove V2
-- =============================================
CREATE PROCEDURE zebra.usp_GetZebraEventInfoByEventId_archive
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
		Declare @tbl table (pathogenTypes varchar(100), transmissionMode varchar(200), vaccination varchar(100), 
							reasons varchar(max), incubation varchar(100));
		--for locations
		Declare @tbl_locations table (GeonameId int, LocationType int, LocalOrIntlSpread int);
		Insert into @tbl_locations(GeonameId, LocationType, LocalOrIntlSpread)
			Select GeonameId, LocationType, LocalOrIntlSpread
			From bd.ufn_ValidLocationsOfEvent(@EventId);

		--1. pathogenTypes
		With ST1 as (
			Select Distinct f3.PathogenType
			From [disease].[Xtbl_Disease_Pathogens] as f1, [disease].[Pathogens] as f2, 
				[disease].[PathogenTypes] as f3
			Where f1.DiseaseId=@diseaseId and f1.PathogenId=f2.PathogenId
				and f2.PathogenTypeId=f3.PathogenTypeId
			)
		Insert into @tbl(pathogenTypes)
		Select stuff(
				(	Select ', '+ST1.PathogenType
					From ST1
					ORDER BY ST1.PathogenType
					For XML PATH ('')
				), 1,2,'') ;
		--2. transmissions
		With ST2 as (
			Select Distinct f2.DisplayName
			From [disease].[Xtbl_Disease_TransmissionMode] as f1, [disease].[TransmissionModes] as f2
			Where f1.DiseaseId=@diseaseId and f1.TransmissionModeId=f2.TransmissionModeId
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
		Declare @minIncubasion decimal(10,2), @maxIncubasion decimal(10,2), @avgIncubasion decimal(10,2)
		Set @minIncubasion=(Select [IncubationMinimumDays] From [disease].[Diseases] Where DiseaseId=@diseaseId)
		Set @maxIncubasion=(Select IncubationMaximumDays From [disease].[Diseases] Where DiseaseId=@diseaseId)
		Set @avgIncubasion=(Select IncubationAverageDays From [disease].[Diseases] Where DiseaseId=@diseaseId)
		--set unit
		declare @d char(1)='d'
		declare @h char(1)='h'
		--str
		Declare @minStr varchar(10), @maxStr varchar(10), @avgStr varchar(10)
		--min
		If @minIncubasion IS NULL Set @minStr='-'
		Else if @minIncubasion<1 Set @minStr=CONCAT(CONVERT(INT, ROUND(@minIncubasion*24, 0)), @h)
		Else Set @minStr=CONCAT(CONVERT(INT, ROUND(@minIncubasion, 0)), @d)
		--max
		If @maxIncubasion IS NULL Set @maxStr='-'
		Else if @maxIncubasion<1 Set @maxStr=CONCAT(CONVERT(INT, ROUND(@maxIncubasion*24, 0)), @h)
		Else Set @maxStr=CONCAT(CONVERT(INT, ROUND(@maxIncubasion, 0)), @d)
		--avg
		If @avgIncubasion IS NULL Set @avgStr='-'
		Else if @avgIncubasion<1 Set @avgStr=CONCAT(CONVERT(INT, ROUND(@avgIncubasion*24, 0)), @h)
		Else Set @avgStr=CONCAT(CONVERT(INT, ROUND(@avgIncubasion, 0)), @d)
		--all
		If  @minStr='-' and @maxStr='-' and @avgStr='-'
			Set @incubation='-'
		Else
			Set @incubation=CONCAT(@minStr, ' to ', @maxStr, ' (',
					@avgStr, ' avg.)');
		Update @tbl set incubation=@incubation;
		--4. vaccination(prevention)
		With T1 as (Select PreventionId From disease.Xtbl_Disease_Preventions Where DiseaseId=@diseaseId)
		Update @tbl set vaccination=(
		select DisplayName from [disease].[Preventions] as f1, T1 
			Where f1.PreventionId=T1.PreventionId and DisplayName='Prophylaxis'
		Union
		select Concat(DisplayName, ' (', convert(varchar(10), CAST(max(RiskReduction)*100 AS INT)), '% eff.)') 
			from [disease].[Preventions] as f1, T1
			Where  f1.PreventionId=T1.PreventionId and DisplayName='Vaccination'
			Group by DisplayName);

		----5. case count
		----5.1 local count
		--Declare @tbl_case_local Table(RepCases int, SuspCases int, ConfCases int, Deaths int);
		--With T0 as (
		--	Select f1.* 
		--	From [surveillance].[Xtbl_Event_Location] as f1, @tbl_locations as f2
		--	where EventId=@EventId and f2.LocalOrIntlSpread=1 and f1.GeonameId=f2.GeonameId
		--	)
		--Insert into @tbl_case_local(RepCases, SuspCases, ConfCases, Deaths)
		--Select Sum(RepCases), Sum(SuspCases), Sum(ConfCases), Sum(Deaths)
		--from T0;		
		----5.2 spread count
		--Declare @tbl_case_spread Table(RepCases int, SuspCases int, ConfCases int, Deaths int);
		--With T0 as (
		--	Select f1.* 
		--	From [surveillance].[Xtbl_Event_Location] as f1, @tbl_locations as f2
		--	where EventId=@EventId and f2.LocalOrIntlSpread=2 and f1.GeonameId=f2.GeonameId
		--	)
		--Insert into @tbl_case_spread(RepCases, SuspCases, ConfCases, Deaths)
		--Select Sum(RepCases), Sum(SuspCases), Sum(ConfCases), Sum(Deaths)
		--from T0;		

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

		--7. users
		--exclude unsubscribedUsers
		Declare @tbl_validUsers table (userId nvarchar(128), UserGeonameId int, Email nvarchar(256),
									UserAdmin1GeonameId int, UserCountryGeonameId int,
									lat decimal(10,5), long decimal(10,5), GridId nvarchar(12),
									IsPaidUser bit, UserProfileLocationName nvarchar(max), AoiGeonameIds varchar(256));
		With T1 as (
			Select UserId From dbo.AspNetUserRoles
			Except
			Select f1.UserId
			From dbo.AspNetUserRoles as f1, dbo.AspNetRoles as f2
			Where f2.Name='UnsubscribedUsers' and f1.RoleId=f2.Id
			),
		T2 as (
			Select distinct f1.UserId, 1 as IsPaidUser
			From dbo.AspNetUserRoles as f1, dbo.AspNetRoles as f2
			Where f2.Name='PaidUsers' and f1.RoleId=f2.Id
			),
		T3 as (
			Select T1.UserId, ISNULL(T2.IsPaidUser, 0) as IsPaidUser
			From T1 Left join T2 on T1.UserId=T2.UserId
			)
		Insert into @tbl_validUsers(userId, UserGeonameId, Email, UserAdmin1GeonameId, 
					UserCountryGeonameId, lat, long, GridId, IsPaidUser, UserProfileLocationName, AoiGeonameIds)
			Select f1.Id, f1.GeonameId, f1.Email, f2.Admin1GeonameId, f2.CountryGeonameId,
				f2.Latitude, f2.Longitude, f1.GridId, T3.IsPaidUser, f1.[Location], f1.AoiGeonameIds
			From dbo.AspNetUsers as f1, place.Geonames as f2, T3
			Where f1.NewOutbreakNotificationEnabled=1 and f1.Id=T3.UserId and f1.GeonameId=f2.GeonameId
		--all user's locations
		Declare @tbl_validUsers_locations table (UserGeonameId int, UserAdmin1GeonameId int, UserCountryGeonameId int)
		Insert into @tbl_validUsers_locations(UserGeonameId, UserAdmin1GeonameId, UserCountryGeonameId)
			Select distinct UserGeonameId, UserAdmin1GeonameId, UserCountryGeonameId
			From @tbl_validUsers

		--8 location of event
		--8.1 local
		Declare @tbl_eventLoc_local table (GeonameId int, lat decimal(10,5), long decimal(10,5), 
								DisplayName nvarchar(500), LocationType int)
		Insert into @tbl_eventLoc_local(GeonameId, lat, long, DisplayName, LocationType)
			Select f1.GeonameId, f2.Latitude, f2.Longitude, f2.DisplayName, f1.LocationType
			From @tbl_locations as f1, place.Geonames as f2
			Where f1.LocalOrIntlSpread=1 and f1.GeonameId=f2.GeonameId
		--8.2 spread
		Declare @tbl_eventLoc_spread table (GeonameId int, lat decimal(10,5), long decimal(10,5), 
								DisplayName nvarchar(500), LocationType int)
		Insert into @tbl_eventLoc_spread(GeonameId, lat, long, DisplayName, LocationType)
			--case 2/4 here
			Select f1.GeonameId, COALESCE(f2.LatPopWeighted, f2.Latitude), 
				COALESCE(f2.LongPopWeighted, f2.Longitude), f2.DisplayName, f1.LocationType
			From @tbl_locations as f1, place.Geonames as f2
			Where f1.LocalOrIntlSpread=2 and f1.GeonameId=f2.GeonameId
		--8.2.2 grids of event (source)
		Declare @tbl_eventGrids_spread table (GridId nvarchar(12), GeonameId int);
		If Exists (Select 1 from @tbl_eventLoc_spread)
			Insert into @tbl_eventGrids_spread(GridId, GeonameId)
				Select f1.GridId, f2.GeonameId
				From bd.HUFFMODEL25KMWORLDHEXAGON as f1, @tbl_eventLoc_spread as f2
				Where f1.SHAPE.STIntersects(GEOGRAPHY::Point(f2.lat, f2.long, 4326)) = 1

		--9 find local users 
		--results local users
		Declare @tbl_userLocal table (userId nvarchar(128), UserGeonameId int, UserGridId nvarchar(12),
					EventLocationNames nvarchar(500), Email nvarchar(256), IsPaidUser bit, 
					UserProfileLocationName nvarchar(max), AoiGeonameIds varchar(256));
		--an interm. table
		Declare @tbl_userLocEventLoc table (UserGeonameId int, EventGeonameId int, EventLocDisplayName nvarchar(500));
		--9.1 user's city/prov/country matches event's local location
		Insert into @tbl_userLocEventLoc(UserGeonameId, EventGeonameId, EventLocDisplayName)
			Select Distinct f1.UserGeonameId, f2.GeonameId as EventGeonameId, f2.DisplayName
			From  @tbl_validUsers_locations as f1, @tbl_eventLoc_local as f2
			Where f1.UserGeonameId=f2.GeonameId or f1.UserAdmin1GeonameId=f2.GeonameId
				or f1.UserCountryGeonameId=f2.GeonameId;
		--cities still need concate locationNames
		With T1 as (
			Select Distinct UserGeonameId,
				stuff(
					(	Select ';'+ST1.EventLocDisplayName
						From @tbl_userLocEventLoc as ST1
						Where ST1.UserGeonameId=ST2.UserGeonameId
						ORDER BY ST1.EventLocDisplayName
						For XML PATH ('')
					), 1,1,'') as EventLocationNames
				From @tbl_userLocEventLoc as ST2
			)
		Insert into @tbl_userLocal(userId, UserGeonameId, Email, EventLocationNames, UserGridId, IsPaidUser, UserProfileLocationName, AoiGeonameIds)
			Select f2.userId, T1.UserGeonameId, f2.Email, T1.EventLocationNames, f2.GridId, f2.IsPaidUser, f2.UserProfileLocationName, f2.AoiGeonameIds
			From T1, @tbl_validUsers as f2
			Where T1.UserGeonameId=f2.UserGeonameId
		--9.2 through city buffer
		Delete from @tbl_userLocEventLoc;
		--9.2.1 user-event geonames
		With T1 as ( --user
			Select distinct UserGeonameId, lat, long
			From @tbl_validUsers
			Where UserGeonameId not in (Select UserGeonameId From @tbl_userLocal)
			)
		Insert into @tbl_userLocEventLoc(UserGeonameId, EventGeonameId, EventLocDisplayName)
			Select T1.UserGeonameId, f2.GeonameId, f2.DisplayName
			From T1, @tbl_eventLoc_local as f2
			Where f2.LocationType=2 AND
				(GEOGRAPHY::Point(f2.lat, f2.long, 4326)).STBuffer(@Distance).STIntersects(GEOGRAPHY::Point(T1.lat, T1.long, 4326)) = 1;
		--9.2.2 Users found through city buffer (may related to multiple cities)
		With T1 as (
			Select Distinct UserGeonameId,
				stuff(
					(	Select ';'+ST1.EventLocDisplayName
						From @tbl_userLocEventLoc as ST1
						Where ST1.UserGeonameId=ST2.UserGeonameId
						ORDER BY ST1.EventLocDisplayName
						For XML PATH ('')
					), 1,1,'') as EventLocationNames
				From @tbl_userLocEventLoc as ST2
			)
		Insert into @tbl_userLocal(userId, UserGeonameId, Email, EventLocationNames, UserGridId, IsPaidUser)
			Select f2.userId, T1.UserGeonameId, f2.Email, T1.EventLocationNames, f2.GridId, f2.IsPaidUser
			From T1, @tbl_validUsers as f2
			Where T1.UserGeonameId=f2.UserGeonameId
		--users remaining for destinations, potential users from spread
		Declare @tbl_validUsersDest table(userId nvarchar(128), UserGeonameId int, Email nvarchar(256),  
								GridId nvarchar(12), IsPaidUser bit, UserProfileLocationName nvarchar(max), AoiGeonameIds varchar(256))
		Insert into @tbl_validUsersDest(userId, UserGeonameId, Email, GridId, IsPaidUser, UserProfileLocationName, AoiGeonameIds)
			Select userId, UserGeonameId, Email, GridId, IsPaidUser, UserProfileLocationName, AoiGeonameIds
			From @tbl_validUsers
			Where UserGeonameId Not in (Select UserGeonameId from @tbl_userLocal)

		--Dates
		Declare @startDate Date, @endDate Date, @endMth int, @endMthDate Date
		Select @startDate=StartDate, @endDate=EndDate from surveillance.[Event] Where EventId=@EventId;
		Set @endMth=MONTH(ISNULL(@endDate, GETUTCDATE()));
		Set @endMthDate=DATEFROMPARTS(2017, @endMth, 1);
		--local event?
		Declare @isLocalOnly bit=(Select [IsLocalOnly] from [surveillance].[Event] where EventId=@EventId)
		
		--10 destination users 
		If @isLocalOnly=0
		Begin --local and global (UserLocationName=UserProfileLocationName)
			Declare @tbl_userDest table (UserId nvarchar(128), Email nvarchar(256), UserLocationName nvarchar(500),
								EventLocationNames nvarchar(4000), DestGridId nvarchar(12),
								IsPaidUser bit, UserProfileLocationName nvarchar(max),
								UserGeonameId int, AoiGeonameIds varchar(256));
				--grid of user in dest
				Declare @tbl_UserDestGrid table (DestGridId nvarchar(12))
				Insert into @tbl_UserDestGrid
					Select distinct f1.GridId
					From @tbl_validUsersDest as f1, [zebra].[EventDestinationGridV3] as f2
					Where f2.EventId=@EventId and f1.GridId=f2.GridId
				--1. find everything except EventLocationName
				Insert into @tbl_userDest(UserId, Email, UserLocationName, UserGeonameId, DestGridId, IsPaidUser, UserProfileLocationName, AoiGeonameIds)
					Select f1.userId, f1.[Email], f1.UserProfileLocationName, f1.UserGeonameId, f1.GridId, f1.IsPaidUser, 
						f1.UserProfileLocationName, f1.AoiGeonameIds
					From @tbl_validUsersDest as f1, @tbl_UserDestGrid as f2
					Where f1.GridId=f2.DestGridId;
				--2. find EventLocationName
				--for performance, use tmp table
				--cross tbl: DestGrid*oriApt
				Declare @tbl_tmpDest1 table (DestGridId nvarchar(12), SourceStationId int)
				--DestGrid * eventLocName
				Declare @tbl_tmpDest2 table (DestGridId nvarchar(12), DisplayName varchar(500));
				--2.1 find Des/user aiport
				With T1 as (
					Select Distinct f1.DestGridId, f2.DestinationStationId
					From @tbl_UserDestGrid as f1, [zebra].[EventDestinationAirport] as f2, 
						[zebra].[GridStation] as f3
					Where f2.EventId=@EventId and
						f1.DestGridId=f3.GridId and f2.DestinationStationId=f3.StationId
						and f3.ValidFromDate=@endMthDate and f3.Probability>0.1
					)
				--2.2 find ori airport
				Insert into @tbl_tmpDest1(DestGridId, SourceStationId)
					Select Distinct T1.DestGridId, f3.SourceStationId
					From T1, [zebra].[StationDestinationAirport] as f2, [zebra].[EventSourceAirport] as f3
						Where f3.EventId=@EventId and f2.ValidFromDate=@endMthDate
							and T1.DestinationStationId=f2.DestinationAirportId
							and f2.StationId=f3.SourceStationId;
				--2.3 find ori grid
				Insert into @tbl_tmpDest2(DestGridId, DisplayName)
				Select Distinct T2.DestGridId, f4.DisplayName
				From @tbl_tmpDest1 as T2, [zebra].[GridStation] as f2, @tbl_eventGrids_spread as f3,
					@tbl_eventLoc_spread as f4
				Where f2.ValidFromDate=@endMthDate
					and T2.SourceStationId=f2.StationId and f2.GridId=f3.GridId
					and f3.GeonameId=f4.GeonameId
				--2.4 fill in EventLocationName
				Update @tbl_userDest set EventLocationNames=f2.EventLocationName
				From @tbl_userDest as f1, (
					Select Distinct DestGridId,
						stuff(
							(	Select ';'+ST1.DisplayName
								From @tbl_tmpDest2 as ST1
								Where ST1.DestGridId=ST2.DestGridId
								ORDER BY ST1.DisplayName
								For XML PATH ('')
							), 1,1,'') as EventLocationName
						From @tbl_tmpDest2 as ST2) as f2
				Where f1.DestGridId=f2.DestGridId;
		
		End --local and global end		

		--10.ProbabilityMax
		Declare @ProbabilityMax decimal(10,3)
		Set @ProbabilityMax=(Select MaxProb From [zebra].[EventDestinationAirport]
				Where EventId=@EventId And DestinationStationId=-1)
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

		--11.final
		If @isLocalOnly=1 --local only
			--local users
			Select f1.DiseaseId, f1.DiseaseName, f5.EventLocationNames as EventLocationName, ISNULL(f3.pathogenTypes, '-') as MicrobeType, 
				ISNULL(transmissionMode, '-') as TransmittedBy, @incubation as IncubationPeriod, 
				ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, f4.Summary as Brief, f4.[LastUpdatedDate],
				ISNULL(@startDate, GETUTCDATE()) as StartDate, ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				-999 as CasesRpted, -999 as CaseSusp, -999 as CaseConf, -999 as Deaths,
				f3.reasons as Reasons, @PriorityTitle as PriorityTitle, f5.UserGeonameId, f5.UserGridId, f5.Email, NULL as UserLocationName, 
				f4.EventTitle, @EventId as EventId, f1.OutbreakPotentialAttributeId, @ProbabilityName as ProbabilityName, 
				f5.IsPaidUser, f5.UserProfileLocationName, f5.userId as UserId, f5.AoiGeonameIds
			From disease.Diseases as f1, @tbl as f3, 
				surveillance.[Event] as f4,
				@tbl_userLocal as f5, [surveillance].[EventPriorities] as f6
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId and f4.PriorityId=f6.PriorityId
		Else --local and global
			--local users
			Select f1.DiseaseId, f1.DiseaseName, f5.EventLocationNames as EventLocationName, 
				ISNULL(f3.pathogenTypes, '-') as MicrobeType, 
				ISNULL(transmissionMode, '-') as TransmittedBy, @incubation as IncubationPeriod, 
				ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate],
				ISNULL(@startDate, GETUTCDATE()) as StartDate, 
				ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				-999 as CasesRpted, -999 as CaseSusp, -999 as CaseConf, -999 as Deaths,
				f3.reasons as Reasons, @PriorityTitle as PriorityTitle, f5.UserGeonameId ,f5.UserGridId, f5.Email, 
				NULL as UserLocationName, f4.EventTitle, @EventId as EventId, f1.OutbreakPotentialAttributeId, 
				@ProbabilityName as ProbabilityName, f5.IsPaidUser, f5.UserProfileLocationName, f5.userId as UserId, f5.AoiGeonameIds
			From disease.Diseases as f1, 
				@tbl as f3, surveillance.[Event] as f4,
				@tbl_userLocal as f5
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId
			Union -- destination users
			Select f1.DiseaseId, f1.DiseaseName, f5.EventLocationNames as EventLocationName, 
				ISNULL(f3.pathogenTypes, '-') as MicrobeType, 
				ISNULL(transmissionMode, '-') as TransmittedBy, @incubation as IncubationPeriod, 
				ISNULL(f3.vaccination, 'Behavioural Only') as Vaccination, 
				f4.Summary as Brief, f4.[LastUpdatedDate],
				ISNULL(@startDate, GETUTCDATE()) as StartDate, 
				ISNULL(@endDate, DATEFROMPARTS(0001, 1, 1)) as EndDate, 
				-999 as CasesRpted, -999 as CaseSusp, -999 as CaseConf, -999 as Deaths,
				f3.reasons as Reasons, @PriorityTitle as PriorityTitle, f5.UserGeonameId,
				f5.DestGridId as UserGridId, f5.Email, f5.UserLocationName, f4.EventTitle, 
				@EventId as EventId, f1.OutbreakPotentialAttributeId,
				@ProbabilityName as ProbabilityName, f5.IsPaidUser, f5.UserProfileLocationName, f5.UserId, f5.AoiGeonameIds
			From disease.Diseases as f1, @tbl as f3, 
				surveillance.[Event] as f4,
				@tbl_userDest as f5
			Where f1.DiseaseId=@diseaseId and f4.EventId=@EventId
	End
	Else
	Begin
		Select 0 as DiseaseId, '-' DiseaseName, '-' as EventLocationName, '-' as MicrobeType, 
			'-' as TransmittedBy, '-' as IncubationPeriod, 
			'-' as Vaccination, '-' as Brief, GETDATE() as LastUpdatedDate, 
			GETDATE() as StartDate, GETDATE() as EndDate, 
			-999 as CasesRpted, -999 as CaseSusp, -999 as CaseConf, -999 as Deaths, 
			'-' as Reasons, 'negligible' as PriorityTitle, 0 UserGeonameId,  '-' as UserGridId, 
			'-' as Email, '-' as UserLocationName, '-' as EventTitle, @EventId as EventId,
			0 as OutbreakPotentialAttributeId, 'Negligible' as ProbabilityName, CAST(0 AS BIT) as IsPaidUser,
			'-' as UserProfileLocationName, '-' as UserId, '-' as AoiGeonameIds
	End
END