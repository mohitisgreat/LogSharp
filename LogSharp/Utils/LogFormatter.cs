using System;

namespace LogSharp.Utils
{
    /// <summary>
    /// A simple class that uses LogSharp.Utils.StringFormatter class
    /// to format the log messages by replacing {props} by their respective
    /// value.
    /// </summary>
    public class LogFormatter
    {
        StringFormatter formatter;

        /// <summary>
        /// Instantiate the LogFormatter.
        /// </summary>
        /// <param name="format">The format to be used.</param>
        public LogFormatter(string format)
        {
            formatter = new StringFormatter(format);
        }
        
        /// <summary>
        /// Return the formatted log message.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <param name="severity">The severity level.</param>
        /// <param name="linenum">Line number</param>
        /// <param name="message">The log message(the value of message property)</param>
        public string FormatString(string filename, LogSeverity severity, int linenum, string message)
        {
            DateTime now = DateTime.Now;

            formatter.SetProperty("Hour", now.Hour.ToString("00"));
            formatter.SetProperty("hour", (((now.Hour + 11) % 12) + 1).ToString("00"));
            formatter.SetProperty("meridian", (now.Hour >= 12) ? "PM" : "AM");
            formatter.SetProperty("minute", now.Minute.ToString("00"));
            formatter.SetProperty("second", now.Second.ToString("00"));
            formatter.SetProperty("year", now.Year.ToString("0000"));
            formatter.SetProperty("month", now.Month.ToString("00"));
            formatter.SetProperty("day", now.Day.ToString("00"));
            formatter.SetProperty("dayofweek", now.DayOfWeek.ToString());
            formatter.SetProperty("dayofyear", now.DayOfYear.ToString("000"));

            formatter.SetProperty("file", filename);
            formatter.SetProperty("line", linenum.ToString());
            formatter.SetProperty("message", message);
            formatter.SetProperty("severity", severityLevels[(int)severity]);

            return formatter.FormatString();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Pattern {
            get { return formatter.Pattern;  }
            set { formatter = new StringFormatter(value); }
        }

        private static string[] severityLevels = {
            "Trace",
            "Info",
            "Debug",
            "Warning",
            "Error",
            "Fatal"
        };
    }
}
