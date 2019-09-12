
-- =============================================
-- Author:		Vivian
-- Create date: 2018-05 
-- Description:	Input: EventId
--				Output: List of article ArticleTitle, URLS, FeedPublishedDate, ArticleFeedName, OriginalSourceURL
-- Modified: 2019-06
-- Adds article sources display name and seqId (GDELT contains CDC and News Media)
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraEventGetArticlesByEventId
	@EventId    AS INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT f1.ArticleTitle, f1.FeedURL, f1.FeedPublishedDate, f3.ArticleFeedName, f1.OriginalSourceURL, f1.OriginalLanguage,
		Case When f3.ArticleFeedId=3 and OriginalSourceURL like '%cdc.gov%' Then 'CDC'
			When f3.ArticleFeedId<>3 Then f3.DisplayName
			Else 'News Media'
		End as DisplayName,
		ISNULL(f3.FullName, '') as FullName,
		Case When f3.ArticleFeedId=3 and OriginalSourceURL like '%cdc.gov%' Then 2
			When f3.ArticleFeedId<>3 Then f3.SeqId
			Else 6
		End as SeqId
	FROM  [surveillance].[ProcessedArticle] as f1 
		Inner Join [surveillance].[Xtbl_Article_Event] as f2 On f1.ArticleId=f2.ArticleId
		Left Join [surveillance].[ArticleFeed] as f3 On f1.ArticleFeedId=f3.ArticleFeedId
	WHERE f2.EventId=@EventId 
END