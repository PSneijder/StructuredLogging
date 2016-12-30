using System.Globalization;
using System.Windows.Data;

namespace StructuredLogging.Desktop.Utilities.Data
{
    public sealed class CultureAwareBinding
        : Binding
    {
        public CultureAwareBinding()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
        }
    }
}