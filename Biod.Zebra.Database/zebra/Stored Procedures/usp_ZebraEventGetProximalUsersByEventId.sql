
-- =============================================
-- Author:		Vivian
-- Create date: 2019-11 
-- Description:	simplified version of usp_ZebraEventGetProximalUsersByEvent
-- Input: EventId
-- Output: Return local spread list of UserId, GeonameId(event)
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetProximalUsersByEventId
	@EventId    AS INT
AS
BEGIN
	SET NOCOUNT ON;

	Declare @diseaseId int=(Select DiseaseId from [surveillance].[Event] Where EventId=@EventId)
	--Event locations
	Declare @tbl_eventLocations table (GeonameId int, SeqId int)
	Insert into @tbl_eventLocations(GeonameId)
		Select GeonameId
		From surveillance.Xtbl_Event_Location
		Where EventId=@EventId
		
	If exists (Select 1 from @tbl_eventLocations)
	Begin --1
		--User X EventLoc
		Declare @tbl_UserIdEventLocation table (UserId nvarchar(128), EventGeonameId int);

		--1.1 loop event locations
		With T1 as (
			select GeonameId, ROW_NUMBER() OVER ( order by GeonameId) as SeqId
			from @tbl_eventLocations
			)
		Update @tbl_eventLocations 
			Set SeqId=T1.SeqId
			From @tbl_eventLocations as f1, T1
			Where f1.GeonameId=T1.GeonameId
			
		--1.2 find local users
		Declare @i int=1
		Declare @maxSeqId int =(Select Max(SeqId) From @tbl_eventLocations)
		Declare @thisLocation int
		While @i<=@maxSeqId
		Begin
			Set @thisLocation=(Select GeonameId From @tbl_eventLocations Where SeqId=@i)
			Insert into @tbl_UserIdEventLocation(UserId, EventGeonameId)
				Select UserId, @thisLocation
				From bd.ufn_ZebraGetLocalUserLocationsByGeonameId(@thisLocation, 1, 1, 1, @diseaseId)
				
			Set @i=@i+1
		End

		--1.3 Output
		Select distinct f1.UserId, f1.EventGeonameId as GeonameId
			From @tbl_UserIdEventLocation as f1, dbo.AspNetUsers as f2, @tbl_eventLocations as f3
			Where f1.UserId=f2.Id and f1.EventGeonameId=f3.GeonameId
	End --1
	Else
		Select TOP (0) '-' as UserId, 0 as GeonameId
END