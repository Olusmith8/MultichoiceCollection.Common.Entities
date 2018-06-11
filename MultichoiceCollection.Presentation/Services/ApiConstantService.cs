using System.Configuration;
using System.Web;

namespace MultichoiceCollection.Presentation.Services
{
    public class ApiConstantService
    {
        /*// "http://10.0.33.3/mcNServices/swagger/"*/
        //public const string BASE_URL = "https://demo3118927.mockable.io/api/";
        public static readonly string BASE_URL = ConfigurationManager.AppSettings["apiBaseUrl"];
        public static readonly string LOGIN = BASE_URL + "auth";
        public static readonly string RESET = BASE_URL + "auth/reset";
        public static readonly string TYPES = BASE_URL + "types";
        public static readonly string TRANSACTION_INQUIRIES = BASE_URL + "transactions/";
        public static readonly string TRANSACTION_CONFIRMATION = BASE_URL + "confirmation/";
    }
}