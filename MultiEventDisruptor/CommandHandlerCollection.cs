using System;
using System.Collections.Generic;
using Disruptor;

namespace MultiEventDisruptor
{
    public class CommandHandlerCollection
    {
        private class DefaultCommandHandler : IEventHandler<ICommand>
        {
            public void OnNext(ICommand data, long sequence, bool endOfBatch) {}
        }

        private static readonly DefaultCommandHandler defaultHandler = new DefaultCommandHandler();

        private readonly Dictionary<Type, object> handlers;

        public CommandHandlerCollection()
        {
            handlers = new Dictionary<Type, object>();
        }

        public void AddHandler(Type handlerType, object commandHandlerInstance)
        {
            handlers[handlerType] = commandHandlerInstance;
        }

        public object GetHandler(Type handlerType)
        {
            object handler;
            if (handlers.TryGetValue(handlerType, out handler))
            {
                return handler;
            }

            return defaultHandler;
        }
    }
}