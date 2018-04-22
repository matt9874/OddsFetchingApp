using ApiClientInterfaces;
using ApiClients.JSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;

namespace ApiClients.JsonClientHelpers
{
    public class JsonRequestInvoker : HttpWebClientProtocol, IWebRequestInvoker
    {
        public T Invoke<T>(WebRequest request, string method, IDictionary<string, object> args)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (method.Length == 0)
                throw new ArgumentException(null, "method");


            using (Stream stream = request.GetRequestStream())
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                var call = new JsonRequest { Method = method, IdStr = 1, Params = args };
                JsonConvert.Export(call, writer);
            }

            using (WebResponse response = GetWebResponse(request))
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                var jsonResponse = JsonConvert.Import<T>(reader);
                if (jsonResponse.HasError)
                {
                    EventLog.WriteEntry("OddsFetchingTriggerer", "Json response has error. Data: " + jsonResponse.Error.Data.ToString());
                    EventLog.WriteEntry("OddsFetchingTriggerer", "Detail: " + jsonResponse.Error.Detail.ToString());
                    throw ExceptionReconstituter.ReconstituteException(jsonResponse.Error);
                }
                else
                {
                    return jsonResponse.Result;
                }
            }
        }
    }
}
