USE DiseasesAPI;
GO


IF OBJECT_ID('bd.VersionInfo', 'U') IS NOT NULL DROP TABLE bd.VersionInfo;
GO

/*IF OBJECT_ID('bd.PrevalenceWeights', 'U') IS NOT NULL DROP TABLE bd.PrevalenceWeights;*/
IF OBJECT_ID('bd.DiseaseTransmission', 'U') IS NOT NULL DROP TABLE bd.DiseaseTransmission;
GO
IF OBJECT_ID('bd.DiseaseSeasonality', 'U') IS NOT NULL DROP TABLE bd.DiseaseSeasonality;
GO 
IF OBJECT_ID('bd.DiseasePreventionMod', 'U') IS NOT NULL DROP TABLE bd.DiseasePreventionMod;
GO
IF OBJECT_ID('bd.DiseasePrevention', 'U') IS NOT NULL DROP TABLE bd.DiseasePrevention;
GO
IF OBJECT_ID('bd.DiseaseActivityMod', 'U') IS NOT NULL DROP TABLE bd.DiseaseActivityMod;
GO
IF OBJECT_ID('bd.DiseaseSeverity', 'U') IS NOT NULL DROP TABLE bd.DiseaseSeverity;
GO
IF OBJECT_ID('bd.DiseaseSeverityMod', 'U') IS NOT NULL DROP TABLE bd.DiseaseSeverityMod;
GO
IF OBJECT_ID('bd.DiseaseIncubation', 'U') IS NOT NULL DROP TABLE bd.DiseaseIncubation;
GO
IF OBJECT_ID('bd.DiseaseSymptom', 'U') IS NOT NULL DROP TABLE bd.DiseaseSymptom;
GO
IF OBJECT_ID('bd.Symptom', 'U') IS NOT NULL DROP TABLE bd.Symptom;
GO
IF OBJECT_ID('bd.SymptomCategory', 'U') IS NOT NULL DROP TABLE bd.SymptomCategory;
GO
IF OBJECT_ID('bd.DiseaseMobileMessage', 'U') IS NOT NULL DROP TABLE bd.DiseaseMobileMessage;
GO

IF OBJECT_ID('bd.MobileMessageSection', 'U') IS NOT NULL DROP TABLE bd.MobileMessageSection;
GO
IF OBJECT_ID('bd.Condition', 'U') IS NOT NULL DROP TABLE bd.Condition;
GO
IF OBJECT_ID('bd.Activity', 'U') IS NOT NULL DROP TABLE bd.Activity;
GO
IF OBJECT_ID('bd.PreventionType', 'U') IS NOT NULL DROP TABLE bd.PreventionType;
GO
IF OBJECT_ID('bd.ModifierCategory', 'U') IS NOT NULL DROP TABLE bd.ModifierCategory;
GO
IF OBJECT_ID('bd.TransmissionMode', 'U') IS NOT NULL DROP TABLE bd.TransmissionMode;
GO
IF OBJECT_ID('bd.Disease', 'U') IS NOT NULL DROP TABLE bd.Disease;
GO


CREATE TABLE bd.VersionInfo (
    modelVersion FLOAT NOT NULL,
    notes VARCHAR(MAX)
);
GO


/*
 *  This "logarithmic" conversion is already baked into the map data.
 * 
CREATE TABLE bd.PrevalenceWeights (
    incidencesMin INT NOT NULL,
    incidencesMax INT NOT NULL,
    multiplier FLOAT NOT NULL
);
GO
*/


