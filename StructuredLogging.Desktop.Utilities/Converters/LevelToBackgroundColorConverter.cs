using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StructuredLogging.Desktop.Utilities.Converters
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    public sealed class LevelToBackgroundColorConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush colorBrush = new SolidColorBrush(Colors.LightGray);

            if (value.Equals("Warning"))
            {
                colorBrush = new SolidColorBrush(Color.FromRgb(255, 235, 156));
            }
            else if (value.Equals("Error"))
            {
                colorBrush = new SolidColorBrush(Color.FromRgb(255, 199, 206));
            }

            return colorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}