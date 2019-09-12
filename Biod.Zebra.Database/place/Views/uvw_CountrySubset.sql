CREATE view place.uvw_CountrySubset 
with SCHEMABINDING
AS
Select G.GeonameId, [Name] as AsciiName, [Population], DisplayName, CountryName, 
       Longitude, Latitude, CPS.SimplifiedShape.STAsText() ShapeAsText,
	G.SearchSeq2 
From place.Geonames G
Inner Join place.CountryProvinceShapes CPS on CPS.GeonameId = G.GeonameId
Where G.LocationType=6;
GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_CountrySubset_GeonameId]
    ON [place].[uvw_CountrySubset]([GeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_AsciiName]
    ON [place].[uvw_CountrySubset]([AsciiName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_DisplayName]
    ON [place].[uvw_CountrySubset]([DisplayName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_Population]
    ON [place].[uvw_CountrySubset]([Population] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CountrySubset_SearchSeq2]
    ON [place].[uvw_CountrySubset]([SearchSeq2] ASC);

