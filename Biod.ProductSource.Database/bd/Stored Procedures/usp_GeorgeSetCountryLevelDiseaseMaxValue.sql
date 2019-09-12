-- =============================================
-- Author:		Vivian
-- Create date: 2019-07 
-- Description:	called after map111 (admin1-level only) is pull from DS, to calculate country-level values
-- =============================================

CREATE PROCEDURE [bd].usp_GeorgeSetCountryLevelDiseaseMaxValue 
	@IsDev bit
AS
BEGIN
    SET NOCOUNT ON;
	If @IsDev=0 --uat/prod
	Begin
		Truncate table bd.DiseaseMapMaxValue_Country

		Insert into bd.DiseaseMapMaxValue_Country(
			OBJECTID
		  ,[GeonameId]
		  ,[Seasonality_Zone]
		  ,D1_prev
		  ,D2_prev
		  ,D3_prev
		  ,D4_prev
		  ,D5_prev
		  ,D6_prev
		  ,D7_prev
		  ,D8_prev
		  ,D9_prev
		  ,D10_prev
		  ,D11_prev
		  ,D12_prev
		  ,D13_prev
		  ,D14_prev
		  ,D15_prev
		  ,D16_prev
		  ,D17_prev
		  ,D18_prev
		  ,D19_prev
		  ,D20_prev
		  ,D21_prev
		  ,D22_prev
		  ,D23_prev
		  ,D24_prev
		  ,D25_prev
		  ,D26_prev
		  ,D27_prev
		  ,D28_prev
		  ,D29_prev
		  ,D30_prev
		  ,D31_prev
		  ,D32_prev
		  ,D33_prev
		  ,D34_prev
		  ,D35_prev
		  ,D36_prev
		  ,D37_prev
		  ,D38_prev
		  ,D39_prev
		  ,D40_prev
		  ,D41_prev
		  ,D42_prev
		  ,D43_prev
		  ,D44_prev
		  ,D45_prev
		  ,D46_prev
		  ,D47_prev
		  ,D48_prev
		  ,D49_prev
		  ,D50_prev
		  ,D51_prev
		  ,D52_prev
		  ,D53_prev
		  ,D54_prev
		  ,D55_prev
		  ,D56_prev
		  ,D57_prev
		  ,D58_prev
		  ,D59_prev
		  ,D60_prev
		  ,D61_prev
		  ,D62_prev
		  ,D63_prev
		  ,D64_prev
		  ,D65_prev
		  ,D66_prev
		  ,D67_prev
		  ,D68_prev
		  ,D69_prev
		  ,D70_prev
		  ,D71_prev
		  ,D72_prev
		  ,D73_prev
		  ,D74_prev
		  ,D75_prev
		  ,D76_prev
		  ,D77_prev
		  ,D78_prev
		  ,D80_prev
		  ,D81_prev
		  ,D82_prev
		  ,D83_prev
		  ,D84_prev
		  ,D85_prev
		  ,D86_prev
		  ,D87_prev
		  ,D88_prev
		  ,D89_prev
		  ,D90_prev
		  ,D91_prev
		  ,D92_prev
		  ,D95_prev
		  ,D98_prev
		  ,D101_prev
		  ,D103_prev
		  ,D104_prev
		  ,D107_prev
		  ,D108_prev
		  ,D109_prev
		  ,D110_prev
		  ,Developed
		  ,Urban_GridCode
		  )
		select 0 as OBJECTID
		  ,countryGeonameId as [geonameId]
		  ,[Seasonality_Zone]
		  ,MAX([D1_prev_max]) as D1_prev
		  ,MAX([D2_prev_max]) as D2_prev
		  ,MAX([D3_prev_max]) as D3_prev
		  ,MAX([D4_prev_max]) as D4_prev
		  ,MAX([D5_prev_max]) as D5_prev
		  ,MAX([D6_prev_max]) as D6_prev
		  ,MAX([D7_prev_max]) as D7_prev
		  ,MAX([D8_prev_max]) as D8_prev
		  ,MAX([D9_prev_max]) as D9_prev
		  ,MAX([D10_prev_max]) as D10_prev
		  ,MAX([D11_prev_max]) as D11_prev
		  ,MAX([D12_prev_max]) as D12_prev
		  ,MAX([D13_prev_max]) as D13_prev
		  ,MAX([D14_prev_max]) as D14_prev
		  ,MAX([D15_prev_max]) as D15_prev
		  ,MAX([D16_prev_max]) as D16_prev
		  ,MAX([D17_prev_max]) as D17_prev
		  ,MAX([D18_prev_max]) as D18_prev
		  ,MAX([D19_prev_max]) as D19_prev
		  ,MAX([D20_prev_max]) as D20_prev
		  ,MAX([D21_prev_max]) as D21_prev
		  ,MAX([D22_prev_max]) as D22_prev
		  ,MAX([D23_prev_max]) as D23_prev
		  ,MAX([D24_prev_max]) as D24_prev
		  ,MAX([D25_prev_max]) as D25_prev
		  ,MAX([D26_prev_max]) as D26_prev
		  ,MAX([D27_prev_max]) as D27_prev
		  ,MAX([D28_prev_max]) as D28_prev
		  ,MAX([D29_prev_max]) as D29_prev
		  ,MAX([D30_prev_max]) as D30_prev
		  ,MAX([D31_prev_max]) as D31_prev
		  ,MAX([D32_prev_max]) as D32_prev
		  ,MAX([D33_prev_max]) as D33_prev
		  ,MAX([D34_prev_max]) as D34_prev
		  ,MAX([D35_prev_max]) as D35_prev
		  ,MAX([D36_prev_max]) as D36_prev
		  ,MAX([D37_prev_max]) as D37_prev
		  ,MAX([D38_prev_max]) as D38_prev
		  ,MAX([D39_prev_max]) as D39_prev
		  ,MAX([D40_prev_max]) as D40_prev
		  ,MAX([D41_prev_max]) as D41_prev
		  ,MAX([D42_prev_max]) as D42_prev
		  ,MAX([D43_prev_max]) as D43_prev
		  ,MAX([D44_prev_max]) as D44_prev
		  ,MAX([D45_prev_max]) as D45_prev
		  ,MAX([D46_prev_max]) as D46_prev
		  ,MAX([D47_prev_max]) as D47_prev
		  ,MAX([D48_prev_max]) as D48_prev
		  ,MAX([D49_prev_max]) as D49_prev
		  ,MAX([D50_prev_max]) as D50_prev
		  ,MAX([D51_prev_max]) as D51_prev
		  ,MAX([D52_prev_max]) as D52_prev
		  ,MAX([D53_prev_max]) as D53_prev
		  ,MAX([D54_prev_max]) as D54_prev
		  ,MAX([D55_prev_max]) as D55_prev
		  ,MAX([D56_prev_max]) as D56_prev
		  ,MAX([D57_prev_max]) as D57_prev
		  ,MAX([D58_prev_max]) as D58_prev
		  ,MAX([D59_prev_max]) as D59_prev
		  ,MAX([D60_prev_max]) as D60_prev
		  ,MAX([D61_prev_max]) as D61_prev
		  ,MAX([D62_prev_max]) as D62_prev
		  ,MAX([D63_prev_max]) as D63_prev
		  ,MAX([D64_prev_max]) as D64_prev
		  ,MAX([D65_prev_max]) as D65_prev
		  ,MAX([D66_prev_max]) as D66_prev
		  ,MAX([D67_prev_max]) as D67_prev
		  ,MAX([D68_prev_max]) as D68_prev
		  ,MAX([D69_prev_max]) as D69_prev
		  ,MAX([D70_prev_max]) as D70_prev
		  ,MAX([D71_prev_max]) as D71_prev
		  ,MAX([D72_prev_max]) as D72_prev
		  ,MAX([D73_prev_max]) as D73_prev
		  ,MAX([D74_prev_max]) as D74_prev
		  ,MAX([D75_prev_max]) as D75_prev
		  ,MAX([D76_prev_max]) as D76_prev
		  ,MAX([D77_prev_max]) as D77_prev
		  ,MAX([D78_prev_max]) as D78_prev
		  ,MAX([D80_prev_max]) as D80_prev
		  ,MAX([D81_prev_max]) as D81_prev
		  ,MAX([D82_prev_max]) as D82_prev
		  ,MAX([D83_prev_max]) as D83_prev
		  ,MAX([D84_prev_max]) as D84_prev
		  ,MAX([D85_prev_max]) as D85_prev
		  ,MAX([D86_prev_max]) as D86_prev
		  ,MAX([D87_prev_max]) as D87_prev
		  ,MAX([D88_prev_max]) as D88_prev
		  ,MAX([D89_prev_max]) as D89_prev
		  ,MAX([D90_prev_max]) as D90_prev
		  ,MAX([D91_prev_max]) as D91_prev
		  ,MAX([D92_prev_max]) as D92_prev
		  ,MAX([D95_prev_max]) as D95_prev
		  ,MAX([D98_prev_max]) as D98_prev
		  ,MAX([D101_prev_max]) as D101_prev
		  ,MAX([D103_prev_max]) as D103_prev
		  ,MAX([D104_prev_max]) as D104_prev
		  ,MAX([D107_prev_max]) as D107_prev
		  ,MAX([D108_prev_max]) as D108_prev
		  ,MAX([D109_prev_max]) as D109_prev
		  ,MAX([D110_prev_max]) as D110_prev
		  ,MIN([Developed_min]) as Developed
		  ,MIN([Urban_GridCode_min]) as Urban_GridCode
		From [bd].DiseaseMapMaxValue_111
		Group by countryGeonameId
		  ,[Seasonality_Zone]

	End
	Else --dev/staging
	Begin
		Truncate table bd.DiseaseMapMaxValue_Country_dev

		Insert into bd.DiseaseMapMaxValue_Country_dev(
			OBJECTID
		  ,[GeonameId]
		  ,[Seasonality_Zone]
		  ,D1_prev
		  ,D2_prev
		  ,D3_prev
		  ,D4_prev
		  ,D5_prev
		  ,D6_prev
		  ,D7_prev
		  ,D8_prev
		  ,D9_prev
		  ,D10_prev
		  ,D11_prev
		  ,D12_prev
		  ,D13_prev
		  ,D14_prev
		  ,D15_prev
		  ,D16_prev
		  ,D17_prev
		  ,D18_prev
		  ,D19_prev
		  ,D20_prev
		  ,D21_prev
		  ,D22_prev
		  ,D23_prev
		  ,D24_prev
		  ,D25_prev
		  ,D26_prev
		  ,D27_prev
		  ,D28_prev
		  ,D29_prev
		  ,D30_prev
		  ,D31_prev
		  ,D32_prev
		  ,D33_prev
		  ,D34_prev
		  ,D35_prev
		  ,D36_prev
		  ,D37_prev
		  ,D38_prev
		  ,D39_prev
		  ,D40_prev
		  ,D41_prev
		  ,D42_prev
		  ,D43_prev
		  ,D44_prev
		  ,D45_prev
		  ,D46_prev
		  ,D47_prev
		  ,D48_prev
		  ,D49_prev
		  ,D50_prev
		  ,D51_prev
		  ,D52_prev
		  ,D53_prev
		  ,D54_prev
		  ,D55_prev
		  ,D56_prev
		  ,D57_prev
		  ,D58_prev
		  ,D59_prev
		  ,D60_prev
		  ,D61_prev
		  ,D62_prev
		  ,D63_prev
		  ,D64_prev
		  ,D65_prev
		  ,D66_prev
		  ,D67_prev
		  ,D68_prev
		  ,D69_prev
		  ,D70_prev
		  ,D71_prev
		  ,D72_prev
		  ,D73_prev
		  ,D74_prev
		  ,D75_prev
		  ,D76_prev
		  ,D77_prev
		  ,D78_prev
		  ,D80_prev
		  ,D81_prev
		  ,D82_prev
		  ,D83_prev
		  ,D84_prev
		  ,D85_prev
		  ,D86_prev
		  ,D87_prev
		  ,D88_prev
		  ,D89_prev
		  ,D90_prev
		  ,D91_prev
		  ,D92_prev
		  ,D95_prev
		  ,D98_prev
		  ,D101_prev
		  ,D103_prev
		  ,D104_prev
		  ,D107_prev
		  ,D108_prev
		  ,D109_prev
		  ,D110_prev
		  ,Developed
		  ,Urban_GridCode
		  )
		select 0 as OBJECTID
		  ,countryGeonameId as [geonameId]
		  ,[Seasonality_Zone]
		  ,MAX([D1_prev_max]) as D1_prev
		  ,MAX([D2_prev_max]) as D2_prev
		  ,MAX([D3_prev_max]) as D3_prev
		  ,MAX([D4_prev_max]) as D4_prev
		  ,MAX([D5_prev_max]) as D5_prev
		  ,MAX([D6_prev_max]) as D6_prev
		  ,MAX([D7_prev_max]) as D7_prev
		  ,MAX([D8_prev_max]) as D8_prev
		  ,MAX([D9_prev_max]) as D9_prev
		  ,MAX([D10_prev_max]) as D10_prev
		  ,MAX([D11_prev_max]) as D11_prev
		  ,MAX([D12_prev_max]) as D12_prev
		  ,MAX([D13_prev_max]) as D13_prev
		  ,MAX([D14_prev_max]) as D14_prev
		  ,MAX([D15_prev_max]) as D15_prev
		  ,MAX([D16_prev_max]) as D16_prev
		  ,MAX([D17_prev_max]) as D17_prev
		  ,MAX([D18_prev_max]) as D18_prev
		  ,MAX([D19_prev_max]) as D19_prev
		  ,MAX([D20_prev_max]) as D20_prev
		  ,MAX([D21_prev_max]) as D21_prev
		  ,MAX([D22_prev_max]) as D22_prev
		  ,MAX([D23_prev_max]) as D23_prev
		  ,MAX([D24_prev_max]) as D24_prev
		  ,MAX([D25_prev_max]) as D25_prev
		  ,MAX([D26_prev_max]) as D26_prev
		  ,MAX([D27_prev_max]) as D27_prev
		  ,MAX([D28_prev_max]) as D28_prev
		  ,MAX([D29_prev_max]) as D29_prev
		  ,MAX([D30_prev_max]) as D30_prev
		  ,MAX([D31_prev_max]) as D31_prev
		  ,MAX([D32_prev_max]) as D32_prev
		  ,MAX([D33_prev_max]) as D33_prev
		  ,MAX([D34_prev_max]) as D34_prev
		  ,MAX([D35_prev_max]) as D35_prev
		  ,MAX([D36_prev_max]) as D36_prev
		  ,MAX([D37_prev_max]) as D37_prev
		  ,MAX([D38_prev_max]) as D38_prev
		  ,MAX([D39_prev_max]) as D39_prev
		  ,MAX([D40_prev_max]) as D40_prev
		  ,MAX([D41_prev_max]) as D41_prev
		  ,MAX([D42_prev_max]) as D42_prev
		  ,MAX([D43_prev_max]) as D43_prev
		  ,MAX([D44_prev_max]) as D44_prev
		  ,MAX([D45_prev_max]) as D45_prev
		  ,MAX([D46_prev_max]) as D46_prev
		  ,MAX([D47_prev_max]) as D47_prev
		  ,MAX([D48_prev_max]) as D48_prev
		  ,MAX([D49_prev_max]) as D49_prev
		  ,MAX([D50_prev_max]) as D50_prev
		  ,MAX([D51_prev_max]) as D51_prev
		  ,MAX([D52_prev_max]) as D52_prev
		  ,MAX([D53_prev_max]) as D53_prev
		  ,MAX([D54_prev_max]) as D54_prev
		  ,MAX([D55_prev_max]) as D55_prev
		  ,MAX([D56_prev_max]) as D56_prev
		  ,MAX([D57_prev_max]) as D57_prev
		  ,MAX([D58_prev_max]) as D58_prev
		  ,MAX([D59_prev_max]) as D59_prev
		  ,MAX([D60_prev_max]) as D60_prev
		  ,MAX([D61_prev_max]) as D61_prev
		  ,MAX([D62_prev_max]) as D62_prev
		  ,MAX([D63_prev_max]) as D63_prev
		  ,MAX([D64_prev_max]) as D64_prev
		  ,MAX([D65_prev_max]) as D65_prev
		  ,MAX([D66_prev_max]) as D66_prev
		  ,MAX([D67_prev_max]) as D67_prev
		  ,MAX([D68_prev_max]) as D68_prev
		  ,MAX([D69_prev_max]) as D69_prev
		  ,MAX([D70_prev_max]) as D70_prev
		  ,MAX([D71_prev_max]) as D71_prev
		  ,MAX([D72_prev_max]) as D72_prev
		  ,MAX([D73_prev_max]) as D73_prev
		  ,MAX([D74_prev_max]) as D74_prev
		  ,MAX([D75_prev_max]) as D75_prev
		  ,MAX([D76_prev_max]) as D76_prev
		  ,MAX([D77_prev_max]) as D77_prev
		  ,MAX([D78_prev_max]) as D78_prev
		  ,MAX([D80_prev_max]) as D80_prev
		  ,MAX([D81_prev_max]) as D81_prev
		  ,MAX([D82_prev_max]) as D82_prev
		  ,MAX([D83_prev_max]) as D83_prev
		  ,MAX([D84_prev_max]) as D84_prev
		  ,MAX([D85_prev_max]) as D85_prev
		  ,MAX([D86_prev_max]) as D86_prev
		  ,MAX([D87_prev_max]) as D87_prev
		  ,MAX([D88_prev_max]) as D88_prev
		  ,MAX([D89_prev_max]) as D89_prev
		  ,MAX([D90_prev_max]) as D90_prev
		  ,MAX([D91_prev_max]) as D91_prev
		  ,MAX([D92_prev_max]) as D92_prev
		  ,MAX([D95_prev_max]) as D95_prev
		  ,MAX([D98_prev_max]) as D98_prev
		  ,MAX([D101_prev_max]) as D101_prev
		  ,MAX([D103_prev_max]) as D103_prev
		  ,MAX([D104_prev_max]) as D104_prev
		  ,MAX([D107_prev_max]) as D107_prev
		  ,MAX([D108_prev_max]) as D108_prev
		  ,MAX([D109_prev_max]) as D109_prev
		  ,MAX([D110_prev_max]) as D110_prev
		  ,MIN([Developed_min]) as Developed
		  ,MIN([Urban_GridCode_min]) as Urban_GridCode
		From [bd].DiseaseMapMaxValue_111_dev
		Group by countryGeonameId
		  ,[Seasonality_Zone]

	End
	Select 1 as Result

END