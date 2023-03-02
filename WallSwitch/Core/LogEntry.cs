using System;

namespace WallSwitch.Core
{
    public class LogEntry
    {
        public DateTime Entered { get; private set; }
        public LogLevel Severity { get; private set; }
        public string Message { get; private set; }

        public LogEntry(DateTime entered, LogLevel severity, string message)
        {
            Entered = entered;
            Severity = severity;
            Message = message;
        }
    }
}
