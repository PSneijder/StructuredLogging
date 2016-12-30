using System.Linq;
using Prism.Regions;

namespace StructuredLogging.Desktop.Utilities.Extensions
{
    public static class PrismExtensions
    {
        public static T GetParameter<T>(this NavigationContext context, string name)
        {
            if (context.Parameters != null && context.Parameters.Any(p => p.Key == name))
            {
                return (T) context.Parameters[name];
            }

            return default(T);
        }
    }
}