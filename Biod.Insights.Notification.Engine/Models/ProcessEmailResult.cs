namespace Biod.Insights.Notification.Engine.Models
{
    public class ProcessEmailResult
    {
        public bool RenderSuccess { get; set; }
        
        public bool DeliverySuccess { get; set; }
        
        public bool DatabaseSaveSuccess { get; private set; }

        private int? _savedEmailId;
        public int? SavedEmailId
        {
            get => _savedEmailId;
            set
            {
                _savedEmailId = value;
                DatabaseSaveSuccess = value != null;
            }
        }

        public bool IsSuccessful()
        {
            return RenderSuccess && DeliverySuccess && DatabaseSaveSuccess;
        }
    }
}