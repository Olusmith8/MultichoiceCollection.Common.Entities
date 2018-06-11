using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using MultichoiceCollection.Services.Interfaces;
using Newtonsoft.Json;

namespace MultichoiceCollection.Services.Implementations
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public class ApiRequestService : IApiRequestService
    {
        public TRetrunType MakeRequest<TRetrunType>(string endpoint, Dictionary<string, string> postData = null,
            HttpVerb method = HttpVerb.GET, NameValueCollection headers = null, bool isJsonBody = true)
        {
            try
            {
               
                var request = (HttpWebRequest) WebRequest.Create(endpoint);
                request.Method = method.ToString();
                request.ContentLength = 0;
                request.ContentType = "application/json";
               

                //if we are going to auth, then use auth to get token
                if (endpoint != ConfigurationManager.AppSettings["apiBaseUrl"] + "auth")
                {
                    var authModel = TokenProvider.GetAuthModel();
                    if (authModel != null)
                    {
                        if(headers == null) headers = new FormCollection();
                        headers["Authorization"] = $"Bearer {authModel.accessToken}";
                    }
                }

                if (headers != null)
                {
                    request.Headers.Add(headers);
                }

                if (method == HttpVerb.POST)
                {
                    if (!isJsonBody)
                    {
                        var data = this.preparePostBody(postData);
                        var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                        request.ContentLength = bytes.Length;

                        using (var writeStream = request.GetRequestStream())
                        {
                            writeStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    else
                    {
                        var data = JsonConvert.SerializeObject(postData);
                        var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                        request.ContentLength = bytes.Length;
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(data);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }

                }

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = string.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                    var passedModel = JsonConvert.DeserializeObject<TRetrunType>(responseValue);

                    return passedModel;
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("404"))
                {
                    throw new Exception("The resource you are trying to access was not found.");
                }
                throw new Exception(exception.Message);

            }
        }

        private string preparePostBody(Dictionary<string, string> postData)
        {
            var data = "";
            var index = 0;
            if (postData == null)
            {
                return data;
            }
            foreach (var item in postData)
            {
                if (index == 0)
                {
                    data += item.Key + "=" + item.Value;
                    index = 1;
                }
                else
                {
                    data += "&" + item.Key + "=" + item.Value;
                }
            }
            return data;
        }

        
        

    }

}