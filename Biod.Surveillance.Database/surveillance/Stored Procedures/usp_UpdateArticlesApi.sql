
-- =============================================
-- Author:		Vivian Hu
-- Create date: 2018-03 
-- Description:	Input: A json string
--				Output: update [ProcessedArticle], Xtbl_Article_Disease, Xtbl_Article_Location_Disease
-- Updated: 2020-01 update FeedPublishedDate in existing articles
-- =============================================

CREATE PROCEDURE surveillance.usp_UpdateArticlesApi
	--'api-dev1.ad.bluedot.global:83'
	@serviceDomainName varchar(128), --'api-prod1.ad.bluedot.global', 'dw1-ubuntu.ad.bluedot.global:81'
	@resultMessage	varchar(500) OUTPUT --@Response:'[]' means successful
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		Set @resultMessage='UpdateArticles: '
		Declare @startDate_all char(19)=convert(varchar, DATEADD(YEAR, -1, GETUTCDATE()), 126)
		Declare @startDate_spam date=DATEADD(MONTH, -1, GETUTCDATE())
		Declare @pageSize varchar(10)='1000'
		Declare @pageNum int=1
		Declare @Response NVARCHAR(max)
		Declare @inputJson NVARCHAR(max)
		Declare @urlStr varchar(500) -- dev and prod api urls are different
		Set @urlStr=@serviceDomainName

		--0. a table to hold all need information
		Declare @tbl_articles table (ArticleId varchar(128),
			ArticleTitle nvarchar(500),
			SystemLastModifiedDate datetime,
			feedName varchar(50),
			FeedURL nvarchar(2000),
			FeedSourceId varchar(100),
			FeedPublishedDate datetime,
			HamTypeId int, 
			OriginalSourceURL nvarchar(2000),
			IsCompleted bit, --
			SimilarClusterId varchar(20), 
			OriginalLanguage varchar(50), 
			Notes nvarchar(max),
			ArticleBody nvarchar(max),
			diseases nvarchar(max),
			locations nvarchar(max),
			relatedArticleSourceIds nvarchar(max)
			) 

		Declare @n int = (Select count(*) from [surveillance].[ProcessedArticle])
		--1. get json API 
		--1.1 first time
		If @n=0
		Begin --1
			--1. imports from feedPublishDate one year
			--1.1 page 1
			Set @inputJson='http://' + @urlStr + '/api/v1/Surveillance/ProcessedArticles?fromFeedPublishedDate='
				+  @startDate_all + '&order=Desc&pageNum=1'	+ '&sort=feedPublishedDate&pageSize=' + @pageSize
			EXECUTE bd.InvokeService @inputJson, @Response OUT
			--1.1.2 2nd and more pages
			While CHARINDEX('[',@Response)=1 AND @Response<>'[]'
			Begin --1.2
				INSERT INTO @tbl_articles(ArticleId, ArticleTitle, SystemLastModifiedDate, feedName,
								FeedURL, FeedSourceId, FeedPublishedDate, HamTypeId, OriginalSourceURL,
								SimilarClusterId, OriginalLanguage, Notes, ArticleBody, diseases, locations,
								relatedArticleSourceIds)
				SELECT _id, title, systemLastModifiedDate, feedName, feedURL, sourceId, feedPublishedDate, 
						spamFilterLabel, originalSourceURL, similarClusterId, originalLanguage, notes, body,
						diseases, locations, relatedArticleSourceIds 
				FROM OPENJSON(replace(@Response, '\n', '\\n'))
				WITH (
					_id varchar(128),
					title nvarchar(500),
					systemLastModifiedDate char(19),
					feedName varchar(50),
					feedURL nvarchar(2000),
					sourceId varchar(100),
					feedPublishedDate char(19),
					spamFilterLabel int,
					originalSourceURL nvarchar(2000),
					similarClusterId varchar(20),
					originalLanguage varchar(50),
					notes nvarchar(max),
					body nvarchar(max),
					diseases nvarchar(max) AS JSON,
					locations nvarchar(max) AS JSON,
					relatedArticleSourceIds nvarchar(max) AS JSON
					) 
				Where spamFilterLabel<>1 or (spamFilterLabel=1 and feedPublishedDate>=@startDate_spam)

				--loop
				Set @pageNum=@pageNum+1
				Set @inputJson='http://' + @urlStr + '/api/v1/Surveillance/ProcessedArticles?fromFeedPublishedDate='
					+  @startDate_all + '&order=Desc&pageNum=' + CONVERT(varchar(10), @pageNum) 
					+ '&sort=feedPublishedDate&pageSize=' + @pageSize
				EXECUTE bd.InvokeService @inputJson, @Response OUT
			End --1.2
		End --1
		--1.2 Not first time insert
		Else
		Begin --2
			--1. imports from last SystemLastModifiedDate
			Set @startDate_all=convert(varchar, (Select DATEADD(SECOND, 0.1, MAX(SystemLastModifiedDate)) From surveillance.ProcessedArticle), 126)
			--Set @startDate_all=convert(varchar, (Select DATEADD(MONTH, -1, MAX(SystemLastModifiedDate)) From surveillance.ProcessedArticle), 126)
			--1.1 page 1
			Set @inputJson='http://' + @urlStr + '/api/v1/Surveillance/ProcessedArticles?order=Desc&fromSystemLastModifiedDate='
				+  @startDate_all + '&pageNum=1&sort=feedPublishedDate&pageSize=' + @pageSize
			EXECUTE bd.InvokeService @inputJson, @Response OUT
			--1.2 2nd and more pages
			While CHARINDEX('[',@Response)=1 AND @Response<>'[]'
			Begin --1.2
				INSERT INTO @tbl_articles(ArticleId, ArticleTitle, SystemLastModifiedDate, feedName,
								FeedURL, FeedSourceId, FeedPublishedDate, HamTypeId, OriginalSourceURL,
								SimilarClusterId, OriginalLanguage, Notes, ArticleBody, diseases, locations,
								relatedArticleSourceIds)
				SELECT _id, title, systemLastModifiedDate, feedName, feedURL, sourceId, feedPublishedDate, 
						spamFilterLabel, originalSourceURL, similarClusterId, originalLanguage, notes, body,
						diseases, locations, relatedArticleSourceIds 
				FROM OPENJSON(replace(@Response, '\n', '\\n'))
				WITH (
					_id varchar(128),
					title nvarchar(500),
					systemLastModifiedDate char(19),
					feedName varchar(50),
					feedURL nvarchar(2000),
					sourceId varchar(100),
					feedPublishedDate char(19),
					spamFilterLabel int,
					originalSourceURL nvarchar(2000),
					similarClusterId varchar(20),
					originalLanguage varchar(50),
					notes nvarchar(max),
					body nvarchar(max),
					diseases nvarchar(max) AS JSON,
					locations nvarchar(max) AS JSON,
					relatedArticleSourceIds nvarchar(max) AS JSON
					) 

				--loop
				Set @pageNum=@pageNum+1
				Set @inputJson='http://' + @urlStr + '/api/v1/Surveillance/ProcessedArticles?order=Desc&fromSystemLastModifiedDate='
					+  @startDate_all + '&pageNum=' + CONVERT(varchar(10), @pageNum) 
					+ '&sort=feedPublishedDate&pageSize=' + @pageSize
				EXECUTE bd.InvokeService @inputJson, @Response OUT
			End --1.2
		End --2

		--2. add new articles
		If exists (select 1 from @tbl_articles)
		Begin --3
			/**Already existed articles**/
			Declare @tbl_temp table (ArticleId varchar(128), ArticleBody nvarchar(max), 
					SystemLastModifiedDate datetime, OriginalLanguage varchar(50), FeedPublishedDate datetime)
			Insert into @tbl_temp(ArticleId, ArticleBody, SystemLastModifiedDate, OriginalLanguage, FeedPublishedDate)
				Select f1.ArticleId, f1.ArticleBody, f1.SystemLastModifiedDate, f1.OriginalLanguage, f1.FeedPublishedDate
				FROM @tbl_articles as f1, surveillance.ProcessedArticle as f2
				Where f1.ArticleId=f2.ArticleId
			--actions if existing articles updated
			If Exists (Select 1 from @tbl_temp)
			Begin
				--update body only
				Update surveillance.ProcessedArticle 
					Set ArticleBody=f2.ArticleBody, SystemLastModifiedDate=f2.SystemLastModifiedDate,
						OriginalLanguage=f2.OriginalLanguage, FeedPublishedDate=f2.FeedPublishedDate
					From surveillance.ProcessedArticle as f1, @tbl_temp as f2
					Where f1.ArticleId=f2.ArticleId
				--remove it from @tbl_articles
				Delete from @tbl_articles Where ArticleId in (Select ArticleId From @tbl_temp)
			End

			/****A. [surveillance].[ProcessedArticle]****/
			--1. Import from ProcessedArticles API, if data tag/type changed, error would be raised here
			INSERT INTO surveillance.ProcessedArticle(ArticleId, ArticleTitle, SystemLastModifiedDate, 
					ArticleFeedId, FeedURL, FeedSourceId, FeedPublishedDate, HamTypeId, 
					OriginalSourceURL, SimilarClusterId, OriginalLanguage, Notes, ArticleBody)
			SELECT ArticleId, ArticleTitle, SystemLastModifiedDate, f2.ArticleFeedId,
					FeedURL, FeedSourceId, FeedPublishedDate, HamTypeId, OriginalSourceURL,
					SimilarClusterId, OriginalLanguage, Notes, ArticleBody
			FROM @tbl_articles as f1, surveillance.ArticleFeed as f2
			Where f1.feedName=f2.ArticleFeedName

			/****B & C:  Xtbl_Article_Location_Disease, Xtbl_Article_Location****/
			--1. tmp table for all diseases
			Declare @tbl_diseases table(ArticleId varchar(128), DiseaseId int)
			Insert into @tbl_diseases(ArticleId, DiseaseId)
			Select distinct ArticleId, floor(cast(diseaseId as decimal(10,2))) as diseaseId 
			FROM @tbl_articles
				CROSS APPLY OPENJSON (diseases) 
					WITH 
					(diseaseId varchar(10)
					)
			Where diseaseId is not NULL

			--2. tmp table for all locations 
			Declare @tbl_locations table(ArticleId varchar(128), LocationGeoNameId int)
			Insert into @tbl_locations(ArticleId, LocationGeoNameId)
			Select distinct ArticleId, bdGeonameId 
			FROM @tbl_articles
				CROSS APPLY OPENJSON (locations) 
					WITH 
					(bdGeonameId int
					)
			Where bdGeonameId is not NULL
			/**************fix geonameId not availabel bug****************/
			--Any GeoNameIds not in place.Geonames?
			Declare @tbl_NotAvailableGeoNameIds table (GeoNameId int)
			Insert into @tbl_NotAvailableGeoNameIds(GeoNameId)
				select LocationGeoNameId from @tbl_locations
				except select GeoNameId from place.Geonames
			--Remove unavailable GeoNameIds
			Delete from @tbl_locations 
			Where LocationGeoNameId in (Select GeoNameId From @tbl_NotAvailableGeoNameIds)
			/***************fix bug end 1***************/

			--3. tmp table for diseases with locations
			Declare @tbl_disease_location table(ArticleId varchar(128), LocationGeoNameId int, DiseaseId int,
				TotalDeathCount int, TotalConfirmedCount int, TotalSuspectedCount int, TotalReportedCount int,
				NewDeathCount int, NewConfirmedCount int, NewSuspectedCount int, NewReportedCount int)
			Insert into @tbl_disease_location(ArticleId, LocationGeoNameId, DiseaseId,
				TotalDeathCount, TotalConfirmedCount, TotalSuspectedCount, TotalReportedCount,
				NewDeathCount, NewConfirmedCount, NewSuspectedCount, NewReportedCount)
			Select distinct ArticleId, bdGeonameId, [disease.diseaseId], [totalCaseCounts.reported],
					[totalCaseCounts.confirmed],[totalCaseCounts.suspected],
					[totalCaseCounts.deaths],[newCaseCounts.reported],
					[newCaseCounts.confirmed],[newCaseCounts.suspected],
					[newCaseCounts.deaths]
			FROM @tbl_articles
				CROSS APPLY OPENJSON (locations) 
					WITH 
					(bdGeonameId int,
					[totalCaseCounts.reported] int,
					[totalCaseCounts.confirmed] int,
					[totalCaseCounts.suspected] int,
					[totalCaseCounts.deaths] int,
					[disease.diseaseId] int,
					[newCaseCounts.reported] int,
					[newCaseCounts.confirmed] int,
					[newCaseCounts.suspected] int,
					[newCaseCounts.deaths] int
					)
			Where [disease.diseaseId] is not null;
			/**************fix geonameId not availabel bug****************/
			--Remove unavailable GeoNameIds
			If Exists (Select 1 from @tbl_disease_location)
				Delete from @tbl_disease_location 
				Where LocationGeoNameId in (Select GeoNameId From @tbl_NotAvailableGeoNameIds);
			/***************fix bug end 2***************/

			--B. Xtbl_Article_Location_Disease: above #3 plus (#1 not in #3)
			With T1 as (
				Select ArticleId, DiseaseId from @tbl_diseases
				Except
				Select ArticleId, DiseaseId from @tbl_disease_location
				)
			Insert into surveillance.Xtbl_Article_Location_Disease(ArticleId, LocationGeoNameId, DiseaseId,
				TotalDeathCount, TotalConfirmedCount, TotalSuspectedCount, TotalReportedCount,
				NewDeathCount, NewConfirmedCount, NewSuspectedCount, NewReportedCount)
			Select ArticleId, LocationGeoNameId, DiseaseId,
				TotalDeathCount, TotalConfirmedCount, TotalSuspectedCount, TotalReportedCount,
				NewDeathCount, NewConfirmedCount, NewSuspectedCount, NewReportedCount
			From @tbl_disease_location
			Union
			Select ArticleId, -1, DiseaseId, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
			From T1;

			--C. Xtbl_Article_Location: in #2 not in #3
			With T2 as (
				Select ArticleId, LocationGeoNameId from @tbl_locations
				Except
				Select ArticleId, LocationGeoNameId from @tbl_disease_location
				)
			Insert into surveillance.Xtbl_Article_Location(ArticleId, LocationGeoNameId)
			Select ArticleId, LocationGeoNameId from T2;

		
			/****D. [surveillance].[Xtbl_RelatedArticles]****/
			--Imports
			Insert into surveillance.Xtbl_RelatedArticles(MainArticleId, RelatedArticleId)
			Select distinct ArticleId, value
			FROM @tbl_articles
				CROSS APPLY OPENJSON (relatedArticleSourceIds) 

		End --3 

		--action!
		If @Response<>N'[]'
			Set @resultMessage = @resultMessage + @Response
		Else
			Set @resultMessage = @resultMessage + 'Success'
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT CONCAT('Failed to update systems in the database. ErrorNumber:', CAST(ERROR_NUMBER() AS VARCHAR(20)),
		              ' ,ErrorSeverity: ', CAST(ERROR_SEVERITY() AS VARCHAR(10)),
					  ' ,ErrorState: ', CAST(ERROR_STATE() AS VARCHAR(10)),
					  ' ,ErrorProcedure: ', CAST(ERROR_PROCEDURE() AS VARCHAR(256)), 
					  ' ,ErrorLine: ', CAST(ERROR_LINE() AS VARCHAR(10)), 
					  ' ,ErrorMessage: ', CAST(ERROR_MESSAGE() AS VARCHAR(MAX))) as ErrorMessage
	END CATCH;
	
END