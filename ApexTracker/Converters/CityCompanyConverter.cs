using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ApexTracker.Converters;

public class CityCompanyConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == DependencyProperty.UnsetValue)
        {
            return "";
        }
        var cityName = (string) value;
        return cityName.Equals("") ? value : $"{value} - ";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}