CREATE TABLE [disease].[Interventions] (
    [InterventionId]   INT            NOT NULL,
    [InterventionType] VARCHAR (100)  NOT NULL,
    [Oral]           BIT            NULL,
	DisplayName varchar(100),
    CONSTRAINT [PK_Interventions] PRIMARY KEY CLUSTERED ([InterventionId] ASC)
);

