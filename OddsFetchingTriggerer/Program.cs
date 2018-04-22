using StructureMap;
using System.ServiceProcess;
using WorkerInterfaces;

namespace OddsFetchingTriggerer
{
    static class Program
    {
        static void Main()
        {
            var container = new Container();
            StructureMapConfig.RegisterComponents(container);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new OddsFetchingTriggerer(container.GetInstance<IWorkerFactory>())
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
