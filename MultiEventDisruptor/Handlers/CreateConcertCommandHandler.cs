using Disruptor;

namespace MultiEventDisruptor
{
    public class CreateConcertCommandHandler : IEventHandler<ICreateConcertCommand>
    {
        public IConcertService ConcertService { get; set; }

        public CreateConcertCommandHandler(IConcertService concertService)
        {
            ConcertService = concertService;
        }

        public void OnNext(ICreateConcertCommand data, long sequence, bool endOfBatch)
        {
            ConcertService.CreateConcert();
        }
    }
}