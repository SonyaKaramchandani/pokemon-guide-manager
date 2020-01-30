
-- =============================================
-- Author:		Vivian
-- Create date: 2019-08 
-- Description:	Output a list of event information w/o disease
-- Behave same as #4 in usp_ZebraEventGetEventSummary (@GeonameIds=AOI)
-- when relevance=1 same as when LocationOnly flag is false
-- when relevance=2 same as when LocationOnly flag is true
-- 2019-09: disease schema change
-- 2019-11: remove HasOutlookReport
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEventGetCustomEventSummary
	  @UserId AS NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON

	Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
	--1. User setting
	--1.1 User's relevance
	Declare @tbl_relevance table (DiseaseId int, RelevanceId int)
	--all diseases from active events
	Insert into @tbl_relevance(DiseaseId)
		Select Distinct DiseaseId
		From surveillance.[Event]
		Where EndDate IS NULL
	--relevance from user settings
	Update @tbl_relevance Set RelevanceId=f2.RelevanceId
	From @tbl_relevance as f1, [zebra].[Xtbl_User_Disease_Relevance] as f2
	Where f1.DiseaseId=f2.DiseaseId and f2.UserId=@UserId
	--mising Relevance to use role's Relevance
	Update @tbl_relevance Set RelevanceId=f3.RelevanceId
		From @tbl_relevance as f1, [dbo].[AspNetUserRoles] as f2, [zebra].[Xtbl_Role_Disease_Relevance] as f3
		Where f1.RelevanceId is NULL and f2.UserId=@UserId and f2.RoleId=f3.RoleId and f1.DiseaseId=f3.DiseaseId
	--1.2 user AOI locations
	Declare @UserAoiGeonameIds varchar(max)=(Select AoiGeonameIds From dbo.AspNetUsers Where Id = @UserId)
	Declare @tbl_UserAoiGeonameIds table (GeonameId int)
	Insert into @tbl_UserAoiGeonameIds
		Select item
		From [bd].[ufn_StringSplit](@UserAoiGeonameIds, ',')
	--1.2.2 find out if each event is local or dest by AOI
	Declare @tbl_EventsFromGeonameIds table (EventId int, IsLocal int)
	Insert into @tbl_EventsFromGeonameIds(EventId, IsLocal)
		Select EventId, IsLocal 
		From [bd].ufn_GetEventsByGeonames(@UserAoiGeonameIds, @Distance, 1)
	
	--2. DieaseIds of active events w/o relevance=3
	Declare @tbl_DiseaseIds table (DiseaseId int)
	--remove any relevance=3
	Insert into @tbl_DiseaseIds
		Select DiseaseId 
		From @tbl_relevance Where RelevanceId<>3 OR RelevanceId IS NULL

	--3. Concat transmissions
	Declare @tbl_transmissions table (DiseaseId int, Transmissions varchar(500));
	With ST2 as (
		Select distinct f1.DiseaseId, f2.DisplayName
		From [disease].[Xtbl_Disease_TransmissionMode] as f1, 
			[disease].[TransmissionModes] as f2, @tbl_DiseaseIds as f3
		Where f1.SpeciesId=1 and
			f1.DiseaseId=f3.DiseaseId AND f1.TransmissionModeId=f2.TransmissionModeId
		)
	Insert into @tbl_transmissions(DiseaseId, Transmissions)
		Select Distinct DiseaseId,
				stuff(
				(	Select ', '+ST1.DisplayName
					From ST2 as ST1
					Where ST1.DiseaseId=ST2.DiseaseId
					ORDER BY ST1.DisplayName
					For XML PATH ('')
				), 1,2,'') as Transmissions
				From ST2;
	--solve &amp; issue
	Update @tbl_transmissions set Transmissions=REPLACE(Transmissions, '&amp;', '&');

	--4. Concat Interventions
	Declare @tbl_interventions table (DiseaseId int, Interventions varchar(500));
	With T1 as (
		Select Distinct f1.DiseaseId, f2.DisplayName as Intervention
		From disease.Xtbl_Disease_Interventions as f1, [disease].[Interventions] as f2
		Where f1.SpeciesId=1 and f1.InterventionId=f2.InterventionId
		Union
		Select DiseaseId, 'Behavioural' as Intervention
		From disease.Diseases Where DiseaseId 
					Not in (Select DiseaseId From disease.Xtbl_Disease_Interventions where SpeciesId=1)
		),
	ST2 as (
		Select T1.*
		From T1, @tbl_DiseaseIds as f1
			Where T1.DiseaseId=f1.DiseaseId
		)
	Insert into @tbl_interventions(DiseaseId, Interventions)
		Select Distinct DiseaseId,
				stuff(
				(	Select ', '+ ST1.Intervention
					From ST2 as ST1
					Where ST1.DiseaseId=ST2.DiseaseId
					ORDER BY ST1.Intervention
					For XML PATH ('')
				), 1,2,'') as Interventions
				From ST2;

	--5. all events (IsLocal from ufn_GetEventsByGeonames)
	Declare @tbl_events table (EventId int, RankId int, RepCases int, Deaths int, 
								IsLocal int, ImportationMaxProbability decimal(5,4), 
								ImportationMinProbability decimal(5,4), LocalSpread int,
								ImpMinVolume decimal(10,3), ImpMaxVolume decimal(10,3), IsOtherEvent bit)
	Insert into @tbl_events(EventId, IsLocal)
		Select f1.EventId, f2.IsLocal
		From surveillance.Event AS f1, @tbl_EventsFromGeonameIds as f2, @tbl_DiseaseIds as f3
		Where f1.EndDate IS NULL and f1.EventId=f2.EventId and f1.DiseaseId=f3.DiseaseId;
	--when relevance=1, add those disease related events back
	Declare @tbl_otherEvents table (EventId int)
	Insert into @tbl_otherEvents
		Select f1.EventId
		From surveillance.Event AS f1, @tbl_relevance as f2
		Where f2.RelevanceId=1 and f1.DiseaseId=f2.DiseaseId and f1.EndDate IS NULL 
			and f1.EventId Not In (Select EventId From @tbl_events)
	--adds other events
	Insert into @tbl_events(EventId, IsOtherEvent)
		Select EventId, 1
		From @tbl_otherEvents;

	--6. cases/risk values
	--6.1 need an id for loop
	With T1 as (
		select [EventId], ROW_NUMBER() OVER ( order by [EventId]) as RankId
		from @tbl_events
		)
	Update @tbl_events Set RankId=T1.RankId
		From @tbl_events as f1, T1
		Where f1.[EventId]=T1.[EventId]
	--6.2 loop to get cases and risk values
	Declare @i int=1, @thisEventId int, @IsLocal int
	Declare @maxRankId int=(Select Max(RankId) From @tbl_events)
	
	While @i<=@maxRankId
	Begin
		select @thisEventId=[EventId], @IsLocal=IsLocal
		from @tbl_events where RankId=@i;
		--case
		With T1 as (
			Select RepCases, Deaths
			From bd.ufn_TotalCaseCountByEventId(@thisEventId, 0)
			)
		Update @tbl_events Set RepCases=T1.RepCases, Deaths=T1.Deaths
			From @tbl_events as f1, T1
			Where f1.EventId=@thisEventId
		--risk values
		--not local
		If @IsLocal=0 
			Update @tbl_events Set ImportationMaxProbability=MaxProb,
									ImportationMinProbability=MinProb,
									ImpMaxVolume=MaxVolume,
									ImpMinVolume=MinVolume,
									IsLocal=0
				From (Select top 1 MaxProb, MinProb, LocalSpread, MaxVolume, MinVolume
						From zebra.EventImportationRisksByUser
						Where UserId=@UserId and EventId=@thisEventId
					) as f1
				Where EventId=@thisEventId
		--local
		Else If @IsLocal=1 
			Update @tbl_events 
				Set ImportationMaxProbability=0, ImportationMinProbability=0, 
					ImpMaxVolume=0, ImpMinVolume=0, IsLocal=1
				Where EventId=@thisEventId
		set @i=@i+1
	End;
	--6.3 missing risk values all set to 0
	Update @tbl_events 
		Set ImportationMaxProbability=0, ImportationMinProbability=0, ImpMaxVolume=0, ImpMinVolume=0
		Where ImportationMaxProbability IS NULL

	--7. to improve performance
	Declare @tbl_countries table (GeonameId int, CountryCentroidAsText nvarchar(max))
	Insert into @tbl_countries(GeonameId, CountryCentroidAsText)
		Select GeonameId, Shape.STAsText()
		From [place].[ActiveGeonames]
		where LocationType=6;

	--8. output
	WITH T1 AS (
		SELECT f2.*, f1.GeonameId, ROW_NUMBER() OVER (PARTITION BY f1.EventId ORDER BY f1.GeonameId DESC) AS rn
		FROM surveillance.Xtbl_Event_Location as f1, @tbl_events as f2
		Where f1.EventId=f2.EventId
		), --used to look for it's country
	T2 AS (
		SELECT *
		FROM T1 WHERE rn=1
		)
	SELECT 
		f1.EventId, f2.EventTitle, ISNULL(f2.StartDate, '1900-01-01') AS StartDate, ISNULL(f2.EndDate, '2900-01-01') AS EndDate, 
		f2.LastUpdatedDate, f3.CountryName, f4.CountryCentroidAsText,
		Case When f9.[MaxExportationProbabilityViaAirports] IS NULL Or f9.[MaxExportationProbabilityViaAirports]<0.01 Then 'negligible'
			When f9.[MaxExportationProbabilityViaAirports]<0.2 And f9.[MaxExportationProbabilityViaAirports]>=0.01 Then 'low'
			When f9.[MaxExportationProbabilityViaAirports]>0.7 Then 'high'
			Else 'medium'
		End as ExportationPriorityTitle, 
		f2.Summary, f2.Notes, f2.IsLocalOnly,
		f6.DiseaseId, f6.DiseaseName, f6.OutbreakPotentialAttributeId, f6.BiosecurityRisk, f7.Transmissions, f8.Interventions, 
		f1.RepCases, f1.Deaths,
		Case When f9.[MaxExportationProbabilityViaAirports] IS NULL Or f9.[MaxExportationProbabilityViaAirports]<0.01 Then 'Negligible'
			When f9.[MaxExportationProbabilityViaAirports]<0.2 And f9.[MaxExportationProbabilityViaAirports]>=0.01 Then 'Low probability'
			When f9.[MaxExportationProbabilityViaAirports]>0.7 Then 'High probability'
			Else 'Medium probability'
		End as ExportationProbabilityName,
		f9.[MinExportationProbabilityViaAirports] as ExportationProbabilityMin, f9.[MaxExportationProbabilityViaAirports] as ExportationProbabilityMax,
		f9.[MinExportationVolumeViaAirports] as ExportationInfectedTravellersMin, f9.[MaxExportationVolumeViaAirports] as ExportationInfectedTravellersMax,
		Case When f1.IsOtherEvent=1 Then 0 Else f1.ImportationMaxProbability End as ImportationMaxProbability,
		Case When f1.IsOtherEvent=1 Then 0 Else f1.ImportationMinProbability End as ImportationMinProbability,
		Case When f1.IsOtherEvent=1 Then 0 Else f1.ImpMaxVolume End as ImportationInfectedTravellersMax,
		Case When f1.IsOtherEvent=1 Then 0 Else f1.ImpMinVolume End as ImportationInfectedTravellersMin,
		f1.IsLocal as LocalSpread
	FROM T2 as f1 INNER JOIN surveillance.Event as f2 ON f1.EventId=f2.EventId 
		INNER JOIN [place].[ActiveGeonames] as f3 ON f1.GeonameId=f3.GeonameId
		INNER JOIN @tbl_countries as f4 ON f3.CountryGeonameId=f4.GeonameId
		LEFT JOIN disease.Diseases as f6 ON f2.DiseaseId=f6.DiseaseId 
		LEFT JOIN @tbl_transmissions as f7 ON f2.DiseaseId=f7.DiseaseId 
		LEFT JOIN @tbl_interventions as f8 ON f6.DiseaseId=f8.DiseaseId
		LEFT JOIN [zebra].[EventExtension] as f9 ON f1.EventId=f9.EventId

END
