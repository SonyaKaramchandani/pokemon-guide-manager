CREATE TABLE [disease].[Diseases] (
    [DiseaseId]                       INT             NOT NULL,
    [DiseaseName]                     NVARCHAR (100)  NOT NULL,
    [DiseaseType]							  VARCHAR (200)   NULL,
    [LastModified]                    DATETIME        NULL,
    [ParentDiseaseId]                 INT             NULL,
    [Pronunciation]                   VARCHAR (100)   NULL,
    [SeverityLevel]                   VARCHAR (100)   NULL,
    [IsChronic]                       BIT             NULL,
    [TreatmentAvailable]              VARCHAR (100)   NULL,
    [BiosecurityRisk]                 VARCHAR (100)   NULL,
	OutbreakPotentialAttributeId	  INT			  NULL,
	IsZoonotic                        BIT             NULL,
    CONSTRAINT [PK_Diseases] PRIMARY KEY CLUSTERED ([DiseaseId] ASC),
    CONSTRAINT FK_Diseases_BiosecurityRisk FOREIGN KEY (BiosecurityRisk) REFERENCES disease.BiosecurityRisk(BiosecurityRiskCode)
);

