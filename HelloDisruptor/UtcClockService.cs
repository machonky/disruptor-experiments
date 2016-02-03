using System;

namespace HelloDisruptor
{
    public class UtcClockService : IUtcClockService
    {
        public DateTimeOffset UtcNow { get { return DateTimeOffset.Now; } }
    }
}