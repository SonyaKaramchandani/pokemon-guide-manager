
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-09 
-- Description:	returns total number of row in usp_GetParentArticleListByFilters w/o paging (helps for # of pages)
-- =============================================

create PROCEDURE surveillance.usp_GetTotalArticleRecordsByFilters
	@ArticleType varchar(20), --all, unprocessed, spam, never be empty
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
		
		--count rows
		Declare @numWithCluster int, @numNoCluster int;

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
							(UserLastModifiedDate IS NOT NULL and CONVERT(date, UserLastModifiedDate)<=@EndDate
							or UserLastModifiedDate IS NULL and CONVERT(date, SystemLastModifiedDate)<=@EndDate)
						)
					)--spam
				--all/unprocessed
				OR ((@ArticleType='unprocessed' and (IsCompleted IS NULL or IsCompleted=0)
					OR @ArticleType='all')
					AND HamTypeId in (select HamTypeId from @tbl_HamType) 
					AND FeedPublishedDate>=@startDate
					AND (@EndDate='1900-01-01' or CONVERT(date, FeedPublishedDate)<=@EndDate)
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
			)
		Select @numWithCluster=count(*) From T1 Where rn=1
		--2. no SimilarCluster
		Select @numNoCluster=count(*) 
		From [surveillance].[ProcessedArticle]
		Where (SimilarClusterId IS NULL OR SimilarClusterId<=0)
			AND 
			(--spam
				(@ArticleType='spam' AND HamTypeId=1 
				AND (UserLastModifiedDate IS NOT NULL and UserLastModifiedDate>=@startDate
					or UserLastModifiedDate IS NULL and SystemLastModifiedDate>=@startDate)
				AND (@EndDate='1900-01-01' or 
						(UserLastModifiedDate IS NOT NULL and CONVERT(date, UserLastModifiedDate)<=@EndDate
						or UserLastModifiedDate IS NULL and CONVERT(date, SystemLastModifiedDate)<=@EndDate)
					)
				)
			--all/unprocessed
			OR ((@ArticleType='unprocessed' and (IsCompleted IS NULL or IsCompleted=0)
				OR @ArticleType='all')
				AND HamTypeId in (select HamTypeId from @tbl_HamType) 
				AND FeedPublishedDate>=@startDate
				AND (@EndDate='1900-01-01' or CONVERT(date, FeedPublishedDate)<=@EndDate)
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
		--save
	Select @numWithCluster + @numNoCluster as TotalRecords

	
END