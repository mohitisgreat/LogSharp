using System;
using System.IO;

using LogSharp;
using LogSharp.Appenders;

namespace FileLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the log manager.
            LogManager.Initialize();
            string logPattern = "{hour}:{minute}:{second} {meridian}  {file}, {line} {severity}: {message}";
            LogManager.Instance.LogPattern = logPattern;

            // Add the appenders.
            LogManager.Instance.AddAppender(new ConsoleAppender());

            FileAppender fileAppender = new FileAppender(Directory.GetCurrentDirectory(), "application_{hour}_{minute}_{second}.log");
            LogManager.Instance.AddAppender(fileAppender);

            // Now, Initialization is completed, Intialization should be done in the startup.
            Logger.Trace("This is a trace message");
            Logger.Info("This is a info message");
        }
    }
}
