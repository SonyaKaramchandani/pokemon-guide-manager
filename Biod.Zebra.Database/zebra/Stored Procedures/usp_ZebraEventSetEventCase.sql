
-- =============================================
-- Author:		Vivian
-- Create date: 2018-09 
-- Description:	Returns a country shape text by geonameId
-- 2019-07 name changed
-- =============================================
CREATE PROCEDURE [zebra].usp_ZebraEventSetEventCase
	@EventId    AS INT
	--,@EventLocationCases nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
	BEGIN TRAN
		--1. Replace historical case count
		Delete from surveillance.Xtbl_Event_Location_history Where EventId=@EventId and EventDateType=1
		Insert into surveillance.Xtbl_Event_Location_history(EventId, [GeonameId],[EventDate], EventDateType,
											[SuspCases],  [ConfCases], [RepCases], [Deaths])
			Select EventId, [GeonameId],[EventDate], 1, [SuspCases],  [ConfCases], [RepCases], [Deaths]
			From surveillance.Xtbl_Event_Location
			Where EventId=@EventId
		
		----2. input new cases
		--Declare @tbl_cases table ([GeonameId] int, [EventDate] date, [SuspCases] int
		--					,[ConfCases] int,[RepCases] int, [Deaths] int) 
		--Insert into @tbl_cases([GeonameId],[EventDate], [SuspCases],  [ConfCases], [RepCases], [Deaths])
		--	Select [GeonameId],[EventDate], [SuspCases],  [ConfCases], [RepCases], [Deaths]
		--	FROM OPENJSON(@EventLocationCases)
		--		WITH ([GeonameId] int
		--		  ,[EventDate] date
		--		  ,[SuspCases] int
		--		  ,[ConfCases] int
		--		  ,[RepCases] int
		--		  ,[Deaths] int
		--			)
		
		----3. replace current case count
		--Delete from surveillance.Xtbl_Event_Location Where EventId=@EventId
		--Insert into surveillance.Xtbl_Event_Location(EventId, [GeonameId],[EventDate],
		--									[SuspCases],  [ConfCases], [RepCases], [Deaths])
		--	Select @EventId, [GeonameId],[EventDate], [SuspCases],  [ConfCases], [RepCases], [Deaths]
		--	From @tbl_cases

		--succeed
		Select Cast (1 as bit) as Result
	--action!
	COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		Select Cast (0 as bit) as Result
	END CATCH;
END