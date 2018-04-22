using ApiClients;
using ApiClients.LoginHelpers;
using ApiClients.JsonClientHelpers;
using DataInterfaces;
using OddsFetchingBL;
using OddsFetchingBLInterfaces;
using OddsFetchingData;
using OddsFetchingEntities;
using OddsFetchingEntities.Mappers;
using OddsFetchingService;
using System.Linq.Expressions;
using StructureMap;
using StructureMap.AutoFactory;
using System;
using Workers;
using ApiClientInterfaces;
using WorkerInterfaces;

namespace OddsFetchingTriggerer
{
    public static class StructureMapConfig
    {
        public static void RegisterComponents(Container container)
        {
            //Program.cs
            container.Configure(x => x.For<IWorkerFactory>().CreateFactory());

            container.Configure(x => x.For<IWorker>().Use<LoginWorker>().Named("loginWorker"));
            container.Configure(x => x.For<IWorker>().Use<TimedWorker>().Ctor<int>("timeInterval").Is(4000000).Ctor<IWorker>().Is<KeepAliveWorker>().Named("keepAlive"));
            container.Configure(x => x.For<IWorker>().Use<TimedWorker>().Ctor<int>("timeInterval").Is(3600000).Ctor<IWorker>().Is<DailyWorker>().Named("daily"));
            container.Configure(x => x.For<IWorker>().Use<TimedWorker>().Ctor<int>("timeInterval").Is(600000).Ctor<IWorker>().Is<FrequentWorker>().Named("frequent"));

            container.Configure(x => x.For<IBettingDataFetchingService>().Use<BetfairFetchingService>());

            //BetfairFetchingService.cs
            container.Configure(x => x.For<IAuthClient>().Use<AuthClient>());
            container.Configure(x => x.For<IKeepAliveClient>().Use<KeepAliveClient>());
            container.Configure(x => x.For<ICompetitionsService>().Use<CompetitionsService>());
            container.Configure(x => x.For<IEventsService>().Use<EventsService>());
            container.Configure(x => x.For<IMarketsService>().Use<MarketsService>());
            container.Configure(x => x.For<IMarketBooksService>().Use<MarketBooksService>());

            //CompetitionsService.cs
            container.Configure(x => x.For<IIdsReader<Competition>>().Use<MongoCompetitionsReader>());

            //EventsService.cs   
            container.Configure(x => x.For<IReader<EventResult>>().Use<JsonResponseClient<EventResult>>());
            container.Configure(x => x.For<IIdsReader<Event>>().Use<MongoEventsReader>());
            container.Configure(x => x.For<ISaver<Event>>().Use<MongoEventsSaver>());

            //MarketsService.cs
            container.Configure(x => x.For<IReader<MarketCatalogue>>().Use<JsonResponseClient<MarketCatalogue>>());
            container.Configure(x => x.For<IIdsReader<MarketCatalogue>>().Use<MongoMarketCataloguesReader>());
            container.Configure(x => x.For<ISaver<MarketCatalogue>>().Use<MongoMarketCataloguesSaver>());
            container.Configure(x => x.For<IUpdater<MarketCatalogue>>().Use<MongoMarketCataloguesUpdater>());

            //MarketBooksService.cs
            container.Configure(x => x.For<IReader<MarketBook>>().Use<JsonResponseClient<MarketBook>>());
            container.Configure(x => x.For<ISaver<MarketBook>>().Use<MongoMarketBooksSaver>());

            //MongoMarketBooksSaver.cs
            container.Configure(x => x.For<ISaver<Runner>>().Use<MongoRunnersSaver>());

            //MongoRunnersSaver.cs
            container.Configure(x => x.For<ISaver<OddsOffer>>().Use<MongoOddsOffersSaver>());

            //JsonResponseClient.cs
            container.Configure(x => x.For<IWebRequestCreator>().Use<JsonPostWebRequestCreator>());
            container.Configure(x => x.For<IWebRequestInvoker>().Use<JsonRequestInvoker>());

            //AuthClient.cs
            container.Configure(x => x.For<IWebRequestHandlerCreator>().Use<WebRequestWithCertHandlerCreator>());

            //MongoMarketCataloguesReader.cs
            container.Configure(x => x.For<IMapper<MarketFilter, Expression<Func<MarketCatalogue, bool>>>>().Use<IsFinishedFilterMapper>());
        }
    }
}
