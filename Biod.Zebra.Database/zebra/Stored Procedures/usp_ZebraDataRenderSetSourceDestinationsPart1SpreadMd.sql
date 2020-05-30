
-- =============================================
-- Author:		Vivian
-- Create date: 2019-12-19 
-- based on revised_prevalence_calculations
-- Description:	1st part of pre-calculations, output source gridId and number of cases
-- 2020-05: PROPALRA, population proportioned case allocation, https://wiki.bluedot.global/pages/viewpage.action?pageId=81429332
--          1. include country level cases 
--          2. country and province remainder cases distribute across grids based on population proportion
-- =============================================
CREATE PROCEDURE zebra.usp_ZebraDataRenderSetSourceDestinationsPart1SpreadMd
	@EventId INT
AS
BEGIN
	SET NOCOUNT ON;

	--clean old data
	Delete from zebra.[EventSourceAirportSpreadMd] Where EventId=@EventId;
	Delete from zebra.EventDestinationAirportSpreadMd Where EventId=@EventId;
	Delete from zebra.EventDestinationGridSpreadMd Where EventId=@EventId
	Delete from zebra.EventExtensionSpreadMd Where EventId=@EventId
	Delete from zebra.[EventSourceDestinationRisk] Where EventId=@EventId
	Delete from zebra.EventSourceGridSpreadMd Where EventId=@EventId

    Drop table if exists #tbl_GridTmp
    Drop table if exists #tbl_Remainder

	If (Select IsLocalOnly from [surveillance].[Event] Where EventId=@EventId)=0
		AND EXISTS (Select 1 from [surveillance].[Xtbl_Event_Location] Where EventId=@EventId)
	BEGIN --event location exists
		Declare @tbl_spreadLocs table (GeonameId int, LocationType int, Admin1GeonameId int, 
									Cases int, Point GEOGRAPHY)
		--A. Event location w/ highest case count
		Insert into @tbl_spreadLocs(GeonameId, LocationType, Admin1GeonameId, Cases, Point)
			Select f1.GeonameId, f2.LocationType, f2.Admin1GeonameId, 
				(Select MAX(v) From (VALUES (RepCases), (ConfCases + SuspCases), (Deaths)) As value(v)),
				case when f2.LocationType=2 then f2.Shape else null end
			From [surveillance].[Xtbl_Event_Location] as f1, [place].[Geonames] as f2
			Where f1.EventId=@EventId and f1.GeonameId=f2.GeonameId;
        
        --B. when case exists somewhere
		If exists (Select 1 from @tbl_spreadLocs)
            And Not Exists (Select GeonameId From @tbl_spreadLocs Where LocationType=4
                            Except 
                            Select Adm1GeonameId From zebra.GridProvince Where PercentPopulation>0)
            And Not Exists (Select GeonameId From @tbl_spreadLocs Where LocationType=6
                            Except 
                            Select CountryGeonameId From zebra.GridCountry Where PercentPopulation>0)
        BEGIN --B
            --final results
            Declare @tbl_gridCase table (GridId nvarchar(12), Cases int)
            --1. city-level
            If Exists (Select 1 From @tbl_spreadLocs Where LocationType=2)
            Begin --1
			    --1.1 geoname-grid
			    Declare @tbl_loc_grid table (GeonameId int, GridId nvarchar(12))
			    Insert into @tbl_loc_grid(GeonameId, GridId)
				    Select f2.GeonameId, f1.gridId
				    From bd.HUFFMODEL25KMWORLDHEXAGON as f1, @tbl_spreadLocs as f2
				    Where f2.LocationType=2 and f1.SHAPE.STIntersects(f2.Point) = 1
			    --1.2 grid-case 
                Insert into @tbl_gridCase(GridId, Cases)
			        Select f1.GridId, sum(f2.Cases) as Cases
			        From @tbl_loc_grid as f1, @tbl_spreadLocs as f2
			        Where f1.GeonameId=f2.GeonameId
			        Group by f1.GridId
            End --1

			--2. province level 
            Declare @tbl_province table (GeonameId int, Cases int);
            --2.1 provinces with cases
			With T1 as ( --province-level cases from cities
				Select Admin1GeonameId, SUM(Cases) as Cases
				From @tbl_spreadLocs
                Where LocationType = 2
				Group by Admin1GeonameId
			)
            --provinces cases unaccounted in cities
 			Insert into @tbl_province(GeonameId, Cases)
				Select f1.Admin1GeonameId, (f1.Cases-f2.Cases)
				From @tbl_spreadLocs as f1, T1 as f2
				Where f1.LocationType=4 And f1.GeonameId=f2.Admin1GeonameId 
                    And f1.Cases>f2.Cases;
            --provinces with no cities
 			With T1 as (--only has prov
				Select GeonameId From @tbl_spreadLocs Where LocationType=4
				Except
				Select Admin1GeonameId From @tbl_spreadLocs Where LocationType=2
				)
 			Insert into @tbl_province(GeonameId, Cases)
				Select f1.GeonameId, f1.Cases
				From @tbl_spreadLocs as f1, T1 as f2
				Where f1.LocationType=4 And f1.GeonameId=f2.GeonameId

            --2.2 allocate cases to grids
            If Exists (Select 1 From @tbl_province)
            Begin --2
                --table to hold cases
			    Declare @tbl_provinceGrid table (GeonameId int, GridId nvarchar(12), Cases int)
                --table to hold remainder
                Create table #tbl_Remainder (GeonameId int, Cases int);
                --table to hold intermediate calculated results
			    Create table #tbl_GridTmp (GeonameId int, GridId nvarchar(12), Cases int, Ranking int)

                --first run to allocate
                Insert into @tbl_provinceGrid(GeonameId, GridId, Cases)
                    Select f1.GeonameId, f2.GridId, FLOOR(f1.Cases*f2.PercentPopulation)
                    From @tbl_province as f1, zebra.GridProvince as f2
                    Where f1.GeonameId=f2.Adm1GeonameId;
                --remainder
                With T1 as (
                    Select GeonameId, sum(Cases) as Cases
                    From @tbl_provinceGrid
                    Group by GeonameId
                    )
                Insert into #tbl_Remainder(GeonameId, Cases)
                    Select f1.GeonameId, f1.Cases-ISNULL(T1.Cases, 0)
                    From @tbl_province as f1 left join T1 on f1.GeonameId=T1.GeonameId

                --2nd and more round 
                If Exists (Select 1 From #tbl_Remainder where Cases>0)
                Begin --2.2
                    --A. allocate cases proportional
                    Insert into #tbl_GridTmp(GeonameId, GridId, Cases)
                        Select f1.GeonameId, f2.GridId, FLOOR(f1.Cases*f2.PercentPopulation)
                        From #tbl_Remainder as f1, zebra.GridProvince as f2
                        Where f1.GeonameId=f2.Adm1GeonameId;

                    --loop
                    While Exists (Select 1 From #tbl_GridTmp Where Cases>0)
                    Begin --loop
                        --add to result
                        Update @tbl_provinceGrid Set cases=f1.Cases + f2.Cases
                            From @tbl_provinceGrid as f1, #tbl_GridTmp as f2
                            Where f1.GeonameId=f2.GeonameId and f1.GridId=f2.GridId;
                        --clean tmp holder
                        truncate table #tbl_GridTmp
                        --clean remainder
                        truncate table #tbl_Remainder;
                        --calculate remainder
                         With T1 as (
                            Select GeonameId, sum(Cases) as Cases
                            From @tbl_provinceGrid
                            Group by GeonameId
                            )
                        Insert into #tbl_Remainder(GeonameId, Cases)
                            Select f1.GeonameId, f1.Cases-ISNULL(T1.Cases, 0)
                            From @tbl_province as f1 left join T1 on f1.GeonameId=T1.GeonameId
                        --allocate cases
                        If Exists (Select 1 From #tbl_Remainder where Cases>0)
                            Insert into #tbl_GridTmp(GeonameId, GridId, cases)
                                Select f1.GeonameId, f2.GridId, FLOOR(f1.Cases*f2.PercentPopulation)
                                From #tbl_Remainder as f1, zebra.GridProvince as f2
                                Where f1.GeonameId=f2.Adm1GeonameId;
                    End --loop end
                    
                    --B. allocate cases non-proportional
                    If Exists (Select 1 From #tbl_Remainder where Cases>0)
                    Begin
                        --clean tmp holder
                        truncate table #tbl_GridTmp
                        --allocate based on rank of population
                        --1. add ranking
                        Insert into #tbl_GridTmp(GeonameId, GridId, Cases, Ranking)
                            Select f1.GeonameId, f2.GridId, 0,
                                ROW_NUMBER() OVER (PARTITION by f2.Adm1GeonameId ORDER BY f2.PercentPopulation DESC)
                            From #tbl_Remainder as f1, zebra.GridProvince as f2
                            Where f1.GeonameId=f2.Adm1GeonameId;
                        --loop to allocate cases
                        While Exists (Select 1 From #tbl_Remainder where Cases>0)
                        Begin --loop
                            --2. add cases in tmp
                            Update #tbl_GridTmp Set Cases=f1.Cases+1
                                From #tbl_GridTmp as f1, #tbl_Remainder as f2
                                Where f1.GeonameId=f2.GeonameId and f1.Ranking<=f2.Cases;
                            --3. remove cases from remainder
                            With T1 as (
                                Select GeonameId, MAX(Ranking) as Ranking
                                From #tbl_GridTmp
                                Group by GeonameId
                                )
                            Update #tbl_Remainder Set Cases=f1.Cases-T1.Ranking
                                From #tbl_Remainder as f1, T1
                                Where f1.GeonameId=T1.GeonameId
                        End --loop end
                        --add to results
                        Update @tbl_provinceGrid Set Cases=f1.Cases+f2.Cases
                            From @tbl_provinceGrid as f1, #tbl_GridTmp as f2
                            Where f1.GeonameId=f2.GeonameId and f1.GridId=f2.GridId;
                    End
                End; --2.2
                
                --add to final results
                With T1 as (
                    Select GridId, SUM(Cases) as Cases
                    From @tbl_provinceGrid
                    Where Cases>0
                    Group by GridId
                    )
		        MERGE @tbl_gridCase AS TARGET
		        USING T1 AS SOURCE
		        ON (TARGET.GridId = SOURCE.GridId)
		        WHEN MATCHED
                    THEN Update SET TARGET.Cases=TARGET.Cases + SOURCE.Cases
                WHEN NOT MATCHED BY TARGET
                    THEN Insert(GridId, Cases)
                    Values(SOURCE.GridId, SOURCE.Cases);
            End --2

            --3. country level
            If Exists (Select 1 From @tbl_spreadLocs Where LocationType=6)
            Begin --3
               Declare @CountryGeonameId int, @CountryCases int;
                --cases from subNational
                Declare @countryCasesSum int 
                Set @countryCasesSum=(Select ISNULL(sum(Cases), 0) From @tbl_province)
                    + (Select ISNULL(sum(Cases), 0) From @tbl_spreadLocs Where LocationType=2)
                --cases raw
                Declare @countryCasesRaw int=(Select Cases From @tbl_spreadLocs Where LocationType=6)
                --has unaccounted country cases
                If @countryCasesRaw>@countryCasesSum
                Begin --3.1
                    Select @CountryGeonameId=GeonameId, @CountryCases=(@countryCasesRaw-@countryCasesSum)
                        From @tbl_spreadLocs 
                        Where LocationType=6
                    --initialize results with ranking
			        Declare @tbl_countryGrid table (GridId nvarchar(12), Cases int, PercentPopulation float, Ranking int)
                    Insert into @tbl_countryGrid(GridId, Cases, PercentPopulation, Ranking)
                        Select GridId, 0, PercentPopulation, ROW_NUMBER() OVER (ORDER BY PercentPopulation DESC)
                        From zebra.GridCountry
                        Where CountryGeonameId=@CountryGeonameId
                    --Cases not allocated yet
                    Declare @remainderCases int=0
                    Declare @remainderCasesProportion int = @CountryCases
                    
                    --A. loop to allocate proportionally
                    While @remainderCasesProportion>0
                    Begin --3.A
                        --remaider to use outside loop
                        Set @remainderCases=@remainderCasesProportion
                        --allocate proportionally
                        Update @tbl_countryGrid 
                            Set Cases=Cases + FLOOR(@remainderCasesProportion*PercentPopulation)
                        --@remainder
                        Set @remainderCasesProportion=@CountryCases-(Select SUM(Cases) From @tbl_countryGrid)
                        --when these two are same, stop loop
                        If @remainderCasesProportion=@remainderCases
                            Set @remainderCasesProportion=0
                    End --3.A

                    --B. allocate non-proportionally
                    While (@remainderCases>0)
                    Begin --3.B
                        --add cases
                        Update @tbl_countryGrid Set Cases=Cases+1 Where Ranking<=@remainderCases
                        --update remainder
                        Set @remainderCases=@remainderCases - (Select MAX(Ranking) From @tbl_countryGrid)
                    End; --3.B

                    --add to final results
                    With T1 as (
                        Select GridId, SUM(Cases) as Cases
                        From @tbl_countryGrid
                        Where Cases>0
                        Group by GridId
                        )
		            MERGE @tbl_gridCase AS TARGET
		            USING T1 AS SOURCE
		            ON (TARGET.GridId = SOURCE.GridId)
		            WHEN MATCHED
                        THEN Update SET TARGET.Cases=TARGET.Cases + SOURCE.Cases
                    WHEN NOT MATCHED BY TARGET
                        THEN Insert(GridId, Cases)
                        Values(SOURCE.GridId, SOURCE.Cases);
                End; --3.1
           End --3

            Select GridId, Cases From @tbl_gridCase
				
		END --B
		Else --no valid spread locations
			--output
			Select '-1' as GridId, 0 as Cases

        Drop table if exists #tbl_GridTmp
        Drop table if exists #tbl_Remainder

	END --event location exists
	ELSE --event location not exists
        --output
		Select '-1' as GridId, 0 as Cases

END