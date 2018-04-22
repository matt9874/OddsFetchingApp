using ApiClientInterfaces;
using System;
using System.Collections.Specialized;
using System.Net;

namespace ApiClients.JsonClientHelpers
{
    public class JsonPostWebRequestCreator : IWebRequestCreator
    {
        private const string _methodType = "POST";
        private const string _contentType = "application/json-rpc";
        private const string _charSet = "ISO-8859-1,utf-8";

        public WebRequest CreateWebRequest(Uri uri, NameValueCollection customHeaders)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = _methodType;
            request.ContentType = _contentType;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, _charSet);
            request.Headers.Add(customHeaders);
            ServicePointManager.Expect100Continue = false;
            return request;
        }
    }
}
