
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09
-- Description:	Output: List of event information w/o disease
--				Simple version of usp_GetZebraEventsInfo
-- 2018-10: Added HasOutlookReport
-- 2018-11: Added filters
-- 2018-12: Added total probability and expectedTravellers and prob cate
-- 2019-04: Added importation risks of V7 (empty when @GeonameIds is empty)
-- 2019-04: Added UserId and to improve performance 
--1. Pass UserId = 1122, GeonameId = '' -> Get Global events
--2. Pass UserId = 1122, GeonameId = '7788, 9900' -> Get profile's AOI = '3344, 5566' -> Check if '7788, 9900' is the same as profile's AOI -> Not the same -> get dynamic events and risk value of '7788, 9900'
--3. Pass UserId = 1122, GeonameId = '3344' -> Get profile's AOI = '3344, 5566' -> Check if '3344' is the same as profile's AOI -> Not the same -> get dynamic events and risk value of '3344'
--4. Pass UserId = 1122, GeonameId = '3344, 5566' -> Get profile's AOI = '3344, 5566' -> Check if '3344, 5566' is the same as profile's AOI -> Yes the same -> get prepopulated events and risk value of '3344, 5566'
--5. Pass UserId = 1122, GeonameId = '5566, 3344 -> Get profile's AOI = '3344, 5566' -> Check if '5566, 3344,' is the same as profile's AOI -> Yes the same -> get prepopulated events and risk value of '3344, 5566'
-- 2019-07 name changed
-- 2019-08 added @LocationOnly, we have the following:
-- 34 measles case globally, 9 of them have risk when AOI is Toronto
-- If the query has no AOI set regardless of the LocationOnly flag, then all 34 will be returned with no risk of importation information (current behaviour for global view)
-- If LocationOnly flag is true, and AOI as Toronto, then only those 9 will be returned (current behaviour)
-- If LocationOnly flag is false, and AOI as Toronto, then all 34 will be returned with the risk of importation on the remaining 25 being zeros.
-- 2019-09: disease schema change
-- 2019-11: remove HasOutlookReport
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetEventSummary
	@UserId AS NVARCHAR(128),            --Id
	@GeonameIds AS VARCHAR(MAX),            --Ids CSV
	@DiseasesIds AS VARCHAR(MAX),           --Ids CSV
    @TransmissionModesIds AS VARCHAR(MAX),  --Ids CSV
    @InterventionMethods AS VARCHAR(MAX),     --InterventionDisplayName's CSV
    @SeverityRisks AS VARCHAR(MAX),         --SeverityLevelDisplayName's CSV high/low
    @BiosecurityRisks AS VARCHAR(MAX),       --BiosecurityRiskCode's CSV 
	@LocationOnly AS BIT
