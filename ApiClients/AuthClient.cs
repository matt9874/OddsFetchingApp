using ApiClientInterfaces;
using ApiClients.LoginHelpers;
using ApiClients.Settings;
using OddsFetchingEntities;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace ApiClients
{
    public class AuthClient: IAuthClient
    {
        private const string _urlExt = "/api/certlogin";

        private IWebRequestHandlerCreator _webRequestHandlerCreator;
        private UserCredentials _userCredentials;
        private HttpClientInitializer _httpClientInitializer;
        private LoginContentCreator _loginContentCreator;

        public AuthClient(IWebRequestHandlerCreator webRequestHandlerCreator)
        {
            _webRequestHandlerCreator = webRequestHandlerCreator;
            _userCredentials = new UserCredentials();
            _httpClientInitializer = new HttpClientInitializer();
            _loginContentCreator = new LoginContentCreator();
        }

        public LoginResponse DoLogin()
        {
            WebRequestHandler handler = _webRequestHandlerCreator.CreateWebRequestHandler();
            HttpClient client = _httpClientInitializer.InitHttpClientInstance(handler);
            FormUrlEncodedContent content = _loginContentCreator.GetLoginBodyAsContent(_userCredentials);
            HttpResponseMessage result = client.PostAsync(_urlExt, content).Result;
            result.EnsureSuccessStatusCode();
            var jsonSerialiser = new DataContractJsonSerializer(typeof(LoginResponse));
            var stream = new MemoryStream(result.Content.ReadAsByteArrayAsync().Result);
            var response = (LoginResponse)jsonSerialiser.ReadObject(stream);
            SessionToken.Instance=response.SessionToken;
            return response;
        }
    }
}