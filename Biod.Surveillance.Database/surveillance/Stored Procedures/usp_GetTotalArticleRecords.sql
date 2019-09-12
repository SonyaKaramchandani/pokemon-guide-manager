
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-08 
-- Description:	returns total number of row in usp_GetParentArticleList w/o paging (helps for # of pages)
-- 2018-09: added @SearchString
-- =============================================

create PROCEDURE surveillance.usp_GetTotalArticleRecords
	@ArticleType varchar(20), --all, unprocessed, spam
	@SearchString varchar(200) = ''
AS
BEGIN
	SET NOCOUNT ON;
		Declare @startDate Datetime
		--@startDate
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

		--count rows
		Declare @numWithCluster int, @numNoCluster int;
		--1. sort aritcles with SimilarClusterId
		With T1 as (
			Select ArticleId,  
				ROW_NUMBER() OVER (PARTITION BY SimilarClusterId ORDER BY FeedPublishedDate DESC) AS rn
			From [surveillance].[ProcessedArticle]
			Where SimilarClusterId IS NOT NULL AND SimilarClusterId>0
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
				-- SearchString
				AND (@SearchString='' or CHARINDEX(@SearchString, [ArticleTitle])>0)
			)
		Select @numWithCluster=count(*) From T1 Where rn=1
		--2. no SimilarCluster
		Select @numNoCluster=count(*) 
		From [surveillance].[ProcessedArticle]
		Where (SimilarClusterId IS NULL OR SimilarClusterId<=0)
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
			-- SearchString
			AND (@SearchString='' or CHARINDEX(@SearchString, [ArticleTitle])>0)


	Select @numWithCluster + @numNoCluster as TotalRecords
	
END