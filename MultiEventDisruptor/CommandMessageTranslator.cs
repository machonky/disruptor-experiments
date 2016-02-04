using Disruptor;
using HelloDisruptor;

namespace MultiEventDisruptor
{
    public class CommandMessageTranslator : IEventTranslator<CommandMessage>
    {
        public ITicketCommandFactory TicketCommandFactory { get; private set; }
        public IObjectIdService ObjectIdService { get; private set; }
        public IUtcClockService UtcClock { get; private set; }

        public CommandMessageTranslator(ITicketCommandFactory ticketCommandFactory, IObjectIdService objectIdService, IUtcClockService utcClock)
        {
            TicketCommandFactory = ticketCommandFactory;
            ObjectIdService = objectIdService;
            UtcClock = utcClock;
        }

        public CommandMessage TranslateTo(CommandMessage eventData, long sequence)
        {
            // Just some way of creating various ticket types. These would ordinarily be user generated and converted from the wire
            ICommand command = sequence%3 == 0
                ? TicketCommandFactory.PurchaseTicket()
                : (sequence%2 == 0 ? TicketCommandFactory.CancelTicket() 
                : (ICommand)TicketCommandFactory.CreateConcert());

            eventData.Init(command, ObjectIdService.NewGuid(), UtcClock.UtcNow);
            return eventData;
        }
    }
}