-- ==========================================================================================
-- Epic:            PROXPAR
-- JIRA:            PT-1379 (https://bluedotglobal.atlassian.net/browse/PT-1379)
-- Description:     Gets the Nested Event Locations up to and including the end date. Both
--                  parameters are optional, but providing them to filter will speed up the
--                  execution.
--
-- NOTE: THIS IS A TEMPORARY STOPGAP SOLUTION. THIS FUNCTION IS NOT NEEDED WHEN WE SWITCH TO
--       THE NEW PRODUCTS DATABASE.
-- ==========================================================================================

create function [surveillance].[ufn_GetLastEventNestedLocation](@endDate date, @diseaseId int = null, @eventId int = null)
    returns @returnResults table
                           (
                               [EventId]   int,
                               [GeonameId] int,
                               [EventDate] date,
                               [SuspCases] int,
                               [ConfCases] int,
                               [RepCases]  int,
                               [Deaths]    int
                           )
as
begin
    -- =====================================================================================================
    -- A: LATEST EVENT LOCATIONS
    -- Populate our table with the latest event locations using the @endDate, filtered by input parameters
    -- =====================================================================================================
    declare @eventLocations table
                            (
                                [EventId]      int,
                                [GeonameId]    int,
                                [LocationType] int,
                                [EventDate]    date,
                                [SuspCases]    int,
                                [ConfCases]    int,
                                [RepCases]     int,
                                [Deaths]       int
                            );

    insert @eventLocations
    select el.[EventId],
           el.[GeonameId],
           g.LocationType,
           el.[EventDate],
           el.[SuspCases],
           el.[ConfCases],
           el.[RepCases],
           el.[Deaths]
    from [surveillance].[ufn_GetLastEventLocation](@endDate) el
             join [surveillance].[Event] e on e.EventId = el.EventId
             join [place].[Geonames] g on g.GeonameId = el.GeonameId
    where (@diseaseId is null or e.DiseaseId = @diseaseId)
      and (@eventId is null or e.EventId = @eventId);


    -- =====================================================================================================
    -- B: CITIES (LocationType = 2)
    -- Since cities are at the bottom of the geoname hierarchy, they do not have nesting applied. we simply
    -- need to make sure that RepCases is the correct number with max(R,C+S,D) logic.
    -- =====================================================================================================

    -- Apply max(R,C+S,D) logic on city cases
    update @eventLocations
    set RepCases = (select max(v) from (values (RepCases), (ConfCases + SuspCases), (Deaths)) as value(v))
    where LocationType = 2;


    -- =====================================================================================================
    -- C: PROVINCES (LocationType = 4)
    -- Provinces need to apply nesting. But first we must make sure our data set has all the provinces:
    --     1. Fill our table with missing provinces (i.e. when only cities reported)
    --     2. Apply max(R,C+S,D) logic so all rows have valid RepCases
    -- 
    -- With the data complete with the set of provinces we now have to apply nesting:
    --     1. Create a table with summed cases of cities for each province
    --     2. Join with the reported province cases to compare the reported cases side by side
    --     3. Update with the higher of the two: Sum of Children vs Province Reported
    --     4. Apply max(R,C+S,D) logic to normalize the provinces so that RepCases is once again highest
    -- =====================================================================================================

    -- Create province entries by summing cases from the city entries if the province entries are not in the table
    insert into @eventLocations
    select EventId,
           g.Admin1GeonameId,
           4,
           max(EventDate),
           sum(SuspCases),
           sum(ConfCases),
           sum(RepCases),
           sum(Deaths)
    from @eventLocations eln
             join [place].[Geonames] g on g.GeonameId = eln.GeonameId
    where g.Admin1GeonameId not in (select GeonameId from @eventLocations)
      and g.Admin1GeonameId is not null
      and g.LocationType = 2
    group by EventId, g.Admin1GeonameId;

    -- Apply max(R,C+S,D) logic on province cases
    update @eventLocations
    set RepCases = (select max(v) from (values (RepCases), (ConfCases + SuspCases), (Deaths)) as value(v))
    where LocationType = 4;

    -- Apply nesting logic: take max of either province cases or sum of city cases
    with SummedCityLocations as (
        select EventId,
               g.Admin1GeonameId as GeonameId,
               max(EventDate)    as EventDate,
               sum(SuspCases)    as SuspCases,
               sum(ConfCases)    as ConfCases,
               sum(RepCases)     as RepCases,
               sum(Deaths)       as Deaths
        from @eventLocations el
                 join [place].[Geonames] g on el.GeonameId = g.GeonameId and el.LocationType = 2
        group by EventId, g.Admin1GeonameId
    )
    update @eventLocations
    set SuspCases = iif(scl.SuspCases > el.SuspCases, scl.SuspCases, el.SuspCases),
        ConfCases = iif(scl.ConfCases > el.ConfCases, scl.ConfCases, el.ConfCases),
        RepCases  = iif(scl.RepCases > el.RepCases, scl.RepCases, el.RepCases),
        Deaths    = iif(scl.Deaths > el.Deaths, scl.Deaths, el.Deaths)
    from SummedCityLocations scl
             join @eventLocations el on el.EventId = scl.EventId and el.GeonameId = scl.GeonameId and el.EventDate = scl.EventDate
             join place.Geonames g on g.GeonameId = scl.GeonameId and g.LocationType = 4;

    -- Apply max(R,C+S,D) logic on province cases after nesting
    update @eventLocations
    set RepCases = (select max(v) from (values (RepCases), (ConfCases + SuspCases), (Deaths)) as value(v))
    where LocationType = 4;


    -- =====================================================================================================
    -- D: COUNTRIES (LocationType = 6)
    -- Countries need to apply nesting. But first we must make sure our data set has all the countries.
    --     1. Fill our table with missing countries, this includes:
    --        - Cities that have no provinces (i.e. Vatican City/Singapore), and countries are not reported
    --        - Provinces that have no countries reported
    --     2. Apply max(R,C+S,D) logic so all rows have valid RepCases
    -- 
    -- With the data complete with the set of countries we now have to apply nesting.
    --     1. Create a table with summed cases of provinces for each country
    --     2. Join with the reported country cases to compare the reported cases side by side
    --     3. Update with the higher of the two: Sum of Children vs Country Reported
    --     4. Apply max(R,C+S,D) logic to normalize the countries so that RepCases is once again highest
    -- =====================================================================================================

    -- Create country entries by summing cases from the province entries OR if cities that have no province, if the country entries are not in the table
    insert into @eventLocations
    select EventId,
           g.CountryGeonameId,
           6,
           max(EventDate),
           sum(SuspCases),
           sum(ConfCases),
           sum(RepCases),
           sum(Deaths)
    from @eventLocations eln
             join [place].[Geonames] g on g.GeonameId = eln.GeonameId
    where g.CountryGeonameId not in (select GeonameId from @eventLocations)
      and (g.Admin1GeonameId is null or eln.LocationType = 4)
    group by EventId, g.CountryGeonameId;

    -- Apply max(R,C+S,D) logic on country cases
    update @eventLocations
    set RepCases = (select max(v) from (values (RepCases), (ConfCases + SuspCases), (Deaths)) as value(v))
    where LocationType = 6;

    -- Apply nesting logic: take max of either country cases or sum of province cases
    with SummedProvinceLocations as (
        select EventId,
               g.CountryGeonameId as GeonameId,
               max(EventDate)     as EventDate,
               sum(SuspCases)     as SuspCases,
               sum(ConfCases)     as ConfCases,
               sum(RepCases)      as RepCases,
               sum(Deaths)        as Deaths
        from @eventLocations el
                 join [place].[Geonames] g on el.GeonameId = g.GeonameId and el.LocationType = 4
        group by EventId, g.CountryGeonameId
    )
    update @eventLocations
    set SuspCases = iif(scl.SuspCases > el.SuspCases, scl.SuspCases, el.SuspCases),
        ConfCases = iif(scl.ConfCases > el.ConfCases, scl.ConfCases, el.ConfCases),
        RepCases  = iif(scl.RepCases > el.RepCases, scl.RepCases, el.RepCases),
        Deaths    = iif(scl.Deaths > el.Deaths, scl.Deaths, el.Deaths)
    from SummedProvinceLocations scl
             join @eventLocations el on el.EventId = scl.EventId and el.GeonameId = scl.GeonameId and el.EventDate = scl.EventDate
             join place.Geonames g on g.GeonameId = scl.GeonameId and g.LocationType = 6;

    -- Apply max(R,C+S,D) logic on country cases after nesting
    update @eventLocations
    set RepCases = (select max(v) from (values (RepCases), (ConfCases + SuspCases), (Deaths)) as value(v))
    where LocationType = 6;


    -- =====================================================================================================
    -- E: OUTPUT RESULTS
    -- =====================================================================================================

    insert @returnResults
    select [EventId],
           [GeonameId],
           [EventDate],
           [SuspCases],
           [ConfCases],
           [RepCases],
           [Deaths]
    from @eventLocations;

    return;
end