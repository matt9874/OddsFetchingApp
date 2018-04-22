using ApiClientInterfaces;
using ApiClients.LoginHelpers;
using OddsFetchingEntities;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace ApiClients
{
    public class KeepAliveClient : IKeepAliveClient
    {
        private const string _urlExt = "/api/keepAlive";

        private HttpClientInitializer _httpClientInitializer;

        public KeepAliveClient()
        {
            _httpClientInitializer = new HttpClientInitializer();
        }

        public void KeepAlive()
        {
            WebRequestHandler handler = new WebRequestHandler();
            HttpClient client = _httpClientInitializer.InitHttpClientInstance(handler);
            SetHeaders(client);

            HttpResponseMessage result = client.PostAsync(_urlExt, null).Result;
            result.EnsureSuccessStatusCode();
            var jsonSerialiser = new DataContractJsonSerializer(typeof(KeepAliveResponse));
            var stream = new MemoryStream(result.Content.ReadAsByteArrayAsync().Result);
            var response = (KeepAliveResponse)jsonSerialiser.ReadObject(stream);
            EventLog.WriteEntry("OddsFetchingTriggerer", string.Format("Staying alive? {0}",response.Status));
        }

        private static void SetHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-Authentication", SessionToken.Instance);
        }
    }
}
