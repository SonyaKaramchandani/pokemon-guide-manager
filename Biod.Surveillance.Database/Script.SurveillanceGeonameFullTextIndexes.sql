CREATE FULLTEXT INDEX ON [place].[Geonames]
    ([DisplayName] LANGUAGE 1033)
    KEY INDEX [PK_Geonames]
    ON [SurveillanceGeonamesCatalog];
GO

CREATE FULLTEXT INDEX ON [place].[uvw_AlternateNames]
    ([AlternateNames] LANGUAGE 1033)
    KEY INDEX [idx_uvw_AlternateNames_GeonameId]
    ON [SurveillanceGeonamesCatalog];
GO
