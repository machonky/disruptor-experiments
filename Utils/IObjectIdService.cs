using System;

namespace HelloDisruptor
{
    public interface IObjectIdService
    {
        Guid NewGuid();
    }
}