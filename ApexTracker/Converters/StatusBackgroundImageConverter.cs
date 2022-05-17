using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ApexTracker.Converters;

public class StatusBackgroundImageConverter: IValueConverter
{
    public ImageSource? SimRunningImageSource { get; set; }
    public ImageSource? SimStoppedImageSource { get; set; }
    
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var gameStatus = (bool)value;
        return gameStatus ? SimRunningImageSource : SimStoppedImageSource;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}