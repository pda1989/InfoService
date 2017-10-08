using InfoService.Interfaces;
using InfoService.Models;
using System;
using System.Reflection;

namespace InfoService.Implementations
{
    public class UpdateCommand : ServiceCommand
    {
        protected IUpdater _updater;

        public UpdateCommand(ILog log, IUpdater updater) : base(log)
        {
            _updater = updater;
        }

        public override OutputMessage Execute(InputMessage message)
        {
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
                    _log?.Write($"Update command error: \n{exception.ToString()}", LogType.Error);
                    return GetErrorMessage(exception.Message);
                }
            }

            return null;
        }
    }
}