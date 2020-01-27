
-- =============================================
-- Author:		Basam Ahmad
-- Create date: 2018-05 
-- Description:	Output: List of event information
--				For Zebra external API, ZebraEventsInfo_Get
-- 2019-07 name changed
-- 2019-09: disease schema change
-- 2019-11: incubation string calls ufn_FormStringFromSeconds
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraApiGetEvents
AS
BEGIN
	--incubation
	Declare @tbl_incubation table(EventId int, minIncubation bigint, maxIncubation bigint, 
				avgIncubation bigint, minStr varchar(20), maxStr varchar(20), avgStr varchar(20))
	--insert incubation
	Insert into @tbl_incubation (EventId, minIncubation, maxIncubation, avgIncubation)
	Select f1.[EventId], f2.IncubationMinimumSeconds, f2.IncubationMaximumSeconds, f2.IncubationAverageSeconds
		From surveillance.Event as f1, [disease].DiseaseSpeciesIncubation as f2
	Where f2.SpeciesId=1 and f1.DiseaseId=f2.DiseaseId
	--insert incubation strings
	Update @tbl_incubation Set minStr=bd.ufn_FormStringFromSeconds(minIncubation),
			maxStr=bd.ufn_FormStringFromSeconds(maxIncubation),
			avgStr=bd.ufn_FormStringFromSeconds(avgIncubation);
	--output
	SELECT  
		E.EventId, E.EventTitle, ISNULL(E.StartDate, '1900-01-01') AS StartDate, 
		ISNULL(E.EndDate, '1900-01-01') AS EndDate, E.LastUpdatedDate, 
		EP.PriorityTitle, E.Summary, E.Notes, D.DiseaseName, f1.avgIncubation as IncubationAverageSeconds, 
		f1.minIncubation as IncubationMinimumSeconds, f1.maxIncubation as IncubationMaximumSecondss, 
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