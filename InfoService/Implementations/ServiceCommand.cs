using InfoService.Interfaces;
using InfoService.Models;

namespace InfoService.Implementations
{
    public abstract class ServiceCommand
    {
        protected ILog _log;

        public ServiceCommand(ILog log)
        {
            _log = log;
        }

        public abstract OutputMessage Execute(InputMessage message);

        public virtual OutputMessage GetErrorMessage(string errorMessage) =>
            new OutputMessage { Result = false, Message = errorMessage };
    }
}