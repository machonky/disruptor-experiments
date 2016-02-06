using Disruptor;

namespace MultiEventDisruptor
{
    public class DefaultCommandHandler : IEventHandler<ICommand>
    {
        public void OnNext(ICommand data, long sequence, bool endOfBatch) { }
    }
}