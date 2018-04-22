using ApiClientInterfaces;
using OddsFetchingBLInterfaces;
using OddsFetchingEntities;
using OddsFetchingEntities.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

namespace OddsFetchingService
{
    public class BetfairFetchingService : IBettingDataFetchingService
    {
        private ICompetitionsService _compsService;
        private IEventsService _eventsService;
        private IMarketsService _marketsService;
        private IMarketBooksService _marketBooksService;
        private IAuthClient _authClient;
        private IKeepAliveClient _keepAliveClient;

        public BetfairFetchingService(ICompetitionsService compsService, IEventsService eventsService, IMarketsService marketsService, IMarketBooksService marketBooksService, IAuthClient authClient, IKeepAliveClient keepAliveClient)
        {
            _compsService = compsService;
            _eventsService = eventsService;
            _marketsService = marketsService;
            _marketBooksService = marketBooksService;
            _authClient = authClient;
            _keepAliveClient = keepAliveClient;
        }
        
        public void Login()
        {
            try
            {
                LoginResponse resp = _authClient.DoLogin();
                if (!(resp.LoginStatus == "SUCCESS"))
                {
                    EventLog.WriteEntry("OddsFetchingTriggerer", string.Format("Failed login attempt. Status: {0}. ", resp.LoginStatus));
                }
            }
            catch (CryptographicException e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", string.Format("Could not load the certificate: {0}. ", e.Message));
            }
            catch (HttpRequestException e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", string.Format("The Betfair Login endpoint returned an HTTP Error: {0}. ", e.Message));
            }
            catch (WebException e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", string.Format("An error occurred whilst attempting to make the request: {0}. ", e.Message));
            }
            catch (System.Exception e)
            {
                EventLog.WriteEntry("OddsFetchingTriggerer", string.Format("An error occurred: {0}. ", e.Message));
            }
        }

    public void FetchAndStoreUntrackedEntities()
        {
            ISet<string> compIds = _compsService.FetchCompetitionIds(null);
            MarketFilter compsFilter = new MarketFilter() { CompetitionIds = compIds };
            ISet<string> trackedEventIds = _eventsService.FetchTrackedEventIds(compsFilter);
            MarketFilter trackedEventsFilter = new MarketFilter() { EventIds = trackedEventIds };
            ISet<string> trackedMarketIds = _marketsService.FetchTrackedMarketIds(trackedEventsFilter);

            if (compIds.IsNullOrEmpty())
            {
                return;
            }

            foreach (string id in compIds)
            {
                MarketFilter eventsFilter = new MarketFilter() { CompetitionIds = new HashSet<string>() { id } };
                ISet<string> eventsIds = _eventsService.FetchAndStoreEvents(eventsFilter, trackedEventIds);
                if (eventsIds != null && eventsIds.Count > 0)
                {
                    foreach (var evId in eventsIds)
                    {
                        MarketFilter marketsFilter = new MarketFilter() { CompetitionIds = new HashSet<string>() { id }, EventIds = new HashSet<string>() { evId } };
                        _marketsService.FetchAndStoreMarkets(marketsFilter, trackedMarketIds);
                    }
                }
            }
        }

        public void FetchAndStoreOdds()
        {
            ISet<string> marketIds = _marketsService.FetchTrackedMarketIds(new MarketFilter() { IsFinished = false });

            if (marketIds.IsNullOrEmpty())
            {
                return;
            }
            _marketBooksService.FetchAndStoreOdds(marketIds);
        }

        public void UpdateStatusOfRecentlyFinishedEntities()
        {
            ISet<string> trackedOpenMarketIds = _marketsService.FetchTrackedMarketIds(new MarketFilter() { IsFinished = false });
            ISet<string> closedMarketIds = _marketBooksService.FetchClosedMarketIds(trackedOpenMarketIds);

            _marketsService.CloseFinishedMarkets(closedMarketIds);
        }

        public void KeepAlive()
        {
            _keepAliveClient.KeepAlive();
        }
    }
}
