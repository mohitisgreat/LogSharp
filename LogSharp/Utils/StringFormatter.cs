using System;
using System.Collections.Generic;
using System.Text;

namespace LogSharp.Utils
{
    /// <summary>
    /// Pattern the string in the form of 'blah blah blah {prop}' by replacing every {prop}
    /// by its value set using SetProperty.
    /// </summary>
    public class StringFormatter
    {
        /// <summary>
        /// Initialize a new formatter with the specified format.
        /// </summary>
        /// <param name="format"></param>
        public StringFormatter(String format)
        {
            Pattern = format;
            values = new Dictionary<string, string>();
        }

        public StringFormatter() : this("")
        {
        }

        /// <summary>
        /// Returns the format which is begin used.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Set the property.
        /// </summary>
        /// <param name="name">The key of the property</param>
        /// <param name="value">The value of the property</param>
        public void SetProperty(string name, string value)
        {
            values[name] = value;
        }

        /// <summary>
        /// Pattern the string
        /// </summary>
        /// <returns>Returns the formatted output.</returns>
        public string FormatString()
        {
            StringBuilder builder = new StringBuilder();

            for (int x = 0; x < Pattern.Length; ++x) {
                if (Pattern[x] == '{') {
                    ++x;

                    StringBuilder identifierBuilder = new StringBuilder();

                    for (; x < Pattern.Length; ++x) {
                        if (Pattern[x] == '}') {
                            string identifier = identifierBuilder.ToString();
                            if (values.ContainsKey(identifier)) {
                                builder.Append(values[identifier]);
                            }

                            break;
                        } else {
                            identifierBuilder.Append(Pattern[x]);
                        }
                    }
                } else {
                    builder.Append(Pattern[x]);
                }
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return FormatString();
        }

        private Dictionary<string, string> values;
    }
}
 