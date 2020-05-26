-- =============================================
-- Author:		Vivian Hu
-- Create date: 2020-02  
-- Description:	Called by usp_ZebraDataRenderSetImportationRiskByGeonameId, usp_ZebraDataRenderSetImportationRiskByGeonameIdSpreadMd
--				Returns all local active events
-- Modified: 2020-05, zebra.ufn_GetProximalEventLocations returns nested total cases (province cases is the actual total)
-- =============================================

create function [bd].[ufn_GetLocalEventsByGeoname](@GeonameId as int)
    returns @returnResults table
                           (
                               EventId int
                           )
as
begin
    declare @eventLocations table
                            (
                                EventId          int,
                                GeonameId        int,
                                RepCases         int,
                                ConfCases        int,
                                SuspCases        int,
                                Deaths           int,
                                LocationType     int,
                                DisplayName      varchar(135),
                                Admin1GeonameId  int,
                                CountryGeonameId int,
                                IsWithinLocation int
                            );

    insert into @eventLocations
    select EventId,
           GeonameId,
           RepCases,
           ConfCases,
           SuspCases,
           Deaths,
           LocationType,
           DisplayName,
           Admin1GeonameId,
           CountryGeonameId,
           IsWithinLocation
    from [zebra].[ufn_GetProximalEventLocations](@GeonameId, null, null);

    -- The function above returns the total nested cases at each event location.
    -- We need to calculate the nested distribution cases: the number of cases distributed across the event locations.

    -- Countries: subtract any cases from its immediate descendants
    with SummedCountryCases as (
        select EventId,
               CountryGeonameId,
               Sum(RepCases) as RepCases
        from @eventLocations
        where LocationType = 4                             -- Provinces
           or Admin1GeonameId is null and LocationType = 2 -- Cities without Provinces
        group by EventId, CountryGeonameId
    )
    update el
    set el.RepCases = (select max(v) from (values (el.RepCases - scc.RepCases), (0)) as value(v)) -- Enforce non-negative cases
    from SummedCountryCases scc,
         @eventLocations el
    where el.LocationType = 6
      and el.EventId = scc.EventId
      and el.GeonameId = scc.CountryGeonameId;

    -- Provinces: subtract any cases from its immediate descendants
    with SummedProvinceCases as (
        select EventId,
               Admin1GeonameId,
               Sum(RepCases) as RepCases
        from @eventLocations
        where LocationType = 2 -- Cities
        group by EventId, Admin1GeonameId
    )
    update el
    set el.RepCases = (select max(v) from (values (el.RepCases - spc.RepCases), (0)) as value(v)) -- Enforce non-negative cases
    from SummedProvinceCases spc,
         @eventLocations el
    where el.LocationType = 4
      and el.EventId = spc.EventId
      and el.GeonameId = spc.Admin1GeonameId;

    -- Return Event IDs that are proximal
    insert into @returnResults
    select distinct EventId
    from @eventLocations
    where IsWithinLocation = 1
      and RepCases > 0;

    return;
end