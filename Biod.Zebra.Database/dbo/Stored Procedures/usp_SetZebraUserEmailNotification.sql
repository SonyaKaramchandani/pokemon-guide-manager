
-- =============================================
-- Author:		Kevin
-- Create date: 2019-105
-- Description:	Inserts into the 
-- =============================================
CREATE PROCEDURE [dbo].[usp_SetZebraUserEmailNotification]
	@userId	       NVARCHAR(128),
	@AoiGeonameIds VARCHAR(256),
	@userEmail     NVARCHAR(256),
	@emailType     INT,
	@eventId       INT = NULL,
	@content       NVARCHAR(MAX),
	@sentDate      DATETIMEOFFSET,
	@title         VARCHAR(256),
	@summary       NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	IF	exists (Select 1 from [dbo].[AspNetUsers] where Id=@userId) 
	AND exists (Select 1 from [dbo].[UserEmailType] where Id=@emailType) 
	AND (@eventId is null OR exists (Select 1 from [surveillance].[Event] where EventId=@eventId))
	BEGIN
		INSERT INTO [dbo].[UserEmailNotification] (UserId, AoiGeonameIds, UserEmail, EmailType, EventId, Content, SentDate, Title, Summary)
			VALUES(@userId, @AoiGeonameIds, @userEmail, @emailType, @eventId, @content, @sentDate, @title, @summary)
	END
END