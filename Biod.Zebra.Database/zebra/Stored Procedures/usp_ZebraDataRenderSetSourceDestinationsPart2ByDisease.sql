
-- =============================================
-- Author:		Vivian
-- Create date: 2019-09-30
-- Description:	similar as in usp_ZebraDataRenderSetSourceDestinationsPart2, takes diseaseId instead of evenId
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart2ByDisease
	@DiseaseId INT,
	@EventsGridCases nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
	BEGIN TRAN
		--input json to table
		Declare @tbl_eventGrids table (GridId nvarchar(12), Cases int,  MinCases int, MaxCases int)
		Insert into @tbl_eventGrids (GridId, Cases, MinCases, MaxCases)
		Select GridId, Cases, MinCases, MaxCases
		FROM OPENJSON(@EventsGridCases)
			WITH (
				GridId nvarchar(12),
				Cases int,
				MinCases int,
				MaxCases int
				)
		--process
		If Exists (Select 1 from @tbl_eventGrids)
		Begin
			--2. timeline and disease
			Declare @startDate Date, @endDate Date, @endMth int
			--is there any event only contains country location? need to consider?
			Select @startDate=MIN(StartDate)
			from surveillance.[Event] 
			Where DiseaseId=@DiseaseId and EndDate Is Null and IsLocalOnly=0 and [SpeciesId]=1
			--resume, need re-think
			Select @endDate= MAX(f1.EventDate)
			From surveillance.Xtbl_Event_Location as f1, surveillance.[Event] as f2
			Where f2.DiseaseId=@DiseaseId and f2.EndDate Is Null and IsLocalOnly=0 and f2.[SpeciesId]=1
				and f1.EventId=f2.EventId
		
			Set @endMth=MONTH(@endDate);
			Insert into zebra.DiseaseEventPrevalence(DiseaseId, EventMonth)
			Values(@DiseaseId, @endMth)

			--3 airport catchment (source)
			Declare @tbl_sourceApt table (SourceAptId int, Probability float, [Population] int,
								minCaseOverPop float, maxCaseOverPop float)
			--3.1 source apts cross grid
			Declare @tbl_sourceGridApt table (GridId nvarchar(12), SourceAptId int, Probability decimal(10,8))
			Insert into @tbl_sourceGridApt(GridId, SourceAptId, Probability)
			Select f1.GridId, f1.[StationId], f1.Probability
				From [zebra].[GridStation] as f1, @tbl_eventGrids as f2
				Where MONTH(ValidFromDate)=@endMth and f1.Probability>=0.1
					and f1.GridId=f2.GridId 
			--3.2 calculate prob for each source apt
			Declare @totalProb float=(Select sum(Probability) From @tbl_sourceGridApt)
			Insert into @tbl_sourceApt(SourceAptId, Probability)
				Select SourceAptId, sum(Probability)/@totalProb
				From @tbl_sourceGridApt
				Group by SourceAptId;
			--3.3 calculate pop size 
			With T1 as (
				Select f1.SourceAptId, sum(f2.[population]*f3.Probability) as pop
				From @tbl_sourceApt as f1, bd.HUFFMODEL25KMWORLDHEXAGON as f2, [zebra].[GridStation] as f3
				Where MONTH(f3.ValidFromDate)=@endMth and f1.SourceAptId=f3.StationId
					and f2.gridId=f3.GridId  and f3.Probability>=0.1
				Group by f1.SourceAptId
				)
			Update @tbl_sourceApt Set [Population]=T1.pop
				From @tbl_sourceApt as f1, T1
				Where f1.SourceAptId=T1.SourceAptId;
			--3.4 calculate min/maxCaseOverPop
			--each apt
			With T1 as (
				Select f1.SourceAptId, sum(f2.MinCases*f1.Probability) as minCP, 
										sum(f2.MaxCases*f1.Probability) as maxCP
				From @tbl_sourceGridApt as f1, @tbl_eventGrids as f2
				Where f1.GridId=f2.GridId
				Group by f1.SourceAptId
				)
			Update @tbl_sourceApt Set minCaseOverPop=f1.Probability*T1.minCP/f1.[Population],
								maxCaseOverPop=f1.Probability*T1.maxCP/f1.[Population]
				From @tbl_sourceApt as f1, T1
				Where f1.SourceAptId=T1.SourceAptId and f1.[Population]<>0
			--overal (to use in model)
			Declare @maxCaseOverPop float, @minCaseOverPop float
			Select @minCaseOverPop=sum(minCaseOverPop), @maxCaseOverPop=sum(maxCaseOverPop)
			From @tbl_sourceApt
			Where [Population]<>0
			--3.6 EventSourceAirport
			Insert into [zebra].DiseaseSourceAirport(DiseaseId, [SourceStationId], Probability)
				Select @DiseaseId, SourceAptId, Probability
				From @tbl_sourceApt 


			Select ISNULL(@minCaseOverPop, -1.0) as MinCaseOverPopulationSize, 
				ISNULL(@maxCaseOverPop, -1.0) as MaxCaseOverPopulationSize, 
				ISNULL(IncubationAverageDays, 1) as DiseaseIncubation, 
				ISNULL(SymptomaticAverageDays, 0) as DiseaseSymptomatic, 
				@startDate as EventStart, @endDate as EventEnd
			From [disease].[Diseases] as f0 
				Left Join [disease].DiseaseSpeciesIncubation as f1 On f0.DiseaseId=f1.DiseaseId and f1.SpeciesId=1
				Left Join disease.DiseaseSpeciesSymptomatic as f2 On f0.DiseaseId=f2.DiseaseId and f2.SpeciesId=1
			Where f0.DiseaseId=@diseaseId 
		End
		Else
			Select TOP(0) -1.0 as MinCaseOverPopulationSize, -1.0 as MaxCaseOverPopulationSize,
				-1 as DiseaseIncubation, -1 as DiseaseSymptomatic, 
				'1900-01-01' as EventStart, '1900-01-01' as EventEnd

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to execute usp_SetZebraSourceDestinationsV6Part2. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
END