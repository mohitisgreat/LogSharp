using System.Collections.Generic;
using LogSharp.Utils;

namespace LogSharp
{
    public class LogManager
    {
        private static LogManager instance;

        private LogFormatter formatter;

        /// <summary>
        /// Returns the new LogFormatter.
        /// </summary>
        LogManager()
        {
            formatter = new LogFormatter("{severity}: {message}");
            Appenders = new List<ILogAppender>();
        }

        /// <summary>
        /// Return the global instance of the LogManager.
        /// </summary>
        public static LogManager Instance {
            get {
                if (instance != null) {
                    return instance;
                } else {
                    // Throw an error.
                    throw new Exceptions.NotIntializedException("LogManager has not been initialized.");
                }
            }
        }

        /// <summary>
        /// Initialize the log manager.
        /// </summary>
        public static void Initialize()
        {
            instance = new LogManager();
        }

        /// <summary>
        /// LogPattern is a string format describes how log message is displayed.
        /// </summary>
        public string LogPattern {
            get {
                return formatter.Pattern;
            }
            set {
                formatter.Pattern = value;
            }
        }

        /// <summary>
        /// The log filter.
        /// </summary>
        public ILogFilter LogFilter { get; set; }

        /// <summary>
        /// The log error handler.
        /// </summary>
        public IErrorHandler ErrorHandler { get; set; }
        
        /// <summary>
        /// Add a new appender to the list
        /// </summary>
        /// <param name="appender">The appender to be added.</param>
        public void AddAppender(ILogAppender appender)
        {
            Appenders.Add(appender);
        }

        /// <summary>
        /// Remove the appender from the list.
        /// </summary>
        /// <param name="appender">The appender to be removed.</param>
        public void RemoveAppender(ILogAppender appender)
        {
            Appenders.Remove(appender);
        }

        /// <summary>
        /// Returns the list of available appenders.
        /// </summary>
        public List<ILogAppender> Appenders { get; private set; }

        /// <summary>
        /// Call the appenders.
        /// </summary>
        /// <param name="severity">The log severity level.</param>
        /// <param name="message">The log message</param>
        /// <param name="filename">The name of the file.</param>
        /// <param name="lineNumber">The line number of the file.</param>
        public void LogMessage(LogSeverity severity, string message, string filename, int lineNumber)
        {
            string formattedMessage = formatter.FormatString(filename, severity, lineNumber, message);

            foreach (ILogAppender appender in Appenders) {
                if (LogFilter == null) {
                    appender.AppendLog(severity, formattedMessage);
                } else {
                    if (LogFilter.IsLoggable(severity, message, filename, lineNumber)) {
                        appender.AppendLog(severity, formattedMessage);
                    }
                }

                if (severity >= LogSeverity.ERROR) {
                    if (ErrorHandler != null) {
                        ErrorHandler.HandleError(severity, message, filename, lineNumber);
                    }
                }
            }
        }
    }
}
