using Disruptor;

namespace MultiEventDisruptor
{
    public class PurchaseTicketCommandHandler : IEventHandler<IPurchaseTicketCommand>
    {
        public IConcertService ConcertService { get; set; }

        public PurchaseTicketCommandHandler(IConcertService concertService)
        {
            ConcertService = concertService;
        }

        public void OnNext(IPurchaseTicketCommand data, long sequence, bool endOfBatch)
        {
            ConcertService.PurchaseTicket();
        }
    }
}