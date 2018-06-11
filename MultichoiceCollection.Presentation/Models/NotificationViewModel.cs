using MultichoiceCollection.Common.Entities.Enum;

namespace MultichoiceCollection.Presentation.Models
{
    public class NotificationViewModel
        {
            public NotificationViewModel(string message, AlertType alertType = AlertType.Success)
            {
                Message = message;
                AlertType = alertType;
            }
            public string Message { get; set; }
            public AlertType AlertType { get; set; }
        public string NotificationId { get; set; }
        public bool CanDismiss { get; set; }
    }

  
}