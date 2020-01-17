
-- =============================================
-- Author:		Vivian
-- Create date: 2020-01 
-- Description:	update zebra.EventImportationRisksByGeoname when an event is published/updated
-- Output: 1-success, -1-failed
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetGeonameImportationRiskByEventId
	@EventId as int
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	BEGIN TRAN
		/*A. Basic info*/
		--1. clean storage
		Delete from zebra.EventImportationRisksByGeoname Where EventId=@EventId
		
		--2. find all aois
		Declare @tbl_userAoi table (GeonameId int, LocationType int, CityPoint GEOGRAPHY, CityBuffer GEOGRAPHY)
		--full aois in each event, assume for now
		Declare @anEvent int = (Select Top 1 EventId From [zebra].[EventImportationRisksByGeoname])
		Insert into @tbl_userAoi(GeonameId)
			Select GeonameId
			From [zebra].[EventImportationRisksByGeoname]
			Where EventId=@anEvent;
		
		--3. event info
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
		Declare @tbl_localSpread table (AoiGeonameId int);
		--1. location same or aoi is event's country
		Insert into @tbl_localSpread
			Select GeonameId
			From @tbl_userAoi
			Where GeonameId=@eventCountryGeonameId
			Union
			Select f1.GeonameId
			From @tbl_userAoi as f1, @tbl_eventLoc as f2
			Where f1.GeonameId=f2.EventGeonameId
		--1.2 clean above aoi
		Delete from @tbl_userAoi Where GeonameId in (Select AoiGeonameId From @tbl_localSpread)
		--2. by admin hierachy
		--2.1 country-loc exists, any user aoi in this country is localSpread
		If Exists (Select 1 From @tbl_eventLoc Where LocationType=6)
			Insert into @tbl_localSpread
				Select f1.GeonameId
				From @tbl_userAoi as f1, place.Geonames as f2
				Where f2.CountryGeonameId=@eventCountryGeonameId and f1.GeonameId=f2.GeonameId;
		Else --2.2 event has city&prov only
			With T1 as (
				Select f1.GeonameId, f2.LocationType, f2.Admin1GeonameId
				From @tbl_userAoi as f1, place.Geonames as f2
				Where f1.GeonameId=f2.GeonameId
				)
			Insert into @tbl_localSpread
				--aoi's city, eventLoc's it's province
				Select T1.GeonameId
				From T1, @tbl_eventLoc as f2
				Where T1.LocationType=2 and f2.LocationType=4 and T1.Admin1GeonameId=f2.EventGeonameId
				Union --aoi's province, eventLoc's it's city
				Select T1.GeonameId
				From T1, @tbl_eventLoc as f2
				Where T1.LocationType=4 and f2.LocationType=2 and T1.GeonameId=f2.Admin1GeonameId
		--2.3 clean above aoi
		Delete from @tbl_userAoi Where GeonameId in (Select AoiGeonameId From @tbl_localSpread)
		
		--3. add info in userAoi
		--locType
		Update @tbl_userAoi Set LocationType=f2.LocationType
			From @tbl_userAoi as f1, place.Geonames as f2
			Where f1.GeonameId=f2.GeonameId
		--points of cities
		Update @tbl_userAoi Set CityPoint=f2.Shape
			From @tbl_userAoi as f1, place.Geonames as f2
			Where f1.LocationType=2 and f1.GeonameId=f2.GeonameId

		--4. Use city buffer
		Declare @Distance int=(Select [Value] From [bd].[ConfigurationVariables] Where [Name]='Distance')
		--4.1 Use event loc city buffer
		If Exists (Select 1 From @tbl_eventLoc Where LocationType=2) and exists (Select 1 From @tbl_userAoi)
		Begin --4.1
			--event city buffer
			Update @tbl_eventLoc 
				Set CityBuffer=CityPoint.STBuffer(@Distance)
				Where LocationType=2
			--intersect event city with
			Insert into @tbl_localSpread
				Select f2.GeonameId --aoi city
				From @tbl_eventLoc as f1, @tbl_userAoi as f2
				Where f1.LocationType=2 and f2.LocationType=2 and f1.CityBuffer.STIntersects(f2.CityPoint)=1
				Union --aoi prov/country
				Select f2.GeonameId
				From @tbl_eventLoc as f1, @tbl_userAoi as f2, [place].[CountryProvinceShapes] as f3
				Where f1.LocationType=2 and f2.LocationType>2 and f2.GeonameId=f3.GeonameId 
					and f1.CityBuffer.STIntersects(f3.SimplifiedShape)=1
			--clean above aoi
			Delete from @tbl_userAoi Where GeonameId in (Select AoiGeonameId From @tbl_localSpread)
		End --4.1
		--4.2 Use aoi city buffer to find event prov/country
		If Exists (Select 1 From @tbl_userAoi Where LocationType=2) and Exists (Select 1 From @tbl_eventLoc Where LocationType>2)
		Begin --4.2
			--user city buffer
			Update @tbl_userAoi 
				Set CityBuffer=CityPoint.STBuffer(@Distance)
				Where LocationType=2
			--event country loc exits, no need to check province
			If Exists (Select 1 From @tbl_eventLoc Where LocationType=6)
				Insert into @tbl_localSpread
					Select f1.GeonameId
					From @tbl_userAoi as f1, [place].[CountryProvinceShapes] as f2
					Where f1.LocationType=2 and f2.GeonameId =@eventCountryGeonameId
						and f1.CityBuffer.STIntersects(f2.SimplifiedShape)=1
			Else --event's province only
				Insert into @tbl_localSpread
					Select distinct f2.GeonameId
					From @tbl_eventLoc as f1, @tbl_userAoi as f2, [place].[CountryProvinceShapes] as f3
					Where f1.LocationType=4 and f2.LocationType=2 and f1.EventGeonameId=f3.GeonameId 
						and f2.CityBuffer.STIntersects(f3.SimplifiedShape)=1
		End --4.2

		--4.3 save to main table
		Insert into zebra.EventImportationRisksByGeoname(GeonameId, LocalSpread, EventId, MinProb, MaxProb, MinVolume, MaxVolume)
			Select AoiGeonameId, 1, @EventId, 0, 0, 0, 0
			From @tbl_localSpread
		--4.4 clean above aoi
		Delete from @tbl_userAoi Where GeonameId in (Select AoiGeonameId From @tbl_localSpread);

		/*D. find dest risk values	*/
		If Exists (Select 1 From @tbl_userAoi)
		Begin --D
			Declare @tbl_imp table (localSpread int, MinProbability decimal(5,4),  
						MaxProbability decimal(5,4), MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3))
			Declare @tbl_imp2 table (AoiGeonameId int, localSpread int, MinProbability decimal(5,4),  
						MaxProbability decimal(5,4), MinExpTravelers decimal(10,3), MaxExpTravelers decimal(10,3))
			Declare  @thisAoiGeonameId int, @thisAoiGeonameIdStr varchar(200)
			Declare MyCursor CURSOR FAST_FORWARD 
			FOR Select GeonameId
				From @tbl_userAoi
	
			OPEN MyCursor
			FETCH NEXT FROM MyCursor
			INTO @thisAoiGeonameId
			Set @thisAoiGeonameIdStr=Convert(varchar(200), @thisAoiGeonameId)

			WHILE @@FETCH_STATUS = 0
			Begin
				Insert into @tbl_imp
				EXEC zebra.usp_ZebraGetImportationRiskSpreadOnly  @EventId, @thisAoiGeonameIdStr

				Insert into @tbl_imp2(AoiGeonameId, localSpread, MinProbability, MaxProbability, MinExpTravelers, MaxExpTravelers)
					Select @thisAoiGeonameId, localSpread, MinProbability, MaxProbability, MinExpTravelers, MaxExpTravelers
					From @tbl_imp
				Delete from @tbl_imp
				
				FETCH NEXT FROM MyCursor
				INTO @thisAoiGeonameId
				Set @thisAoiGeonameIdStr=Convert(varchar(200), @thisAoiGeonameId)
			End

			CLOSE MyCursor
			DEALLOCATE MyCursor

			--save
			Insert into zebra.EventImportationRisksByGeoname(GeonameId, LocalSpread, EventId, MinProb, MaxProb, MinVolume, MaxVolume)
				Select AoiGeonameId, 0, @EventId, MinProbability, MaxProbability, MinExpTravelers, MaxExpTravelers
				From @tbl_imp2

		End --D
		Select 1 as Result
	--action!
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		Select -1 as Result
	END CATCH;

END