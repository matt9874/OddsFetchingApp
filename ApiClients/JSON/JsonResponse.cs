using Newtonsoft.Json;

namespace ApiClients.JSON
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonResponse<T>
    {
        [JsonProperty(PropertyName = "jsonrpc", NullValueHandling = NullValueHandling.Ignore)]
        public string JsonRpc { get; set; }

        [JsonProperty(PropertyName = "result", NullValueHandling = NullValueHandling.Ignore)]
        public T Result { get; set; }

        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public ApiClients.Exception Error { get; set; }

        [JsonProperty(PropertyName = "id")]
        public object IdStr { get; set; }

        [JsonIgnore]
        public bool HasError
        {
            get { return Error != null; }
        }
    }
}
