CREATE TABLE [disease].[TransmissionModes] (
    [TransmissionModeId] INT           NOT NULL,
    [TransmissionMode]   VARCHAR (100) NOT NULL,
    [DisplayName]        VARCHAR (100) NULL,
    CONSTRAINT [PK_TransmissionModes] PRIMARY KEY CLUSTERED ([TransmissionModeId] ASC)
);

