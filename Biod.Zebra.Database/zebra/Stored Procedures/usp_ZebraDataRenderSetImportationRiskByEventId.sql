
-- =============================================
-- Author:		Vivian
-- Create date: 2019-04 
-- Description:	Calculate importation risks when an event is published
-- Output: 1-success, 0-event is local only, -1-failed
-- Modified: change user location to AOIs
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetImportationRiskByEventId
	@EventId as int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		Delete from zebra.EventImportationRisksByUser Where EventId=@EventId
		--can spread
		If (Select IsLocalOnly From surveillance.Event Where EventId=@EventId)=0
		Begin --1
			--go through each subscribedUsers user
			Declare @tbl_users table (UserId nvarchar(128), RankId int);
			With T1 as (
				Select Id as UserId From dbo.AspNetUsers
				Except
				Select f1.UserId
				From dbo.AspNetUserRoles as f1, dbo.AspNetRoles as f2
				Where f2.Name='UnsubscribedUsers' and f1.RoleId=f2.Id
				),
			T2 as (
				select UserId, ROW_NUMBER() OVER ( order by UserId) as rankId
				From T1
				)
			Insert into @tbl_users(UserId, RankId)
				select Id, ROW_NUMBER() OVER ( order by Id) as RankId
				From [dbo].[AspNetUsers]
			--loop
			Declare @UserGeonameIds AS varchar(256)
			Declare @i int=1, @thisUserId nvarchar(128)
			Declare @maxRankId int=(Select Max(RankId) From @tbl_users)
			Declare @tbl_imp table (localSpread int, MinProbability decimal(5,4),  
						MaxProbability decimal(5,4), MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3),
						ImportationPriorityTitle varchar(20), ImportationProbabilityName varchar(30), NumberOfAois int)

			While @i<=@maxRankId
			Begin --2
				select @thisUserId=UserId from @tbl_users where RankId=@i;
				Select @UserGeonameIds=AoiGeonameIds
					From [dbo].[AspNetUsers] Where Id=@thisUserId
				--Use user profile location when Aoi is empty (it won't happen "as the AOI will not be empty" dev-zebra 22May2019)
				--If @UserGeonameIds=''
				--	Select @UserGeonameIds=GeonameId
				--		From [dbo].[AspNetUsers] Where Id=@thisUserId

				--gets all info from risk SP
				Insert into @tbl_imp
				EXEC [zebra].usp_ZebraEventGetImportationRisk @EventId, @UserGeonameIds
				--insert the necessary one
				Insert into zebra.EventImportationRisksByUser 
						(UserId, localSpread, EventId, MinProb, MaxProb, MinVolume, MaxVolume)
					Select @thisUserId, localSpread, @EventId, 
						ISNULL(MinProbability,0), ISNULL(MaxProbability,0), 
						ISNULL(MinExpTravelers,0), ISNULL(MaxExpTravelers,0)
					From @tbl_imp

				Delete from @tbl_imp
				set @i=@i+1

			End; --2
			
			Select 1 as Result
		End --1
		Else --Can not spread
			Select 0 as Result
	--action!
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;

END