using System;

namespace LogSharp.Appenders
{
    public class ConsoleAppender: ILogAppender
    {
        public ConsoleAppender()
        {
        }

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