/* NOTE:  info template for each disease is app-specific, so keep elsewhere... */
CREATE TABLE bd.Disease (
    diseaseId INT PRIMARY KEY IDENTITY(1,1), 
    name NVARCHAR(64) NOT NULL,
    colloquialNames NVARCHAR(256),           /* Comma-separated list */
    searchTerms NVARCHAR(256),               /* Comma-separated list */
    pronunciation NVARCHAR(64),
    diseaseType VARCHAR(32) NOT NULL,
    microbe VARCHAR(64),
    mapGranularity VARCHAR(32),             /* If NULL or empty, then no map exists. */
    extentVetted BIT NOT NULL DEFAULT 0,
    prevalenceVetted BIT NOT NULL DEFAULT 0,
    canUseForAnalytics BIT NOT NULL DEFAULT 0,
    presenceBitmask INT NOT NULL DEFAULT -1,    /* first 3 bits represent city 1st world, 2nd world, 3rd world, respectively.  next 3 bits, are rural. */
    preventability float NOT NULL,          /* 0 is not preventable, 1 is prevantable, 0.5 is "maybe". */
    modelWeight FLOAT NOT NULL,
    environmentalFactors VARCHAR(64),
    notes VARCHAR(MAX),
    lastModified DATETIME NOT NULL          /* Should be updated if anything here OR in related tables changes */
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='disease_lastModified' AND object_id = OBJECT_ID('bd.Disease')) DROP INDEX disease_lastModified ON bd.Disease;
CREATE INDEX disease_lastModified ON bd.Disease(lastModified);
GO


CREATE TABLE bd.TransmissionMode (
    id INT PRIMARY KEY IDENTITY(1,1), 
    mode VARCHAR(64) NOT NULL, 
    multiplier FLOAT NOT NULL, 
    description VARCHAR(MAX),
    preventions VARCHAR(MAX)
);
GO

