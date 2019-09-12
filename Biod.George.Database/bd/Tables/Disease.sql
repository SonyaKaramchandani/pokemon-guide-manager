CREATE TABLE [bd].[Disease] (
    [diseaseId]            INT            IDENTITY (1, 1) NOT NULL,
    [name]                 NVARCHAR (64)  NOT NULL,
    [colloquialNames]      NVARCHAR (256) NULL,
    [searchTerms]          NVARCHAR (2000) NULL,
    [pronunciation]        NVARCHAR (64)  NULL,
    [diseaseType]          VARCHAR (32)   NOT NULL,
    [microbe]              VARCHAR (64)   NULL,
    [mapGranularity]       VARCHAR (32)   NULL,
    [extentVetted]         BIT            DEFAULT ((0)) NOT NULL,
    [prevalenceVetted]     BIT            DEFAULT ((0)) NOT NULL,
    [canUseForAnalytics]   BIT            DEFAULT ((0)) NOT NULL,
    [presenceBitmask]      INT            DEFAULT ((-1)) NOT NULL,
    [preventability]       FLOAT (53)     NOT NULL,
    [modelWeight]          FLOAT (53)     NOT NULL,
    [environmentalFactors] VARCHAR (64)   NULL,
    [notes]                VARCHAR (MAX)  NULL,
    [lastModified]         DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [disease_lastModified]
    ON [bd].[Disease]([lastModified] ASC);

