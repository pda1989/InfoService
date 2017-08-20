namespace InfoService
{
    public class TestCommand : ServiceCommandDecorator
    {
        public TestCommand(ILog log, IServiceCommand command) : base(log, command)
        {
        }

        public override OutputMessage Execute(InputMessage message)
        {
            var outputMessage = base.Execute(message);
            if (outputMessage != null)
                return outputMessage;

            if (message.Command == "Test")
            {
                _log?.Write("Test executed");
                return new OutputMessage { Message = "Test" };
            }

            return null;
        }
    }
}
