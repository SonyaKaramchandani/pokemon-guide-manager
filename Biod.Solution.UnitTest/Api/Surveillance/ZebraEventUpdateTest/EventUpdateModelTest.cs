using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Biod.Zebra.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest.Api.Surveillance
{
    /// <summary>
    /// Tests the EventUpdateModel data annotation model validation for the required fields
    /// </summary>
    [TestClass]
    public class EventUpdateModelTest
    {
        /// <summary>
        /// Tests for failed model validation for invalid Event ID
        /// </summary>
        [TestMethod]
        public void NullEventId()
        {
            var model = new EventUpdateModel()
            {
                eventID = null,
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Event ID not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Event ID
        /// </summary>
        [TestMethod]
        public void EmptyEventId()
        {
            var model = new EventUpdateModel()
            {
                eventID = "",
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Event ID not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Event ID
        /// </summary>
        [TestMethod]
        public void NonNumericEventId()
        {
            var model = new EventUpdateModel()
            {
                eventID = "ABC",
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Event ID not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Alert Radius
        /// </summary>
        [TestMethod]
        public void NullAlertRadius()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                alertRadius = null,
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Alert Radius not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Alert Radius
        /// </summary>
        [TestMethod]
        public void NonBooleanAlertRadius()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                alertRadius = "123",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Alert Radius not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Reason Id
        /// </summary>
        [TestMethod]
        public void NullReasonIdArray()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                alertRadius = "true",
                reasonIDs = null,
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Reason Id Array not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Start Date
        /// </summary>
        [TestMethod]
        public void NullStartDate()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = null
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Start Date not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for failed model validation for invalid Start Date
        /// </summary>
        [TestMethod]
        public void EmptyStartDate()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = ""
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsFalse(isModelStateValid, "Invalid Start Date not failing model validation");
            Assert.AreEqual(results.Count, 1, "Unexpected errors returned in model validation");
        }

        /// <summary>
        /// Tests for successful model validation for a valid model
        /// </summary>
        [TestMethod]
        public void ValidModel()
        {
            var model = new EventUpdateModel()
            {
                eventID = "123",
                alertRadius = "true",
                reasonIDs = new string[0],
                startDate = DateTime.Now.ToString()
            };

            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.IsTrue(isModelStateValid, "Model validation failing for a valid model");
        }

    }
}
