using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultichoiceCollection.Services.Implementations
{
    public class TokenProvider
    {
        private static AuthModel _authToken;

        public static AuthModel GetAuthModel()
        {
            try
            {
                if (_authToken != null && _authToken.expirationdate < DateTime.Now) return _authToken;
                var api = new ApiRequestService();
                var body = new Dictionary<string, string>
                {
                    ["clientId"] = ConfigurationManager.AppSettings["clientId"],
                    ["password"] = ConfigurationManager.AppSettings["password"]
                };
                var endPoint = ConfigurationManager.AppSettings["apiBaseUrl"] + "auth";
                _authToken = api.MakeRequest<AuthModel>(endPoint, body, HttpVerb.POST);
                return _authToken;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
