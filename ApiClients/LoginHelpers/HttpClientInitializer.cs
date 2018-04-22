using ApiClients.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApiClients.LoginHelpers
{
    internal class HttpClientInitializer
    {
        private const string _baseUrl = "https://identitysso.betfair.com";
        private const string _appKeyHeader = "X-Application";
        private AppCredentials _appCredentials;
        private const string _mediaType = "application/json";

        public HttpClientInitializer()
        {
            _appCredentials = new AppCredentials();
        }

        internal HttpClient InitHttpClientInstance(WebRequestHandler clientHandler)
        {
            var client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add(_appKeyHeader, _appCredentials.ApplicationKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
            return client;
        }
    }
}
