namespace Biod.Insights.Data.CustomModels
{
    public class usp_ZebraDataRenderSetImportationRiskByGeonameId_Result
    {
        public int Result { get; set; }
        
        public enum StoredProcedureReturnCode
        {
            Failure = -1,
            NoOperation = 0,
            OperationCompleted = 1
        }
    }
}