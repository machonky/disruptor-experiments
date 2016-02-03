using System;

namespace HelloDisruptor
{
    public class IncrementCounterCommand
    {
        public Guid ObjectId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public IncrementCounterCommand(Guid objectId, DateTimeOffset timestamp)
        {
            ObjectId = objectId;
            Timestamp = timestamp;
        }
    }
}