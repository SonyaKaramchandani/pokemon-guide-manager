
-- =============================================
-- Author:		Vivian
-- Create date: 2020-02 
-- Description:	update zebra.EventImportationRisksByGeonameSpreadMd when an event is published/updated.
--				Risk values are calculated regardless if the event is local spread or not
-- Output: 1-success, -1-failed
-- =============================================
CREATE PROCEDURE [zebra].[usp_ZebraDataRenderSetGeonameImportationRiskByEventIdSpreadMd]
	@EventId as int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		/*A. Basic info*/
		--1. clean storage
		Delete from zebra.EventImportationRisksByGeonameSpreadMd Where EventId=@EventId
		
		--2. find aois from all users
		Declare @tbl_userAoi table (GeonameId int, LocationType int, Admin1GeonameId int, CountryGeonameId int,
								CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY, IsLocal bit)
		Insert into @tbl_userAoi(GeonameId, IsLocal)
		Select distinct value, 0
		From [dbo].[UserProfile]
		cross apply STRING_SPLIT(AoiGeonameIds, ',')
		--add loc info
		Update @tbl_userAoi
			Set LocationType=f2.LocationType, Admin1GeonameId=f2.Admin1GeonameId, 
				CountryGeonameId=f2.CountryGeonameId, CityPoint=f2.Shape
			From @tbl_userAoi as f1, place.Geonames as f2
			Where f1.GeonameId=f2.GeonameId
		
		--3. event loc
		Declare @tbl_eventLoc table (EventGeonameId int, LocationType int, Admin1GeonameId int, CountryGeonameId int,
									CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY)
		Insert into @tbl_eventLoc(EventGeonameId, LocationType, Admin1GeonameId, CountryGeonameId, CityPoint)
			Select f1.GeonameId, f2.LocationType, f2.Admin1GeonameId, f2.CountryGeonameId,
				f2.Shape
			From surveillance.Xtbl_Event_Location as f1, place.Geonames as f2
			Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId;
		--one event one country
		Declare @eventCountryGeonameId int=(Select Top 1 CountryGeonameId From @tbl_eventLoc);

		/*B. look for local spread*/
		--1. location same or aoi is event's country (process this first for performance)
		Update @tbl_userAoi Set IsLocal=1
			From @tbl_userAoi as f1, @tbl_eventLoc as f2
			Where f1.GeonameId=@eventCountryGeonameId or f1.GeonameId=f2.EventGeonameId

		--2. by admin hierachy
		--2.1 country-loc exists, any user aoi in this country is localSpread
		If Exists (Select 1 From @tbl_eventLoc Where LocationType=6)
			Update @tbl_userAoi Set IsLocal=1
				From @tbl_userAoi
				Where IsLocal=0 and CountryGeonameId=@eventCountryGeonameId ;
		Else --2.2 event has city&prov only
			Update  @tbl_userAoi Set IsLocal=1
			From @tbl_userAoi as f1, @tbl_eventLoc as f2
			Where f1.IsLocal=0 and
				(--aoi's city, eventLoc's it's province
				f1.LocationType=2 and f2.LocationType=4 and f1.Admin1GeonameId=f2.EventGeonameId
				--aoi's province, eventLoc's it's city
				OR f1.LocationType=4 and f2.LocationType=2 and f1.GeonameId=f2.Admin1GeonameId)
		
		--3. Use city buffer
		Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
		--3.1 Use event loc city buffer
		If Exists (Select 1 From @tbl_userAoi Where IsLocal=0) and exists (Select 1 From @tbl_eventLoc Where LocationType=2)
		Begin --3.1
			--event city buffer
			Update @tbl_eventLoc 
				Set CityBuffer=CityPoint.STBuffer(@Distance)
				Where LocationType=2
			--intersect user city with event city
			Update  @tbl_userAoi Set IsLocal=1
				From  @tbl_userAoi as f1, @tbl_eventLoc as f2
				Where f1.IsLocal=0 and f1.LocationType=2 and f2.LocationType=2 
					and f2.CityBuffer.STIntersects(f1.CityPoint)=1
			-- aoi prov/country with event city
			Update  @tbl_userAoi Set IsLocal=1
				From  @tbl_userAoi as f1, @tbl_eventLoc as f2, [place].[CountryProvinceShapes] as f3
				Where f1.IsLocal=0 and f1.LocationType>2 and f2.LocationType=2 and f1.GeonameId=f3.GeonameId 
					and f2.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		End --3.1

		--3.2 Use aoi city buffer to find event prov/country
		If Exists (Select 1 From @tbl_userAoi Where IsLocal=0 and LocationType=2) 
			and Exists (Select 1 From @tbl_eventLoc Where LocationType>2)
		Begin --3.2
			--user city buffer
			Update @tbl_userAoi 
				Set CityBuffer=CityPoint.STBuffer(@Distance)
				Where LocationType=2
			--event country loc exits, no need to check province
			If Exists (Select 1 From @tbl_eventLoc Where LocationType=6)
				Update  @tbl_userAoi Set IsLocal=1
					From @tbl_userAoi as f1, [place].[CountryProvinceShapes] as f2
					Where f1.LocationType=2 and f2.GeonameId =@eventCountryGeonameId
						and f1.CityBuffer.STIntersects(f2.SimplifiedShape)=1
			Else --event's province only
				Update  @tbl_userAoi Set IsLocal=1
					From @tbl_userAoi as f1, @tbl_eventLoc as f2, [place].[CountryProvinceShapes] as f3
					Where f1.LocationType=2 and f2.LocationType=4 and f2.EventGeonameId=f3.GeonameId 
						and f1.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		End --3.2

		/*C. find dest risk values	*/
		If Exists (Select 1 From @tbl_userAoi)
		Begin --D
			Declare @tbl_imp table (AoiGeonameId int, MinProbability decimal(5,4), MaxProbability decimal(5,4), 
									MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3))
			Declare  @thisAoiGeonameId int
			Declare MyCursor CURSOR FAST_FORWARD 
			FOR Select GeonameId
				From @tbl_userAoi
			OPEN MyCursor
			FETCH NEXT FROM MyCursor
			INTO @thisAoiGeonameId

			WHILE @@FETCH_STATUS = 0
			Begin
				Insert into @tbl_imp
				EXEC zebra.usp_ZebraGetEventImportationRiskByGeoname  @EventId, @thisAoiGeonameId

				FETCH NEXT FROM MyCursor
				INTO @thisAoiGeonameId
			End

			CLOSE MyCursor
			DEALLOCATE MyCursor

			--save
			Insert into zebra.EventImportationRisksByGeonameSpreadMd(GeonameId, EventId, LocalSpread, MinProb, MaxProb, MinVolume, MaxVolume)
				Select f1.AoiGeonameId, @EventId, f2.IsLocal, f1.MinProbability, f1.MaxProbability, f1.MinExpTravelers, f1.MaxExpTravelers
				From @tbl_imp as f1, @tbl_userAoi as f2
				Where f1.AoiGeonameId=f2.GeonameId
		End --C

		Select 1 as Result
	--action!
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;

END