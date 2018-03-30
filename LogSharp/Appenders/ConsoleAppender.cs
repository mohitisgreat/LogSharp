using System;

namespace LogSharp.Appenders
{
    public class ConsoleAppender: ILogAppender
    {
        public ConsoleAppender()
        {
        }

        /// <summary>
        /// Append the log message to the console
        /// </summary>
        /// <param name="logSeverity">The log severity message.</param>
        /// <param name="message">The log message</param>
        public void AppendLog(LogSeverity logSeverity, string message)
        {
            if (logSeverity >= LogSeverity.ERROR) {
                Console.Error.WriteLine(message);
            } else {
                Console.WriteLine(message);
            }
        }
    }
}
