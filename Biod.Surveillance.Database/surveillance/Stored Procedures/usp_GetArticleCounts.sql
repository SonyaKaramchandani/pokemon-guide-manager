
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-09 
-- Description:	Show count of all/unprocessed articles of last year of FeedPublishedDate
--				and spam articles of last month of max(SystemLastModifiedDate, UserLastModifiedDate)
--					in UserLastModifiedDate and (SystemLastModifiedDate and UserLastModifiedDate=NULL)
-- =============================================

create PROCEDURE surveillance.usp_GetArticleCounts
AS
BEGIN
	SET NOCOUNT ON;
		--process start date
		Declare @startDate_all Datetime,  @startDate_unprocessed Datetime,  @startDate_ham Datetime
		--1. all
		Set @startDate_all= (Select DATEADD(year, -1, MAX(FeedPublishedDate)) 
				From [surveillance].[ProcessedArticle]
				Where HamTypeId<>1);
		--2. unprocessed
		Set @startDate_unprocessed= (Select DATEADD(year, -1, MAX(FeedPublishedDate)) 
				From [surveillance].[ProcessedArticle]
				Where HamTypeId<>1 AND (IsCompleted IS NULL or IsCompleted=0));
		--3. spam
		Declare @MaxDateUser datetime, @MaxDateSystem datetime, @MaxDate datetime
		--max date of two
		select @MaxDateUser=max([UserLastModifiedDate]), @MaxDateSystem=max([SystemLastModifiedDate])
			From [surveillance].[ProcessedArticle]
			Where HamTypeId=1
		--max of all
		Set @MaxDate=@MaxDateSystem
		If @MaxDateUser IS NOT NULL AND @MaxDateUser>@MaxDateSystem
			Set @MaxDate=@MaxDateUser
		--one month before
		Set @startDate_ham= DATEADD(MONTH, -1, @MaxDate) 
		
		--counts
		Declare @allCount int, @unprocessedCount int, @spamCount int
		--all
		Select @allCount=Count(*) 
		From [surveillance].[ProcessedArticle]
		Where HamTypeId<>1 AND FeedPublishedDate>=@startDate_all

		--unprocessed
		Select @unprocessedCount=Count(*) 
		From [surveillance].[ProcessedArticle]
		Where HamTypeId<>1 AND FeedPublishedDate>=@startDate_unprocessed 
			and (IsCompleted IS NULL or IsCompleted=0)

		--spam
		Select @spamCount=Count(*) 
		From [surveillance].[ProcessedArticle]
		Where HamTypeId=1 
			AND (UserLastModifiedDate IS NOT NULL and UserLastModifiedDate>=@startDate_ham
				or UserLastModifiedDate IS NULL and SystemLastModifiedDate>=@startDate_ham)

		Select @allCount as AllCount, @unprocessedCount as UnprocessedCount, @spamCount as SpamCount
	
END