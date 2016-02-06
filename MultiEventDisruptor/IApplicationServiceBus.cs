using HelloDisruptor;

namespace MultiEventDisruptor
{
    public interface IApplicationServiceBus
    {
        IObjectIdService ObjectIdService { get; }
        IUtcClockService UtcClockService { get; }
    }
}