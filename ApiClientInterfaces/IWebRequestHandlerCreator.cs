using System.Net.Http;

namespace ApiClientInterfaces
{
    public interface IWebRequestHandlerCreator
    {
        WebRequestHandler CreateWebRequestHandler();
    }
}
