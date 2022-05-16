using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace ApexTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool SimulatorStatus { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            StatusBadge.MouseDown += StatusBadge_OnMouseDown;
        }

        private void ToggleSimulatorStatus()
        {
            if (!SimulatorStatus)
            {
                StatusBadge.Style = FindResource("SuccessBadge") as Style;
                BackgroundBrush.ImageSource = FindResource("BackgroundArrowSuccess") as ImageSource;
                StatusBadgeText.Foreground = Brushes.Black;
                StatusBadgeText.Text = "Simulatorul este pornit!";
                SimulatorStatus = true;
                return;
            }
            StatusBadge.Style = FindResource("DangerBadge") as Style;
            BackgroundBrush.ImageSource = FindResource("BackgroundArrowDanger") as ImageSource;
            StatusBadgeText.Foreground = Brushes.White;
            StatusBadgeText.Text = "Simulatorul este oprit!";
            SimulatorStatus = false;
        }

        private void StatusBadge_OnMouseDown(object sender, RoutedEventArgs e)
        {
            ToggleSimulatorStatus();
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
    }
}