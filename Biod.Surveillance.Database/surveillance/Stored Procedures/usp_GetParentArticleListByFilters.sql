
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-09 
-- Description:	usp_GetParentArticleList + filters
-- =============================================

create PROCEDURE surveillance.usp_GetParentArticleListByFilters
	@ArticleType varchar(20), --all, unprocessed, spam, never be empty
	@PageStart int, --(first row - 1)
	@PageLength int,--page size
	@StartDate datetime='1900-01-01', 
	@EndDate  datetime='1900-01-01', 
	@HamTypeId varchar(20), -- never be empty
	@ArticleSourceFeedIds varchar(20) = '', --maybe empty
	@DiseaseIds varchar(100) = '', --maybe empty
	@LocationIds varchar(100) = '', --maybe empty
	@SearchString varchar(200) = ''
AS
BEGIN
	SET NOCOUNT ON;
		--1. process input filters
		--tables to hold input hamType
		Declare @tbl_HamType table (HamTypeId int)
		If @HamTypeId<>'' --actuall never be empty
			Insert into @tbl_HamType
			Select item From [bd].[ufn_StringSplit](@HamTypeId, ',')
		--tables to hold input ArticleSource
		Declare @tbl_ArticleSource table (ArticleSourceId int)
		If @ArticleSourceFeedIds<>''
			Insert into @tbl_ArticleSource
			Select item From [bd].[ufn_StringSplit](@ArticleSourceFeedIds, ',')
		--tables to hold input Disease
		Declare @tbl_Disease table (DiseaseId int)
		If @DiseaseIds<>''
			Insert into @tbl_Disease
			Select item From [bd].[ufn_StringSplit](@DiseaseIds, ',')
		--tables to hold input Location
		Declare @tbl_Location table (LocationId int)
		If @LocationIds<>''
			Insert into @tbl_Location
			Select item From [bd].[ufn_StringSplit](@LocationIds, ',')
		--1.2 StartDate
		If @StartDate='1900-01-01'
		BEGIN
			If @ArticleType in ('all', 'unprocessed')
				Set @startDate= (Select DATEADD(year, -1, MAX(FeedPublishedDate)) 
						From [surveillance].[ProcessedArticle]
						Where HamTypeId<>1 AND (@ArticleType='all' 
							OR @ArticleType='unprocessed' AND (IsCompleted IS NULL or IsCompleted=0)));
			Else If @ArticleType='spam'
			Begin
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
			End;
		END;
		
		--2. output
		--2.1 a tmp table before HasChildArticle
		Declare @tbl_results table (ArticleId varchar(128), ArticleTitle nvarchar(500), 
			SimilarClusterId decimal(20,2), IsCompleted bit, IsRead bit, FeedPublishedDate datetime,
			FeedPublishedDateToString char(10), ArticleFeedId int, ArticleFeedName varchar(50),
			MayHaveChildArticle bit);

		--1. sort aritcles with SimilarClusterId
		With T1 as (
			Select ArticleId, ArticleTitle, SimilarClusterId, IsCompleted, IsRead, FeedPublishedDate, ArticleFeedId, 
				ROW_NUMBER() OVER (PARTITION BY SimilarClusterId ORDER BY FeedPublishedDate DESC) AS rn
			From [surveillance].[ProcessedArticle]
			Where SimilarClusterId IS NOT NULL AND SimilarClusterId>0
				AND
				(
					(@ArticleType='spam' AND HamTypeId=1 
					AND (UserLastModifiedDate IS NOT NULL and UserLastModifiedDate>=@startDate
						or UserLastModifiedDate IS NULL and SystemLastModifiedDate>=@startDate)
					AND (@EndDate='1900-01-01' or 
							(UserLastModifiedDate IS NOT NULL and UserLastModifiedDate<=@EndDate
							or UserLastModifiedDate IS NULL and SystemLastModifiedDate<=@EndDate)
						)
					)--spam
				--all/unprocessed
				OR ((@ArticleType='unprocessed' and (IsCompleted IS NULL or IsCompleted=0)
					OR @ArticleType='all')
					AND HamTypeId in (select HamTypeId from @tbl_HamType) 
					AND FeedPublishedDate>=@startDate
					AND (@EndDate='1900-01-01' or FeedPublishedDate<=@EndDate)
					)
				)
				-- filter in ArticleSource
				AND (@ArticleSourceFeedIds='' or ArticleFeedId in (Select ArticleSourceId From @tbl_ArticleSource))
				-- filter in Disease
				AND (@DiseaseIds='' or ArticleId in 
						(Select ArticleId 
							From @tbl_Disease as f1, [surveillance].Xtbl_Article_Location_Disease as f2
							Where f1.DiseaseId=f2.DiseaseId))
				-- filter in Location
				AND (@LocationIds='' or ArticleId in 
						(Select ArticleId 
							From @tbl_Location as f1, [surveillance].Xtbl_Article_Location_Disease as f2
							Where f1.LocationId=f2.LocationGeoNameId))
				-- SearchString
				AND (@SearchString='' or CHARINDEX(@SearchString, [ArticleTitle])>0)
			),
		--2. first row of T1
		T2 as (
			Select * From T1 Where rn=1
			),
		--3. combine with no SimilarCluster
		T3 as (
			Select ArticleId, ArticleTitle, SimilarClusterId, IsCompleted, IsRead, FeedPublishedDate, ArticleFeedId,
				0 as MayHaveChildArticle 
			From [surveillance].[ProcessedArticle]
			Where (SimilarClusterId IS NULL OR SimilarClusterId<=0)
				AND 
				(--spam
					(@ArticleType='spam' AND HamTypeId=1 
					AND (UserLastModifiedDate IS NOT NULL and UserLastModifiedDate>=@startDate
						or UserLastModifiedDate IS NULL and SystemLastModifiedDate>=@startDate)
					AND (@EndDate='1900-01-01' or 
							(UserLastModifiedDate IS NOT NULL and UserLastModifiedDate<=@EndDate
							or UserLastModifiedDate IS NULL and SystemLastModifiedDate<=@EndDate)
						)
					)
				--all/unprocessed
				OR ((@ArticleType='unprocessed' and (IsCompleted IS NULL or IsCompleted=0)
					OR @ArticleType='all')
					AND HamTypeId in (select HamTypeId from @tbl_HamType) 
					AND FeedPublishedDate>=@startDate
					AND (@EndDate='1900-01-01' or FeedPublishedDate<=@EndDate)
					)
				)
				-- filter in ArticleSource
				AND (@ArticleSourceFeedIds='' or ArticleFeedId in (Select ArticleSourceId From @tbl_ArticleSource))
				-- filter in Disease
				AND (@DiseaseIds='' or ArticleId in 
						(Select Distinct ArticleId 
							From @tbl_Disease as f1, [surveillance].Xtbl_Article_Location_Disease as f2
							Where f1.DiseaseId=f2.DiseaseId))
				-- filter in Location
				AND (@LocationIds='' or ArticleId in 
						(Select Distinct ArticleId 
							From @tbl_Location as f1, [surveillance].Xtbl_Article_Location_Disease as f2
							Where f1.LocationId=f2.LocationGeoNameId))
				-- SearchString
				AND (@SearchString='' or CHARINDEX(@SearchString, [ArticleTitle])>0)
			Union
			Select ArticleId, ArticleTitle, SimilarClusterId, IsCompleted, IsRead, FeedPublishedDate, ArticleFeedId,
				1 as MayHaveChildArticle
			from T2
			)
		--save
		Insert into @tbl_results(ArticleId, ArticleTitle, SimilarClusterId, IsCompleted, IsRead,
			FeedPublishedDate,FeedPublishedDateToString, ArticleFeedId, ArticleFeedName, MayHaveChildArticle)
		Select ArticleId, ArticleTitle, SimilarClusterId, IsCompleted, IsRead, FeedPublishedDate,
				format(FeedPublishedDate, 'yyyy-MM-dd') AS FeedPublishedDateToString,
				T3.ArticleFeedId, f2.ArticleFeedName, MayHaveChildArticle
		From T3, [surveillance].[ArticleFeed] as f2
		Where T3.ArticleFeedId=f2.ArticleFeedId
		Order by FeedPublishedDate DESC
		OFFSET @PageStart ROWS FETCH NEXT @PageLength ROWS ONLY;
		-- #of articles in SimilarClusterId>1 --> HasChildArticle=1
		With T1 as (
			Select f1.SimilarClusterId, count(*) as c 
			From [surveillance].[ProcessedArticle] as f1,
				(Select distinct SimilarClusterId From @tbl_results Where MayHaveChildArticle=1) as f2
			Where f1.SimilarClusterId=f2.SimilarClusterId
			Group by f1.SimilarClusterId
		)
		Select ArticleId, ArticleTitle, f1.SimilarClusterId, IsCompleted, IsRead,
			FeedPublishedDate,FeedPublishedDateToString, ArticleFeedId, ArticleFeedName,
			Case When f1.MayHaveChildArticle=1 and T1.c>1 then CAST(1 AS BIT)
				Else CAST(0 AS BIT)
			End as HasChildArticle
		From @tbl_results as f1 LEFT JOIN T1
		ON convert(varchar(20), f1.SimilarClusterId)=convert(varchar(20), T1.SimilarClusterId)
		Order by FeedPublishedDate desc

	
END