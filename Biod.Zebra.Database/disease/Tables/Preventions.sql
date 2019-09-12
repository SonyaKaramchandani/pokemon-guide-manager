CREATE TABLE [disease].[Preventions] (
    [PreventionId]   INT            NOT NULL,
    [PreventionType] VARCHAR (100)  NOT NULL,
    [Oral]           BIT            NULL,
    [RiskReduction]  DECIMAL (4, 2) NULL,
    [Duration]       VARCHAR (100)  NULL,
    [DisplayName]    VARCHAR (100)  NULL,
    CONSTRAINT [PK_Preventions] PRIMARY KEY CLUSTERED ([PreventionId] ASC)
);

