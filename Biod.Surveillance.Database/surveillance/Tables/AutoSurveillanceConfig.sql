CREATE TABLE [surveillance].[AutoSurveillanceConfig]
(
  [Id]              INT NOT NULL PRIMARY KEY, 
  [DiseaseId]       INT NOT NULL, 
  [GeonameId]       INT NULL, 
  [Source]          NVARCHAR(MAX) NULL, 
  [IncludeChildren] BIT NOT NULL, 
  CONSTRAINT [FK_AutoSurveillanceConfig_Disease] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases]([DiseaseId]),
  CONSTRAINT [FK_AutoSurveillanceConfig_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames]([GeonameId])
)
