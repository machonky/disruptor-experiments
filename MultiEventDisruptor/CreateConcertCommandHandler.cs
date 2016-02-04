using Disruptor;

namespace MultiEventDisruptor
{
    public class CreateConcertCommandHandler : IEventHandler<ICreateConcertCommand>
    {
        public void OnNext(ICreateConcertCommand data, long sequence, bool endOfBatch)
        {
            
        }
    }
}