
-- =============================================
-- Author:		Vivian
-- Create date: 2019-10 
-- Description:	Output details of an event, format same as usp_ZebraEventGetCustomEventSummary
-- When @GeonameIds is empty use, user's aoi
-- 2019-11: remove HasOutlookReport
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEventGetEventSummaryByEventId
	@UserId AS NVARCHAR(128),
	@GeonameIds AS VARCHAR(MAX),            --Ids CSV
	@EventId AS INT --pass it only when EndDate IS NULL
AS
BEGIN
	SET NOCOUNT ON

	Declare @DiseaseId int=(Select DiseaseId From surveillance.Event Where EventId=@EventId)
	--1. Concat transmissions
	Declare @Transmissions varchar(500)='-';
	With ST2 as (
		Select distinct f2.DisplayName
		From [disease].[Xtbl_Disease_TransmissionMode] as f1, 
			[disease].[TransmissionModes] as f2
		Where f1.SpeciesId=1 and f1.DiseaseId=@DiseaseId 
			and f1.TransmissionModeId=f2.TransmissionModeId
		)
	select @Transmissions=
			stuff(
				(	Select ', '+ST1.DisplayName
					From ST2 as ST1
					ORDER BY ST1.DisplayName
					For XML PATH ('')
				), 1,2,'') 
	--solve &amp; issue
	set @Transmissions=REPLACE(@Transmissions, '&amp;', '&');

	--2. Concat Interventions
	Declare @Interventions varchar(500);
	With T1 as (
		Select Distinct f2.DisplayName
		From disease.Xtbl_Disease_Interventions as f1, [disease].[Interventions] as f2
		Where f1.SpeciesId=1 and f1.DiseaseId=@DiseaseId
			and f1.InterventionId=f2.InterventionId
		Union
		Select 'Behavioural' as Intervention
		Where @DiseaseId Not in 
			(Select DiseaseId From disease.Xtbl_Disease_Interventions where SpeciesId=1)
		)
	select @Interventions=
			stuff(
				(	Select ', '+T1.DisplayName
					From T1
					ORDER BY T1.DisplayName
					For XML PATH ('')
				), 1,2,'') 

	--3. case count
	Declare @RepCases int, @Deaths int
	Select @RepCases=RepCases, @Deaths=Deaths
	From bd.ufn_TotalCaseCountByEventId(@EventId, 0)
		
	--4. Country
	Declare @tbl_country table (CountryName nvarchar(64), CountryCentroidAsText nvarchar(max));
	With T1 as (
		Select Top 1 f2.GeonameId, f2.CountryGeonameId, f2.CountryName
		From [surveillance].[Xtbl_Event_Location] as f1, [place].[ActiveGeonames] as f2
		Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId
		)
	Insert into @tbl_country(CountryName, CountryCentroidAsText)
		Select T1.CountryName, f1.Shape.STAsText()
		From T1, [place].[ActiveGeonames] as f1
		where f1.LocationType=6 and T1.CountryGeonameId=f1.GeonameId;

	--5. output
	--A. no geonames input, means all importation risks are nulls
	If @GeonameIds=N''
		SELECT 
			f1.EventId, f1.EventTitle, ISNULL(f1.StartDate, '1900-01-01') AS StartDate, ISNULL(f1.EndDate, '2900-01-01') AS EndDate, 
			f1.LastUpdatedDate, f2.CountryName, f2.CountryCentroidAsText,
			Case When f4.[MaxExportationProbabilityViaAirports] IS NULL Or f4.[MaxExportationProbabilityViaAirports]<0.01 Then 'negligible'
				When f4.[MaxExportationProbabilityViaAirports]<0.2 And f4.[MaxExportationProbabilityViaAirports]>=0.01 Then 'low'
				When f4.[MaxExportationProbabilityViaAirports]>0.7 Then 'high'
				Else 'medium'
			End as ExportationPriorityTitle, 
			f1.Summary, f1.Notes, f1.IsLocalOnly,
			f3.DiseaseId, f3.DiseaseName, f3.OutbreakPotentialAttributeId, f3.BiosecurityRisk, 
			@Transmissions AS Transmissions, @Interventions AS Interventions, @RepCases AS RepCases, @Deaths AS Deaths,
			Case When f4.[MaxExportationProbabilityViaAirports] IS NULL Or f4.[MaxExportationProbabilityViaAirports]<0.01 Then 'Negligible'
				When f4.[MaxExportationProbabilityViaAirports]<0.2 And f4.[MaxExportationProbabilityViaAirports]>=0.01 Then 'Low probability'
				When f4.[MaxExportationProbabilityViaAirports]>0.7 Then 'High probability'
				Else 'Medium probability'
			End as ExportationProbabilityName,
			f4.[MinExportationProbabilityViaAirports] as ExportationProbabilityMin, f4.[MaxExportationProbabilityViaAirports] as ExportationProbabilityMax,
			f4.[MinExportationVolumeViaAirports] as ExportationInfectedTravellersMin, f4.[MaxExportationVolumeViaAirports] as ExportationInfectedTravellersMax,
			NULL as ImportationMaxProbability, 
			NULL as ImportationMinProbability,
			NULL as ImportationInfectedTravellersMax, 
			NULL as ImportationInfectedTravellersMin,
			NULL as LocalSpread
		FROM (Select * from surveillance.Event WHERE EventId=@EventId) as f1  
			INNER JOIN @tbl_country as f2 ON 1=1 
			INNER JOIN disease.Diseases as f3 ON f3.DiseaseId=f1.DiseaseId 
			LEFT JOIN [zebra].[EventExtension] as f4 ON f4.EventId=f1.EventId
	Else --B. has geonames input
	Begin --B
		Declare @useUserAoi bit=1
		If @GeonameIds<>N''
		Begin --5
			--5.1 user loc from input
			Declare @tbl_UserGeonameIds table (GeonameId int)
			Insert into @tbl_UserGeonameIds
				Select item
				From [bd].[ufn_StringSplit](@GeonameIds, ',')
			--5.2 user loc from aoi
			Declare @UserAoiGeonameIds varchar(256)=(Select AoiGeonameIds From dbo.AspNetUsers Where Id = @UserId)
			Declare @tbl_UserAoiGeonameIds table (GeonameId int)
			Insert into @tbl_UserAoiGeonameIds
				Select item
				From [bd].[ufn_StringSplit](@UserAoiGeonameIds, ',')
			--5.3 compare input geonameIds and user profile AOI
			Declare @tbl_compare table (InputGeonameId int, AoiGeonameId int)
			Insert into @tbl_compare(InputGeonameId, AoiGeonameId)
				Select f1.GeonameId, f2.GeonameId
				From @tbl_UserGeonameIds as f1 FULL OUTER JOIN @tbl_UserAoiGeonameIds as f2
				On f1.GeonameId=f2.GeonameId
			--5.4 Not the same
			If exists (Select 1 from @tbl_compare Where InputGeonameId is NULL or AoiGeonameId is NULL)
				Set @useUserAoi=0
		End --5

		--6. output
		If @useUserAoi=1 --user user aoi
			SELECT 
				f1.EventId, f1.EventTitle, ISNULL(f1.StartDate, '1900-01-01') AS StartDate, ISNULL(f1.EndDate, '2900-01-01') AS EndDate, 
				f1.LastUpdatedDate, f2.CountryName, f2.CountryCentroidAsText,
				Case When f4.[MaxExportationProbabilityViaAirports] IS NULL Or f4.[MaxExportationProbabilityViaAirports]<0.01 Then 'negligible'
					When f4.[MaxExportationProbabilityViaAirports]<0.2 And f4.[MaxExportationProbabilityViaAirports]>=0.01 Then 'low'
					When f4.[MaxExportationProbabilityViaAirports]>0.7 Then 'high'
					Else 'medium'
				End as ExportationPriorityTitle, 
				f1.Summary, f1.Notes, f1.IsLocalOnly,
				f3.DiseaseId, f3.DiseaseName, f3.OutbreakPotentialAttributeId, f3.BiosecurityRisk, 
				@Transmissions AS Transmissions, @Interventions AS Interventions, @RepCases AS RepCases, @Deaths AS Deaths,
				Case When f4.[MaxExportationProbabilityViaAirports] IS NULL Or f4.[MaxExportationProbabilityViaAirports]<0.01 Then 'Negligible'
					When f4.[MaxExportationProbabilityViaAirports]<0.2 And f4.[MaxExportationProbabilityViaAirports]>=0.01 Then 'Low probability'
					When f4.[MaxExportationProbabilityViaAirports]>0.7 Then 'High probability'
					Else 'Medium probability'
				End as ExportationProbabilityName,
				f4.[MinExportationProbabilityViaAirports] as ExportationProbabilityMin, f4.[MaxExportationProbabilityViaAirports] as ExportationProbabilityMax,
				f4.[MinExportationVolumeViaAirports] as ExportationInfectedTravellersMin, f4.[MaxExportationVolumeViaAirports] as ExportationInfectedTravellersMax,
				f5.MaxProb as ImportationMaxProbability, f5.[MinProb] as ImportationMinProbability,
				f5.[MaxVolume] as ImportationInfectedTravellersMax, f5.[MinVolume] as ImportationInfectedTravellersMin,
				f5.LocalSpread as LocalSpread
			FROM (Select * from surveillance.Event WHERE EventId=@EventId) as f1  
				INNER JOIN @tbl_country as f2 ON 1=1 
				INNER JOIN disease.Diseases as f3 ON f3.DiseaseId=f1.DiseaseId
				LEFT JOIN [zebra].[EventExtension] as f4 ON f4.EventId=f1.EventId
				LEFT JOIN [zebra].[EventImportationRisksByUser] as f5 ON f5.EventId=f1.EventId and f5.UserId=@UserId
		Else --use user input
		Begin--6
			--local event?
			Declare @localSpread bit
			Set @localSpread=zebra.ufn_GetEventStatusByGeonames (@EventId, @GeonameIds)
			-- save importation risks
			Declare @tbl_importation table (LocalSpread int, MinProbability decimal(5,4),  
						MaxProbability decimal(5,4), 
						MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3))
			--non-local, calculate importation risk
			If @localSpread=0
				--gets all info from risk SP
				Insert into @tbl_importation
					EXEC [zebra].usp_ZebraGetImportationRiskSpreadOnly @EventId, @GeonameIds
			Else --local event
				Insert into @tbl_importation
					values(1, 0, 0, 0, 0)
			--output
			SELECT 
				f1.EventId, f1.EventTitle, ISNULL(f1.StartDate, '1900-01-01') AS StartDate, ISNULL(f1.EndDate, '2900-01-01') AS EndDate, 
				f1.LastUpdatedDate, f2.CountryName, f2.CountryCentroidAsText,
				Case When f4.[MaxExportationProbabilityViaAirports] IS NULL Or f4.[MaxExportationProbabilityViaAirports]<0.01 Then 'negligible'
					When f4.[MaxExportationProbabilityViaAirports]<0.2 And f4.[MaxExportationProbabilityViaAirports]>=0.01 Then 'low'
					When f4.[MaxExportationProbabilityViaAirports]>0.7 Then 'high'
					Else 'medium'
				End as ExportationPriorityTitle, 
				f1.Summary, f1.Notes, f1.IsLocalOnly,
				f3.DiseaseId, f3.DiseaseName, f3.OutbreakPotentialAttributeId, f3.BiosecurityRisk, 
				@Transmissions AS Transmissions, @Interventions AS Interventions, @RepCases AS RepCases, @Deaths AS Deaths,
				Case When f4.[MaxExportationProbabilityViaAirports] IS NULL Or f4.[MaxExportationProbabilityViaAirports]<0.01 Then 'Negligible'
					When f4.[MaxExportationProbabilityViaAirports]<0.2 And f4.[MaxExportationProbabilityViaAirports]>=0.01 Then 'Low probability'
					When f4.[MaxExportationProbabilityViaAirports]>0.7 Then 'High probability'
					Else 'Medium probability'
				End as ExportationProbabilityName,
				f4.[MinExportationProbabilityViaAirports] as ExportationProbabilityMin, f4.[MaxExportationProbabilityViaAirports] as ExportationProbabilityMax,
				f4.[MinExportationVolumeViaAirports] as ExportationInfectedTravellersMin, f4.[MaxExportationVolumeViaAirports] as ExportationInfectedTravellersMax,
				f5.MaxProbability as ImportationMaxProbability, 
				f5.MinProbability as ImportationMinProbability,
				f5.MaxExpTravelers as ImportationInfectedTravellersMax, 
				f5.MinExpTravelers as ImportationInfectedTravellersMin,
				f5.LocalSpread as LocalSpread
			FROM (Select * from surveillance.Event WHERE EventId=@EventId) as f1  
				INNER JOIN @tbl_country as f2 ON 1=1 
				INNER JOIN disease.Diseases as f3 ON f3.DiseaseId=f1.DiseaseId 
				LEFT JOIN [zebra].[EventExtension] as f4 ON f4.EventId=f1.EventId
				LEFT JOIN @tbl_importation as f5 ON 1=1
		End--6
	End --B


END
