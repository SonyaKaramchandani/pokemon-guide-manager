USE [BiodZebra]

Truncate table [surveillance].[Xtbl_Article_Event]
Truncate table [surveillance].[Xtbl_Article_Location]
Truncate table [surveillance].[Xtbl_Article_Location_Disease]
Truncate table [surveillance].[Xtbl_Event_Location]
Truncate table [surveillance].[Xtbl_Event_Reason]
Delete from [surveillance].[ProcessedArticle]
Truncate table [zebra].[EventDestinationAirport]
Truncate table [zebra].EventDestinationGridV3
Truncate table [zebra].EventSourceAirport
Delete from [surveillance].[Event]
Truncate table [zebra].[EventPrevalence]