using System;
using System.Collections.Specialized;
using System.Net;

namespace ApiClientInterfaces
{
    public interface IWebRequestCreator
    {
        WebRequest CreateWebRequest(Uri uri, NameValueCollection customHeaders);
    }
}
