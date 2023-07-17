using ActionManagement.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace ActionManagement.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.DataContext = new MainWindowViewModel();
            this.LoadInitialParameters();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs ea)
        {
            Application.Current.MainWindow.Activate();
        }

        private void LoadInitialParameters()
        {
            this.InitializeProgressBar();
            this.TestRunnerMenuButton.Focus();
            Keyboard.Focus(this.TestRunnerMenuButton);
        }

        private void InitializeProgressBar()
        {
            this.ProgressBar.Value = 0;
            this.StatusBar.Visibility = Visibility.Hidden;
            this.ProgressBarPercentage.Visibility = Visibility.Hidden;
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs ea)
        {
            if (ea.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
