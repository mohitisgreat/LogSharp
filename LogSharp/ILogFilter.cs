namespace LogSharp
{
    public interface ILogFilter
    {
        bool IsLoggable(LogSeverity severity, string message, string filename, int linenumber);
    }
}
