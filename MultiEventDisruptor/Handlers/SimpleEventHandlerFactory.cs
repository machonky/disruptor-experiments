using System;

namespace MultiEventDisruptor
{
    public class SimpleEventHandlerFactory
    {
        public SimpleEventHandler<TConsumerServiceType, EventType> CreateEventHandler<TConsumerServiceType, EventType>(TConsumerServiceType consumerService, Action<TConsumerServiceType, EventType, long, bool> onNextImpl)
        {
            return new SimpleEventHandler<TConsumerServiceType, EventType>(consumerService, onNextImpl);
        }

        public SimpleEventHandler<EventType> CreateEventHandler<EventType>(Action<EventType, long, bool> onNextImpl)
        {
            return new SimpleEventHandler<EventType>(onNextImpl);
        }
    }
}