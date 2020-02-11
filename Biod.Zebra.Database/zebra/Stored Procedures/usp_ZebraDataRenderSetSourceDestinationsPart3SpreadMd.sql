
-- =============================================
-- Author:		Vivian
-- Create date: 2019-12 ~ 2020-01
-- Description:	3rd part of pre-calculations, take Prevelance in EventSourceAirportSpreadMd (calculated in R)
--				calculate risk values of source/destination apts and destination grids, save in 3 tables
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart3SpreadMd
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN

		--1. timeline
		Declare @endMth int=(Select MONTH(MAX(EventDate)) From surveillance.Xtbl_Event_Location Where EventId=@EventId);

		--2. risk values in source 
		--2.1 a source to a dest --8(individual), 9(b), 11(b)
		Insert into [zebra].[EventSourceDestinationRisk]
					([EventId], [SourceAirportId], [DestinationAirportId], 
					Volume, MinProb, MaxProb, MinExpVolume, MaxExpVolume)
			Select @EventId, f1.SourceStationId, f2.DestinationAirportId, f2.Volume,
				1-POWER((1-f1.MinPrevelance), f2.Volume), 
				1-POWER((1-f1.MaxPrevelance), f2.Volume), 
				f1.MinPrevelance*f2.Volume, 
				f1.MaxPrevelance*f2.Volume
			From [zebra].[EventSourceAirportSpreadMd] as f1, [zebra].[StationDestinationAirport] as f2
			Where f1.EventId=@EventId and MONTH(f2.ValidFromDate)=@endMth and f1.SourceStationId=f2.StationId
		--2.2 a source to all dest --9(a), 11(a), saves it in sourceApt table
		Update [zebra].[EventSourceAirportSpreadMd]
			Set MinProb=1-POWER((1-f1.MaxPrevelance), f2.OutboundVolume),
				MaxProb=1-POWER((1-f1.MinPrevelance), f2.OutboundVolume),
				MinExpVolume=f1.MaxPrevelance*f2.OutboundVolume,
				MaxExpVolume=f1.MinPrevelance*f2.OutboundVolume
			From [zebra].[EventSourceAirportSpreadMd] as f1, [zebra].[AirportRanking] as f2
			Where f1.EventId=@EventId and MONTH(f2.EndDate)=@endMth and f1.SourceStationId=f2.StationId
		
		--3. risk values in dest (also grand total) 
		Declare @tbl_desApts table(DestinationStationId INT, Volume INT, 
					MinProb float, MaxProb float, MinExpVolume float, MaxExpVolume float)
		--3.1 all to a dest, volume not weighted anymore --10(b)(from 9b), 12(b)(from 11b)
		Insert into @tbl_desApts
				(DestinationStationId, Volume, MinProb, MaxProb, MinExpVolume, MaxExpVolume)
			Select [DestinationAirportId], SUM(Volume), 
				1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(MinProb, 1)),0))),
				1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(MaxProb, 1)),0))),
				SUM(MinExpVolume), SUM(MaxExpVolume)
			From [zebra].[EventSourceDestinationRisk]
			Where EventId=@EventId
			Group by [DestinationAirportId]
		--3.2 all source to all dest --10(a)(from 9a) 12(a)(from 11a)
		Insert into zebra.EventExtensionSpreadMd ([EventId], [AirportsDestinationVolume], 
				[MinExportationProbabilityViaAirports], [MaxExportationProbabilityViaAirports],
				[MinExportationVolumeViaAirports],[MaxExportationVolumeViaAirports])
			Select @EventId,  SUM(f2.Volume),
				1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(f1.MinProb, 1)),0))),
				1 - EXP(SUM(ISNULL(LOG(1 - NULLIF(f1.MaxProb, 1)),0))),
				SUM(f1.MinExpVolume), SUM(f1.MaxExpVolume)
			From [zebra].[EventSourceAirportSpreadMd] as f1, @tbl_desApts as f2
			Where f1.EventId=@EventId 
			Group by f1.EventId
		--3.3 delete below threshold ones
		Declare @NotificationThreshold decimal(5,2)
			=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='NotificationThreshold')
		Delete from @tbl_desApts Where MaxProb<@NotificationThreshold
		--3.4 insert into main with more station info
		Insert into [zebra].EventDestinationAirportSpreadMd
				(EventId, DestinationStationId, Volume, MinProb, MaxProb, MinExpVolume, MaxExpVolume,
				StationName, StationCode, CityDisplayName, Longitude, Latitude)
			Select @EventId, f1.DestinationStationId, f1.Volume, f1.MinProb, f1.MaxProb, 
				f1.MinExpVolume, f1.MaxExpVolume, f2.StationGridName, f2.StationCode,
				f3.DisplayName, f2.Longitude, f2.Latitude
			From @tbl_desApts as f1
				LEFT JOIN [zebra].[Stations] as f2 ON f1.DestinationStationId=f2.StationId 
				LEFT JOIN [place].[Geonames] as f3 ON f2.CityGeonameId=f3.GeonameId

		/*4. Destination grids*/
		Declare @DestinationCatchmentThreshold decimal(5,2)
			=(Select Top 1 [Value] From [bd].[ConfigurationVariables] 
				Where [Name]='DestinationCatchmentThreshold')
		Insert into zebra.EventDestinationGridSpreadMd(GridId, EventId)
			Select Distinct f1.GridId, @EventId
			From [zebra].[GridStation] as f1, zebra.EventDestinationAirportSpreadMd as f2
			Where f2.[EventId]=@EventId and f2.DestinationStationId>0
				and MONTH(f1.ValidFromDate)=@endMth
				and f1.Probability>=@DestinationCatchmentThreshold
				and f1.StationId=f2.DestinationStationId

		Select 1 as Result
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;

END