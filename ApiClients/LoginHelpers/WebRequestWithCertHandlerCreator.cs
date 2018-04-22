using ApiClientInterfaces;
using ApiClients.Settings;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace ApiClients.LoginHelpers
{
    public class WebRequestWithCertHandlerCreator: IWebRequestHandlerCreator
    {
        private CertificateSettings _certSettings;

        public WebRequestWithCertHandlerCreator()
        {
            _certSettings = new CertificateSettings();
        }

        public WebRequestHandler CreateWebRequestHandler()
        {
            var serviceLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string path = baseDirectory + _certSettings.FilePath + _certSettings.FileName;
            var cert = new X509Certificate2(path, "");
            var clientHandler = new WebRequestHandler();
            clientHandler.ClientCertificates.Add(cert);
            return clientHandler;
        }
    }
}
