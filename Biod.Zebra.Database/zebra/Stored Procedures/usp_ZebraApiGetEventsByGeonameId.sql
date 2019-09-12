
-- =============================================
-- Author:		Vivian
-- Create date: 2018-06 
-- Description:	Input: GeonameId and date range
--				Output: List of published, Ongoing event information
-- 2019-07 name changed
-- 2019-09: disease schema change
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraApiGetEventsByGeonameId
	@GeonameId AS INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  
		E.EventId, E.EventTitle, E.StartDate, 'Ongoing' AS EndDate, E.LastUpdatedDate, 
		EP.PriorityTitle, E.Summary, E.Notes, D.DiseaseName, f1.IncubationAverageDays, 
		f1.IncubationMinimumDays, f1.IncubationMaximumDays, D.IsChronic, D.TreatmentAvailable, D.SeverityLevel, 
		TM.TransmissionMode, E.CreatedDate, 
		EL.GeonameId, EL.EventDate, EL.SuspCases, EL.ConfCases, EL.RepCases, EL.Deaths,
		G.DisplayName AS LocationDisplayName, G.Population, 
		COALESCE(G.LatPopWeighted, G.Latitude) as Latitude,
		COALESCE(G.LongPopWeighted, G.Longitude) as Longitude, 
		G.CountryName
	FROM surveillance.Event AS E INNER JOIN
		surveillance.Xtbl_Event_Location AS EL ON E.EventId = EL.EventId INNER JOIN
		place.Geonames AS G ON EL.GeonameId = G.GeonameId INNER JOIN
		surveillance.EventPriorities AS EP ON E.PriorityId = EP.PriorityId INNER JOIN
		disease.Diseases D ON E.DiseaseId = D.DiseaseId INNER JOIN
		[disease].DiseaseSpeciesIncubation as f1 ON D.DiseaseId=f1.DiseaseId LEFT JOIN
		disease.Xtbl_Disease_TransmissionMode AS DTM ON E.DiseaseId = DTM.DiseaseId LEFT JOIN
		disease.TransmissionModes AS TM ON DTM.TransmissionModeId = TM.TransmissionModeId
	WHERE f1.SpeciesId=1 and DTM.SpeciesId=1 and
		E.IsPublished = 1 AND E.EndDate IS Null AND G.GeonameId = @GeonameId
END