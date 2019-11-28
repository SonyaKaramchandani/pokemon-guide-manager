
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	2nd part of pre-calculations, take output from part 1 plus # of cases with confidence interval
--				Calculate source airport part
--				Output MinCaseOverPopulationSize, MaxCaseOverPopulationSize, DiseaseIncubation, DiseaseSymptomatic, EventStart, EventEnd
-- Modification 21Jun2019: change end of event length from Enddate to date_of_last_reported_case
-- 2019-07 name changed
-- 2019-09: disease schema change
-- 2019-11: calculate DiseaseIncubation/DiseaseSymptomatic from seconds to days
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart2
	@EventId INT,
	@EventGridCases nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
	BEGIN TRAN
		--input json to table
		Declare @tbl_eventGrids table (GridId nvarchar(12), Cases int,  MinCases int, MaxCases int)
		Insert into @tbl_eventGrids (GridId, Cases, MinCases, MaxCases)
		Select GridId, Cases, MinCases, MaxCases
		FROM OPENJSON(@EventGridCases)
			WITH (
				GridId nvarchar(12),
				Cases int,
				MinCases int,
				MaxCases int
				)

		--1. clean existing data, done in part1

		If Exists (Select 1 from @tbl_eventGrids)
		Begin
			Declare @SourceCatchmentThreshold decimal(5,2)=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='SourceCatchmentThreshold')
			--2. timeline and disease
			Declare @startDate Date, @endDate Date, @endMth int
			Declare @diseaseId int

			Select @startDate=StartDate, @diseaseId=DiseaseId
			from surveillance.[Event] Where EventId=@EventId;

			Set @endDate= (Select MAX(EventDate) From surveillance.Xtbl_Event_Location Where EventId=@EventId)
		
			Set @endMth=MONTH(@endDate);

			--3 airport catchment (source)
			Declare @tbl_sourceApt table (SourceAptId int, Probability float, [Population] int,
								minCaseOverPop float, maxCaseOverPop float)
			--3.1 source apts cross grid
			Declare @tbl_sourceGridApt table (GridId nvarchar(12), SourceAptId int, Probability decimal(10,8))
			Insert into @tbl_sourceGridApt(GridId, SourceAptId, Probability)
			Select f1.GridId, f1.[StationId], f1.Probability
				From [zebra].[GridStation] as f1, @tbl_eventGrids as f2
				Where MONTH(ValidFromDate)=@endMth and f1.Probability>=@SourceCatchmentThreshold
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
					and f2.gridId=f3.GridId  and f3.Probability>=@SourceCatchmentThreshold
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
			Insert into [zebra].[EventSourceAirport](EventId, [SourceStationId], [StationName]
							,[StationCode], [CityDisplayName], [CountryName], [NumCtryAirports]
							,[Volume], [CtryRank], [WorldRank], Probability)
				Select @EventId, f1.SourceAptId, f2.StationGridName, f2.[StationCode], f4.DisplayName
						,f5.DisplayName, f3.[NumCtryAirports], f3.OutboundVolume, f3.[CtryRank], f3.[WorldRank]
						,f1.Probability
				From @tbl_sourceApt as f1 
					INNER JOIN [zebra].[Stations] as f2 ON f1.SourceAptId=f2.StationId 
					INNER JOIN [zebra].[AirportRanking] as f3 ON f1.SourceAptId=f3.StationId
					Left JOIN [place].[Geonames] as f4 ON f2.CityGeonameId=f4.GeonameId
					Left JOIN [place].[Geonames] as f5 ON f3.CtryGeonameId=f5.GeonameId --countryGeonameId not in stations api
					Where MONTH(f3.EndDate)=@endMth


			Select ISNULL(@minCaseOverPop, -1.0) as MinCaseOverPopulationSize, 
				ISNULL(@maxCaseOverPop, -1.0) as MaxCaseOverPopulationSize, 
				Case 
					When IncubationAverageSeconds IS NULL Then 1
					When IncubationAverageSeconds/86400<1 Then 1
					Else ROUND(IncubationAverageSeconds/86400.0, 2)
				End As DiseaseIncubation,
				Case
					When SymptomaticAverageSeconds IS NULL Then 0
					Else ROUND(SymptomaticAverageSeconds/86400.0, 2)
				End As DiseaseSymptomatic, 
				@startDate as EventStart, @endDate as EventEnd
			From [disease].[Diseases] as f0 Left Join [disease].DiseaseSpeciesIncubation as f1 On f0.DiseaseId=f1.DiseaseId and f1.SpeciesId=1
				Left Join disease.DiseaseSpeciesSymptomatic as f2 On f0.DiseaseId=f2.DiseaseId and f2.SpeciesId=1
			Where f0.DiseaseId=@diseaseId 

		End

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