-- =============================================
-- Author:		Kevin
-- Create date: 2019-05
-- Description:	Gets the first login dates of users
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetFirstLoginDateByUser]
	@userId nvarchar(128) = NULL
AS
BEGIN
	select u.Id, u.UserName, lt.LoginDateTime as FirstLoginDate
	from [dbo].[AspNetUsers] as u
	left join (
		select ult.UserId, min(ult.LoginDateTime) as LoginDateTime
		from [dbo].[UserLoginTrans] as ult
		WHERE @userId is NULL or ult.UserId = @userId
		group by ult.UserId
	) as lt on u.Id = lt.UserId
	WHERE @userId is NULL or u.Id = @userId
	order by lt.LoginDateTime asc
END
