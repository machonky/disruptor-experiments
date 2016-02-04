using Disruptor;

namespace MultiEventDisruptor
{
    public class PurchaseTicketCommandHandler : IEventHandler<IPurchaseTicketCommand>
    {
        public void OnNext(IPurchaseTicketCommand data, long sequence, bool endOfBatch)
        {
            
        }
    }
}