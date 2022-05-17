using System;
using System.Globalization;
using System.Windows.Data;

namespace ApexTracker.Converters;

public class StatusTextConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var gameStatus = (bool)value;
        return gameStatus ? "Simulatorul este pornit!" : "Simulatorul este oprit!";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}