CREATE PROCEDURE [zebra].[usp_ZebraGetUserWeeklyEmailData]
(
  @UserId nvarchar(128)
)
AS
BEGIN
	SET NOCOUNT ON;
	--1. User: needs: 1-AOI displayname concated, 2-Email, 3-paided
	Declare @tbl_validUsers table (UserId nvarchar(128), UserAoiGeonameIds varchar(max), Email nvarchar(256),
								IsPaidUser bit, DoNotTrackEnabled bit, SeqId int, UserAoiLocationNames nvarchar(2000), 
								UserAoiGeonameIdsLastWeek varchar(max), AoiChanged bit, IsNewUser bit);
	Declare @tbl_UserGeonames table (UserId nvarchar(128), GeonameId int, DisplayName varchar(200));
	Declare @tbl_UserGeonamesLastWeek table (UserId nvarchar(128), GeonameId int);
	--1.1 valid user only, paid or not paid
	Insert into @tbl_validUsers(UserId, UserAoiGeonameIds, Email, IsPaidUser, DoNotTrackEnabled, AoiChanged, IsNewUser)
		Select f1.Id, f1.AoiGeonameIds, f1.Email, T3.IsPaidUser, DoNotTrackEnabled, 0, 0
		From dbo.AspNetUsers as f1, zebra.ufn_GetUsersPaidStatus() as T3
		Where f1.Id = @UserId and f1.WeeklyOutbreakNotificationEnabled=1 and f1.Id=T3.UserId;
	--add UserAoiGeonameIdsLastWeek
	Update @tbl_validUsers Set UserAoiGeonameIdsLastWeek=f2.AoiGeonameIds
		From @tbl_validUsers as f1, zebra.UserAois_history as f2
		Where f1.UserId=f2.UserId;
	--need seq to loop
	With T1 as (
		select UserId, ROW_NUMBER() OVER ( order by UserId) as RankId
		from @tbl_validUsers
		)
	Update @tbl_validUsers Set SeqId=T1.RankId
		From @tbl_validUsers as f1, T1
		Where f1.UserId=T1.UserId
	--1.2 expand to userXgeonames
	Declare @i int=1
	Declare @maxSeqId_User int =(Select Max(SeqId) From @tbl_validUsers)
	Declare @thisAoi varchar(256), @lastAoi varchar(256)
	While @i<=@maxSeqId_User
	Begin
		--now
		Set @thisAoi=(Select UserAoiGeonameIds From @tbl_validUsers Where SeqId=@i)
		Insert into @tbl_UserGeonames(UserId, GeonameId)
			Select f1.UserId, f2.item
			From @tbl_validUsers as f1, [bd].[ufn_StringSplit](@thisAoi, ',') as f2
			Where SeqId=@i
		--last week
		Set @lastAoi=(Select UserAoiGeonameIdsLastWeek From @tbl_validUsers Where SeqId=@i)
		--existing users
		If @lastAoi IS NOT NULL
			Insert into @tbl_UserGeonamesLastWeek(UserId, GeonameId)
				Select f1.UserId, f2.item
				From @tbl_validUsers as f1, [bd].[ufn_StringSplit](@lastAoi, ',') as f2
				Where SeqId=@i
		Else --new users
			Update @tbl_validUsers Set IsNewUser=1 Where SeqId=@i
		Set @i=@i+1
	End
	--1.3 add displayname
	Update @tbl_UserGeonames Set DisplayName=f2.DisplayName
		From @tbl_UserGeonames as f1, [place].[ActiveGeonames] as f2
		Where f1.GeonameId=f2.GeonameId
	--1.4 concat displayname
	Update @tbl_validUsers Set UserAoiLocationNames=f2.UserAoiLocationNames
		From @tbl_validUsers as f1, 
			(Select distinct UserId, stuff(
					(	Select '; '+ST1.DisplayName
						From @tbl_UserGeonames as ST1
						Where ST1.UserId=ST2.UserId
						ORDER BY ST1.DisplayName
						For XML PATH ('')
					), 1,2,'') as UserAoiLocationNames
				From @tbl_UserGeonames as ST2) as f2
		Where f1.UserId=f2.UserId;
	--1.5 find users whose aois have changed
	Declare @tbl_UserGeonamesOldNew table (UserId nvarchar(128), GeonameId int, GeonameIdLastWeek int)
	Insert into @tbl_UserGeonamesOldNew(UserId, GeonameId, GeonameIdLastWeek)
		Select f1.UserId, f1.GeonameId, f2.GeonameId
		From @tbl_UserGeonames as f1 Full Join @tbl_UserGeonamesLastWeek as f2
		On f1.UserId=f2.UserId and f1.GeonameId=f2.GeonameId;
	--save in @tbl_validUsers
	Update @tbl_validUsers Set AoiChanged=1
		Where UserId in (Select UserId From @tbl_UserGeonamesOldNew
						Where GeonameId Is Null Or GeonameIdLastWeek Is Null);

	--2. Events, active only
	Declare @tbl_events table (EventId int, RankId int, RepCases int, EventTitle varchar(200),
							IsNewEvent bit, DeltaNewRepCases int, DeltaNewDeaths int, IsLocalOnly bit); 
	--2.1 total rep cases needed for local events order
	--2.1.1 need an id for loop
	Insert into @tbl_events(EventId, RankId, IsNewEvent, EventTitle, IsLocalOnly)
		select [EventId], ROW_NUMBER() OVER ( order by [EventId]) as RankId,
			Case When DATEDIFF(d, CreatedDate, GETUTCDATE())>6 Then 0 Else 1 End,
			EventTitle, IsLocalOnly
		from [surveillance].[Event]
		Where EndDate IS NULL
	--2.1.2 loop
	Declare @thisEventId int
	Declare @maxSeqId_Event int=(Select Max(RankId) From @tbl_events)
	Set @i=1
	--loop event
	While @i<=@maxSeqId_Event
	Begin
		select @thisEventId=[EventId] from @tbl_events where RankId=@i;
		--2.2.1 rep cases
		Update @tbl_events Set RepCases=T1.RepCases
			From @tbl_events as f1, bd.ufn_TotalCaseCountByEventId(@thisEventId, 0) as T1
			Where f1.EventId=@thisEventId
		set @i=@i+1
	End; 
	--2.3  delta cases, deaths
	With T1 as (
		Select f1.EventId, f1.GeonameId, 
			f1.RepCases-ISNULL(f2.RepCases, 0) as DeltaNewRepCases,
			f1.Deaths-ISNULL(f2.Deaths, 0) as DeltaNewDeaths
		From surveillance.Xtbl_Event_Location as f1 
			left join surveillance.Xtbl_Event_Location_history as f2
		On f1.EventId=f2.EventId And f1.GeonameId=f2.GeonameId
		Where f2.EventDateType=2
		),
	T2 as (
		Select EventId, SUM(DeltaNewRepCases) as SumDeltaNewRepCases From T1 
		Where DeltaNewRepCases>0 Group by EventId
		),
	T3 as (
		Select EventId, SUM(DeltaNewDeaths) as SumDeltaNewDeaths From T1 
		Where DeltaNewDeaths>0 Group by EventId
		)
	Update @tbl_events 
		Set DeltaNewRepCases=T2.SumDeltaNewRepCases, DeltaNewDeaths=T3.SumDeltaNewDeaths
		From @tbl_events as f1
		left join T2 on f1.EventId=T2.EventId
		left join T3 on f1.EventId=T3.EventId

	--4. prepare relevanceType
	Declare @tbl_relevance table (UserId nvarchar(128), EventId int, RelevanceId int, DiseaseId int)
	--4.1 full list
	Insert into @tbl_relevance(UserId, EventId, DiseaseId)
	Select f1.UserId, f2.EventId, f3.DiseaseId
		From @tbl_validUsers as f1, @tbl_events as f2, [surveillance].[Event] as f3
		Where f2.EventId=f3.EventId
	--relevance from user setting
	Update @tbl_relevance Set RelevanceId=f2.RelevanceId
		From @tbl_relevance as f1, [zebra].[Xtbl_User_Disease_Relevance] as f2
		Where f1.DiseaseId=f2.DiseaseId and f1.UserId=f2.UserId
	--mising Relevance to use role's Relevance
	Update @tbl_relevance Set RelevanceId=f3.RelevanceId
		From @tbl_relevance as f1, [dbo].[AspNetUserRoles] as f2, [zebra].[Xtbl_Role_Disease_Relevance] as f3
		Where f1.RelevanceId is NULL and f1.UserId=f2.UserId and f2.RoleId=f3.RoleId and f1.DiseaseId=f3.DiseaseId
	
	--5. Output with user by event
	Select f2.UserId, f2.Email, f2.IsPaidUser, f2.DoNotTrackEnabled, f2.UserAoiLocationNames, 
		f3.EventId, f3.EventTitle, f3.IsNewEvent, f3.RepCases, 
		ISNULL(f3.DeltaNewRepCases,0) as DeltaNewRepCases, ISNULL(f3.DeltaNewDeaths,0) as DeltaNewDeaths,
		f1.LocalSpread, ISNULL(f5.RelevanceId, 2) as RelevanceId, f3.IsLocalOnly
	From @tbl_validUsers as f2 
		cross Join @tbl_events as f3  
		Left Join zebra.EventImportationRisksByUser as f1 On f1.UserId=f2.UserId and f1.EventId=f3.EventId
		Left Join @tbl_relevance as f5 on f2.UserId=f5.UserId and f3.EventId=f5.EventId
END
