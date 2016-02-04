namespace MultiEventDisruptor
{
    public class TicketCommandFactory : ITicketCommandFactory
    {
        public ICreateConcertCommand CreateConcert()
        {
            return new CreateConcertCommand();
        }

        public IPurchaseTicketCommand PurchaseTicket()
        {
            return new PurchaseTicketCommand();
        }

        public ICancelTicketCommand CancelTicket()
        {
            return new CancelTicketCommand();
        }
    }
}