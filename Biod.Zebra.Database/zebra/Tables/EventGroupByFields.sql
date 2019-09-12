CREATE TABLE zebra.EventGroupByFields (
    [Id]   INT    NOT NULL,
	DisplayName VARCHAR(100) NOT NULL,
	ColumnName VARCHAR(100) NOT NULL,
    DisplayOrder   INT    NOT NULL,
    IsDefault   BIT    NOT NULL,
    IsHidden   BIT    NOT NULL,
    CONSTRAINT [PK_EventGroupByFields] PRIMARY KEY CLUSTERED ([Id])
);

GO
