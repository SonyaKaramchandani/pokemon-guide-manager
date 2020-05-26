CREATE TABLE [hcw].[HcwCases]
(
    [HcwCaseId]                                   INT             NOT NULL IDENTITY (1,1),
    [UserId]                                      NVARCHAR(128)   NOT NULL,
    [CreatedDate]                                 DATETIMEOFFSET  NOT NULL,
    [LastUpdatedDate]                             DATETIMEOFFSET  NOT NULL,
    [GeonameId]                                   INT             NOT NULL,
    [ArrivalDate]                                 DATETIMEOFFSET  NOT NULL,
    [DepartureDate]                               DATETIMEOFFSET  NOT NULL,
    [SymptomOnsetDate]                            DATETIMEOFFSET  NOT NULL,
    [PrimarySyndromes]                            NVARCHAR(MAX)   NOT NULL,
    [InitialCaseOutput]                           NVARCHAR(MAX)   NULL,
    [RefinementBySymptomsDiseaseIds]              [nvarchar](max) NULL,
    [RefinementBySymptomsQuestions]               [nvarchar](max) NULL,
    [RefinementBySymptomsAnswers]                 [nvarchar](max) NULL,
    [RefinementBySymptomsOutput]                  [nvarchar](max) NULL,
    [RefinementByActivitiesAndVaccinesDiseaseIds] [nvarchar](max) NULL,
    [RefinementByActivitiesAndVaccinesQuestions]  [nvarchar](max) NULL,
    [RefinementByActivitiesAndVaccinesAnswers]    [nvarchar](max) NULL,
    [RefinementByActivitiesAndVaccinesOutput]     [nvarchar](max) NULL,
    [DiagnosedDiseaseId]                          INT             NULL,
    [OtherDiagnosedDiseaseName]                   NVARCHAR(MAX)   NULL,
    CONSTRAINT [PK_HcwCases] PRIMARY KEY CLUSTERED ([HcwCaseId] ASC),
    CONSTRAINT [FK_hcw.HcwCases_UserProfile_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_hcw.HcwCases_place.Geonames] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames] ([GeonameId])
);

GO
