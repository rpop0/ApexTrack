using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SCSSdkClient;

namespace ApexTracker.Converters;

public class OdometerConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == DependencyProperty.UnsetValue)
        {
            return "";
        }
        var odometer = (float) value;
        return odometer.Equals(0) ? "" : $"{odometer:N0} ";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}

public class OdometerSymbolConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == DependencyProperty.UnsetValue)
        {
            return "";
        }
        var game = (SCSGame) value;
        if (game == SCSGame.Unknown) return "";
        return game == SCSGame.Ats ? "Mi" : "Km";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}