using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ApexTracker.Converters;

public class TruckImageConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == DependencyProperty.UnsetValue || (string) value == "")
        {
            return "https://cdn.apex-express.net/no-truck.png";
        }
        var valStr = value.ToString();
        return $"https://cdn.apex-express.net/{valStr}.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}