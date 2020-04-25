CREATE TABLE [hcw].[HcwCases]
(
    [HcwCaseId]                 INT            NOT NULL IDENTITY (1,1),
    [UserId]                    NVARCHAR(128)  NOT NULL,
    [CreatedDate]               DATETIMEOFFSET NOT NULL,
    [LastUpdatedDate]           DATETIMEOFFSET NOT NULL,
    [GeonameId]                 INT            NOT NULL,
    [ArrivalDate]               DATETIMEOFFSET  NOT NULL,
    [DepartureDate]             DATETIMEOFFSET  NOT NULL,
    [SymptomOnsetDate]          DATETIMEOFFSET  NOT NULL,
    [PrimarySyndromes]          NVARCHAR(MAX)  NOT NULL,
    [InitialCaseOutput]         NVARCHAR(MAX)  NULL,
    [RefinementDiseaseIds]      NVARCHAR(MAX)  NULL,
    [RefinementQuestions]       NVARCHAR(MAX)  NULL,
    [RefinementAnswers]         NVARCHAR(MAX)  NULL,
    [RefinementOutput]          NVARCHAR(MAX)  NULL,
    [DiagnosedDiseaseId]        INT            NULL,
    [OtherDiagnosedDiseaseName] NVARCHAR(MAX)  NULL,
    CONSTRAINT [PK_HcwCases] PRIMARY KEY CLUSTERED ([HcwCaseId] ASC),
    CONSTRAINT [FK_hcw.HcwCases_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_hcw.HcwCases_place.Geonames] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId])
);

GO
