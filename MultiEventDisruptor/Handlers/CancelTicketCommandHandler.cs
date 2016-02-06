using Disruptor;

namespace MultiEventDisruptor
{
    public class CancelTicketCommandHandler : IEventHandler<ICancelTicketCommand>
    {
        public IConcertService ConcertService { get; set; }

        public CancelTicketCommandHandler(IConcertService concertService)
        {
            ConcertService = concertService;
        }

        public void OnNext(ICancelTicketCommand data, long sequence, bool endOfBatch)
        {
            ConcertService.CancelTicket();
        }
    }
}