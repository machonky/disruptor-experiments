using System;

namespace HelloDisruptor
{
    public class ObjectIdService : IObjectIdService
    {
        public Guid NewGuid()
        {
            return Guid.NewGuid();
        }
    }
}