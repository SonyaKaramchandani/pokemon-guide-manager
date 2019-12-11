CREATE TABLE [bd].[PreventionTypeInterventionCategory] (
	InterventionCategoryId	NVARCHAR (128) NOT NULL,
    [id]  INT IDENTITY (1, 1)       NOT NULL,
    [type] VARCHAR (64) NOT NULL,
	InterventionTypeId INT NOT NULL,
    PRIMARY KEY CLUSTERED (InterventionCategoryId ASC)
);

