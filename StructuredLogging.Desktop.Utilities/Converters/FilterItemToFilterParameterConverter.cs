using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using StructuredLogging.DataContracts.Query;
using StructuredLogging.Desktop.Utilities.Models.Filter;

namespace StructuredLogging.Desktop.Utilities.Converters
{
    [ValueConversion(typeof(object[]), typeof(FilterItem))]
    public sealed class FilterItemToFilterParameterConverter
        : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!values.Any())
            {
                return null;
            }

            QueryFilterProperty property = (QueryFilterProperty) values[0];
            bool isChecked = (bool) values[1];

            return new FilterItem(property, isChecked);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
