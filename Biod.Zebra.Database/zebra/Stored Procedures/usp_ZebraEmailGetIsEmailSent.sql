
-- =============================================
-- Author:		Rahnuma Kazi
-- Create date: 2019-05 
-- Description:	Return boolean 
-- 2019-07 name changed			
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEmailGetIsEmailSent
	  @EventId int,
	  @EmailType int, 
	  @Email nvarchar(50),
	  @AoiGeonameIds VARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON
	IF exists (Select 1 from [dbo].[UserEmailNotification] 
	           where EventId = @EventId AND EmailType = @EmailType AND UserEmail= @Email AND AoiGeonameIds = @AoiGeonameIds)
		Select ISNULL(CAST(1 AS BIT), 1) AS IsExists
		
	Else
		Select ISNULL(CAST(0 AS BIT),0) AS IsExists
END
