
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-02 
-- Description:	calls place.usp_UpdateGeonames and surveillance.usp_UpdateArticleApi
--				Output: main switchboard, calls other SPs, finishes all updating
-- =============================================

create PROCEDURE surveillance.usp_GetChildArticleList
	@ArticleType varchar(20), --all, unprocessed, spam
	@ArticleId varchar(128),
	@SimilarClusterId decimal(20,2)
AS
BEGIN
	SET NOCOUNT ON;

		Declare @startDate Datetime
		--
		If @ArticleType in ('all', 'unprocessed')
			Set @startDate= (Select DATEADD(year, -1, MAX(FeedPublishedDate)) 
					From [surveillance].[ProcessedArticle]
					Where HamTypeId<>1 AND (@ArticleType='all' 
						OR @ArticleType='unprocessed' AND (IsCompleted IS NULL or IsCompleted=0)));
		ELSE If @ArticleType='spam'
		BEGIN
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
			Set @startDate= DATEADD(MONTH, -1, @MaxDate) 
		END;

	Select [ArticleId], [ArticleTitle], f1.[ArticleFeedId], f2.ArticleFeedName, [HamTypeId], [IsRead], [IsCompleted]
      ,[SimilarClusterId], format(FeedPublishedDate, 'yyyy-MM-dd') AS FeedPublishedDateToString
	From [surveillance].[ProcessedArticle] as f1, [surveillance].[ArticleFeed] as f2
	Where f1.[SimilarClusterId]= @SimilarClusterId
		AND (
			(@ArticleType='spam' AND HamTypeId=1 
			AND (UserLastModifiedDate IS NOT NULL and UserLastModifiedDate>=@startDate
				or UserLastModifiedDate IS NULL and SystemLastModifiedDate>=@startDate)
				)--spam
			--all/unprocessed
			OR ((@ArticleType='unprocessed' and (IsCompleted IS NULL or IsCompleted=0)
				OR @ArticleType='all')
				AND HamTypeId<>1 AND FeedPublishedDate>=@startDate
				)
			)
		AND f1.[ArticleId]<>@ArticleId AND f1.ArticleFeedId=f2.ArticleFeedId
	Order by FeedPublishedDate
	
	
END