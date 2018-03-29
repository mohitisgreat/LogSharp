using System.IO;
using System.Runtime.CompilerServices;

namespace LogSharp
{
    public class Logger
    {
        /// <summary>
        /// Log the trace message
        /// </summary>
        /// <param name="message">The log message</param>
        public static void Trace(
            string message,
            [CallerFilePath] string filename = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogManager.Instance.LogMessage(LogSeverity.TRACE, message, Path.GetFileName(filename), lineNumber);
        }

        /// <summary>
        /// Log the information message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Info(
            string message,
            [CallerFilePath] string filename = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogManager.Instance.LogMessage(LogSeverity.INFO, message, Path.GetFileName(filename), lineNumber);
        }

        /// <summary>
        /// Log the debug message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Debug(
            string message,
            [CallerFilePath] string filename = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogManager.Instance.LogMessage(LogSeverity.DEBUG, message, Path.GetFileName(filename), lineNumber);
        }

        /// <summary>
        /// Log the warning message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Warning(
            string message,
            [CallerFilePath] string filename = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogManager.Instance.LogMessage(LogSeverity.WARNING, message, Path.GetFileName(filename), lineNumber);
        }

        /// <summary>
        /// Log the error message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Error(
            string message,
            [CallerFilePath] string filename = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogManager.Instance.LogMessage(LogSeverity.ERROR, message, Path.GetFileName(filename), lineNumber);
        }

        /// <summary>
        /// Log the fatal message.
        /// </summary>
        /// <param name="message">The log message.</param>
        public static void Fatal(
            string message,
            [CallerFilePath] string filename = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogManager.Instance.LogMessage(LogSeverity.FATAL, message, Path.GetFileName(filename), lineNumber);
        }
    }
}
