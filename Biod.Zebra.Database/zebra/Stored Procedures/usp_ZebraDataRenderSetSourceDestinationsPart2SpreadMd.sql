
-- =============================================
-- Author:		Vivian
-- Create date: 2019-12 
-- Description:	2nd part of pre-calculations, take output from part 1 plus # of cases with confidence interval.
--				Calculate source airport part, save apt info and CaseOverPop in [EventSourceAirportSpreadMd].
--				Pass R to calculate prevalance of each source apt
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd
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
		
		--calculates source airports
		If Exists (Select 1 from @tbl_eventGrids)
		Begin
			Declare @SourceCatchmentThreshold decimal(5,2)
				=(Select Top 1 [Value] From [bd].[ConfigurationVariables] 
					Where [Name]='SourceCatchmentThreshold')
			Declare @endMth int
				= (Select MONTH(MAX(EventDate)) From surveillance.Xtbl_Event_Location Where EventId=@EventId)
			
			--1 source apts cross grid
			Declare @tbl_sourceGridApt table (GridId nvarchar(12), SourceAptId int, Probability decimal(10,8))
			Insert into @tbl_sourceGridApt(GridId, SourceAptId, Probability)
			Select f1.GridId, f1.[StationId], f1.Probability
				From [zebra].[GridStation] as f1, @tbl_eventGrids as f2
				Where MONTH(ValidFromDate)=@endMth and f1.Probability>=@SourceCatchmentThreshold
					and f1.GridId=f2.GridId 
			--2 source apts
			Declare @tbl_sourceApt table 
				(SourceAptId int, [Population] int, minCaseOverPop float, maxCaseOverPop float)
			--1.1 calculate pop size 
			Insert into @tbl_sourceApt(SourceAptId, [Population])
				Select f1.SourceAptId, sum(f2.[population]*f1.Probability) as pop
				From @tbl_sourceGridApt as f1, bd.HUFFMODEL25KMWORLDHEXAGON as f2
				Where f1.GridId=f2.GridId
				Group by f1.SourceAptId;
			--1.2 calculate min/maxCaseOverPop
			--each apt
			With T1 as (
				Select f1.SourceAptId, sum(f2.MinCases*f1.Probability) as minCP, 
										sum(f2.MaxCases*f1.Probability) as maxCP
				From @tbl_sourceGridApt as f1, @tbl_eventGrids as f2
				Where f1.GridId=f2.GridId
				Group by f1.SourceAptId
				)
			Update @tbl_sourceApt Set minCaseOverPop=T1.minCP/f1.[Population],
								maxCaseOverPop=T1.maxCP/f1.[Population]
				From @tbl_sourceApt as f1, T1
				Where f1.SourceAptId=T1.SourceAptId and f1.[Population]<>0
			--for pop=0
			Update @tbl_sourceApt Set minCaseOverPop=-1.0, maxCaseOverPop=-1.0
				Where [Population]=0
			--1.3 insert all columns needed
			Insert into [zebra].[EventSourceAirportSpreadMd](EventId, [SourceStationId], [StationName]
							,[StationCode], [CityDisplayName], [CountryName], [NumCtryAirports]
							,[Volume], [CtryRank], [WorldRank], MinCaseOverPop, MaxCaseOverPop)
				Select @EventId, f1.SourceAptId, f2.StationGridName, f2.[StationCode], f4.DisplayName
						,f5.DisplayName, f3.[NumCtryAirports], f3.OutboundVolume, f3.[CtryRank], f3.[WorldRank]
						,f1.minCaseOverPop, f1.maxCaseOverPop
				From @tbl_sourceApt as f1 
					INNER JOIN [zebra].[Stations] as f2 ON f1.SourceAptId=f2.StationId 
					INNER JOIN [zebra].[AirportRanking] as f3 ON f1.SourceAptId=f3.StationId
					Left JOIN [place].[Geonames] as f4 ON f2.CityGeonameId=f4.GeonameId
					Left JOIN [place].[Geonames] as f5 ON f3.CtryGeonameId=f5.GeonameId --countryGeonameId not in stations api
					Where MONTH(f3.EndDate)=@endMth

			--1 means something inserted
			Select 1 as Result
		End
		Else
			--0 means input @EventGridCases is empty
			Select 0 as Result

	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to execute usp_ZebraDataRenderSetSourceDestinationsPart2SpreadMd. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
END