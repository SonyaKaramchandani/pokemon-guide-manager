-- =============================================
-- Author:		Kevin
-- Create date: 2019-05
-- Description:	Gets the list of users given a Group Name
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUsersByGroup]
	@groupName nvarchar(256) = NULL
AS
BEGIN
	SELECT u.* from [dbo].[AspNetUsers] as u
	left JOIN [dbo].[UserGroup] as ug on ug.Id=u.UserGroupId
	WHERE @groupName is NULL or @groupName=ug.Name
END
