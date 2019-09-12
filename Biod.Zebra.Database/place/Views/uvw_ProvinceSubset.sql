CREATE view place.uvw_ProvinceSubset 
with SCHEMABINDING
AS
Select G.GeonameId, [Name] as AsciiName, [Population], DisplayName, 
       Longitude, Latitude, CPS.SimplifiedShape.STAsText() ShapeAsText
From place.Geonames G
Inner Join place.CountryProvinceShapes CPS on CPS.GeonameId = G.GeonameId
Where G.LocationType=4 and CPS.Shape IS NOT NULL;

GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_ProvinceSubset_GeonameId]
    ON [place].[uvw_ProvinceSubset]([GeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_ProvinceSubset_AsciiName]
    ON [place].[uvw_ProvinceSubset]([AsciiName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_ProvinceSubset_DisplayName]
    ON [place].[uvw_ProvinceSubset]([DisplayName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_ProvinceSubset_Population]
    ON [place].[uvw_ProvinceSubset]([Population] ASC);

