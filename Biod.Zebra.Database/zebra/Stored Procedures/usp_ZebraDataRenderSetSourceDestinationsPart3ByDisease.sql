
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	similar as in usp_ZebraDataRenderSetSourceDestinationsPart3, takes diseaseId instead of evenId
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart3ByDisease
	@DiseaseId INT,
	@MinPrevelance float,
	@MaxPrevelance float
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. clean existing data
		--Done in part 1

		--2. timeline and disease
		Declare @endMth int=(Select EventMonth From zebra.DiseaseEventPrevalence Where DiseaseId=@DiseaseId);
		
		--needed for V7
		Update zebra.DiseaseEventPrevalence 
			Set MinPrevelance=@MinPrevelance, MaxPrevelance=@MaxPrevelance
			Where DiseaseId=@DiseaseId

		/*Destination apts*/
		Declare @tbl_desApts table(DestinationStationId INT, Volume INT, 
					MinProb float, MaxProb float)
		--1. total weighted passengers (ppt #8)
		--1.1 for each individual dest apt (#8.b)
		Insert into @tbl_desApts(DestinationStationId, Volume)
			Select f1.DestinationAirportId, ROUND(SUM(f1.Volume*f2.Probability), 0)
			From [zebra].[StationDestinationAirport] as f1, [zebra].[DiseaseSourceAirport] as f2
			Where f2.DiseaseId=@DiseaseId and MONTH(f1.ValidFromDate)=@endMth 
				and f1.StationId=f2.[SourceStationId]
			Group by f1.DestinationAirportId
		--1.2 for total dest apt (#8.a)
		Insert into @tbl_desApts(DestinationStationId, Volume)
			Select -1, SUM(Volume)
			From @tbl_desApts

		--2 prob of at least of one exportation(ppt #9)
		Update @tbl_desApts 
			Set MinProb=1-POWER((1-@MinPrevelance), Volume),
				MaxProb=1-POWER((1-@MaxPrevelance), Volume)
		--2.2 only keep >0.01 (ppt #10)
		Delete from @tbl_desApts Where MaxProb<0.01 and DestinationStationId<>-1

		--3 Insert into main table
		If Exists (Select 1 from @tbl_desApts)
		Begin
			--3.1 save prob
			Insert into zebra.DiseaseEventDestinationAirport
					(DiseaseId, DestinationStationId, Volume, MinProb, MaxProb)
				Select @DiseaseId, DestinationStationId, Volume, MinProb, MaxProb
				From @tbl_desApts
			--3.2 expected travelers (ppt #10)
			Update zebra.DiseaseEventDestinationAirport 
				Set MinExpVolume=@MinPrevelance*Volume,
					MaxExpVolume=@MaxPrevelance*Volume
				Where DiseaseId=@DiseaseId;

			--/*4. Destination grids*/
			Insert into zebra.DiseaseEventDestinationGrid(GridId, DiseaseId)
				Select Distinct f1.GridId, @DiseaseId
				From [zebra].[GridStation] as f1, zebra.DiseaseEventDestinationAirport as f2
				Where f2.DiseaseId=@DiseaseId and
					MONTH(f1.ValidFromDate)=@endMth and f2.DestinationStationId>0
					and f1.Probability>0.1 and f2.DiseaseId=@DiseaseId
					and f1.StationId=f2.DestinationStationId
		End

		Select 1 as Result
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;

END