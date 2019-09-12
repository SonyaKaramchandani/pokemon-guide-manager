-- =============================================
-- Author:		Kevin
-- Create date: 2019-06
-- Description:	Gets the last modified date of the user
-- =============================================
CREATE PROCEDURE [dbo].[usp_ZebraAnalyticsGetUserLastModifiedDate]
	@userId nvarchar(128)
AS
BEGIN
	select top(1) utl.UserId, utl.ModifiedUTCDatetime as ModifiedDate
	from [dbo].[UserTransLog] as utl
	where utl.UserId = @userId
	order by utl.ModifiedUTCDatetime desc
END
