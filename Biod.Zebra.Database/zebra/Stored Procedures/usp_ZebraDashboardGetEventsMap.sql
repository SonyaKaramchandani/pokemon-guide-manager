
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Initial map of dashboard (V3)
--				(V3)
-- 17Sept2018: Added CountryShapeAsText
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDashboardGetEventsMap
AS
BEGIN
	SET NOCOUNT ON;
	WITH T1 as (
		Select Distinct EL.EventId, EL.GeonameId
		From [surveillance].[Xtbl_Event_Location] EL INNER JOIN
		surveillance.Event E ON E.EventId = EL.EventId
		WHERE E.EndDate IS NULL
	),
	T2 as (
	SELECT f1.CountryGeonameId, Count(Distinct T1.EventId) as NumOfEvents
	FROM [place].[ActiveGeonames] as f1, T1
	WHERE f1.GeonameId=T1.GeonameId
	GROUP BY CountryGeonameId
	)
	Select T2.CountryGeonameId, f1.DisplayName as CountryName, NumOfEvents,
		f1.Shape.STAsText() as CountryPoint--, f3.shape as CountryShape
	From [place].[ActiveGeonames] as f1, T2--, [place].[CountryProvinceShapes] as f3
	Where f1.GeonameId=T2.CountryGeonameId and f1.[LocationType]=6 --and f3.[LocationType]=6
		--and T2.CountryGeonameId=f3.GeonameId

END