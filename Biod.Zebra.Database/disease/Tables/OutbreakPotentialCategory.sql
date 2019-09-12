CREATE TABLE [disease].OutbreakPotentialCategory(
	Id int NOT NULL, --for purpose of pk
	AttributeId int NOT NULL,
	[Rule] nvarchar(500) NOT NULL,
	[NeedsMap] bit NOT NULL,
	MapThreshold varchar(20) NULL,
	[EffectiveMessageDescription] [nvarchar](2000) NOT NULL,
	[EffectiveMessage] [nvarchar](20) NOT NULL,
	[IsLocalTransmissionPossible] [bit] NOT NULL,
CONSTRAINT PK_OutbreakPotentialCategory PRIMARY KEY CLUSTERED (Id)
);
