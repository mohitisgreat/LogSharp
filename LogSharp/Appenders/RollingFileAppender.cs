using System.IO;
using System;

using LogSharp.Utils;

namespace LogSharp.Appenders
{
    /// <summary>
    /// Appender that'll log messages to file and if the file extends it limit it
    /// moves to the next file.
    /// </summary>
    public class RollingFileAppender: ILogAppender
    {
        public RollingFileAppender()
        {
            // Default Settings
            MaxFileSize = 1024;
            FilePattern = "application_{filenumber}.log";
            FileCount = 0;
            ParentDirectory = Directory.GetCurrentDirectory();

            // Initialize a file for writing.
            NextFile();
        }

        public RollingFileAppender(uint fileSize, string filePattern, string parentDirectory)
        {
            MaxFileSize = fileSize;
            FilePattern = filePattern;
            ParentDirectory = parentDirectory;
            FileCount = 0;

            // If directory doesn't exists throw an error.
            if (!Directory.Exists(parentDirectory)) {
                throw new DirectoryNotFoundException($"No such directory or folder as {parentDirectory}");
            }

            // Initialize a file for writing.
            NextFile();
        }

        /// <summary>
        /// Append the log message to the current log file.
        /// </summary>
        /// <param name="logSeverity"></param>
        /// <param name="message"></param>
        public void AppendLog(LogSeverity logSeverity, string message)
        {
            uint messageSize = (uint)(message.Length);

            if ((writer.BaseStream.Length + messageSize) > MaxFileSize)
                NextFile();

            writer.WriteLine(message);
        }

        /// <summary>
        /// Represents the size of file after which it switch to next file.
        /// </summary>
        public uint MaxFileSize { get; set; }

        /// <summary>
        /// Represents the pattern of the file.
        /// A new property filenumber is available which represents the number of the file.
        /// </summary>
        public string FilePattern 
        {
            get { return Formatter.Pattern; }
            set { Formatter = new StringFormatter(value); }
        }

        /// <summary>
        /// Represents the parent directory of the log files.
        /// </summary>
        public string ParentDirectory { get; set; }

        /// <summary>
        /// Represent the number of files created.
        /// </summary>
        public int FileCount { get; set; }

        /// <summary>
        /// Represents the current file in which log messages are begin written.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Initialize the formatter to format the file pattern to get a file name.
        /// </summary>
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
            Formatter.SetProperty("filenumber", FileCount.ToString());
        }

        // Roll to the next file.
        private void NextFile()
        {
            if (writer != null) {
                writer.Close();
            }

            InitializeFormatter();

            string fileName = Path.Combine(ParentDirectory, Formatter.ToString());

            writer = new StreamWriter(File.Open(fileName, FileMode.Append)) {
                AutoFlush = true
            };

            ++FileCount;
        }
        
        private StringFormatter Formatter { get; set; }
        private StreamWriter writer;
    }
}
