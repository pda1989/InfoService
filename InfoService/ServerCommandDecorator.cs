namespace InfoService
{
    public abstract class ServiceCommandDecorator : ServiceCommand
    {
        protected ServiceCommand _command;
        protected ILog _log;


        public ServiceCommandDecorator(ILog log, ServiceCommand command)
        {
            _log = log;
            _command = command;
        }

        public override OutputMessage Execute(InputMessage message)
        {
            return _command?.Execute(message);
        }
    }
}
