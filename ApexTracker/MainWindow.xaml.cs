using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ApexTracker.Animations.Animations;


namespace ApexTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            var tracker = new TrackerService();
            DataContext = tracker;
        }
        
        private void Border1_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Border1.Focus();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinButton_OnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch (SettingsControl.IsVisible)
            {
                case true:
                    SettingsControl.BeginAnimation(MarginProperty, GetRollUpAnimation(0.3, Height));
                    await Task.Delay(300);
                    SettingsControl.Visibility = Visibility.Collapsed;
                    SettingsButton.Style = FindResource("SettingsButton") as Style;
                    break;
                case false:
                    SettingsControl.Visibility = Visibility.Visible;
                    SettingsControl.BeginAnimation(MarginProperty, GetRollDownAnimation(0.3, Height));
                    SettingsButton.Style = FindResource("SettingsButtonActive") as Style;
                    break;
            }
        }
    }
}