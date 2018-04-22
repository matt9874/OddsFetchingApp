namespace WorkerInterfaces
{
    public interface IWorker
    {
        void DoWork();
        string Name { get; }
    }
}
