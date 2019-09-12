CREATE TABLE [bd].[DiseaseMobileMessage] (
    [diseaseId] INT           NOT NULL,
    [sectionId] INT           NOT NULL,
    [message]   VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([diseaseId] ASC, [sectionId] ASC),
    FOREIGN KEY ([diseaseId]) REFERENCES [bd].[Disease] ([diseaseId]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([sectionId]) REFERENCES [bd].[MobileMessageSection] ([id]) ON UPDATE CASCADE
);

