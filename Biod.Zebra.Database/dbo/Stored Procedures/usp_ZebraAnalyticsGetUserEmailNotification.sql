-- =============================================
-- Author:		Kevin
-- Create date: 2019-06
-- Description:	Gets the emails based on the query
-- =============================================
CREATE PROCEDURE [dbo].[usp_ZebraAnalyticsGetUserEmailNotification]
	@userId nvarchar(128) = NULL,
	@email nvarchar(256) = NULL,
	@emailType int = NULL,
	@startDate datetimeoffset(7) = NULL,
	@endDate datetimeoffset(7) = NULL
AS
BEGIN
	select Id, UserId, AoiGeonameIds, UserEmail, EmailType, EventId, SentDate
	from [dbo].[UserEmailNotification]
	WHERE (@userId is NULL or UserId = @userId)
		and (@email is null or UserEmail = @email)
		and (@emailType is null or EmailType = @emailType)
		and (@startDate is null or SentDate >= @startDate)
		and (@endDate is null or SentDate < @endDate)
	order by Id asc
END
