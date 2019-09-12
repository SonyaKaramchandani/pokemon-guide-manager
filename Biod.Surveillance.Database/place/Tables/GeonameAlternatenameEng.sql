Create TABLE place.GeonameAlternatenameEng 
(GeonameId int NOT NULL,
AlternatenameEng varchar(200) NOT NULL,
LocationType int
CONSTRAINT PK_GeonameAlternatenameEng PRIMARY KEY CLUSTERED (GeonameId, AlternatenameEng),
CONSTRAINT [FK_GeonameAlternatenameEng_GeonameId] FOREIGN KEY (GeonameId) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [idx_GeonameAlternatenameEng_AlternatenameEng]
    ON [place].GeonameAlternatenameEng(AlternatenameEng ASC);
GO

CREATE NONCLUSTERED INDEX [idx_GeonameAlternatenameEng_LocationType]
    ON [place].GeonameAlternatenameEng(LocationType ASC);
