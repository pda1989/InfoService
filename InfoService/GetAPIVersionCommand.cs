namespace InfoService
{
    public class GetAPIVersionCommand : ServiceCommandDecorator
    {
        public GetAPIVersionCommand(ILog log, IServiceCommand command) : base(log, command)
        {
        }

        public override OutputMessage Execute(InputMessage message)
        {
            var outputMessage = base.Execute(message);
            if (outputMessage != null)
                return outputMessage;

            if (message.Command == "GetAPIVersion")
            {
                var version = ServiceHelper.GetAPIVersion();
                return new OutputMessage { Message = version.ToString() };
            }

            return null;
        }
    }
}
