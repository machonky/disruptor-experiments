namespace MultiEventDisruptor
{
    public interface ITicketCommandFactory
    {
        ICreateConcertCommand CreateConcert();

        IPurchaseTicketCommand PurchaseTicket();

        ICancelTicketCommand CancelTicket();
    }
}