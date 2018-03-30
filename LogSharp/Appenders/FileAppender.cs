using System;
using System.IO;

using LogSharp.Utils;

namespace LogSharp.Appenders
{
    /// <summary>
    /// A appender to append log messages to the file.
    /// </summary>
    public class FileAppender : ILogAppender
    {
        /// <summary>
        /// Construct a new file appender.
        /// </summary>
        /// <param name="parentFolder">The path to the folder where log files will be created.</param>
        /// <param name="filePattern">The file pattern.</param>
        public FileAppender(string parentFolder, string filePattern)
        {
            Formatter = new StringFormatter(filePattern);
            ParentFolder = parentFolder;

            // If directory doesn't exists throw an error.
            if (!Directory.Exists(parentFolder)) {
                throw new DirectoryNotFoundException($"No such folder or directory as '{parentFolder}'");
            }

            InitializeFormatter();

            FileName = Path.Combine(ParentFolder, Formatter.ToString());
            FileStream fileStream = File.Open(FileName, FileMode.Append);

            writer = new StreamWriter(fileStream) {
                AutoFlush = true
            };

            Logger.Debug($"File appender initialized for {FileName}");
        }

        ~FileAppender()
        {
            if (writer == null) {
                writer.Close();
            }
        }

        /// <summary>
        /// Append the log message to the file stream.
        /// </summary>
        public void AppendLog(LogSeverity logSeverity, string message)
        {
            if (writer != null) {
                writer.WriteLine(message);
            }
        }

        /// <summary>
        /// Represents parent folder where the log files are created.
        /// <summary>
        public string ParentFolder { get; set; }

        /// <summary>
        /// Represents the pattern of the log file.
        /// </summary>
        public string FilePattern 
        {
            get { return Formatter.Pattern; }
            set { Formatter.Pattern = value; }
        }

        /// <summary> 
        /// Returns the path of the log file which is open.
        /// </summary>
        public string FileName { get; private set; }

        private StringFormatter Formatter { get; set; }

        private void InitializeFormatter()
        {
            Formatter.SetProperty("Hour", DateTime.Now.Hour.ToString("00"));
            Formatter.SetProperty("hour", (((DateTime.Now.Hour + 11) % 12) + 1).ToString("00"));
            Formatter.SetProperty("meridian", (DateTime.Now.Hour >= 12) ? "PM" : "AM");
            Formatter.SetProperty("minute", DateTime.Now.Minute.ToString("00"));
            Formatter.SetProperty("second", DateTime.Now.Second.ToString("00"));
            Formatter.SetProperty("year", DateTime.Now.Year.ToString("0000"));
            Formatter.SetProperty("month", DateTime.Now.Month.ToString("00"));
            Formatter.SetProperty("day", DateTime.Now.Day.ToString("00"));
            Formatter.SetProperty("dayofweek", DateTime.Now.DayOfWeek.ToString());
            Formatter.SetProperty("dayofyear", DateTime.Now.DayOfYear.ToString("000"));
            Formatter.SetProperty("weekday", DateTime.Now.ToString("dddd"));
        }

        private StreamWriter writer;
    }
}
