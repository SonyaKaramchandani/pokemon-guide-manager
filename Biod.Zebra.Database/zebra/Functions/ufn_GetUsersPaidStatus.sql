
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2019-06  
-- Description:	Input: None
--				Output: A list of SubscribedUsers with paid/unpaid
-- Modified: 2020-01 removes Subscribed filter
-- =============================================

CREATE FUNCTION zebra.ufn_GetUsersPaidStatus()
RETURNS @returnResults TABLE (UserId nvarchar(128), IsPaidUser bit)
AS
BEGIN
	
	With T2 as (
		Select distinct f1.UserId, 1 as IsPaidUser
		From dbo.AspNetUserRoles as f1, dbo.AspNetRoles as f2
		Where f2.Name='PaidUsers' and f1.RoleId=f2.Id
		)
	Insert into @returnResults(UserId, IsPaidUser)
		Select T1.Id as UserId, ISNULL(T2.IsPaidUser, 0) as IsPaidUser
		From dbo.AspNetUsers as T1 Left join T2 on T1.Id=T2.UserId
	
	Return
END