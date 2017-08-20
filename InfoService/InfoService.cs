using System;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;

namespace InfoService
{
    public partial class InfoService : ServiceBase
    {
        protected ILog _log;
        protected IInfoServer _server;

        public InfoService(ILog log, IInfoServer server)
        {
            InitializeComponent();

            _log = log;
            _server = server;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var settings = ServiceSettings.GetInstance();
                settings.LoadSettings();

                _log?.Write($"Info service started (API ver. {ServiceHelper.GetAPIVersion()})\nTimer interval: {settings.TimerInterval}");
                
                StartTimer(settings.TimerInterval);
            }
            catch (Exception exception)
            {
                _log?.Write(exception.Message);
            }
        }

        protected override void OnStop()
        {
            _log?.Write("Info service stoped");
        }

        protected void StartTimer(int interval)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += (sender, e) => PerformCommand();
            timer.Start();
        }

        protected void PerformCommand()
        {
            try
            {
                _server?.PerformCommand();
            }
            catch (Exception exception)
            {
                _log?.Write(exception.Message);
            }
        }
    }
}
