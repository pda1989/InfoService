using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InfoService
{
    public class GetInfoCommand : ServiceCommand
    {
        protected IInstanceInfo _info;
        protected ISerializer _serializer;


        public GetInfoCommand(ILog log, IInstanceInfo info, ISerializer serializer) : base(log)
        {
            _info = info;
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
