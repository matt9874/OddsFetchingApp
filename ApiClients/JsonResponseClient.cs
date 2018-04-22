using OddsFetchingEntities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using DataInterfaces;
using ApiClients.Settings;
using ApiClients.JsonClientHelpers;
using System.Diagnostics;
using ApiClientInterfaces;

namespace ApiClients
{
    public class JsonResponseClient<T>: IReader<T>
        where T : Entity
    {
        private AppCredentials _appCredentials;

        private NameValueCollection _customHeaders { get; set; }
        private const string _sessionTokenHeader = "X-Authentication";
        private const string _appKeyHeader = "X-Application";

        private ArgumentsAdder<T> _argumentsAdder;
        private UrlSuffixGetter<T> _urlSuffixGetter;

        private IWebRequestCreator _webRequestCreator;
        private const string _endPoint = "https://api.betfair.com/exchange/betting/json-rpc/v1";
        private IWebRequestInvoker _webRequestInvoker;
        
        public JsonResponseClient(IWebRequestCreator webRequestCreator, IWebRequestInvoker webRequestInvoker)
        {
            _appCredentials = new AppCredentials();

            _customHeaders = new NameValueCollection();
            if (_appCredentials.ApplicationKey != null)
            {
                SetAppKeyHeader(_appCredentials.ApplicationKey);
            }

            _argumentsAdder = new ArgumentsAdder<T>();
            _urlSuffixGetter = new UrlSuffixGetter<T>();
            _webRequestCreator = webRequestCreator;
            _webRequestInvoker = webRequestInvoker;
        }

        private void SetAppKeyHeader(string appKey)
        {
            _customHeaders[_appKeyHeader] = appKey;
        }
        private void SetSessionTokenHeader(string sessionToken)
        {
            
        }

        public IEnumerable<T> Read(MarketFilter filter)
        {
            _customHeaders[_sessionTokenHeader] = SessionToken.Instance;

            var args = new Dictionary<string, object>();
            args=_argumentsAdder.AddAdditionalArguments(args, filter);

            string urlSuffix = _urlSuffixGetter.GetUrlSuffix();

            var request = _webRequestCreator.CreateWebRequest(new Uri(_endPoint), _customHeaders);

            return _webRequestInvoker.Invoke<List<T>>(request, urlSuffix, args);
        }
    }
}
