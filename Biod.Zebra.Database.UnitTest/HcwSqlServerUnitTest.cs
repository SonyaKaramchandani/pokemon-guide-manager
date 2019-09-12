using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Biod.Zebra.Database.UnitTest
{
    [TestClass()]
    public class HcwSqlServerUnitTest : SqlDatabaseTestClass
    {

        public HcwSqlServerUnitTest()
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction hcw_usp_HcwGetDiseaseByIncubationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction hcw_usp_HcwGetDiseaseSymptomScoreTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HcwSqlServerUnitTest));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition expectedSchemaCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition expectedSchemaCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition expectedSchemaCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition expectedSchemaCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            this.hcw_usp_HcwGetDiseaseByIncubationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.hcw_usp_HcwGetDiseaseDetailInfoTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            //this.hcw_usp_HcwGetDiseaseSymptomScoreTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.hcw_usp_HcwGetDiseaseVaccineInfoTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            hcw_usp_HcwGetDiseaseByIncubationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            //hcw_usp_HcwGetDiseaseSymptomScoreTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            expectedSchemaCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition();
            expectedSchemaCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition();
            expectedSchemaCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            expectedSchemaCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExpectedSchemaCondition();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            // 
            // hcw_usp_HcwGetDiseaseByIncubationTest_TestAction
            // 
            hcw_usp_HcwGetDiseaseByIncubationTest_TestAction.Conditions.Add(expectedSchemaCondition1);
            resources.ApplyResources(hcw_usp_HcwGetDiseaseByIncubationTest_TestAction, "hcw_usp_HcwGetDiseaseByIncubationTest_TestAction");
            // 
            // hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction
            // 
            hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction.Conditions.Add(expectedSchemaCondition2);
            resources.ApplyResources(hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction, "hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction");
            // 
            // hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction
            // 
            hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction.Conditions.Add(expectedSchemaCondition3);
            hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction.Conditions.Add(rowCountCondition1);
            resources.ApplyResources(hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction, "hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction");
            // 
            // hcw_usp_HcwGetDiseaseSymptomScoreTest_TestAction
            // 
            //resources.ApplyResources(hcw_usp_HcwGetDiseaseSymptomScoreTest_TestAction, "hcw_usp_HcwGetDiseaseSymptomScoreTest_TestAction");
            // 
            // hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction
            // 
            hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction.Conditions.Add(expectedSchemaCondition4);
            hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction.Conditions.Add(rowCountCondition2);
            resources.ApplyResources(hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction, "hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction");
            // 
            // hcw_usp_HcwGetDiseaseByIncubationTestData
            // 
            this.hcw_usp_HcwGetDiseaseByIncubationTestData.PosttestAction = null;
            this.hcw_usp_HcwGetDiseaseByIncubationTestData.PretestAction = null;
            this.hcw_usp_HcwGetDiseaseByIncubationTestData.TestAction = hcw_usp_HcwGetDiseaseByIncubationTest_TestAction;
            // 
            // hcw_usp_HcwGetDiseaseDetailInfoTestData
            // 
            this.hcw_usp_HcwGetDiseaseDetailInfoTestData.PosttestAction = null;
            this.hcw_usp_HcwGetDiseaseDetailInfoTestData.PretestAction = null;
            this.hcw_usp_HcwGetDiseaseDetailInfoTestData.TestAction = hcw_usp_HcwGetDiseaseDetailInfoTest_TestAction;
            // 
            // hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData
            // 
            this.hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData.PosttestAction = null;
            this.hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData.PretestAction = null;
            this.hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData.TestAction = hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest_TestAction;
            // 
            // hcw_usp_HcwGetDiseaseSymptomScoreTestData
            // 
            //this.hcw_usp_HcwGetDiseaseSymptomScoreTestData.PosttestAction = null;
            //this.hcw_usp_HcwGetDiseaseSymptomScoreTestData.PretestAction = null;
            //this.hcw_usp_HcwGetDiseaseSymptomScoreTestData.TestAction = hcw_usp_HcwGetDiseaseSymptomScoreTest_TestAction;
            // 
            // hcw_usp_HcwGetDiseaseVaccineInfoTestData
            // 
            this.hcw_usp_HcwGetDiseaseVaccineInfoTestData.PosttestAction = null;
            this.hcw_usp_HcwGetDiseaseVaccineInfoTestData.PretestAction = null;
            this.hcw_usp_HcwGetDiseaseVaccineInfoTestData.TestAction = hcw_usp_HcwGetDiseaseVaccineInfoTest_TestAction;
            // 
            // expectedSchemaCondition1
            // 
            expectedSchemaCondition1.Enabled = true;
            expectedSchemaCondition1.Name = "expectedSchemaCondition1";
            resources.ApplyResources(expectedSchemaCondition1, "expectedSchemaCondition1");
            expectedSchemaCondition1.Verbose = false;
            // 
            // expectedSchemaCondition2
            // 
            expectedSchemaCondition2.Enabled = true;
            expectedSchemaCondition2.Name = "expectedSchemaCondition2";
            resources.ApplyResources(expectedSchemaCondition2, "expectedSchemaCondition2");
            expectedSchemaCondition2.Verbose = false;
            // 
            // expectedSchemaCondition3
            // 
            expectedSchemaCondition3.Enabled = true;
            expectedSchemaCondition3.Name = "expectedSchemaCondition3";
            resources.ApplyResources(expectedSchemaCondition3, "expectedSchemaCondition3");
            expectedSchemaCondition3.Verbose = false;
            // 
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 1;
            // 
            // expectedSchemaCondition4
            // 
            expectedSchemaCondition4.Enabled = true;
            expectedSchemaCondition4.Name = "expectedSchemaCondition4";
            resources.ApplyResources(expectedSchemaCondition4, "expectedSchemaCondition4");
            expectedSchemaCondition4.Verbose = false;
            // 
            // rowCountCondition2
            // 
            rowCountCondition2.Enabled = true;
            rowCountCondition2.Name = "rowCountCondition2";
            rowCountCondition2.ResultSet = 1;
            rowCountCondition2.RowCount = 1;
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

        //[TestMethod()]
        public void hcw_usp_HcwGetDiseaseByIncubationTest()
        {
            SqlDatabaseTestActions testActions = this.hcw_usp_HcwGetDiseaseByIncubationTestData;
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

        //[TestMethod()]
        public void hcw_usp_HcwGetDiseaseDetailInfoTest()
        {
            SqlDatabaseTestActions testActions = this.hcw_usp_HcwGetDiseaseDetailInfoTestData;
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

        //[TestMethod()]
        public void hcw_usp_HcwGetDiseaseInfoByDiseaseIdTest()
        {
            SqlDatabaseTestActions testActions = this.hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData;
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

        //[TestMethod()]
        //public void hcw_usp_HcwGetDiseaseSymptomScoreTest()
        //{
        //    SqlDatabaseTestActions testActions = this.hcw_usp_HcwGetDiseaseSymptomScoreTestData;
        //    // Execute the pre-test script
        //    // 
        //    System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
        //    SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
        //    try
        //    {
        //        // Execute the test script
        //        // 
        //        System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
        //        SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
        //    }
        //    finally
        //    {
        //        // Execute the post-test script
        //        // 
        //        System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
        //        SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        //    }
        //}

        //[TestMethod()]
        public void hcw_usp_HcwGetDiseaseVaccineInfoTest()
        {
            SqlDatabaseTestActions testActions = this.hcw_usp_HcwGetDiseaseVaccineInfoTestData;
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
        private SqlDatabaseTestActions hcw_usp_HcwGetDiseaseByIncubationTestData;
        private SqlDatabaseTestActions hcw_usp_HcwGetDiseaseDetailInfoTestData;
        private SqlDatabaseTestActions hcw_usp_HcwGetDiseaseInfoByDiseaseIdTestData;
        //private SqlDatabaseTestActions hcw_usp_HcwGetDiseaseSymptomScoreTestData;
        private SqlDatabaseTestActions hcw_usp_HcwGetDiseaseVaccineInfoTestData;
    }
}
