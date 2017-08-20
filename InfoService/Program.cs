using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Reflection;

namespace InfoService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var log = new ServiceEventLog("Info Service");
            try
            {
                var jsonConverter = new JsonSerialyzer();

                // settings
                string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string settingsFileName = Path.Combine(dirName, "ServiceSettings.json");
                var settingsProvider = new FileSettingsProvider(settingsFileName, jsonConverter);
                ServiceSettings.GetInstance().SetSettingsProvider(settingsProvider);

                // commands
                var commands = new Stack<ServiceCommandDecorator>();
                commands.Push(new TestCommand(log, null));
                commands.Push(new GetAPIVersionCommand(log, commands.Peek()));

                // handlers
                var messageHandler = new MessageHandler(jsonConverter, commands.Peek());
                var server = new WebServer(messageHandler);


                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new InfoService(log, server)
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception exception)
            {
                log.Write(exception.Message, LogType.Error);
                throw;
            }
        }
    }
}
