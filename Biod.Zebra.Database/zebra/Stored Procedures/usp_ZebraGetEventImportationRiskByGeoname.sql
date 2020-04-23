
-- =============================================
-- Author:		Vivian
-- Create date: 2020-02 
-- Description:	Returns one event's risk values in one aoi
-- called by usp_ZebraDataRenderSetGeonameImportationRiskByEventIdSpreadMd
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraGetEventImportationRiskByGeoname
	@EventId int, 
	@AoiGeonameId int
AS
BEGIN
	SET NOCOUNT ON
	--1. aoi info
	Declare @locType int, @point GEOGRAPHY
	Select @locType=LocationType, @point=Shape
		From place.Geonames
		Where GeonameId=@AoiGeonameId

	--2. find dest grids
	Declare @tbl_userGrid table (GridId nvarchar(12))
	If @locType=2	--city
		Insert into @tbl_userGrid (GridId)
			Select gridId
			From [bd].[HUFFMODEL25KMWORLDHEXAGON]
			Where SHAPE.STIntersects(@point) = 1
	Else If @locType=4	--province
		Insert into @tbl_userGrid (GridId)
			Select GridId
			From [zebra].[GridProvince]
			Where Adm1GeonameId=@AoiGeonameId
	Else If @locType=6	--country
		Insert into @tbl_userGrid (GridId)
			Select GridId
			From [zebra].GridCountry
			Where CountryGeonameId=@AoiGeonameId

	--3. calculate risk values based on dest airports (similar as 10(a)12(a), but derived from 10(b)12(b))
	Declare @DestinationCatchmentThreshold decimal(5,2)
		=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='DestinationCatchmentThreshold');
	With T1 as (
		Select Distinct f4.DestinationStationId, f4.MinProb, f4.MaxProb, f4.MinExpVolume, f4.MaxExpVolume
		From @tbl_userGrid as f1, [zebra].[EventDestinationGridSpreadMd] as f2, 
			[zebra].[GridStation] as f3, zebra.EventDestinationAirportSpreadMd as f4
		Where f2.EventId=@EventId and MONTH(f3.ValidFromDate)=MONTH(GETUTCDATE())
			and f3.Probability>=@DestinationCatchmentThreshold and f4.EventId=@EventId
			and f1.GridId=f2.GridId and f2.GridId=f3.GridId and f3.StationId=f4.DestinationStationId
		)
		,T2 as (Select @AoiGeonameId as AoiGeonameId,
			ISNULL(1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(MinProb, 1)),0))), 0) as MinProbability, 
			ISNULL(1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(MaxProb, 1)),0))), 0) as MaxProbability, 
			ISNULL(SUM(MinExpVolume), 0) as MinExpTravelers, ISNULL(SUM(MaxExpVolume), 0) as MaxExpTravelers
 		From T1
         )
        ,Tmin as (
            Select distinct MinProb From T1 Where MinProb>=1
            )
        ,Tmax as (
            Select distinct MaxProb From T1 Where MaxProb>=1
            )
        Select T2.AoiGeonameId, COALESCE(Tmin.MinProb, T2.MinProbability) as MinProbability, 
            COALESCE(Tmax.MaxProb, T2.MaxProbability) as MaxProbability,
            T2.MinExpTravelers, T2.MaxExpTravelers
        From T2 left join Tmin on 1=1
            left join Tmax on 1=1

END