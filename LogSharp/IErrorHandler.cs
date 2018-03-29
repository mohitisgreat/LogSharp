namespace LogSharp
{
    /// <summary>
    /// The interface to handle the error messages.
    /// </summary>
    public interface IErrorHandler
    {
        /// <summary>
        /// The method which is called when message is logged of severity level >= LogSeverity.ERROR
        /// </summary>
        /// <param name="severity">The log severity level.</param>
        /// <param name="message">The log message.</param>
        /// <param name="filename">The logger file name.</param>
        /// <param name="linenumber">The logger file line number.</param>
        void HandleError(LogSeverity severity, string message, string filename, int linenumber);
    }
}
