using HelloDisruptor;
using Microsoft.Practices.Unity;

namespace MultiEventDisruptor
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterInstance<IObjectIdService>(new ObjectIdService());
            container.RegisterInstance<IUtcClockService>(new UtcClockService());
            container.RegisterInstance<ITicketCommandFactory>(new TicketCommandFactory());

            var theApp = container.Resolve<TicketingApplication>();
            theApp.Run();
        }
    }
}
