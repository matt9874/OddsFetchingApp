namespace WorkerInterfaces
{
    public interface ITimedWorker : IWorker
    {
        void StopTimer();
    }
}