CREATE TABLE bd.DiseaseTransmission (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    mode INT NOT NULL REFERENCES bd.TransmissionMode(id) ON DELETE CASCADE ON UPDATE CASCADE,
    rank INT NOT NULL DEFAULT 1,
    agents VARCHAR(64),                     /* Comma-separated list */
    contact VARCHAR(64),                    /* Comma-separated list */
    actions VARCHAR(64)                     /* Comma-separated list */
    PRIMARY KEY (diseaseId, mode)
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseTransmission_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseTransmission')) DROP INDEX diseaseTransmission_diseaseId ON bd.DiseaseTransmission;
CREATE INDEX diseaseTransmission_diseaseId ON bd.DiseaseTransmission(diseaseId);
GO

CREATE TABLE bd.DiseaseSeasonality (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    zone INT NOT NULL,
    fromMonth INT NOT NULL,
    toMonth INT NOT NULL,
    offSeasonWeight FLOAT NOT NULL DEFAULT 0.0,
    PRIMARY KEY (diseaseId, zone)
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseSeasonality_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseSeasonality')) DROP INDEX diseaseSeasonality_diseaseId ON bd.DiseaseSeasonality;
CREATE INDEX diseaseSeasonality_diseaseId ON bd.DiseaseSeasonality(diseaseId);
GO

CREATE TABLE bd.PreventionType (
    id INT PRIMARY KEY IDENTITY(1,1), 
    type VARCHAR(64) NOT NULL
);
GO

CREATE TABLE bd.ModifierCategory (
    id INT PRIMARY KEY IDENTITY(1,1), 
    categoryLabel VARCHAR(32) NOT NULL
);
GO

CREATE TABLE bd.DiseasePrevention (
    id INT PRIMARY KEY IDENTITY(1,1),       /* Need a primary key here since these will be sent through API as "modifiers" */
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    type INT NOT NULL REFERENCES bd.PreventionType(id) ON UPDATE CASCADE,
    riskReduction FLOAT NOT NULL DEFAULT 0.0,
    availability VARCHAR(64),
    category INT NOT NULL REFERENCES bd.ModifierCategory(id) ON UPDATE CASCADE,
    travel BIT NOT NULL DEFAULT 0,
    oral BIT NOT NULL DEFAULT 0,
    duration VARCHAR(64),
    notes VARCHAR(MAX)
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseasePrevention_diseaseId' AND object_id = OBJECT_ID('bd.DiseasePrevention')) DROP INDEX diseasePrevention_diseaseId ON bd.DiseasePrevention;
CREATE INDEX diseasePrevention_diseaseId ON bd.DiseasePrevention(diseaseId);
GO

CREATE TABLE bd.Activity (
    id INT PRIMARY KEY IDENTITY(1,1), 
    activity VARCHAR(64) NOT NULL,
    description VARCHAR(MAX)
);
GO

CREATE TABLE bd.DiseaseActivityMod (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    activity INT NOT NULL REFERENCES bd.Activity(id) ON UPDATE CASCADE,
    scale FLOAT NOT NULL DEFAULT 1.0
    PRIMARY KEY (diseaseId, activity)
);
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseAcivityMod_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseActivityMod')) DROP INDEX diseaseActivityMod_diseaseId ON bd.DiseaseActivityMod;
CREATE INDEX diseaseActivityMod_diseaseId ON bd.DiseaseActivityMod(diseaseId);
GO

CREATE TABLE bd.Condition (
    conditionId INT PRIMARY KEY, 
    category INT NOT NULL REFERENCES bd.ModifierCategory(id) ON UPDATE CASCADE,
    condition VARCHAR(64) NOT NULL,
    question VARCHAR(256) NOT NULL,
    description VARCHAR(MAX)
);
GO

CREATE TABLE bd.DiseaseSeverity (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    level INT NOT NULL,
    treatmentAvailable FLOAT NOT NULL,        /* 0.0 means no treatment is available.  1.0 means it's fully treatable. */
    chronic BIT NOT NULL DEFAULT 0,
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseSeverity_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseSeverity')) DROP INDEX diseaseSeverity_diseaseId ON db.DiseaseSeverity;
CREATE UNIQUE CLUSTERED INDEX diseaseSeverity_diseaseId ON bd.DiseaseSeverity(diseaseId);
GO

CREATE TABLE bd.DiseaseSeverityMod (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    conditionId INT NOT NULL REFERENCES bd.Condition(conditionId) ON UPDATE CASCADE,
    addend INT NOT NULL DEFAULT 1,
    conditionParameter FLOAT NOT NULL DEFAULT -1.0
    PRIMARY KEY (diseaseId, conditionId)
);
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseSeverityMod_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseSeverityMod')) DROP INDEX diseaseSeverityMod_diseaseId ON db.DiseaseSeverityMod;
CREATE INDEX diseaseSeverityMod_diseaseId ON bd.DiseaseSeverityMod(diseaseId);
GO

CREATE TABLE bd.DiseasePreventionMod (
    prevention INT NOT NULL REFERENCES bd.DiseasePrevention(id) /*ON DELETE CASCADE ON UPDATE CASCADE*/,    /* TODO */
    conditionId INT NOT NULL REFERENCES bd.Condition(conditionId) ON DELETE CASCADE ON UPDATE CASCADE,
    messageId INT NOT NULL,
    PRIMARY KEY (prevention, conditionId)
);
GO

CREATE TABLE bd.DiseaseIncubation (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    minimumDays FLOAT NOT NULL DEFAULT 0.0,
    maximumDays FLOAT NOT NULL,
    averageDays FLOAT NOT NULL,
    notes VARCHAR(512),
    source VARCHAR(MAX)
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseIncubation_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseIncubation')) DROP INDEX diseaseIncubation_diseaseId ON db.DiseaseIncubation;
CREATE INDEX diseaseIncubation_diseaseId ON bd.DiseaseIncubation(diseaseId);
GO

/* For "Systems" and/or "Syndromes"... */
CREATE TABLE bd.SymptomCategory (
    symptomCategoryId INT PRIMARY KEY IDENTITY(1,1), 
    name VARCHAR(256) NOT NULL,
    notes VARCHAR(MAX)
    /* TAI:  "parent" field for hierarchy? */
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='symptomCategory_name' AND object_id = OBJECT_ID('bd.SymptomCategory')) DROP INDEX symptomCategory_name ON bd.SymptomCategory;
CREATE INDEX symptomCategory_name ON bd.SymptomCategory(name);
GO


CREATE TABLE bd.Symptom (
    symptomId INT PRIMARY KEY IDENTITY(1,1), 
    name VARCHAR(256) NOT NULL,
    symptomCategoryId INT NOT NULL REFERENCES bd.SymptomCategory(symptomCategoryId) ON DELETE CASCADE,
    altNames VARCHAR(256),
    definition VARCHAR(MAX),
    definitionSource VARCHAR(256)
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='symptom_name' AND object_id = OBJECT_ID('bd.Symptom')) DROP INDEX symptom_name ON bd.Symptom;
CREATE INDEX symptom_name ON bd.Symptom(name);
GO


CREATE TABLE bd.DiseaseSymptom (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    symptomId INT NOT NULL REFERENCES bd.Symptom(symptomId) ON DELETE CASCADE ON UPDATE CASCADE,
    associationScore FLOAT NOT NULL DEFAULT 0.0,
    notes VARCHAR(MAX)
    PRIMARY KEY (diseaseId, symptomId)
);
GO
IF EXISTS(SELECT * FROM sys.indexes WHERE name='diseaseSymptom_diseaseId' AND object_id = OBJECT_ID('bd.DiseaseSymptom')) DROP INDEX diseaseSymptom_diseaseId ON bd.DiseaseSymptom;
CREATE INDEX diseaseSymptom_diseaseId ON bd.DiseaseSymptom(diseaseId);
GO


CREATE TABLE bd.MobileMessageSection (
    id INT PRIMARY KEY IDENTITY(1,1), 
    sectionName VARCHAR(64) NOT NULL
);
GO

CREATE TABLE bd.DiseaseMobileMessage (
    diseaseId INT NOT NULL REFERENCES bd.Disease(diseaseId) ON DELETE CASCADE ON UPDATE CASCADE,
    sectionId INT NOT NULL REFERENCES bd.MobileMessageSection(id) ON UPDATE CASCADE,
    message VARCHAR(MAX),
    PRIMARY KEY (diseaseId, sectionId)
);
GO


SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('bd.diseaseConditionsAroundPoint_sp', 'P') IS NOT NULL DROP PROCEDURE bd.diseaseConditionsAroundPoint_sp
GO
CREATE PROCEDURE [bd].[diseaseConditionsAroundPoint_sp] 
    @latitude FLOAT,
    @longitude FLOAT,
    @radius FLOAT = 50000.0          -- in meters
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @srid INT;
    DECLARE @p geometry, @c geometry;
    DECLARE @latradius FLOAT, @longradius FLOAT;
    DECLARE @longStr VARCHAR(12), @latStr VARCHAR(12), @leftLongStr VARCHAR(12), @rightLongStr VARCHAR(12), @topLatStr VARCHAR(12), @bottomLatStr VARCHAR(12);

    SET @latradius = @radius / 110574.0;
    SET @longradius = @radius / (111320.0 * cos(@latitude));

    SET @longStr = STR(@longitude, 12, 8);
    SET @latStr = STR(@latitude, 12, 8);
    SET @leftLongStr = STR(@longitude - @longradius, 12, 8);
    SET @rightLongStr = STR(@longitude + @longradius, 12, 8);
    SET @topLatStr = STR(@latitude - @latradius, 12, 8);
    SET @bottomLatStr = STR(@latitude + @latradius, 12, 8);

    SET @srid = 4326;     -- 4326 is the Spatial Resolution ID (SRID) for the GCS_WGS_1984 projection
    SET @p = geometry::STPointFromText('POINT (' + @longStr + ' ' + @latStr + ')', @srid);
    SET @c = geometry::STGeomFromText('CURVEPOLYGON ((' + @leftLongStr + ' ' + @latStr + ', ' + @longStr + ' ' + @topLatStr + ', ' + @rightLongStr + ' ' + @latStr + ', ' + @longStr + ' ' + @bottomLatStr + ', ' + @leftLongStr + ' ' + @latStr + '))', @srid);

    -- Distance returned in degrees
    SELECT *, (Shape.STDistance(@p) * 110574.0) as Distance  FROM [map].[DiseaseConditions_GCS] WHERE Shape.STIntersects(@c) != 0 ORDER BY Distance;
END
GO
GRANT EXECUTE ON OBJECT::bd.diseaseConditionsAroundPoint_sp TO [bd-api];
GO
