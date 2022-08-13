using System;
using System.Globalization;
using System.Windows.Data;
using SCSSdkClient;
using System.Linq;
using System.Windows;

namespace ApexTracker.Converters;

public class IncomeConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var income = (ulong) value;
        return income.Equals(0) ? "" : $"{income:N0} ";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}

public class IncomeCurrencyConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Any(x => x == DependencyProperty.UnsetValue))
        {
            return "";
        }
        var game = (SCSGame) values[0];
        var deliveryStarted = (bool) values[1];
        if (game == SCSGame.Unknown || !deliveryStarted) return "";

        return game == SCSGame.Ats ? "$ " : "€ ";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}