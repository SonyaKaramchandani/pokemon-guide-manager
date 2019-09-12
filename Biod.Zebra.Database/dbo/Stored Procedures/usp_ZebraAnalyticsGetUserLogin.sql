-- =============================================
-- Author:		Kevin
-- Create date: 2019-06
-- Description:	Gets the user login history
-- =============================================
CREATE PROCEDURE [dbo].[usp_ZebraAnalyticsGetUserLogin]
	@userId nvarchar(128) = NULL,
	@startDate datetimeoffset(7) = NULL,
	@endDate datetimeoffset(7) = NULL
AS
BEGIN
	select *
	from [dbo].[UserLoginTrans] as ult
	WHERE (@userId is NULL or ult.UserId = @userId)
		and (@startDate is null or ult.LoginDateTime >= @startDate)
		and (@endDate is null or ult.LoginDateTime < @endDate)
	order by ult.UserLoginTransID asc
END
