using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;
using HelloDisruptor;

namespace MultiEventDisruptor
{
    public class TicketingApplication
    {
        public IObjectIdService ObjectIdService { get; private set; }
        public IUtcClockService UtcClock { get; private set; }

        public TicketingApplication(IObjectIdService objectIdService, IUtcClockService utcClock)
        {
            ObjectIdService = objectIdService;
            UtcClock = utcClock;

            var inputBufferSize = 2 << 10;

            var claimStrategy = new SingleThreadedClaimStrategy(inputBufferSize);
            var waitStrategy = new SleepingWaitStrategy();
            var commandHandlers = new CommandHandlerCollection();
            // Register command handlers...

            var inputDisruptor = new Disruptor<CommandMessage>(() => new CommandMessage(), claimStrategy, waitStrategy, TaskScheduler.Default);
            inputDisruptor.HandleEventsWith(new CommandMessageHandler(commandHandlers));

            // Publishers and translators to input buffer
        }

        public void Run()
        {
            
        }
    }
}