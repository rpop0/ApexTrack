using System;
using System.Globalization;
using System.Windows.Data;

namespace ApexTracker.Converters;

public class OdometerConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return $"{(float) value:N0}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}