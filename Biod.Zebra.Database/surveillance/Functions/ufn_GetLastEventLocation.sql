-- ==========================================================================================
-- Epic:            PROXPAR
-- JIRA:            PT-1379 (https://bluedotglobal.atlassian.net/browse/PT-1379)
-- Description:     Gets the latest event locations that is up to and including the end date.
--
-- NOTE: THIS IS A TEMPORARY STOPGAP SOLUTION. THIS FUNCTION IS NOT NEEDED WHEN WE SWITCH TO
--       THE NEW PRODUCTS DATABASE.
-- ==========================================================================================

create function [surveillance].[ufn_GetLastEventLocation](@endDate date)
    returns table
        as
        return
        with NumberedLocations as (
            select el.*,
                   ROW_NUMBER() over (partition by el.EventId, el.GeonameId order by el.EventDate desc) as RowNumber
            from [surveillance].[EventLocation] el
                     join [surveillance].[Event] e on e.EventId = el.EventId and (e.EndDate is null or e.EndDate >= el.EventDate)
            where el.EventDate <= @endDate
        )
        select [EventId],
               [GeonameId],
               [EventDate],
               [SuspCases],
               [ConfCases],
               [RepCases],
               [Deaths]
        from NumberedLocations
        where RowNumber = 1
