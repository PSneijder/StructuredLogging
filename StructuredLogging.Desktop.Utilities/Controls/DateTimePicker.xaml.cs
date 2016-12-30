
using System;
using System.Windows;

namespace StructuredLogging.Desktop.Utilities.Controls
{
    public partial class DateTimePicker
    {
        public static readonly DependencyProperty SelectedStartDateProperty = DependencyProperty.Register("SelectedStartDate",
                typeof(DateTime), typeof(DateTimePicker),
                    new FrameworkPropertyMetadata(DateTime.Now,
                        OnSelectedDateChanged,
                            CoerceDate));

        public static readonly DependencyProperty SelectedEndDateProperty = DependencyProperty.Register("SelectedEndDate",
                typeof(DateTime), typeof(DateTimePicker),
                    new FrameworkPropertyMetadata(DateTime.Now,
                        OnSelectedDateChanged,
                            CoerceDate));

        public DateTime SelectedStartDate
        {
            get { return (DateTime) GetValue(SelectedStartDateProperty); }
            set { SetValue(SelectedStartDateProperty, value); }
        }
        public DateTime SelectedEndDate
        {
            get { return (DateTime)GetValue(SelectedEndDateProperty); }
            set { SetValue(SelectedEndDateProperty, value); }
        }

        public DateTimePicker()
        {
            InitializeComponent();
        }

        public static void OnSelectedDateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var me = (DateTimePicker)obj;
            var date = (DateTime)args.NewValue;

        }

        private static object CoerceDate(DependencyObject d, object value)
        {
            DateTimePicker me = (DateTimePicker)d;
            DateTime current = Convert.ToDateTime(value);

            return current;
        }
    }
}