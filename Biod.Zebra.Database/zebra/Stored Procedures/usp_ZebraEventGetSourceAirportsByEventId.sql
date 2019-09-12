
-- =============================================
-- Author:		Vivian
-- Create date: 2018-08 
-- Description:	query source airports from zebra.EventSourceAirport
--				(V3)
-- Modification 21Jun2019: remove @endMth, not used in output
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetSourceAirportsByEventId
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;
	Declare @totalVolume INT, @totalAptsWorld int
	--total volumes from source (montly alreay applied)
	Select @totalVolume=sum(Volume) 
		From zebra.EventSourceAirport
		Where EventId=@EventId
	--total number of apts
	Set @totalAptsWorld=(Select Top 1 NumWorldAirports 
	From [zebra].[AirportRanking])

	If @totalVolume>0
	Begin
		Select CityDisplayName, StationName, StationCode, Volume, CtryRank, CountryName,
			NumCtryAirports, WorldRank, @totalAptsWorld as TotalAptsWorld, 
			@totalVolume as TotalVolume
		From zebra.EventSourceAirport
	    Where EventId=@EventId
		Order By Volume Desc
	End
	Else
		Select '-' as CityDisplayName, '-' as StationName, '-' as StationCode, 0 as Volume, 
			0 as CtryRank, '-' as CountryName, 0 as NumCtryAirports, 0 as WorldRank, 
			0 as TotalAptsWorld, CAST(0 as int) as TotalVolume

END