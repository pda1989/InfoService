namespace InfoService
{
    public abstract class ServiceCommand
    {
        protected ILog _log;


        public ServiceCommand(ILog log)
        {
            _log = log;
        }

        public abstract OutputMessage Execute(InputMessage message);

        public virtual OutputMessage GetErrorMessage(string errorMessage)
        {
            return new OutputMessage { Result = false, Message = errorMessage };
        }
    }
}
