CREATE view place.uvw_CitySubset 
with SCHEMABINDING
AS
Select GeonameId, [Name] as AsciiName, DisplayName, [Population], Longitude, Latitude, Shape.STAsText() ShapeAsText,
	SearchSeq2
From place.Geonames
Where LocationType=2;

GO
CREATE UNIQUE CLUSTERED INDEX [idx_uvw_CitySubset_GeonameId]
    ON [place].[uvw_CitySubset]([GeonameId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CitySubset_AsciiName]
    ON [place].[uvw_CitySubset]([AsciiName] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CitySubset_Population]
    ON [place].[uvw_CitySubset]([Population] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_uvw_CitySubset_SearchSeq2]
    ON [place].[uvw_CitySubset]([SearchSeq2] ASC);

