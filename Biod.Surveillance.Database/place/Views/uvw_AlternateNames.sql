CREATE VIEW [place].[uvw_AlternateNames]
    WITH SCHEMABINDING
    AS  
    SELECT        GeonameId, Alternatenames
    FROM          place.Geonames
GO

CREATE UNIQUE CLUSTERED INDEX [idx_uvw_AlternateNames_GeonameId]
    ON [place].[uvw_AlternateNames]([GeonameId] ASC);

GO
