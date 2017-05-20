using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Octobot.Extensions
{
    public static class StringExtensions
    {
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }


        public static byte[] ToByteArray(this string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string JoinWith(this string value, string separator, params string[] values)
        {
            return values.Where(IsNotEmpty).Aggregate(value, (current, newValue) => current + separator + newValue);
        }

        public static string RemoveWhitespace(this string value)
        {
            return (value ?? "").Replace(" ", string.Empty);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        public static string AsNullIfWhiteSpace(this string value)
        {
            return value.IsNullOrWhiteSpace() ? null : value;
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format.Replace("\n", Environment.NewLine).Replace("{nl}", Environment.NewLine), args);
        }

        private static readonly Regex WordBoundRegex = new Regex(@"{\w+?}");

        // See the following article on some implementations that include tests and can take dynamic objects/dictionaries too
        // http://haacked.com/archive/2009/01/04/fun-with-named-formats-string-parsing-and-edge-cases.aspx/
        public static string FormatRouteWith(this string route, params object[] args)
        {
            var result = route;
            var matchCollection = WordBoundRegex.Matches(route).Cast<Match>().Select(m => m.Value).Distinct().ToList();

            for (var i = 0; i < matchCollection.Count(); i++)
            {
                var replacement = args[i] == null ? "" : args[i].ToString();
                result = result.Replace(matchCollection[i], replacement);
            }
            if (WordBoundRegex.IsMatch(result))
            {
                throw new FormatException(
                    $"Not able to match all Route params. Route has {matchCollection.Count} items, and only {args?.Length ?? 0} args passed.");
            }
            return result;
        }

        public static string RemoveAllNoAlphanumericCharacters(this string str)
        {
            return Regex.Replace(str, @"[\W]", "");
        }

        public static string TrimTo(this string str, char c)
        {
            var indexOf = str.IndexOf(c);
            return indexOf == -1 ? string.Empty : str.Substring(indexOf + 1);
        }

        public static int AsInt(this string value)
        {
            return int.Parse(value);
        }

        public static bool IsNumber(this string value)
        {
            double temp;
            return double.TryParse(value, out temp);
        }

        public static decimal AsDecimal(this string value)
        {
            return decimal.Parse(value);
        }

        public static decimal AsPositiveOrNegativeDecimal(this string value)
        {
            return
                decimal.Parse(value,
                    NumberStyles.Number | NumberStyles.AllowParentheses | NumberStyles.AllowThousands |
                    NumberStyles.AllowDecimalPoint);
        }

        public static long AsLong(this string value)
        {
            return long.Parse(value);
        }

        public static List<long> AsLong(this IEnumerable<string> value)
        {
            return value.Select(long.Parse).ToList();
        }

        public static decimal? AsNullableDecimal(this string value)
        {
            if (value.IsNullOrWhiteSpace()) return null;
            return decimal.Parse(value);
        }
        
        public static long? AsNullableLong(this string value)
        {
            if (value.IsNullOrWhiteSpace()) return null;
            return long.Parse(value);
        }

        public static bool IsWrappedWith(this string value, string wrapper)
        {
            return value.StartsWith(wrapper) && value.EndsWith(wrapper);
        }

        public static string RemoveWrappingCharacters(this string value, string wrapper)
        {
            return value.IsWrappedWith(wrapper) ? value.Substring(wrapper.Length, value.Length - 2 * wrapper.Length) : value;
        }

        public static bool YesNoToBoolean(this string value)
        {
            if (string.Equals(value, "y", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.Equals(value, "n", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "no", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            throw new Exception($"Cannot convert string [{value}] to true/false.");
        }

        public static bool YesNoOrTrueFalseToBoolean(this string value, bool? defaultValue = null)
        {
            if (string.Equals(value, "y", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "yes", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.Equals(value, "n", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "no", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (bool.TryParse(value, out bool outBool))
            {
                return outBool;
            }
            if (defaultValue == null)
                throw new Exception($"Cannot convert string [{value}] to true/false.");
            return defaultValue.Value;
        }

        public static bool? TrueFalseToNullableBoolean(this string value)
        {
            return value.IsNullOrWhiteSpace() ? default(bool?) : TrueFalseToBoolean(value);
        }

        public static bool TrueFalseToBoolean(this string value, bool defaultValue = false)
        {
            bool outBool;
            return bool.TryParse(value, out outBool) ? outBool : defaultValue;
        }

        public static string TrimEndComma(this string value)
        {
            return value.TrimEnd(',', ' ');
        }

        public static DateTime ToDateTime(this string input, bool throwExceptionIfFailed = false)
        {
            var valid = DateTime.TryParse(input, out DateTime result);
            if (valid) return result;

            if (throwExceptionIfFailed)
                throw new FormatException($"'{input}' cannot be converted as DateTime");
            return result;
        }

        public static long ToLong(this string input, bool throwExceptionIfFailed = false)
        {
            long result;
            var valid = long.TryParse(input, out result);
            if (valid) return result;

            if (throwExceptionIfFailed)
                throw new FormatException($"'{input}' cannot be converted to long");
            return result;
        }

        public static string FirstLetterToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);
        public static string RandomString(int size)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        
        public static string BuildEscapedCsvLine(this string[] fields)
        {
            for (var i = 0; i < fields.Length; i++)
            {
                if (fields[i] != null && fields[i].Contains(","))
                {
                    fields[i] = '"' + fields[i] + '"';
                }
            }
            return string.Join(",", fields);
        }

        public static string StripDomainName(this string value)
        {
            var indexOfDomain = value.IndexOf("\\", StringComparison.Ordinal);
            return indexOfDomain != -1 ? value.Substring(indexOfDomain + 1) : value;
        }
    }
}