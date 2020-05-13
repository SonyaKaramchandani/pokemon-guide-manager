CREATE PROCEDURE [surveillance].[usp_UpdateEventNestedLocations]
  @EventId int

AS
BEGIN

-- Copy cities only
insert into surveillance.EventNestedLocation
select el.*, null, null, null, null
from surveillance.EventLocation el
join place.Geonames g on g.GeonameId = el.GeonameId and g.LocationType = 2
where el.EventId = @EventId

-- Apply max(R,C+S,D) logic on city cases
update surveillance.EventNestedLocation
set RepCases = (Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v))
where EventId = @EventId

-- Create province entries by summing cases from the city entries
insert into surveillance.EventNestedLocation
select EventId, g.Admin1GeonameId, EventDate, sum(SuspCases), sum(ConfCases), sum(RepCases), sum(Deaths), null, null, null, null
from surveillance.EventNestedLocation eln
join place.Geonames g on g.GeonameId = eln.GeonameId
where g.Admin1GeonameId is not null and EventId = @EventId
group by EventId, g.Admin1GeonameId, EventDate

-- Insert standalone provinces (no city entries)
insert into surveillance.EventNestedLocation
select el.*, null, null, null, null
from surveillance.EventLocation el
join place.Geonames g on g.GeonameId = el.GeonameId and g.LocationType = 4
where el.EventId = @EventId and not exists (select 1 from surveillance.EventNestedLocation
                  where EventId = el.EventId and GeonameId = el.GeonameId and EventDate = el.EventDate)

-- Apply max(R,C+S,D) logic on province cases
update surveillance.EventNestedLocation
set RepCases = (Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v))
where EventId = @EventId

-- Apply nesting logic: take max of either province cases or sum of city cases
update surveillance.EventNestedLocation
set RepCases = case when enl.RepCases > el.RepCases then enl.RepCases else el.RepCases end,
    SuspCases = case when enl.SuspCases > el.SuspCases then enl.SuspCases else el.SuspCases end,
    ConfCases = case when enl.ConfCases > el.ConfCases then enl.ConfCases else el.ConfCases end,
    Deaths = case when enl.Deaths > el.Deaths then enl.Deaths else el.Deaths end
from surveillance.EventNestedLocation enl
join surveillance.EventLocation el on el.EventId = enl.EventId and el.GeonameId = enl.GeonameId and el.EventDate = enl.EventDate
join place.Geonames g on g.GeonameId = enl.GeonameId and g.LocationType = 4
where enl.EventId = @EventId

-- Apply max(R,C+S,D) logic on nested province cases
update surveillance.EventNestedLocation
set RepCases = (Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v))
where EventId = @EventId

-- Create country entries by summing cases from the cities that have no provinces (Singapore, Vatican,...)
insert into surveillance.EventNestedLocation
select EventId, g.CountryGeonameId, EventDate, sum(SuspCases), sum(ConfCases), sum(RepCases), sum(Deaths), null, null, null, null
from surveillance.EventNestedLocation enl
join place.Geonames g on g.GeonameId = enl.GeonameId
where g.Admin1GeonameId is null and enl.EventId = @EventId
group by EventId, g.CountryGeonameId, EventDate

-- Update country entries (created in previous step) by adding summed cases from the province entries
;with provinceCases as (
select cenl.EventId, cenl.GeonameId, cenl.EventDate,
       sum(penl.RepCases) as RepCases,
       sum(penl.SuspCases) as SuspCases,
       sum(penl.ConfCases) as ConfCases,
       sum(penl.Deaths) as Deaths
from surveillance.EventNestedLocation cenl
join surveillance.EventNestedLocation penl on penl.EventId = cenl.EventId and penl.EventDate = cenl.EventDate
join place.Geonames g on g.GeonameId = penl.GeonameId and g.LocationType = 4 and g.CountryGeonameId = cenl.GeonameId
where cenl.EventId = @EventId
group by cenl.EventId, cenl.GeonameId, cenl.EventDate
)
update surveillance.EventNestedLocation
set RepCases += provinceCases.RepCases,
    SuspCases += provinceCases.SuspCases,
    ConfCases += provinceCases.ConfCases,
    Deaths += provinceCases.Deaths
from provinceCases
where provinceCases.EventId = surveillance.EventNestedLocation.EventId
  and provinceCases.GeonameId = surveillance.EventNestedLocation.GeonameId
  and provinceCases.EventDate = surveillance.EventNestedLocation.EventDate

-- Create country entries by summing cases from the province entries
insert into surveillance.EventNestedLocation
select EventId, g.CountryGeonameId, EventDate, sum(SuspCases), sum(ConfCases), sum(RepCases), sum(Deaths), null, null, null, null
from surveillance.EventNestedLocation eln
join place.Geonames g on g.GeonameId = eln.GeonameId
where g.LocationType = 4 and EventId = @EventId
  and not exists (select 1 from surveillance.EventNestedLocation
                  where EventId = eln.EventId and GeonameId = g.CountryGeonameId and EventDate = eln.EventDate)
group by EventId, g.CountryGeonameId, EventDate

-- Insert standalone countries (no city/province entries)
insert into surveillance.EventNestedLocation
select el.*, null, null, null, null
from surveillance.EventLocation el
join place.Geonames g on g.GeonameId = el.GeonameId and g.LocationType = 6
where EventId = @EventId and not exists (select 1 from surveillance.EventNestedLocation
                  where EventId = el.EventId and GeonameId = el.GeonameId and EventDate = el.EventDate)

-- Apply max(R,C+S,D) logic
update surveillance.EventNestedLocation
set RepCases = (Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v))
where EventId = @EventId

-- Apply nesting logic: take max of either country cases or sum of province cases
update surveillance.EventNestedLocation
set RepCases = case when enl.RepCases > el.RepCases then enl.RepCases else el.RepCases end,
    SuspCases = case when enl.SuspCases > el.SuspCases then enl.SuspCases else el.SuspCases end,
    ConfCases = case when enl.ConfCases > el.ConfCases then enl.ConfCases else el.ConfCases end,
    Deaths = case when enl.Deaths > el.Deaths then enl.Deaths else el.Deaths end
from surveillance.EventNestedLocation enl
join surveillance.EventLocation el on el.EventId = enl.EventId and el.GeonameId = enl.GeonameId and el.EventDate = enl.EventDate
join place.Geonames g on g.GeonameId = enl.GeonameId and g.LocationType = 6
where enl.EventId = @EventId

-- Final max(R,C+S,D) logic
update surveillance.EventNestedLocation
set RepCases = (Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v))
where EventId = @EventId

-- Finally, calculate incremental counts for each row
;with NumberedEventLocations as (
select *,
	ROW_NUMBER() over (partition by EventId, GeonameId order by EventDate) as RowNumber
from [surveillance].[EventNestedLocation] enl
where enl.EventId = @EventId
)
update [surveillance].[EventNestedLocation]
set NewSuspCases = nel.SuspCases - isnull(prev.SuspCases, 0),
    NewConfCases = nel.ConfCases - isnull(prev.ConfCases, 0),
    NewRepCases = nel.RepCases - isnull(prev.repcases, 0),
    NewDeaths = nel.Deaths - isnull(prev.Deaths, 0) 
from surveillance.EventNestedLocation enl
join NumberedEventLocations nel on nel.EventId = enl.EventId and nel.GeonameId = enl.GeonameId and nel.EventDate = enl.EventDate
left join NumberedEventLocations prev on prev.EventId = nel.EventId and prev.GeonameId = nel.GeonameId and prev.RowNumber = nel.RowNumber - 1

END
