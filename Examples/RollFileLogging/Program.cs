using System.IO;

using LogSharp;
using LogSharp.Appenders;

namespace RollFileLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeLogging();

            for (int x = 0; x < 1000; ++x) {
                Logger.Warning($"X is at {x}");
            }
        }

        static void InitializeLogging()
        {
            LogManager.Initialize();

            string logPattern = "[{hour}:{minute}:{second} {meridian}] {severity}: {message}";
            LogManager.Instance.LogPattern = logPattern;

            string directory = Path.Combine(Directory.GetCurrentDirectory(), "Log");
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }

            RollingFileAppender fileAppender = new RollingFileAppender(1024, "application_{filenumber}.log", directory);

            LogManager.Instance.AddAppender(fileAppender);
            LogManager.Instance.AddAppender(new ConsoleAppender());
        }
    }
}
