using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ApexTracker.Animations;

public static class Animations
{
    public static ThicknessAnimation GetRollUpAnimation(double seconds, double height)
    {
        return new ThicknessAnimation {
            Duration = new Duration(TimeSpan.FromSeconds(seconds)),
            From = new Thickness(0),
            To = new Thickness(0,0,0,height),
            DecelerationRatio = 0.9f
        };
    }
    
    
    public static ThicknessAnimation GetRollDownAnimation(double seconds, double height)
    {
        return new ThicknessAnimation {
            Duration = new Duration(TimeSpan.FromSeconds(seconds)),
            From = new Thickness(0,0,0,height),
            To = new Thickness(0),
            DecelerationRatio = 0.9f
        };
    }
}