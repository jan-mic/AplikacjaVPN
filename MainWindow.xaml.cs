using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool running = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnToggleRun_Click(object sender, RoutedEventArgs e)
        {
            if(running)
            {
                //stop
                tbStatus.Text = "Stopped";
                btnToggleRun.Content = "Run";
            }
            else
            {
                //run
                tbStatus.Text = "Running";
                btnToggleRun.Content = "Stop";
            }
            
            running = !running;
        }
    }
}
