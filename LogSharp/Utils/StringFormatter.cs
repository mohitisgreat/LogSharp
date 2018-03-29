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
            this.Pattern = format;

            values = new Dictionary<string, string>();
            tokens = new List<Token>();

            position = 0;

            while (position < Pattern.Length) {
                if (Pattern[position] == '{') {
                    tokens.Add(new Token(TokenType.IDENTIFIER, Identifier()));
                } else {
                    tokens.Add(new Token(TokenType.STRINGLIT, StringLit()));
                }
            }
        }

        /// <summary>
        /// Returns the format which is begin used.
        /// </summary>
        public string Pattern { get; }

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
            foreach (Token tok in tokens) {
                if (tok.Type == TokenType.IDENTIFIER) {
                    if (values.ContainsKey(tok.Value)) {
                        builder.Append(values[tok.Value]);
                    }
                } else if (tok.Type == TokenType.STRINGLIT) {
                    builder.Append(tok.Value);
                }
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return FormatString();
        }

        /// <summary>
        /// Returns the next identifier in curly braces.
        /// </summary>
        private string Identifier()
        {
            try {
                if (Pattern[position] == '{') {
                    ++position;

                    StringBuilder stringBuilder = new StringBuilder();
                    while (Pattern[position] != '}') {
                        stringBuilder.Append(Pattern[position]);
                        ++position;
                    }
                    ++position;

                    return stringBuilder.ToString();
                } else {
                    return null; // Not an identifier.
                }
            } catch (IndexOutOfRangeException) {
                throw new Exception("No closing '}' found.");
            }
        }

        /// <summary>
        /// Returns the string breaks on curly braces.
        /// </summary>
        private string StringLit()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (position < Pattern.Length && Pattern[position] != '{') {
                stringBuilder.Append(Pattern[position]);
                ++position;
            }
            return stringBuilder.ToString();
        }
        private Dictionary<string, string> values;
        private List<Token> tokens;
        private int position;

        enum TokenType
        {
            IDENTIFIER,
            STRINGLIT
        }
        
        struct Token
        {
            public TokenType Type;
            public string Value;

            public Token(TokenType tokenType, string value)
            {
                this = new Token {
                    Type = tokenType,
                    Value = value
                };
            }
        }
    }
}
 