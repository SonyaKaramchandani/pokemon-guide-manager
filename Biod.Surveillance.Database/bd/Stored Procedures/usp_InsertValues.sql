
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-04 
-- Description:	Some table values are pre-set, fixed
-- =============================================

create PROCEDURE bd.usp_InsertValues 
AS
BEGIN
	SET NOCOUNT ON;
	Declare @n int

	--ArticleFeed
	Set @n=(select count(*) from [surveillance].ArticleFeed)
	If @n=0  
		Insert into [surveillance].ArticleFeed Values(1, 'ProMED'), (2, 'WHO'), (3, 'GDELT')

	--HamType
	Set @n=(select count(*) from [surveillance].HamType)
	If @n=0  
		Insert into [surveillance].HamType Values(1, 'Spam'), (2, 'Disease information'), (3, 'Disease activity')

	--EventPriorities
	Set @n=(select count(*) from [surveillance].[EventPriorities])
	If @n=0  
		Insert into [surveillance].[EventPriorities](PriorityId, PriorityTitle)
			Values(1, 'Low'), (2, 'Medium'), (3, 'High')

	--[EventCreationReasons]
	Set @n=(select count(*) from [surveillance].[EventCreationReasons])
	If @n=0  
		Insert into [surveillance].[EventCreationReasons](ReasonId, ReasonName)
			Values(1, 'Outbreak reported'), (2, 'International spread likely'), (3, 'Local spread likely'),
			(4, 'No control measures'), (5, 'New location'), (6, 'New pathogen'), (7, 'Vector in new location'),
			(8, 'New pathology'), (9, 'New population affected'), (10, 'Widespread zoonotic potential'), 
			(11, 'Potential biological weapon')

	
END