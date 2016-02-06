using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;

namespace MultiEventDisruptor
{
    public class TicketingApplication
    {
        public IApplicationServiceBus ApplicationServiceBus { get; set; }
        private const int Million = 1000000;
        private const int Iterations = 10*Million;

        private readonly Disruptor<CommandMessage> inputDisruptor;
        private readonly CommandMessageTranslator commandMessageTranslator;

        public TicketingApplication(ITicketCommandFactory ticketCommandFactory, IConcertService concertService, IApplicationServiceBus applicationServiceBus)
        {
            ApplicationServiceBus = applicationServiceBus;

            var inputBufferSize = 2 << 10;

            var claimStrategy = new SingleThreadedClaimStrategy(inputBufferSize);
            var waitStrategy = new SleepingWaitStrategy();
            var commandHandlers = new CommandHandlerCollection();

            // Register command handlers...
            commandHandlers.AddHandler(typeof(PurchaseTicketCommand), new PurchaseTicketCommandHandler(concertService));
            commandHandlers.AddHandler(typeof(CancelTicketCommand), new CancelTicketCommandHandler(concertService));
            commandHandlers.AddHandler(typeof(CreateConcertCommand), new CreateConcertCommandHandler(concertService));

            inputDisruptor = new Disruptor<CommandMessage>(() => new CommandMessage(), claimStrategy, waitStrategy, TaskScheduler.Default);
            inputDisruptor.HandleEventsWith(new CommandMessageHandler(commandHandlers));

            // Publishers and translators to input buffer
            commandMessageTranslator = new CommandMessageTranslator(ticketCommandFactory, ApplicationServiceBus);
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