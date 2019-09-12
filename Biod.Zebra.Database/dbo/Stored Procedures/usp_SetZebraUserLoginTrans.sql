
-- =============================================
-- Author:		Vivian
-- Create date: 2018-10 
-- Description:	Input: UserID
--				Inserts into login track table
-- =============================================
CREATE PROCEDURE dbo.usp_SetZebraUserLoginTrans
	@UserId    AS NVARCHAR (128)
AS
BEGIN
	SET NOCOUNT ON;
		IF exists (Select 1 from [dbo].[AspNetUsers] where Id=@UserId)
		BEGIN
			INSERT INTO [dbo].[UserLoginTrans]([UserId])
				VALUES(@UserID)

		END
END