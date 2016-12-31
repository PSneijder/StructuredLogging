using Lucene.Net.Documents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StructuredLogging.Extensions
{
    static class FieldExtensions
    {
        public static DateTime AsDateTime(this Field field)
        {
            return Convert.ToDateTime(field.StringValue);
        }

        public static double AsDouble(this Field field)
        {
            return Convert.ToDouble(field.StringValue);
        }

        public static string AsString(this Field field)
        {
            return field.StringValue;
        }

        public static Dictionary<string, object> ToDictionary(this Field field)
        {
            var value = field.StringValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                return new Dictionary<string, object>();
            }

            var properties = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);

            return properties ?? new Dictionary<string, object>();
        }
    }
}