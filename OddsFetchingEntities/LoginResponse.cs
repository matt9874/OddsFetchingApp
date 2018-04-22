using System.Runtime.Serialization;

namespace OddsFetchingEntities
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember(Name = "sessionToken")]
        public string SessionToken { get; set; }
        [DataMember(Name = "loginStatus")]
        public string LoginStatus { get; set; }
    }
}
