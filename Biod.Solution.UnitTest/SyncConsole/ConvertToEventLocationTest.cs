using Biod.Surveillance.Zebra.SyncConsole;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biod.Zebra.Library.EntityModels.Surveillance;
using System;

namespace Biod.Solution.UnitTest.SyncConsole
{
    /// <summary>
    /// Tests the ConvertToEventLocation method in the SyncConsole program
    /// </summary>
    [TestClass]
    public class ConvertToEventLocationTest
    {
        private readonly Random random = new Random();

        /// <summary>
        /// Tests whether the exception is thrown when null is passed for the event location model
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "An event location of null was inappropriately allowed")]
        public void NullObject()
        {
            Program.ConvertToEventLocation(null);
        }

        /// <summary>
        /// Tests whether all fields are correctly converted to the output model
        /// </summary>
        [TestMethod]
        public void AllFields()
        {
            int geonameId = random.Next(1, 10000);
            DateTime dateTime = DateTime.Now.AddMinutes(random.Next(1, 10000));
            int suspCases = random.Next(1, 10000);
            int confCases = random.Next(1, 10000);
            int repCases = random.Next(1, 10000);
            int deaths = random.Next(1, 10000);

            SurveillanceXtbl_Event_Location location = new SurveillanceXtbl_Event_Location()
            {
                GeonameId = geonameId,
                EventDate = dateTime,
                SuspCases = suspCases,
                ConfCases = confCases,
                RepCases = repCases,
                Deaths = deaths
            };

            SurveillanceXtbl_Event_Location result = Program.ConvertToEventLocation(location);

            Assert.AreEqual(result.GeonameId, geonameId, "GeonameId not mapped correctly");
            Assert.AreEqual(result.SuspCases, suspCases, "SuspCases not mapped correctly");
            Assert.AreEqual(result.ConfCases, confCases, "ConfCases not mapped correctly");
            Assert.AreEqual(result.RepCases, repCases, "RepCases not mapped correctly");
            Assert.AreEqual(result.Deaths, deaths, "Deaths not mapped correctly");
        }

        /// <summary>
        /// Tests whether the fields with null are correctly being set to a default in the converted model
        /// </summary>
        [TestMethod]
        public void EmptyFields()
        {
            SurveillanceXtbl_Event_Location location = new SurveillanceXtbl_Event_Location()
            {
                SuspCases = null,
                ConfCases = null,
                RepCases = null,
                Deaths = null
            };

            SurveillanceXtbl_Event_Location result = Program.ConvertToEventLocation(location);

            Assert.AreEqual(result.SuspCases, 0, "Empty SuspCases field did not default to 0");
            Assert.AreEqual(result.ConfCases, 0, "Empty ConfCases field did not default to 0");
            Assert.AreEqual(result.RepCases, 0, "Empty RepCases field did not default to 0");
            Assert.AreEqual(result.Deaths, 0, "Empty Deaths field did not default to 0");
        }
    }
}
