CREATE TABLE [place].[GeonameAlternatenameEng] (
    [GeonameId]        INT           NOT NULL,
    [AlternatenameEng] VARCHAR (200) NOT NULL,
    [LocationType]     INT           NULL,
	[AlternateNameId]  INT           IDENTITY(1, 1),
    CONSTRAINT [PK_GeonameAlternatenameEng] PRIMARY KEY CLUSTERED ([GeonameId] ASC, [AlternatenameEng] ASC),
    CONSTRAINT [FK_GeonameAlternatenameEng_GeonameId] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [idx_GeonameAlternatenameEng_AlternatenameEng]
    ON [place].[GeonameAlternatenameEng]([AlternatenameEng] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_GeonameAlternatenameEng_LocationType]
    ON [place].[GeonameAlternatenameEng]([LocationType] ASC);


GO 
CREATE UNIQUE INDEX [idx_GeonameAlternatenameEng_AlternateNameId] 
      ON [place].[GeonameAlternatenameEng]([AlternateNameId]);








