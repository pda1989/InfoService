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
        static void Main(params string[] args)
        {
            if (args.Length == 0)
            {
                var log = new ServiceEventLog("Info Service");
                try
                {
                    var jsonConverter = new JsonSerialyzer();

                    // update
                    var updater = new WebUpdater();

                    // settings
                    string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string settingsFileName = Path.Combine(dirName, "ServiceSettings.json");
                    var settingsProvider = new FileSettingsProvider(settingsFileName, jsonConverter);
                    ServiceSettings.GetInstance().SetSettingsProvider(settingsProvider);

                    // commands
                    var commands = new Stack<ServiceCommandDecorator>();
                    commands.Push(new GetAPIVersionCommand(log, null));
                    commands.Push(new UpdateCommand(log, commands.Peek(), updater));

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
                    log.Write($"Application error: \n{exception.ToString()}", LogType.Error);
                    throw;
                }
            }
            else if (args[0] == "/I")
                ServiceHelper.InstallService(Assembly.GetExecutingAssembly().GetName().Name);
            else if (args[0] == "/U")
                ServiceHelper.UninstallService(Assembly.GetExecutingAssembly().GetName().Name);
            else if (args[0] == "/S")
                ServiceHelper.StartService(Assembly.GetExecutingAssembly().GetName().Name);
            else
                Console.WriteLine($"Unknown parameter {args[0]}");
        }
    }
}
