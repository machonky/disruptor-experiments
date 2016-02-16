using System;
using Disruptor;

namespace MultiEventDisruptor
{
    public class SimpleEventHandler<TEventType> : IEventHandler<TEventType>
    {
        private Action<TEventType, long, bool> OnNextImpl { get; set; }

        public SimpleEventHandler(Action<TEventType, long, bool> onNextImpl)
        {
            OnNextImpl = onNextImpl;
        }

        public void OnNext(TEventType data, long sequence, bool endOfBatch)
        {
            OnNextImpl(data, sequence, endOfBatch);
        }
    }

    public class SimpleEventHandler<TConsumerServiceType, EventType> : IEventHandler<EventType>
    {
        public TConsumerServiceType BusinessLogic { get; private set; }
        private Action<TConsumerServiceType, EventType, long, bool> OnNextImpl { get; set; }

        public SimpleEventHandler(TConsumerServiceType businessLogic, Action<TConsumerServiceType, EventType, long, bool> onNextImpl)
        {
            BusinessLogic = businessLogic;
            OnNextImpl = onNextImpl;
        }

        public void OnNext(EventType data, long sequence, bool endOfBatch)
        {
            OnNextImpl(BusinessLogic, data, sequence, endOfBatch);
        }
    }
}