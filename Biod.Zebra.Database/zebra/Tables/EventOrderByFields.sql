CREATE TABLE zebra.EventOrderByFields  (
    [Id]   INT    NOT NULL,
	DisplayName VARCHAR(100) NOT NULL,
	ColumnName VARCHAR(100) NOT NULL,
    DisplayOrder   INT    NOT NULL,
    IsDefault   BIT    NOT NULL,
    IsHidden   BIT    NOT NULL,
    CONSTRAINT [PK_EventOrderByFields] PRIMARY KEY CLUSTERED ([Id])
);

GO
