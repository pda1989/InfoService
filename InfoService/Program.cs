using Autofac;
using InfoService.Helpers;
using InfoService.Implementations;
using InfoService.Interfaces;
using InfoService.Models;
using System;
using System.Net.Http;
using System.Reflection;
using System.ServiceProcess;

namespace InfoService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(params string[] args)
        {
            if (args.Length == 0)
            {
                var log = new ServiceEventLog("Info Service");
                try
                {
                    // IoC container
                    var builder = new ContainerBuilder();

                    // Common
                    builder.RegisterType<JsonSerialyzer>().As<ISerializer>();
                    builder.RegisterInstance(log)
                        .As<ILog>()
                        .SingleInstance();

                    // Settings
                    builder.RegisterType<FileSettingsProvider>().As<ISettingsProvider>();
                    builder.RegisterType<ServiceSettings>().SingleInstance();

                    // Commands
                    builder.RegisterType<GetAPIVersionCommand>();

                    builder.RegisterType<WebUpdater>().As<IUpdater>();
                    builder.RegisterType<UpdateCommand>();

                    builder.RegisterType<PCInfo>().As<IInstanceInfo>();
                    builder.RegisterType<GetInfoCommand>();

                    builder.Register(c =>
                    {
                        var handler = new MessageHandler(c.Resolve<ISerializer>());
                        handler.AddCommand(c.Resolve<GetAPIVersionCommand>());
                        handler.AddCommand(c.Resolve<UpdateCommand>());
                        handler.AddCommand(c.Resolve<GetInfoCommand>());
                        return handler;
                    }).As<IMessageHandler>();

                    // Server
                    builder.RegisterType<HttpClient>();
                    builder.RegisterType<WebServer>().As<IInfoServer>();

                    IContainer container = builder.Build();

                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new InfoService(log, container.Resolve<IInfoServer>(), container.Resolve<ServiceSettings>())
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