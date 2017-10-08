using InfoService.Helpers;
using InfoService.Implementations;
using InfoService.Interfaces;
using System;
using System.ServiceProcess;
using System.Timers;

namespace InfoService
{
    public partial class InfoService : ServiceBase
    {
        protected ILog _log;
        protected IInfoServer _server;
        protected ServiceSettings _settings;

        public InfoService(ILog log, IInfoServer server, ServiceSettings settings)
        {
            InitializeComponent();

            _log = log;
            _server = server;
            _settings = settings;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _settings?.LoadSettings();

                _log?.Write($"Info service started (API ver. {ServiceHelper.GetAPIVersion()})\nTimer interval: {_settings?.TimerInterval}");

                StartTimer(_settings?.TimerInterval ?? 60000);
            }
            catch (Exception exception)
            {
                _log?.Write($"Service error: \n{exception.ToString()}");
            }
        }

        protected override void OnStop()
        {
            _log?.Write("Info service stopped");
        }

        protected void PerformCommand()
        {
            try
            {
                _server?.PerformCommand();
            }
            catch (Exception exception)
            {
                _log?.Write($"Service error: \n{exception.ToString()}");
            }
        }

        protected void StartTimer(int interval)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += (sender, e) => PerformCommand();
            timer.Start();
        }
    }
}