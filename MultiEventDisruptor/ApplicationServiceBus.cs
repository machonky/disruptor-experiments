using HelloDisruptor;

namespace MultiEventDisruptor
{
    public class ApplicationServiceBus : IApplicationServiceBus
    {
        public IObjectIdService ObjectIdService { get; private set; }
        public IUtcClockService UtcClockService { get; private set; }

        public ApplicationServiceBus(IObjectIdService objectIdService, IUtcClockService utcClockService)
        {
            ObjectIdService = objectIdService;
            UtcClockService = utcClockService;
        }
    }
}