
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	3rd part of pre-calculations, take output from part 2 plus # of cases with confidence interval
--				calculate destination apts and destination grids, save to 3 tables
-- Modification 21Jun2019: change end of event length from Enddate to date_of_last_reported_case
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart3
	@EventId INT,
	@MinPrevelance float,
	@MaxPrevelance float
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		--1. clean existing data, done in part1

		--2. timeline and disease
		Declare @endMth int

		Set @endMth=(Select MONTH(MAX(EventDate)) From surveillance.Xtbl_Event_Location Where EventId=@EventId);
		
		--needed for V7
		Insert into zebra.EventPrevalence(EventId, MinPrevelance, MaxPrevelance, EventMonth)
			Select @EventId, @MinPrevelance, @MaxPrevelance, @endMth

		/*Destination apts*/
		Declare @tbl_desApts table(DestinationStationId INT, Volume INT, 
					MinProb float, MaxProb float)
		--1. total weighted passengers (ppt #8)
		--1.1 for each individual dest apt (#8.b)
		Insert into @tbl_desApts(DestinationStationId, Volume)
			Select f1.DestinationAirportId, ROUND(SUM(f1.Volume*f2.Probability), 0)
			From [zebra].[StationDestinationAirport] as f1, [zebra].[EventSourceAirport] as f2
			Where f2.EventId=@EventId and MONTH(f1.ValidFromDate)=@endMth 
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
			Insert into zebra.EventDestinationAirport
					(EventId, DestinationStationId, Volume, MinProb, MaxProb)
				Select @EventId, DestinationStationId, Volume, MinProb, MaxProb
				From @tbl_desApts
			--3.1 expected travelers (ppt #10)
			Update zebra.EventDestinationAirport 
				Set MinExpVolume=@MinPrevelance*Volume,
					MaxExpVolume=@MaxPrevelance*Volume
				Where [EventId]=@EventId;
			--3.2 insert more station info
			Update [zebra].EventDestinationAirport
			Set StationName=f2.StationGridName, StationCode=f2.StationCode,
				CityDisplayName=f3.DisplayName, Longitude=f2.Longitude, Latitude=f2.Latitude
			From [zebra].EventDestinationAirport as f1
				INNER JOIN [zebra].[Stations] as f2 ON f1.DestinationStationId=f2.StationId 
				Left JOIN [place].[Geonames] as f3 ON f2.CityGeonameId=f3.GeonameId
				Where f1.[EventId]=@EventId;

			/*4. Destination grids*/
			Declare @DestinationCatchmentThreshold decimal(5,2)=(Select Top 1 [Value] From [bd].[ConfigurationVariables] Where [Name]='DestinationCatchmentThreshold')
			Insert into zebra.EventDestinationGridV3(GridId, EventId)
				Select Distinct f1.GridId, @EventId
				From [zebra].[GridStation] as f1, zebra.EventDestinationAirport as f2
				Where f2.[EventId]=@EventId and
					MONTH(f1.ValidFromDate)=@endMth and f2.DestinationStationId>0
					and f1.Probability>=@DestinationCatchmentThreshold and f2.EventId=@EventId
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