using System;

namespace HelloDisruptor
{
    public interface IUtcClockService
    {
        DateTimeOffset UtcNow { get; }
    }
}