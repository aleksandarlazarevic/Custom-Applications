using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ActionManagement.UIUtilities
{
    public class NegatableBooleanToVisibilityConverter : IValueConverter
    {
        public bool Negate { get; set; }

        public Visibility FalseVisisbility { get; set; }

        public NegatableBooleanToVisibilityConverter()
        {
            this.FalseVisisbility = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue;
            bool result = bool.TryParse(value.ToString(), out booleanValue);
            if (!result)
            {
                return value;
            }

            if (booleanValue && !this.Negate)
            {
                return Visibility.Visible;
            }

            if (booleanValue && this.Negate)
            {
                return this.FalseVisisbility;
            }

            if (!booleanValue && this.Negate)
            {
                return Visibility.Visible;
            }

            if (!booleanValue && !this.Negate)
            {
                return this.FalseVisisbility;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
