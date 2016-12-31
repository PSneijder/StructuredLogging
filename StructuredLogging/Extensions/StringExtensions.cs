using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace StructuredLogging.Extensions
{
    static class StringExtensions
    {
        public static string FormatWith(this string format, object source)
        {
            return FormatWith(format, null, source);
        }

        public static string FormatWith(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            Regex regex = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            List<object> values = new List<object>();

            string rewrittenFormat = regex.Replace(format, match =>
            {
                Group startGroup = match.Groups["start"];
                Group propertyGroup = match.Groups["property"];
                Group formatGroup = match.Groups["format"];
                Group endGroup = match.Groups["end"];

                values.Add(propertyGroup.Value == "0" ? source : DataBinder.Eval(source, propertyGroup.Value));

                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }
    }
}