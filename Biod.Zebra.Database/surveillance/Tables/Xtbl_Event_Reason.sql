CREATE TABLE [surveillance].[Xtbl_Event_Reason] (
    [EventId]  INT NOT NULL,
    [ReasonId] INT NOT NULL,
    CONSTRAINT [PK_Xtbl_Event_Reason] PRIMARY KEY CLUSTERED ([EventId] ASC, [ReasonId] ASC),
    CONSTRAINT [FK_Xtbl_Event_Reason_Event] FOREIGN KEY ([EventId]) REFERENCES [surveillance].[Event] ([EventId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Xtbl_Event_Reason_Reason] FOREIGN KEY ([ReasonId]) REFERENCES [surveillance].[EventCreationReasons] ([ReasonId]) ON DELETE CASCADE
);

