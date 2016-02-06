using System;
using System.Collections.Generic;

namespace MultiEventDisruptor
{
    public class CommandHandlerCollection
    {
        private static readonly DefaultCommandHandler DefaultHandler = new DefaultCommandHandler();

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

            return DefaultHandler;
        }
    }
}