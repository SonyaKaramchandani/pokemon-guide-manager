using System;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.UnitTest.Data
{
    public class GeonameQueryBuilderDatabaseFixture : IDisposable
    {
        public static readonly Geonames COUNTRY_A = new Geonames
        {
            GeonameId = 1,
            LocationType = (int) LocationType.Country,
            CountryGeonameId = 1,
            CountryName = "Canada",
            Name = "Canada",
            DisplayName = "Canada",
            Latitude = 60.10867m,
            Longitude = -113.64258m,
            CountryProvinceShapes = new CountryProvinceShapes {SimplifiedShapeText = "Shape Text for Canada"}
        };

        public static readonly Geonames COUNTRY_B = new Geonames
        {
            GeonameId = 2,
            LocationType = (int) LocationType.Country,
            CountryGeonameId = 2,
            CountryName = "United States",
            Name = "United States of America",
            DisplayName = "United States",
            Latitude = 39.76000m,
            Longitude = -98.50000m,
            CountryProvinceShapes = new CountryProvinceShapes {SimplifiedShapeText = "Shape Text for United States"}
        };

        public static readonly Geonames PROVINCE_A1 = new Geonames
        {
            GeonameId = 3,
            LocationType = (int) LocationType.Province,
            CountryGeoname = COUNTRY_A,
            Admin1GeonameId = 3,
            CountryName = "Canada",
            Name = "British Columbia",
            DisplayName = "British Columbia, Canada",
            Latitude = 53.99983m,
            Longitude = -125.00320m,
            CountryProvinceShapes = new CountryProvinceShapes {SimplifiedShapeText = "Shape Text for British Columbia"}
        };

        public static readonly Geonames PROVINCE_A2 = new Geonames
        {
            GeonameId = 4,
            LocationType = (int) LocationType.Province,
            CountryGeoname = COUNTRY_A,
            Admin1GeonameId = 4,
            CountryName = "Canada",
            Name = "Ontario",
            DisplayName = "Ontario, Canada",
            Latitude = 49.25014m,
            Longitude = -84.49983m,
            CountryProvinceShapes = new CountryProvinceShapes {SimplifiedShapeText = "Shape Text for Ontario"}
        };

        public static readonly Geonames PROVINCE_B1 = new Geonames
        {
            GeonameId = 5,
            LocationType = (int) LocationType.Province,
            CountryGeoname = COUNTRY_B,
            Admin1GeonameId = 5,
            CountryName = "United States",
            Name = "Washington",
            DisplayName = "Washington, United States",
            Latitude = 47.50012m,
            Longitude = -120.50147m,
            CountryProvinceShapes = new CountryProvinceShapes {SimplifiedShapeText = "Shape Text for Washington"}
        };

        public static readonly Geonames CITY_A1_A = new Geonames
        {
            GeonameId = 6,
            LocationType = (int) LocationType.City,
            CountryGeoname = COUNTRY_A,
            CountryName = "Canada",
            Admin1Geoname = PROVINCE_A1,
            Name = "Vancouver",
            DisplayName = "Vancouver, British Columbia, Canada",
            Latitude = 49.24861m,
            Longitude = -123.10784m,
            CountryProvinceShapes = null
        };

        public static readonly Geonames CITY_A1_B = new Geonames
        {
            GeonameId = 7,
            LocationType = (int) LocationType.City,
            CountryGeoname = COUNTRY_A,
            CountryName = "Canada",
            Admin1Geoname = PROVINCE_A1,
            Name = "Richmond",
            DisplayName = "Richmond, British Columbia, Canada",
            Latitude = 49.17003m,
            Longitude = -123.13683m,
            CountryProvinceShapes = null
        };

        public static readonly Geonames CITY_B1_A = new Geonames
        {
            GeonameId = 8,
            LocationType = (int) LocationType.City,
            CountryGeoname = COUNTRY_B,
            CountryName = "United States",
            Admin1Geoname = PROVINCE_B1,
            Name = "Seattle",
            DisplayName = "Seattle, Washington, United States",
            Latitude = 47.60621m,
            Longitude = -122.33207m,
            CountryProvinceShapes = null
        };

        public static readonly Geonames UNKNOWN = new Geonames
        {
            GeonameId = 9,
            LocationType = null,
            CountryGeoname = null,
            CountryName = null,
            Admin1Geoname = null,
            Name = "Mars",
            DisplayName = "Crater Lake, Mars",
            Latitude = null,
            Longitude = null,
            Shape = null
        };

        public BiodZebraContext DbContext { get; }

        public GeonameQueryBuilderDatabaseFixture()
        {
            DbContext = new BiodZebraContext(new DbContextOptionsBuilder<BiodZebraContext>()
                .UseInMemoryDatabase("GeonameQueryBuilderDatabaseFixture")
                .Options);

            DbContext.Geonames.Add(UNKNOWN);
            DbContext.Geonames.Add(COUNTRY_A);
            DbContext.Geonames.Add(COUNTRY_B);
            DbContext.Geonames.Add(PROVINCE_A1);
            DbContext.Geonames.Add(PROVINCE_A2);
            DbContext.Geonames.Add(PROVINCE_B1);
            DbContext.Geonames.Add(CITY_A1_A);
            DbContext.Geonames.Add(CITY_A1_B);
            DbContext.Geonames.Add(CITY_B1_A);

            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}