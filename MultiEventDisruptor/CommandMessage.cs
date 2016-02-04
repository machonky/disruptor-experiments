using System;

namespace MultiEventDisruptor
{
    public class CommandMessage
    {
        public void Init(ICommand command, Guid objectGuid, DateTimeOffset timestamp)
        {
            Command = command;
            ObjectGuid = objectGuid;
            Timestamp = timestamp;
        }

        public Guid ObjectGuid { get; private set; }

        public DateTimeOffset Timestamp { get; private set; }

        public ICommand Command { get; private set; }
    }
}
