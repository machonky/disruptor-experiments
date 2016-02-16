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
            RegisterCommandHandlers(concertService, commandHandlers);

            inputDisruptor = new Disruptor<CommandMessage>(() => new CommandMessage(), claimStrategy, waitStrategy, TaskScheduler.Default);
            inputDisruptor.HandleEventsWith(new CommandMessageHandler(commandHandlers));

            // Publishers and translators to input buffer
            commandMessageTranslator = new CommandMessageTranslator(ticketCommandFactory, ApplicationServiceBus);
        }

        private static void RegisterCommandHandlers(IConcertService concertService, CommandHandlerCollection commandHandlers)
        {
            var factory = new SimpleEventHandlerFactory();

            commandHandlers.AddHandler(typeof (PurchaseTicketCommand), factory.CreateEventHandler(concertService,
                (IConcertService service, IPurchaseTicketCommand cmd, long seq, bool endOfBatch) =>
                {
                    concertService.PurchaseTicket(/*IPurchaseTicketCommand data*/);
                }));

            commandHandlers.AddHandler(typeof(CancelTicketCommand), factory.CreateEventHandler(concertService,
                (IConcertService service, ICancelTicketCommand cmd, long seq, bool endOfBatch) =>
                {
                    concertService.CancelTicket(/*ICancelTicketCommand data*/);
                }));

            commandHandlers.AddHandler(typeof(CreateConcertCommand), factory.CreateEventHandler(concertService,
                (IConcertService service, ICreateConcertCommand cmd, long seq, bool endOfBatch) =>
                {
                    concertService.CreateConcert(/*ICreateConcertCommand data*/);
                }));
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