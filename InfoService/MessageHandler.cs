namespace InfoService
{
    public class MessageHandler : IMessageHandler
    {
        protected ISerializer _converter;
        protected IServiceCommand _command;


        public MessageHandler(ISerializer converter, IServiceCommand command)
        {
            _converter = converter;
            _command = command;
        }

        public virtual string ProcessMessage(string message)
        {
            var result = _converter?.Deserialize<InputMessage>(message);
            return ProcessMessage(result);
        }

        public virtual string ProcessMessage(InputMessage message)
        {
            if (message == null)
                return GetErrorMessage("Message is null");

            var outputMessage = _command?.Execute(message);
            if (outputMessage == null)
                return GetErrorMessage("Unknown command");

            return _converter?.Serialize(outputMessage);
        }

        public string GetErrorMessage(string error)
        {
            var outputMessage = new OutputMessage
            {
                Result = false,
                Message = error ?? string.Empty
            };
            return _converter?.Serialize(outputMessage);
        }
    }
}
