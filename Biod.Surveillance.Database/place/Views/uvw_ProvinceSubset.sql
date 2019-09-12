CREATE view place.uvw_ProvinceSubset 
with SCHEMABINDING
AS
Select GeonameId, [Name] as AsciiName, [Population], DisplayName
From place.Geonames
Where LocationType=4;

GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_ProvinceSubset_GeonameId]
    ON [place].uvw_ProvinceSubset([GeonameId] ASC);

GO
CREATE NONCLUSTERED INDEX [idx_uvw_ProvinceSubset_AsciiName]
    ON [place].uvw_ProvinceSubset(AsciiName ASC);
GO
CREATE NONCLUSTERED INDEX [idx_uvw_ProvinceSubset_DisplayName]
    ON [place].uvw_ProvinceSubset(DisplayName ASC);

GO
CREATE NONCLUSTERED INDEX [idx_uvw_ProvinceSubset_Population]
    ON [place].uvw_ProvinceSubset([Population] ASC);
GO
