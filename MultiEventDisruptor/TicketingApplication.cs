using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;
using HelloDisruptor;

namespace MultiEventDisruptor
{
    public class TicketingApplication
    {
        private const int Million = 1000000;
        private const int Iterations = 10*Million;

        private readonly Disruptor<CommandMessage> inputDisruptor;
        private readonly CommandMessageTranslator commandMessageTranslator;
        public IObjectIdService ObjectIdService { get; private set; }
        public IUtcClockService UtcClock { get; private set; }

        public TicketingApplication(ITicketCommandFactory ticketCommandFactory, IObjectIdService objectIdService, IUtcClockService utcClock)
        {
            ObjectIdService = objectIdService;
            UtcClock = utcClock;

            var inputBufferSize = 2 << 10;

            var claimStrategy = new SingleThreadedClaimStrategy(inputBufferSize);
            var waitStrategy = new SleepingWaitStrategy();
            var commandHandlers = new CommandHandlerCollection();

            // Register command handlers...
            commandHandlers.AddHandler(typeof(PurchaseTicketCommand), new PurchaseTicketCommandHandler());
            commandHandlers.AddHandler(typeof(CancelTicketCommand), new CancelTicketCommandHandler());
            commandHandlers.AddHandler(typeof(CreateConcertCommand), new CreateConcertCommandHandler());

            inputDisruptor = new Disruptor<CommandMessage>(() => new CommandMessage(), claimStrategy, waitStrategy, TaskScheduler.Default);
            inputDisruptor.HandleEventsWith(new CommandMessageHandler(commandHandlers));

            // Publishers and translators to input buffer
            commandMessageTranslator = new CommandMessageTranslator(ticketCommandFactory, ObjectIdService, UtcClock);
        }

        public void Run()
        {
            inputDisruptor.Start();
            for (int i = 0; i < Iterations; i++)
            {
                inputDisruptor.PublishEvent(commandMessageTranslator.TranslateTo);
            }
            inputDisruptor.Shutdown();
        }
    }
}