AS
BEGIN

	SET NOCOUNT ON
	Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
	--1. filter by locations (with all local and spread events) 
	Declare @tbl_EventsFromGeonameIds table (EventId int, IsLocal int)
	If @GeonameIds<>N'' --only need EndDate IS NULL events from ufn_GetEventsByGeonames
		Insert into @tbl_EventsFromGeonameIds(EventId, IsLocal)
			Select EventId, IsLocal From [bd].ufn_GetEventsByGeonames(@GeonameIds, @Distance, 1)
	Else --need to fill in with something
		Insert into @tbl_EventsFromGeonameIds(EventId)
			Select -1
	
	--2. filter by DieaseIds
	Declare @tbl_DiseaseIds table (DiseaseId int)
	Declare @hasDiseaseFilter bit=0
	--no disease filter
	If (@DiseasesIds='' AND @TransmissionModesIds='' 
		AND @InterventionMethods='' AND @SeverityRisks='' AND @BiosecurityRisks='')
		Insert into @tbl_DiseaseIds
			Select -1
	Else --has disease filter
	Begin
		Insert into @tbl_DiseaseIds
			Select DiseasesId From zebra.ufn_GetDiseasesFromFilterInfo(@DiseasesIds, 
					@TransmissionModesIds, @InterventionMethods, @SeverityRisks, @BiosecurityRisks);
		Set @hasDiseaseFilter=1
	End

	--3. Concat transmissions
	Declare @tbl_transmissions table (DiseaseId int, Transmissions varchar(500));
	With ST2 as (
		Select f1.DiseaseId, f2.DisplayName
		From [disease].[Xtbl_Disease_TransmissionMode] as f1, 
			[disease].[TransmissionModes] as f2, @tbl_DiseaseIds as f3
		Where (@hasDiseaseFilter=0 OR f1.DiseaseId=f3.DiseaseId)
			AND f1.SpeciesId=1 AND f1.TransmissionModeId=f2.TransmissionModeId
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
		Select Distinct f1.DiseaseId, f2.DisplayName as Prevension
		From disease.Xtbl_Disease_Interventions as f1, [disease].Interventions as f2
		Where f1.SpeciesId=1 and f1.InterventionId=f2.InterventionId
		Union
		Select DiseaseId, 'Behavioural' as Prevension
		From disease.Diseases Where DiseaseId 
					Not in (Select DiseaseId From disease.Xtbl_Disease_Interventions Where SpeciesId=1)
		),
	ST2 as (
		Select T1.*
		From T1, @tbl_DiseaseIds as f1
			Where @hasDiseaseFilter=0 OR T1.DiseaseId=f1.DiseaseId
		)
	Insert into @tbl_interventions(DiseaseId, Interventions)
		Select Distinct DiseaseId,
				stuff(
				(	Select ', '+ ST1.Prevension
					From ST2 as ST1
					Where ST1.DiseaseId=ST2.DiseaseId
					ORDER BY ST1.Prevension
					For XML PATH ('')
				), 1,2,'') as Interventions
				From ST2;

	--5. all events (IsLocal from ufn_GetEventsByGeonames)
	Declare @tbl_events table (EventId int, RankId int, RepCases int, Deaths int, 
								IsLocal int, ImportationMaxProbability decimal(5,4), 
								ImportationMinProbability decimal(5,4), LocalSpread int,
								ImpMinVolume decimal(10,3), ImpMaxVolume decimal(10,3), IsOtherEvent bit)
	Declare @tbl_otherEvents table (EventId int)
	--no filtering items on disease
	If @hasDiseaseFilter=0
		--no location filter
		If @GeonameIds=N'' 
			Insert into @tbl_events(EventId)
				Select EventId
				From surveillance.Event
				Where EndDate IS NULL
		Else --has location filter
		Begin
			Insert into @tbl_events(EventId, IsLocal)
				Select f1.EventId, f1.IsLocal
				From @tbl_EventsFromGeonameIds as f1, surveillance.Event as f2
				Where f2.EndDate IS NULL and f1.EventId=f2.EventId
			If @LocationOnly=0 --needs other events
				Insert into @tbl_otherEvents
					Select EventId
					From surveillance.Event
					Where EndDate IS NULL and EventId Not In (Select EventId From @tbl_events)
		End
	--has filtering items (join @tbl_DiseaseIds)
	Else
		--no location filter
		If @GeonameIds=N''
			Insert into @tbl_events(EventId)
				Select f1.EventId
				From surveillance.Event AS f1,@tbl_DiseaseIds as f3
				Where f1.DiseaseId=f3.DiseaseId and f1.EndDate IS NULL;
		Else --has location filter
		Begin
			Insert into @tbl_events(EventId, IsLocal)
				Select f1.EventId, f2.IsLocal
				From surveillance.Event AS f1, @tbl_EventsFromGeonameIds as f2, @tbl_DiseaseIds as f3
				Where f1.EndDate IS NULL and f1.EventId=f2.EventId and f1.DiseaseId=f3.DiseaseId;
			If @LocationOnly=0 --needs other events
				Insert into @tbl_otherEvents
					Select f1.EventId
					From surveillance.Event AS f1,@tbl_DiseaseIds as f3
					Where f1.DiseaseId=f3.DiseaseId and f1.EndDate IS NULL 
						and f1.EventId Not In (Select EventId From @tbl_events)
		End;
	--adds other events
	Insert into @tbl_events(EventId, IsOtherEvent)
		Select EventId, 1
		From @tbl_otherEvents;

	--6. cases
	--6.1 need an id for loop
	With T1 as (
		select [EventId], ROW_NUMBER() OVER ( order by [EventId]) as RankId
		from @tbl_events
		)
	Update @tbl_events Set RankId=T1.RankId
		From @tbl_events as f1, T1
		Where f1.[EventId]=T1.[EventId]
	--6.2 loop to get cases
	Declare @i int=1, @thisEventId int, @IsLocal int
	Declare @maxRankId int=(Select Max(RankId) From @tbl_events)
	
	While @i<=@maxRankId
	Begin
		select @thisEventId=[EventId], @IsLocal=IsLocal 
		from @tbl_events where RankId=@i;

		With T1 as (
			Select RepCases, Deaths
			From bd.ufn_TotalCaseCountByEventId(@thisEventId, 0)
			)
		Update @tbl_events Set RepCases=T1.RepCases, Deaths=T1.Deaths
			From @tbl_events as f1, T1
			Where f1.EventId=@thisEventId

		set @i=@i+1
	End;

	--7 ImportationMaxProbability/Min
	If @GeonameIds<>N''
	Begin --7
		-- to get importation risks
		Declare @tbl_imp table (LocalSpread int, MinProbability decimal(5,4),  
					MaxProbability decimal(5,4), 
					MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3))
		--7.1 user locations
		Declare @tbl_UserGeonameIds table (GeonameId int)
		Insert into @tbl_UserGeonameIds
			Select item
			From [bd].[ufn_StringSplit](@GeonameIds, ',')
		--7.2 User AOI, which can't be empty
		Declare @UserAoiGeonameIds varchar(max)=(Select AoiGeonameIds From dbo.AspNetUsers Where Id = @UserId)
		Declare @tbl_UserAoiGeonameIds table (GeonameId int)
		Insert into @tbl_UserAoiGeonameIds
			Select item
			From [bd].[ufn_StringSplit](@UserAoiGeonameIds, ',')
		--7.3 compare input geonameIds and user profile AOI
		Declare @tbl_compare table (InputGeonameId int, AoiGeonameId int)
		Insert into @tbl_compare(InputGeonameId, AoiGeonameId)
			Select f1.GeonameId, f2.GeonameId
			From @tbl_UserGeonameIds as f1 FULL OUTER JOIN @tbl_UserAoiGeonameIds as f2
			On f1.GeonameId=f2.GeonameId
		--Not the same, use user input GeonameIds resume
		If exists (Select 1 from @tbl_compare Where InputGeonameId is NULL or AoiGeonameId is NULL)
		Begin --1
			Set @i=1
			While @i<=@maxRankId
			Begin --2
				select @thisEventId=[EventId], @IsLocal=IsLocal 
				from @tbl_events where RankId=@i;
				--importation risks
				If @IsLocal=0 --not local
				Begin --3
					--gets all info from risk SP
					Insert into @tbl_imp
						EXEC [zebra].usp_ZebraGetImportationRiskSpreadOnly @thisEventId, @GeonameIds
					--keep the necessary one
					Update @tbl_events Set ImportationMaxProbability=MaxProbability,
											ImportationMinProbability=MinProbability,
											ImpMaxVolume=MaxExpTravelers,
											ImpMinVolume=MinExpTravelers,
											IsLocal=0
						From (Select top 1 MaxProbability, MinProbability, 
								MaxExpTravelers, MinExpTravelers, LocalSpread From @tbl_imp
							) as f1
						Where EventId=@thisEventId
					Delete from @tbl_imp
				End --3
				Else If @IsLocal=1 --local
					Update @tbl_events 
					Set ImportationMaxProbability=0, ImportationMinProbability=0, 
						ImpMaxVolume=0, ImpMinVolume=0, IsLocal=1
					Where EventId=@thisEventId

				set @i=@i+1
			End --2
		End --1
		--same, use preprocessed AOI
		Else 
		Begin --4
			Set @i=1
			While @i<=@maxRankId
			Begin --5
				select @thisEventId=[EventId], @IsLocal=IsLocal 
				from @tbl_events where RankId=@i;
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
			End --5
		End --4
	Update @tbl_events 
		Set ImportationMaxProbability=0, ImportationMinProbability=0, ImpMaxVolume=0, ImpMinVolume=0
		Where ImportationMaxProbability IS NULL
	End --7
	--leave all events (no @GeonameIds) as NULL

	--to improve performance
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
		f6.DiseaseId, f6.DiseaseName, f6.OutbreakPotentialAttributeId, f6.BiosecurityRisk, 
		f7.Transmissions, f8.Interventions, f1.RepCases, f1.Deaths,
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