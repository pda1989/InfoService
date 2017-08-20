namespace InfoService
{
    public abstract class ServiceCommandDecorator : IServiceCommand
    {
        protected IServiceCommand _command;
        protected ILog _log;


        public ServiceCommandDecorator(ILog log, IServiceCommand command)
        {
            _log = log;
            _command = command;
        }

        public virtual OutputMessage Execute(InputMessage message)
        {
            return _command?.Execute(message);
        }
    }
}
