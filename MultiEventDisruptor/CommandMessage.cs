using System;

namespace MultiEventDisruptor
{
    public class CommandMessage
    {
        public void Init(ICommand command, DateTimeOffset timestamp)
        {
            Command = command;
            Timestamp = timestamp;
        }

        public DateTimeOffset Timestamp { get; private set; }

        public ICommand Command { get; private set; }
    }
}
