-- ==========================================================================================
-- Epic:            PROXPAR
-- JIRA:            PT-1379 (https://bluedotglobal.atlassian.net/browse/PT-1379)
-- Description:     Given a geoname id, find all the event locations that are considered
--                  in or proximal. All event locations of the event is returned, where the
--                  flag IsWithinLocation determines whether it is in or proximal. This is
--                  to allow any computation related to the geoname hierarchy before removing
--                  those locations.
-- ==========================================================================================

create function zebra.ufn_GetProximalEventLocations(@geonameId int,
                                                    @diseaseId int = null,
                                                    @eventId int = null)
    returns table
        as
        return
        with
            -- Event IDs filtered based on provided disease id and event id if applicable
            relevantEventIds as (
                select EventId
                from [surveillance].[Event]
                where (@diseaseId is null or DiseaseId = @diseaseId)
                  and (@eventId is null or EventId = @eventId)
            ),

            -- Shape of the provided geoname id with the buffer applied
            inputGidShape as (
                select top 1 coalesce(
                                     cps.SimplifiedShapeWithBuffer,
                                     g.Shape.STBuffer((select top 1 cast([Value] as int) from [bd].[ConfigurationVariables] where [Name] = 'Distance'))
                                 ) as Shape
                from [place].[Geonames] as g
                         left join [place].CountryProvinceShapes cps on cps.GeonameId = g.GeonameId -- Left join since cities don't have pre-computed shapes
                where g.GeonameId = @geonameId
            ),

            -- Case Count for Event Locations intersected with the input geoname id shape
            allRelevantEventLocs as (
                select enl.EventId,
                       enl.GeonameId,
                       enl.RepCases,
                       enl.ConfCases,
                       enl.SuspCases,
                       enl.Deaths,
                       g.LocationType,
                       g.DisplayName,
                       g.Admin1GeonameId,
                       g.CountryGeonameId,
                       case
                           when g.Admin1GeonameId = @geonameId or g.CountryGeonameId = @geonameId
                               -- Event location is already within the input geoname id
                               then cast(1 as bit)
                           when cps.SimplifiedShape is not null
                               -- For Countries and Provinces use the buffered shape and intersect with the input geoname shape
                               then cps.SimplifiedShape.STIntersects((select top 1 * from inputGidShape))
                           else
                               -- For Cities, use the lat/long point and intersect with the input geoname shape
                               g.Shape.STIntersects((select top 1 * from inputGidShape))
                           end IsWithinLocation
                from [surveillance].[uvw_LastEventNestedLocation] enl
                         join [place].[Geonames] g on g.GeonameId = enl.GeonameId -- Join geonames to have latitude/longitude available for fallback
                         left join [place].[CountryProvinceShapes] cps on cps.GeonameId = enl.GeonameId -- Left join since cities don't have pre-computed shapes
                where EventId in (select * from relevantEventIds)
            ),

            -- Filtered events from allRelevantEventLocs, where only events with at least 1 event location being within the input geoname is kept
            proximalEventIds as (
                select distinct EventId
                from allRelevantEventLocs
                where IsWithinLocation = 1
                group by EventId, GeonameId
                having count(*) > 0
            )
        select *
        from allRelevantEventLocs
        where EventId in (select * from proximalEventIds)