using InfoService.Interfaces;
using InfoService.Models;
using System;

namespace InfoService.Implementations
{
    public class GetInfoCommand : ServiceCommand
    {
        protected IInstanceInfo _info;
        protected ISerializer _serializer;

        public GetInfoCommand(ILog log, IInstanceInfo info, ISerializer serializer) : base(log)
        {
            _info = info;
            _serializer = serializer;
        }

        public override OutputMessage Execute(InputMessage message)
        {
            if (message.Command == "GetInfo")
            {
                try
                {
                    if (_info == null)
                        throw new ArgumentNullException("IInstanceInfo is not initiated");

                    string result = _serializer?.Serialize(_info);
                    // needs to convert result to Base64
                    _log?.Write(result);
                    return new OutputMessage { Message = result };
                }
                catch (Exception exception)
                {
                    _log?.Write($"Get info command error: \n{exception.ToString()}");
                    return GetErrorMessage(exception.Message);
                }
            }

            return null;
        }
    }
}