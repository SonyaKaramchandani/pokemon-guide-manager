
-- =============================================
-- Author:		Basam
-- Create date: 2019-06 
-- Description:	Input: IPAddress
--				Inserts into AppRequestIPLog table
-- =============================================
CREATE PROCEDURE dbo.usp_SetZebraRequestInfo
	@IPAddress AS NVARCHAR (MAX),
	@IsPrivateIpAddress AS BIT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[AppRequestInfoLog]([RequestIPAddress], [IsPrivateIpAddress])
				VALUES(@IPAddress, @IsPrivateIpAddress)
END