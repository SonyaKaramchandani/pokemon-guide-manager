
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2018-05 
-- Description:	Input: GeonameId (from a user)
--				Output: List of event information by GeonameId with SHAPE
--				For Zebra external API, ZebraEventOrigin_Get
-- Modification: 2018-08(Vivian) Removed STIntersects and EndDate IS NULL
-- Modification 9Aug2018(Vivian): Output: List of event information for both local and destination
--				Algorithm same as usp_GetZebraEventInfoByEventId
-- Modification 16Sept2018(Vivian): Added V3
-- Modification 7Nov2018(Vivian): V5 (didn't use function because it only applies to one event)
-- 18Dec2018: Removed version
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraApiGetEventByGeonameId
	@GeonameId    AS INT, --user's location
	@Distance int=100000 -- 100km
AS
BEGIN
	SET NOCOUNT ON;
		--Find result events through geonameId
		Declare @tbl_resultEvents table (EventId int, RankId int)
		Insert into @tbl_resultEvents (EventId)
			Select EventId From [bd].ufn_GetEventsByGeonames(@GeonameId, @Distance, 0);

		If Exists (Select 1 from @tbl_resultEvents)
		BEGIN

			--add sequence
			With T1 as (
				select [EventId], ROW_NUMBER() OVER ( order by [EventId]) as RankId
				from @tbl_resultEvents
				)
			Update @tbl_resultEvents Set RankId=T1.RankId
			From @tbl_resultEvents as f1, T1
			Where f1.[EventId]=T1.[EventId]

			--reasons
			Declare @tbl_reasons table (EventId int, Reasons varchar(500));
			With ST1 as (
				Select f1.EventId, f3.ReasonName
				From @tbl_resultEvents as f1, surveillance.Xtbl_Event_Reason as f2, 
					surveillance.EventCreationReasons as f3
				Where f1.EventId=f2.EventId and f2.ReasonId=f3.ReasonId
				)
			Insert into @tbl_reasons (EventId, Reasons)
				Select distinct ST2.EventId, 
					substring(
						(	Select ', '+ST1.ReasonName  AS [text()]
							From  ST1
							Where ST1.EventId = ST2.EventId
							ORDER BY ST1.EventId
							For XML PATH ('')
						), 3, 500) airportCode
				from ST1 as ST2;

			--case count
			Declare @tbl_casesCount table (EventId int, RepCases int, ConfCases int, Deaths int, SuspCases int);
			Declare @i int=1
			Declare @i_max int =(Select MAX(RankId) From @tbl_resultEvents)
			Declare @thisEventId int
			While @i<=@i_max
			Begin
				Set @thisEventId=(select [EventId] from @tbl_resultEvents where RankId=@i)

				Insert into @tbl_casesCount(EventId, RepCases, ConfCases, Deaths, SuspCases)
					Select @thisEventId, RepCases, ConfCases, Deaths, SuspCases
					From bd.ufn_TotalCaseCountByEventId(@thisEventId, 0)

				set @i=@i+1
			End;

			--output
			With T1 as (
				Select Distinct f1.EventId, f1.GeonameId, f3.DisplayName
				From [surveillance].[Xtbl_Event_Location] as f1, @tbl_resultEvents as f2, [place].[ActiveGeonames] as f3
				Where f1.EventId=f2.EventId and f1.GeonameId=f3.GeonameId
				),
			T2 as (
				Select Distinct EventId,
					stuff(
						(	Select '; '+ST1.DisplayName
							From T1 as ST1
							Where ST1.EventId=ST2.EventId
							ORDER BY ST1.DisplayName
							For XML PATH ('')
						), 1,1,'') as LocationName
					From T1 as ST2
				)
			Select E.EventId, D.DiseaseId, D.DiseaseName, E.EventTitle, E.StartDate, E.EndDate, 
				E.Summary, EP.PriorityTitle, EL.ConfCases, SuspCases, RepCases, Deaths,
				T2.LocationName, E.LastUpdatedDate, RS.Reasons, 
				Case When f2.MaxProb IS NULL Or f2.MaxProb<0.01 Then 'Negligible'
					When f2.MaxProb<0.2 And f2.MaxProb>=0.01 Then 'Low probability'
					When f2.MaxProb>0.7 Then 'High probability'
					Else 'Medium probability'
				End as ProbabilityName
			From @tbl_resultEvents as f1 INNER JOIN surveillance.Event AS E ON f1.EventId=E.EventId
				INNER JOIN T2 ON f1.EventId=T2.EventId
				LEFT JOIN @tbl_reasons AS RS ON f1.EventId=RS.EventId
				LEFT JOIN disease.Diseases AS D ON D.DiseaseId = E.DiseaseId
				LEFT JOIN surveillance.EventPriorities AS EP ON E.PriorityId = EP.PriorityId
				LEFT JOIN @tbl_casesCount AS EL ON f1.EventId = EL.EventId
				LEFT JOIN (Select * from [zebra].[EventDestinationAirport] Where DestinationStationId=-1)
					as f2 ON f1.EventId=f2.EventId
			ORDER BY E.EventId, E.StartDate
		END --1
		ELSE
			Select 0 EventId, 0 DiseaseId, '-' DiseaseName, '-' EventTitle, GETDATE() StartDate, GETDATE() EndDate, 
				'-' Summary, 'negligible' PriorityTitle, 0 ConfCases, 
				0 SuspCases, 0 RepCases, 0 Deaths,
				'-' LocationName, GETDATE() LastUpdatedDate, '-' Reasons, 
				'Negligible' as ProbabilityName
END