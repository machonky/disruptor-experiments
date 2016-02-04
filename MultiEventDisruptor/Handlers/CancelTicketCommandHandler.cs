using Disruptor;

namespace MultiEventDisruptor
{
    public class CancelTicketCommandHandler : IEventHandler<ICancelTicketCommand>
    {
        public void OnNext(ICancelTicketCommand data, long sequence, bool endOfBatch)
        {
            
        }
    }
}