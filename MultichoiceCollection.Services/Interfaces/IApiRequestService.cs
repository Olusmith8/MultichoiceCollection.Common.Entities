using System.Collections.Generic;
using System.Collections.Specialized;
using MultichoiceCollection.Services.Implementations;

namespace MultichoiceCollection.Services.Interfaces
{
    public interface IApiRequestService
    {
        TRetrunType MakeRequest<TRetrunType>(string endpoint, Dictionary<string, string> postData = null,
            HttpVerb method = HttpVerb.GET, NameValueCollection headers = null, bool isJsonBody = true);
    }
}