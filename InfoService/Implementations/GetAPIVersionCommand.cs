using InfoService.Helpers;
using InfoService.Interfaces;
using InfoService.Models;

namespace InfoService.Implementations
{
    public class GetAPIVersionCommand : ServiceCommand
    {
        public GetAPIVersionCommand(ILog log) : base(log)
        {
        }

        public override OutputMessage Execute(InputMessage message)
        {
            if (message.Command == "GetAPIVersion")
            {
                _log.Write("Hello");
                var version = ServiceHelper.GetAPIVersion();
                return new OutputMessage { Message = version.ToString() };
            }

            return null;
        }
    }
}