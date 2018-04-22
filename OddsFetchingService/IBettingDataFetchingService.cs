namespace OddsFetchingService
{
    public interface IBettingDataFetchingService
    {
        void FetchAndStoreUntrackedEntities();
        void FetchAndStoreOdds();
        void UpdateStatusOfRecentlyFinishedEntities();
        void Login();
        void KeepAlive();
    }
}
