USE [BiodZebra]

Truncate table [surveillance].[Xtbl_Article_Event]
Truncate table [surveillance].[Xtbl_Article_Location]
Truncate table [surveillance].[Xtbl_Article_Location_Disease]
Truncate table [surveillance].[Xtbl_Event_Location]
Truncate table [surveillance].[Xtbl_Event_Reason]
Delete from [surveillance].[ProcessedArticle]
--for old model
Truncate table [zebra].[EventDestinationAirport]
Truncate table [zebra].EventDestinationGridV3
Truncate table [zebra].EventSourceAirport
Truncate table [zebra].[EventPrevalence]
delete from [zebra].EventImportationRisksByUser
--for SPREADMD
truncate table [zebra].eventSourceGridSpreadMd
truncate table [zebra].EventSourceAirportSpreadMd
truncate table [zebra].EventSourceDestinationRisk
truncate table [zebra].EventExtensionSpreadMd
truncate table [zebra].EventDestinationAirportSpreadMd
truncate table [zebra].EventDestinationGridSpreadMd
truncate table [zebra].EventImportationRisksByGeonameSpreadMd
--ONLY RUN following three when copy data from one server to another
--DON'T RUN if only re-calculate risk models
Delete from [surveillance].[Event]
delete from [surveillance].Xtbl_Event_Location_history
delete from [zebra].[UserAois_history]

--for restore users?
truncate table [zebra].[Xtbl_User_Disease_Relevance]
truncate table [zebra].[Xtbl_Role_Disease_Relevance]
--delete from place.ActiveGeonames

