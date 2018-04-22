using System.Runtime.Serialization;

namespace OddsFetchingEntities
{
    [DataContract]
    public class KeepAliveResponse
    {
        [DataMember(Name = "token")]
        public string SessionToken { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "product")]
        public string Product { get; set; }
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}
