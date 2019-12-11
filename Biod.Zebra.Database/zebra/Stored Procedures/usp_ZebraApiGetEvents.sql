
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2018-05 
-- Description:	Output: List of event information
--				For Zebra external API, ZebraEventsInfo_Get
-- 2019-07 name changed
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraApiGetEvents
AS
BEGIN
	--incubation
	Declare @tbl_incubation table(EventId int, minIncubation decimal(10,2), maxIncubation decimal(10,2), 
				avgIncubation decimal(10,2), minStr varchar(10), maxStr varchar(10), avgStr varchar(10))
	--insert incubation
	Insert into @tbl_incubation (EventId, minIncubation, maxIncubation, avgIncubation)
	Select f1.[EventId], f2.IncubationMinimumDays, f2.IncubationMaximumDays, f2.IncubationAverageDays
		From surveillance.Event as f1, [disease].DiseaseSpeciesIncubation as f2
	Where f2.SpeciesId=1 and f1.DiseaseId=f2.DiseaseId
	--set unit
	declare @d char(1)='d'
	declare @h char(1)='h'
	--insert incubation strings
	Update @tbl_incubation Set minStr=Case When minIncubation IS NULL Then '-'
				When minIncubation<1 Then CONCAT(CONVERT(INT, ROUND(minIncubation*24, 0)), @h)
				Else CONCAT(CONVERT(INT, ROUND(minIncubation, 0)), @d)
				End,
			maxStr=Case When maxIncubation IS NULL Then '-'
				When maxIncubation<1 Then CONCAT(CONVERT(INT, ROUND(maxIncubation*24, 0)), @h)
				Else CONCAT(CONVERT(INT, ROUND(maxIncubation, 0)), @d)
				End,
			avgStr=Case When avgIncubation IS NULL Then '-'
				When avgIncubation<1 Then CONCAT(CONVERT(INT, ROUND(avgIncubation*24, 0)), @h)
				Else CONCAT(CONVERT(INT, ROUND(avgIncubation, 0)), @d)
				End;
	--output
	SELECT  
		E.EventId, E.EventTitle, ISNULL(E.StartDate, '1900-01-01') AS StartDate, 
		ISNULL(E.EndDate, '1900-01-01') AS EndDate, E.LastUpdatedDate, 
		EP.PriorityTitle, E.Summary, E.Notes, D.DiseaseName, f1.avgIncubation as IncubationAverageDays, 
		f1.minIncubation as IncubationMinimumDays, f1.maxIncubation as IncubationMaximumDays, 
		D.IsChronic, D.TreatmentAvailable, D.SeverityLevel, 
		TM.TransmissionMode, E.CreatedDate, 
		EL.GeonameId, EL.EventDate, EL.SuspCases, EL.ConfCases, EL.RepCases, EL.Deaths,
		G.DisplayName AS LocationDisplayName, G.Population, 		
		COALESCE(G.LatPopWeighted, G.Latitude) as Latitude,
		COALESCE(G.LongPopWeighted, G.Longitude) as Longitude, 
		G.CountryName,
		G.Shape.STAsText() as LocationPoint, G2.Shape.STAsText() as CountryPoint, 
		Case WHEN G.LocationType=2 THEN 'City' 
			WHEN G.LocationType=4 THEN 'Province'
			WHEN G.LocationType=6 THEN 'Country'
		End as LocationType,
		Case When minStr='-' and maxStr='-' and avgStr='-' Then '-'
			Else CONCAT(minStr, ' to ', maxStr, ' (', avgStr, ' avg.)')
		End as Incubation
	FROM surveillance.Event AS E INNER JOIN
		surveillance.Xtbl_Event_Location AS EL ON E.EventId = EL.EventId INNER JOIN
		[place].[ActiveGeonames] AS G ON EL.GeonameId = G.GeonameId INNER JOIN
        [place].[ActiveGeonames] AS G2 ON G.CountryGeonameId = G2.GeonameId INNER JOIN
		surveillance.EventPriorities AS EP ON E.PriorityId = EP.PriorityId INNER JOIN
		@tbl_incubation AS f1 ON E.EventId = f1.EventId INNER JOIN
		disease.Diseases D ON E.DiseaseId = D.DiseaseId LEFT JOIN
		disease.Xtbl_Disease_TransmissionMode AS DTM ON E.DiseaseId = DTM.DiseaseId LEFT JOIN
		disease.TransmissionModes AS TM ON DTM.TransmissionModeId = TM.TransmissionModeId
	WHERE DTM.SpeciesId=1
END