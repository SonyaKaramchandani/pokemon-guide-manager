CREATE TABLE [disease].[TransmissionModes] (
    [TransmissionModeId] INT           NOT NULL,
    [TransmissionMode]   VARCHAR (100) NOT NULL,
	DisplayName varchar(100),
    CONSTRAINT [PK_TransmissionModes] PRIMARY KEY CLUSTERED ([TransmissionModeId] ASC)
);

