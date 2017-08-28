using System;
using System.Reflection;

namespace InfoService
{
    public class UpdateCommand : ServiceCommandDecorator
    {
        private IUpdater _updater;
        
        public UpdateCommand(ILog log, ServiceCommand command, IUpdater updater) : base(log, command)
        {
            _updater = updater;
        }

        public override OutputMessage Execute(InputMessage message)
        {
            var outputMessage = base.Execute(message);
            if (outputMessage != null)
                return outputMessage;

            if (message.Command == "UpdateService")
            {
                try
                {
                    if (message.Parameters.Count < 3)
                        throw new ArgumentException("Not enough arguments for update");

                    var version = Version.Parse(message.Parameters[0]);
                    string updateUri = message.Parameters[1];
                    string md5 = message.Parameters[2];

                    var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    if (currentVersion < version)
                        _updater?.Update(Assembly.GetExecutingAssembly(), updateUri, md5);
                    else
                    {
                        string resultMessage = $"The current version ({currentVersion}) is newer than {version}";
                        _log?.Write(resultMessage);
                        return new OutputMessage { Message = resultMessage };
                    }
                }
                catch (Exception exception)
                {
                    _log?.Write(exception.Message, LogType.Error);
                    return GetErrorMessage(exception.Message);
                }
            }

            return null;
        }

    }
}
