using System;

namespace MultichoiceCollection.Services.Implementations
{
    public class AuthModel
    {
        public string accessToken { get; set; }
        public DateTime expirationdate { get; set; }
        public string clientAppName { get; set; }
    }
}