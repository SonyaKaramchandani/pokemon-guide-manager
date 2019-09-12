CREATE view place.uvw_GeonamesSubset 
with SCHEMABINDING
AS
select GeonameId, DisplayName, LocationType
From place.Geonames
Where LocationType is not NULL;

GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_GeonamesSubset_GeonameId]
    ON [place].[uvw_GeonamesSubset]([GeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_GeonamesSubset_LocationType]
    ON [place].[uvw_GeonamesSubset]([LocationType] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_GeonamesSubset_DisplayName]
    ON [place].[uvw_GeonamesSubset]([DisplayName] ASC);

