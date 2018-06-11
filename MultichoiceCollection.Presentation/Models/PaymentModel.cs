using System.ComponentModel.DataAnnotations;

namespace MultichoiceCollection.Presentation.Models
{
    public class PaymentModel
    {
        [Required]
        public string SmartCardNumber { get; set; }
        [Required]
        public string SubscriptionType { get; set; }
        [Required]
        public string Duration { get; set; }
    }



    

}