using ApiClients.Settings;
using System.Collections.Generic;
using System.Net.Http;

namespace ApiClients.LoginHelpers
{
    internal class LoginContentCreator
    {
        internal FormUrlEncodedContent GetLoginBodyAsContent(UserCredentials userCredentials)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", userCredentials.UserName));
            postData.Add(new KeyValuePair<string, string>("password", userCredentials.Password));
            return new FormUrlEncodedContent(postData);
        }
    }
}
