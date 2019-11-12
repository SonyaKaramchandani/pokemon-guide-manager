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
Truncate table [zebra].[EventPrevalence]
Delete from [surveillance].[Event]
delete from [surveillance].Xtbl_Event_Location_history
delete from [zebra].[EventDestinationAirport_history]
delete from [zebra].[EventDestinationGrid_history]
delete from [zebra].EventImportationRisksByUser
delete from [zebra].[EventImportationRisksByUser_history]
delete from [zebra].[EventPrevalence_history]
delete from [zebra].[UserAois_history]
truncate table [zebra].[Xtbl_User_Disease_Relevance]
truncate table [zebra].[Xtbl_Role_Disease_Relevance]
delete from place.ActiveGeonames