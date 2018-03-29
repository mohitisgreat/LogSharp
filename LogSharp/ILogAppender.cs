namespace LogSharp
{
    public interface ILogAppender
    {
        /// <summary>
        /// Appends the log message in the specific device.
        /// </summary>
        /// <param name="logSeverity">The log severity level.</param>
        /// <param name="message">The log message which is formatted.</param>
        void AppendLog(LogSeverity logSeverity, string message);
    }
}
