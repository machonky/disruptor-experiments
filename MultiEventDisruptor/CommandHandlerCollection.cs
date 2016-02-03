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

        private readonly Dictionary<Type, IEventHandler<ICommand>> handlers;

        public CommandHandlerCollection()
        {
            handlers = new Dictionary<Type, IEventHandler<ICommand>>();
        }

        public void AddHandler(Type handlerType, IEventHandler<ICommand> commandHandlerInstance)
        {
            handlers[handlerType] = commandHandlerInstance;
        }

        public IEventHandler<ICommand> GetHandler(Type handlerType)
        {
            IEventHandler<ICommand> handler;
            if (handlers.TryGetValue(handlerType, out handler))
            {
                return handler;
            }

            return defaultHandler;
        }
    }
}