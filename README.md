# LogSharp

A flexible logging library completely written in C#.

## Example

    import LogSharp;
    import LogSharp.Appenders;

    public class SomeClass
    {
        public void someFunction()
        {
            Logger.Trace("This is a trace message.");
            Logger.Info("This is a info message");
            Logger.Debug("This is a debug message");
            Logger.Warning("This is a warning message");
            Logger.Error("This is a error message");
            Logger.Fatal("This is a fatal message");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the Logger here.
            LogManager.Intialize();
            LogManager.Instance.AddAppender(new ConsoleAppender());
            
            // Set the log pattern
            LogManager.Instance.LogPattern = "[{hour}:{minute}:{second} {meridian}] [{file}, {line}] {severity}: {message}";

            SomeClass someClass = new SomeClass();
            someClass.someFunction();
        }
    }