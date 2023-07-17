using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ActionManagement.UIUtilities
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bolleanValue = false;
            if (value is bool)
            {
                bolleanValue = (bool)value;
            }
            else if (value is bool?)
            {
                bool? tmp = (bool?)value;
                bolleanValue = tmp.HasValue ? tmp.Value : false;
            }

            return bolleanValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }
            else
            {
                return false;
            }
        }
    }
}
