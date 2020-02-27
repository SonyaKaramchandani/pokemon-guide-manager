CREATE TABLE [place].[CountryProvinceShapes] (
  [GeonameId]            INT           NOT NULL,
  [Shape]                GEOGRAPHY     NULL,
  [SimplifiedShape]      GEOGRAPHY     NULL,
  [LocationType]         INT           NULL,
  [SimplifiedShapeText]  VARCHAR(MAX)  NULL,
  CONSTRAINT [PK_CountryProvinceShapes] PRIMARY KEY CLUSTERED ([GeonameId] ASC),
  CONSTRAINT [FK_CountryProvinceShapes_Geoname] FOREIGN KEY ([GeonameId]) REFERENCES [place].[Geonames]([GeonameId])
);


GO
CREATE NONCLUSTERED INDEX [idx_CountryProvinceShapes_LocationType]
    ON [place].[CountryProvinceShapes]([LocationType] ASC);

GO

CREATE SPATIAL INDEX sidx_SimplifiedShape ON [place].[CountryProvinceShapes]
(
	[SimplifiedShape]
)USING  GEOGRAPHY_AUTO_GRID 
WITH (
CELLS_PER_OBJECT = 12, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

-- =============================================
-- Author:		Basam
-- Create date: 2018-11
-- Description:	Update SimplifiedShape on place.CountryProvinceShapes 
-- =============================================

--CREATE TRIGGER place.utr_CountryProvinceShapesInsertedOrUpdated
--ON [place].[CountryProvinceShapes]
--AFTER INSERT, UPDATE
--AS
--	DECLARE @GeonameId INT = 0
--	SELECT @GeonameId = GeonameId FROM inserted WHERE [Shape] IS NOT NULL
--	IF (@GeonameId <> 0)
--	BEGIN
--		UPDATE [place].[CountryProvinceShapes]
--		SET [SimplifiedShape] = [bd].[ufn_RemoveArtefacts]([Shape].Reduce(300))
--		  WHERE GeonameId = @GeonameId
--	END
--GO
