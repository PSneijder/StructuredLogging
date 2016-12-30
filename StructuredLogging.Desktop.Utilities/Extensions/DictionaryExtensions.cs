using System.Collections.Generic;
using System.Linq;

namespace StructuredLogging.Desktop.Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetParameter<T>(this IDictionary<string, object> dictionary, string name)
        {
            if (dictionary != null && dictionary.Keys.Any(key => key == name))
            {
                return (T) dictionary[name];
            }

            return default(T);
        }
    }
}