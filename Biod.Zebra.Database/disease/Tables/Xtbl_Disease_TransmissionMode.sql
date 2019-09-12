CREATE TABLE [disease].[Xtbl_Disease_TransmissionMode] (
    [DiseaseId]          INT NOT NULL,
    [SpeciesId]		     INT NOT NULL DEFAULT 1,
    [TransmissionModeId] INT NOT NULL,
    CONSTRAINT [PK_Xtbl_Disease_TransmissionMode] PRIMARY KEY CLUSTERED ([DiseaseId] ASC, [SpeciesId] ASC, [TransmissionModeId] ASC),
    CONSTRAINT [FK_Xtbl_Disease_TransmissionMode_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [disease].[Diseases] ([DiseaseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_TransmissionMode_SpeciesId] FOREIGN KEY ([SpeciesId]) REFERENCES [disease].[Species] (SpeciesId) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Disease_TransmissionMode_TransmissionModeId] FOREIGN KEY ([TransmissionModeId]) REFERENCES [disease].[TransmissionModes] ([TransmissionModeId]) ON DELETE CASCADE
);

