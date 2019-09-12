using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Solution.UnitTest
{
    [TestClass()]
    public class SqlServerGeorgeDiseasesAPIDatabaseUnitTest : SqlDatabaseTestClass
    {

        public SqlServerGeorgeDiseasesAPIDatabaseUnitTest()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction bd_diseaseConditionsAroundPoint_spTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerGeorgeDiseasesAPIDatabaseUnitTest));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction bd_diseaseConditionsByGeonameId_spTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction bd_usp_HcwGetDiseaseIntroductionTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction bd_usp_PullRegularTablesTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            this.bd_diseaseConditionsAroundPoint_spTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.bd_diseaseConditionsByGeonameId_spTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.bd_usp_HcwGetDiseaseIntroductionTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.bd_usp_PullRegularTablesTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            bd_diseaseConditionsAroundPoint_spTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            bd_diseaseConditionsByGeonameId_spTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            bd_usp_HcwGetDiseaseIntroductionTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            bd_usp_PullRegularTablesTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // bd_diseaseConditionsAroundPoint_spTestData
            // 
            this.bd_diseaseConditionsAroundPoint_spTestData.PosttestAction = null;
            this.bd_diseaseConditionsAroundPoint_spTestData.PretestAction = null;
            this.bd_diseaseConditionsAroundPoint_spTestData.TestAction = bd_diseaseConditionsAroundPoint_spTest_TestAction;
            // 
            // bd_diseaseConditionsAroundPoint_spTest_TestAction
            // 
            bd_diseaseConditionsAroundPoint_spTest_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(bd_diseaseConditionsAroundPoint_spTest_TestAction, "bd_diseaseConditionsAroundPoint_spTest_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // bd_diseaseConditionsByGeonameId_spTestData
            // 
            this.bd_diseaseConditionsByGeonameId_spTestData.PosttestAction = null;
            this.bd_diseaseConditionsByGeonameId_spTestData.PretestAction = null;
            this.bd_diseaseConditionsByGeonameId_spTestData.TestAction = bd_diseaseConditionsByGeonameId_spTest_TestAction;
            // 
            // bd_diseaseConditionsByGeonameId_spTest_TestAction
            // 
            bd_diseaseConditionsByGeonameId_spTest_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(bd_diseaseConditionsByGeonameId_spTest_TestAction, "bd_diseaseConditionsByGeonameId_spTest_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // bd_usp_HcwGetDiseaseIntroductionTestData
            // 
            this.bd_usp_HcwGetDiseaseIntroductionTestData.PosttestAction = null;
            this.bd_usp_HcwGetDiseaseIntroductionTestData.PretestAction = null;
            this.bd_usp_HcwGetDiseaseIntroductionTestData.TestAction = bd_usp_HcwGetDiseaseIntroductionTest_TestAction;
            // 
            // bd_usp_HcwGetDiseaseIntroductionTest_TestAction
            // 
            bd_usp_HcwGetDiseaseIntroductionTest_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(bd_usp_HcwGetDiseaseIntroductionTest_TestAction, "bd_usp_HcwGetDiseaseIntroductionTest_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // bd_usp_PullRegularTablesTestData
            // 
            this.bd_usp_PullRegularTablesTestData.PosttestAction = null;
            this.bd_usp_PullRegularTablesTestData.PretestAction = null;
            this.bd_usp_PullRegularTablesTestData.TestAction = bd_usp_PullRegularTablesTest_TestAction;
            // 
            // bd_usp_PullRegularTablesTest_TestAction
            // 
            bd_usp_PullRegularTablesTest_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(bd_usp_PullRegularTablesTest_TestAction, "bd_usp_PullRegularTablesTest_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void bd_diseaseConditionsAroundPoint_spTest()
        {
            SqlDatabaseTestActions testActions = this.bd_diseaseConditionsAroundPoint_spTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void bd_diseaseConditionsByGeonameId_spTest()
        {
            SqlDatabaseTestActions testActions = this.bd_diseaseConditionsByGeonameId_spTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void bd_usp_HcwGetDiseaseIntroductionTest()
        {
            SqlDatabaseTestActions testActions = this.bd_usp_HcwGetDiseaseIntroductionTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void bd_usp_PullRegularTablesTest()
        {
            SqlDatabaseTestActions testActions = this.bd_usp_PullRegularTablesTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions bd_diseaseConditionsAroundPoint_spTestData;
        private SqlDatabaseTestActions bd_diseaseConditionsByGeonameId_spTestData;
        private SqlDatabaseTestActions bd_usp_HcwGetDiseaseIntroductionTestData;
        private SqlDatabaseTestActions bd_usp_PullRegularTablesTestData;
    }
}
