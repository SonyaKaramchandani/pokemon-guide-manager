
-- =============================================
-- Author:		Vivian
-- Create date: 2019-04 
-- Description:	Calculate importation risks when a user is registered or user's location changed
-- Output: 1-success, 0-userId not exist or no location, -1-failed
-- 2019-07 name changed
-- 2019-09 include IsLocalOnly events
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetImportationRiskByUserId
	@UserId as nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		Delete from zebra.EventImportationRisksByUser Where UserId=@UserId
		--User loc info
		Declare @UserGeonameIds AS varchar(256)
		Select @UserGeonameIds=AoiGeonameIds
			From [dbo].[AspNetUsers] Where Id=@UserId

		If @UserGeonameIds IS NOT NULL
		Begin --1
			--events with sequence
			Declare @tbl_events table (EventId int, RankId int)
			Insert into @tbl_events(EventId, RankId)
				select [EventId], ROW_NUMBER() OVER ( order by [EventId]) as RankId
				From surveillance.Event

			--loop
			Declare @i int=1, @thisEventId int
			Declare @maxRankId int=(Select Max(RankId) From @tbl_events)
			Declare @tbl_imp table (LocalSpread int, MinProbability decimal(5,4),  
						MaxProbability decimal(5,4), MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3),
						ImportationPriorityTitle varchar(20), ImportationProbabilityName varchar(30), NumberOfAois int)

			While @i<=@maxRankId
			Begin --2

				select @thisEventId=[EventId] from @tbl_events where RankId=@i;

				--gets all info from risk SP
				Insert into @tbl_imp
				EXEC [zebra].usp_ZebraEventGetImportationRisk @thisEventId, @UserGeonameIds
				--insert the necessary one
				Insert into zebra.EventImportationRisksByUser 
						(UserId, LocalSpread, EventId, MinProb, MaxProb, MinVolume, MaxVolume)
					Select @UserId, LocalSpread, @thisEventId, 
						ISNULL(MinProbability,0), ISNULL(MaxProbability,0), 
						ISNULL(MinExpTravelers,0), ISNULL(MaxExpTravelers,0)
					From @tbl_imp

				Delete from @tbl_imp
				set @i=@i+1

			End; --2
			
			Select 1 as Result
		End --1
		Else
			Select 0 as Result
	--action!
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;
END