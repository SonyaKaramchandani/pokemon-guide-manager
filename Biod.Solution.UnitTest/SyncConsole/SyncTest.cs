using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Biod.Surveillance.Zebra.SyncConsole.EntityModels;
using Biod.Surveillance.Zebra.SyncConsole;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Biod.Zebra.Library.Infrastructures.Log;

namespace Biod.Solution.UnitTest.SyncConsole
{
    /// <summary>
    /// Tests the Sync method in the SyncConsole program
    /// </summary>
    [TestClass]
    public class SyncTest
    {
        private readonly HttpClient successHttpClient = new HttpClient(new CustomHttpMessageHandler.SuccessHandler());
        private readonly HttpClient failureHttpClient = new HttpClient(new CustomHttpMessageHandler.FailureHandler());
        private readonly IConsoleLogger noOpConsoleLogger = new NoOpConsoleLogger();

        private Mock<BiodSurveillanceDataModelEntities> mockDbContext;
        private MockDbSet dbMock;

        [TestInitialize()]
        public void Initialize()
        {
            dbMock = new MockDbSet();
            mockDbContext = dbMock.MockContext;

            successHttpClient.BaseAddress = new Uri("http://localhost");
            failureHttpClient.BaseAddress = new Uri("http://localhost");
        }

        /// <summary>
        /// Tests that the number of successful updates is correct.
        /// </summary>
        [TestMethod]
        public async Task TestAllSuccessCount()
        {
            int actualCount = mockDbContext.Object.Events.Where(e => e.IsPublished == true).Count();

            int result = await Program.Sync(mockDbContext.Object, successHttpClient, noOpConsoleLogger);

            Assert.AreEqual(actualCount, result);
        }

        /// <summary>
        /// Tests that the number of failed updates is correct.
        /// </summary>
        [TestMethod]
        public async Task TestAllFailureCount()
        {
            int result = await Program.Sync(mockDbContext.Object, failureHttpClient, noOpConsoleLogger);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Console that does no-operation, used for dependency injection
        /// </summary>
        public class NoOpConsoleLogger : IConsoleLogger
        {
            public void UpdateConsole(string message)
            {
                return;
            }
        }
    }
}
