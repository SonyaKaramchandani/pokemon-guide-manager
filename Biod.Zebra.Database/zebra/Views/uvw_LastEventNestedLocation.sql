CREATE VIEW [surveillance].[uvw_LastEventNestedLocation]
AS
WITH NumberedLocations as (
SELECT *,	ROW_NUMBER() OVER (PARTITION BY EventId, GeonameId ORDER BY EventDate desc) AS RowNumber
FROM [surveillance].[EventNestedLocation]
)
SELECT [EventId]
      ,[GeonameId]
      ,[EventDate]
      ,[SuspCases]
      ,[ConfCases]
      ,[RepCases]
      ,[Deaths]
      ,[NewSuspCases]
      ,[NewConfCases]
      ,[NewRepCases]
      ,[NewDeaths]
FROM NumberedLocations
WHERE RowNumber = 1
