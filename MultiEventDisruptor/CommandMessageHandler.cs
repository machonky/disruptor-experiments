using Disruptor;

namespace MultiEventDisruptor
{
    public class CommandMessageHandler : IEventHandler<CommandMessage>
    {
        private readonly CommandHandlerCollection _commandHandlers;

        public CommandMessageHandler(CommandHandlerCollection commandHandlers)
        {
            _commandHandlers = commandHandlers;
        }

        public void OnNext(CommandMessage data, long sequence, bool endOfBatch)
        {
            var command = data.Command;
            ((dynamic)_commandHandlers.GetHandler(command.GetType())).OnNext((dynamic)command, sequence, endOfBatch);
        }
    }
}