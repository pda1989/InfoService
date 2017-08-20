using System;
using System.Diagnostics;

namespace InfoService
{
    public class ServiceEventLog : ILog, IDisposable
    {
        protected EventLog _eventLog = new EventLog();


        public ServiceEventLog(string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException("Event log source cannot be empty");

            if (!EventLog.SourceExists(source))
                EventLog.CreateEventSource(source, "Application");
            _eventLog.Source = source;
        }

        public virtual void Dispose()
        {
            _eventLog?.Dispose();
        }

        public virtual void Write(string message, LogType type = LogType.Information)
        {
            EventLogEntryType eventType;
            switch (type)
            {
                case LogType.Information:
                    eventType = EventLogEntryType.Information;
                    break;
                case LogType.Error:
                    eventType = EventLogEntryType.Error;
                    break;
                default:
                    eventType = EventLogEntryType.Information;
                    break;
            }

            _eventLog?.WriteEntry(message, eventType);
        }
    }
}
