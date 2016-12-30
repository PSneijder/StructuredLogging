using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StructuredLogging.Desktop.Utilities.Converters
{
    [ValueConversion(typeof(ICollection), typeof(Visibility))]
    public sealed class EmptyListToVisibilityConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            ICollection list = value as ICollection;

            if (list != null)
            {
                return list.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}