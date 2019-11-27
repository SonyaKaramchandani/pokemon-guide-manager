using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Biod.Zebra.Library.Models;
using System;
using Biod.Zebra.Library.Infrastructures;
using System.Linq;

namespace Biod.Solution.UnitTest.Models.Event
{
    [TestClass]
    public class EventCaseCountModelTest
    {
        private const int GEONAME_ID_TORONTO = 1;
        private const int GEONAME_ID_ONTARIO = 2;
        private const int GEONAME_ID_CANADA = 3;
        private const int GEONAME_ID_MONTREAL = 4;
        private const int GEONAME_ID_OTTAWA = 5;
        private const int GEONAME_ID_QUEBEC = 6;
        private const int GEONAME_ID_UNITED_STATES = 7;
        private readonly DateTime EventDate = DateTime.Now;

        #region Build Dependency Tree

        [TestMethod]
        public void Test_Tree_BuildComplete()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);

            Assert.AreEqual(1, models.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_CANADA }.SetEquals(new HashSet<int>(models.Keys)));
            Assert.AreEqual(1, models[GEONAME_ID_CANADA].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_ONTARIO }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_TORONTO, GEONAME_ID_OTTAWA }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Keys)));
        }

        [TestMethod]
        public void Test_Tree_BuildMissingCity()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_QUEBEC, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_QUEBEC,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);

            Assert.AreEqual(1, models.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_CANADA }.SetEquals(new HashSet<int>(models.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_CANADA].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_ONTARIO, GEONAME_ID_QUEBEC }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_TORONTO, GEONAME_ID_OTTAWA }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Keys)));
            Assert.AreEqual(0, models[GEONAME_ID_CANADA].Children[GEONAME_ID_QUEBEC].Children.Count);
        }

        [TestMethod]
        public void Test_Tree_BuildMissingProvince()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);

            Assert.AreEqual(1, models.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_CANADA }.SetEquals(new HashSet<int>(models.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_CANADA].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_ONTARIO, GEONAME_ID_MONTREAL }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_TORONTO, GEONAME_ID_OTTAWA }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Keys)));
            Assert.AreEqual(0, models[GEONAME_ID_CANADA].Children[GEONAME_ID_MONTREAL].Children.Count);
        }

        [TestMethod]
        public void Test_Tree_BuildMissingCountry()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);

            Assert.AreEqual(2, models.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_ONTARIO, GEONAME_ID_MONTREAL }.SetEquals(new HashSet<int>(models.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_ONTARIO].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_TORONTO, GEONAME_ID_OTTAWA }.SetEquals(new HashSet<int>(models[GEONAME_ID_ONTARIO].Children.Keys)));
            Assert.AreEqual(0, models[GEONAME_ID_MONTREAL].Children.Count);
        }

        [TestMethod]
        public void Test_Tree_BuildMultiCountry()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 5} },
                { GEONAME_ID_QUEBEC, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_QUEBEC,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 7} },
                { GEONAME_ID_UNITED_STATES, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_UNITED_STATES,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_UNITED_STATES, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 5} }
            };
            EventCaseCountModel.BuildDependencyTree(models);

            Assert.AreEqual(2, models.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_CANADA, GEONAME_ID_UNITED_STATES }.SetEquals(new HashSet<int>(models.Keys)));
            Assert.AreEqual(2, models[GEONAME_ID_CANADA].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_ONTARIO, GEONAME_ID_QUEBEC }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children.Keys)));
            Assert.AreEqual(1, models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_TORONTO }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children.Keys)));
            Assert.AreEqual(1, models[GEONAME_ID_CANADA].Children[GEONAME_ID_QUEBEC].Children.Count);
            Assert.IsTrue(new HashSet<int> { GEONAME_ID_MONTREAL }.SetEquals(new HashSet<int>(models[GEONAME_ID_CANADA].Children[GEONAME_ID_QUEBEC].Children.Keys)));
            Assert.AreEqual(0, models[GEONAME_ID_CANADA].Children[GEONAME_ID_ONTARIO].Children[GEONAME_ID_TORONTO].Children.Count);
            Assert.AreEqual(0, models[GEONAME_ID_CANADA].Children[GEONAME_ID_QUEBEC].Children[GEONAME_ID_MONTREAL].Children.Count);
        }

        #endregion

        #region Flatten Dependency Tree

        [TestMethod]
        public void Test_Tree_FlattenComplete()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null, 
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                    {
                        { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO, 
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                            {
                                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} }
                            } 
                        } }
                    } 
                } }
            };
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(4, flattened.Count);
            Assert.IsTrue(new HashSet<int> { 
                GEONAME_ID_TORONTO, 
                GEONAME_ID_OTTAWA, 
                GEONAME_ID_ONTARIO, 
                GEONAME_ID_CANADA 
            }.SetEquals(new HashSet<int>(flattened.Keys)));
        }

        [TestMethod]
        public void Test_Tree_FlattenMissingCity()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                    {
                        { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                            {
                                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} }
                            }
                        } },
                        { GEONAME_ID_QUEBEC, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_QUEBEC, Admin1GeonameId = GEONAME_ID_QUEBEC,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1 } }
                    }
                } }
            };
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(5, flattened.Count);
            Assert.IsTrue(new HashSet<int> { 
                GEONAME_ID_TORONTO, 
                GEONAME_ID_OTTAWA, 
                GEONAME_ID_ONTARIO, 
                GEONAME_ID_QUEBEC, 
                GEONAME_ID_CANADA 
            }.SetEquals(new HashSet<int>(flattened.Keys)));
        }

        [TestMethod]
        public void Test_Tree_FlattenMissingProvince()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                    {
                        { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                            {
                                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} }
                            }
                        } },
                        { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } }
                    }
                } }
            };
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(5, flattened.Count);
            Assert.IsTrue(new HashSet<int> { 
                GEONAME_ID_TORONTO, 
                GEONAME_ID_OTTAWA, 
                GEONAME_ID_ONTARIO, 
                GEONAME_ID_MONTREAL, 
                GEONAME_ID_CANADA 
            }.SetEquals(new HashSet<int>(flattened.Keys)));
        }

        [TestMethod]
        public void Test_Tree_FlattenMissingCountry()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                    {
                        { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                        { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA, Admin1GeonameId = GEONAME_ID_ONTARIO,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} }
                    }
                } },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } }
            };
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(4, flattened.Count);
            Assert.IsTrue(new HashSet<int> { 
                GEONAME_ID_TORONTO, 
                GEONAME_ID_OTTAWA, 
                GEONAME_ID_ONTARIO, 
                GEONAME_ID_MONTREAL 
            }.SetEquals(new HashSet<int>(flattened.Keys)));
        }

        [TestMethod]
        public void Test_Tree_FlattenMultiCountry()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 7, Children = new Dictionary<int, EventCaseCountModel>
                    {
                        { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, Children = new Dictionary<int, EventCaseCountModel>
                            {
                                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3} }
                            }
                        } },
                        { GEONAME_ID_QUEBEC, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_QUEBEC, Admin1GeonameId = GEONAME_ID_QUEBEC,
                            CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2, Children = new Dictionary<int, EventCaseCountModel>
                            {
                                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 5 } }
                            }
                        } }
                    }
                } },
                { GEONAME_ID_UNITED_STATES, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_UNITED_STATES, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_UNITED_STATES, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 5 } }
            };
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(6, flattened.Count);
            Assert.IsTrue(new HashSet<int> { 
                GEONAME_ID_TORONTO, 
                GEONAME_ID_ONTARIO, 
                GEONAME_ID_MONTREAL, 
                GEONAME_ID_QUEBEC, 
                GEONAME_ID_CANADA, 
                GEONAME_ID_UNITED_STATES 
            }.SetEquals(new HashSet<int>(flattened.Keys)));
        }

        #endregion

        #region Nesting Calculation

        /// <summary>
        /// Verifies that if the city-level case count is higher than the province level and the country level, 
        /// the city-level case count will be considered as the case count for all levels.
        /// </summary>
        [TestMethod]
        public void Test_Nesting_CityIncreased()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(3, flattened.Count);
            Assert.AreEqual(4, flattened[GEONAME_ID_TORONTO].GetNestedCaseCount());
            Assert.AreEqual(4, flattened[GEONAME_ID_ONTARIO].GetNestedCaseCount());
            Assert.AreEqual(4, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that if the province-level case count is higher than the country level, 
        /// the province-level case count will be considered as the case count for country level
        /// and the city level will be untouched.
        /// </summary>
        [TestMethod]
        public void Test_Nesting_ProvinceIncreased()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 5} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(3, flattened.Count);
            Assert.AreEqual(2, flattened[GEONAME_ID_TORONTO].GetNestedCaseCount());
            Assert.AreEqual(5, flattened[GEONAME_ID_ONTARIO].GetNestedCaseCount());
            Assert.AreEqual(5, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that if the city-level case count is less than the province level, 
        /// and the province-level case count is less than the country level,
        /// then no case count will be nested.
        /// </summary>
        [TestMethod]
        public void Test_Nesting_NoIncrease()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 3} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(3, flattened.Count);
            Assert.AreEqual(1, flattened[GEONAME_ID_TORONTO].GetNestedCaseCount());
            Assert.AreEqual(2, flattened[GEONAME_ID_ONTARIO].GetNestedCaseCount());
            Assert.AreEqual(3, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that if the province-level case count does not exist, 
        /// the city-level case count will be nested properly.
        [TestMethod]
        public void Test_Nesting_NoProvince()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(2, flattened.Count);
            Assert.AreEqual(3, flattened[GEONAME_ID_TORONTO].GetNestedCaseCount());
            Assert.AreEqual(3, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that if only the city-level case count exists, 
        /// no nesting will be done.
        [TestMethod]
        public void Test_Nesting_CityOnly()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(1, models.Count);
            Assert.AreEqual(3, models[GEONAME_ID_TORONTO].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that if only the province-level case count exists, 
        /// no nesting will be done.
        [TestMethod]
        public void Test_Nesting_ProvinceOnly()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(1, flattened.Count);
            Assert.AreEqual(2, flattened[GEONAME_ID_ONTARIO].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that if only the country-level case count exists, 
        /// no nesting will be done.
        [TestMethod]
        public void Test_Nesting_CountryOnly()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 3} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(1, flattened.Count);
            Assert.AreEqual(3, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that when there are multiple city case counts, only cities that belong to the same province get nested together.
        /// </summary>
        [TestMethod]
        public void Test_Nesting_MultiCityIncreased()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(5, flattened.Count);
            Assert.AreEqual(4, flattened[GEONAME_ID_TORONTO].GetNestedCaseCount());
            Assert.AreEqual(4, flattened[GEONAME_ID_OTTAWA].GetNestedCaseCount());
            Assert.AreEqual(4, flattened[GEONAME_ID_MONTREAL].GetNestedCaseCount());
            Assert.AreEqual(8, flattened[GEONAME_ID_ONTARIO].GetNestedCaseCount());
            Assert.AreEqual(12, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
        }

        /// <summary>
        /// Verifies that when there are multiple province case counts, only provinces that belong to the same country get nested together.
        /// </summary>
        [TestMethod]
        public void Test_Nesting_MultiProvinceIncreased()
        {
            var models = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3} },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO,
                    Admin1GeonameId = GEONAME_ID_ONTARIO, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1} },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 5} },
                { GEONAME_ID_QUEBEC, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_QUEBEC,
                    Admin1GeonameId = GEONAME_ID_QUEBEC, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2} },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 7} },
                { GEONAME_ID_UNITED_STATES, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_UNITED_STATES,
                    Admin1GeonameId = null, CountryGeonameId = GEONAME_ID_UNITED_STATES, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 5} }
            };
            EventCaseCountModel.BuildDependencyTree(models);
            EventCaseCountModel.ApplyNesting(models);
            var flattened = EventCaseCountModel.FlattenTree(models);

            Assert.AreEqual(6, flattened.Count);
            Assert.AreEqual(3, flattened[GEONAME_ID_TORONTO].GetNestedCaseCount());
            Assert.AreEqual(3, flattened[GEONAME_ID_ONTARIO].GetNestedCaseCount());
            Assert.AreEqual(5, flattened[GEONAME_ID_MONTREAL].GetNestedCaseCount());
            Assert.AreEqual(5, flattened[GEONAME_ID_QUEBEC].GetNestedCaseCount());
            Assert.AreEqual(8, flattened[GEONAME_ID_CANADA].GetNestedCaseCount());
            Assert.AreEqual(5, flattened[GEONAME_ID_UNITED_STATES].GetNestedCaseCount());
        }

        #endregion

        #region Delta Calculation

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     1
        /// Canada      1  -->  Canada      1
        /// </summary>
        [TestMethod]
        public void Test_Delta_NoIncrease()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            Assert.IsTrue(AreModelsEqual(new Dictionary<int, EventCaseCountModel> { }, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     2  -->  Toronto     1
        /// Canada      3  -->  Canada      2
        /// </summary>
        [TestMethod]
        public void Test_Delta_Decreased()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 3, ChildrenCaseCount = 2 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 2, ChildrenCaseCount = 1 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            Assert.IsTrue(AreModelsEqual(new Dictionary<int, EventCaseCountModel> { }, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     2
        /// Ontario     1  -->  Ontario     1
        /// Canada      1  -->  Canada      1
        /// </summary>
        [TestMethod]
        public void Test_Delta_CityIncreased()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO, 
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO, 
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null, 
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 2 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 2 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     1
        /// Ontario     1  -->  Ontario     2
        /// Canada      1  -->  Canada      1
        /// </summary>
        [TestMethod]
        public void Test_Delta_ProvinceIncreased1()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 2 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     1
        /// Ontario     7  -->  Ontario     10
        /// Canada      1  -->  Canada      1
        /// Ottawa      2  -->  Ottawa      2
        /// </summary>
        [TestMethod]
        public void Test_Delta_ProvinceIncreased2()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 7, ChildrenCaseCount = 3 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 7 } },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 10, ChildrenCaseCount = 3 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 10 } },
                { GEONAME_ID_OTTAWA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_OTTAWA, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 3 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     1
        /// Ontario     1  -->  Ontario     1
        /// Canada      1  -->  Canada      2
        /// </summary>
        [TestMethod]
        public void Test_Delta_CountryIncreased()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 2, ChildrenCaseCount = 1 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     1
        /// Ontario     1  -->  Ontario     1
        /// Canada      1  -->  Canada      1
        ///                -->  Montreal    1
        /// </summary>
        [TestMethod]
        public void Test_Delta_NewCity()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     2
        /// Ontario     1  -->  Ontario     2
        /// Canada      1  -->  Canada      1
        /// </summary>
        [TestMethod]
        public void Test_Delta_CityAndProvinceIncreasedEqually()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2, ChildrenCaseCount = 2 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 2 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     2
        /// Ontario     1  -->  Ontario     3
        /// Canada      1  -->  Canada      1
        /// </summary>
        [TestMethod]
        public void Test_Delta_CityAndProvinceIncreased1()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 3, ChildrenCaseCount = 2 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 3 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     4
        /// Ontario     6  -->  Ontario     8
        /// Canada      6  -->  Canada      8
        /// </summary>
        [TestMethod]
        public void Test_Delta_CityAndProvinceIncreased2()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 6, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 6, ChildrenCaseCount = 6 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 8, ChildrenCaseCount = 4 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 8, ChildrenCaseCount = 8 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 2 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     4  -->  Toronto     5
        /// Ontario     1  -->  Ontario     3
        /// Canada      1  -->  Canada      11
        ///                -->  Montreal    6
        /// </summary>
        [TestMethod]
        public void Test_Delta_ProvinceIncreasedLessThanCity()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 4, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 4 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 4 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 5, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 3, ChildrenCaseCount = 5 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 11, ChildrenCaseCount = 11 } },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 6, ChildrenCaseCount = 0 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } },
                { GEONAME_ID_MONTREAL, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_MONTREAL, Admin1GeonameId = GEONAME_ID_QUEBEC,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 6 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     2
        /// Ontario     1  -->  Ontario     3
        /// Canada      1  -->  Canada      4
        /// </summary>
        [TestMethod]
        public void Test_Delta_AllLevelsIncreased()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 3, ChildrenCaseCount = 2 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 4, ChildrenCaseCount = 3 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        /// <summary>
        /// Raw Data:
        /// Toronto     1  -->  Toronto     3
        ///                -->  Ontario     1
        /// Canada      1  -->  Canada      3
        /// </summary>
        [TestMethod]
        public void Test_Delta_CityIncreasedAndNewProvince()
        {
            var previous = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 1, ChildrenCaseCount = 0 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 1, ChildrenCaseCount = 1 } }
            };

            var current = new Dictionary<int, EventCaseCountModel>()
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 3, ChildrenCaseCount = 0 } },
                { GEONAME_ID_ONTARIO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_ONTARIO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.PROVINCE, RawCaseCount = 1, ChildrenCaseCount = 3 } },
                { GEONAME_ID_CANADA, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_CANADA, Admin1GeonameId = null,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.COUNTRY, RawCaseCount = 3, ChildrenCaseCount = 3 } }
            };
            var delta = EventCaseCountModel.GetIncreasedCaseCount(previous, current);

            var expected = new Dictionary<int, EventCaseCountModel>
            {
                { GEONAME_ID_TORONTO, new EventCaseCountModel { EventDate = EventDate, GeonameId = GEONAME_ID_TORONTO, Admin1GeonameId = GEONAME_ID_ONTARIO,
                    CountryGeonameId = GEONAME_ID_CANADA, LocationType = Constants.LocationType.CITY, RawCaseCount = 2 } }
            };
            Assert.IsTrue(AreModelsEqual(expected, delta));
        }

        #endregion

        #region Helper Functions
        
        private bool AreModelsEqual(Dictionary<int, EventCaseCountModel> expected, Dictionary<int, EventCaseCountModel> actual)
        {
            return (expected.Count == actual.Count) && !expected.Except(actual).Any() && !actual.Except(expected).Any();
        }

        #endregion
    }
}
