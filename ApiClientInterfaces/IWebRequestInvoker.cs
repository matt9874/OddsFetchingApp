using System.Collections.Generic;
using System.Net;

namespace ApiClientInterfaces
{
    public interface IWebRequestInvoker
    {
        T Invoke<T>(WebRequest request, string method, IDictionary<string, object> args);
    }
}
