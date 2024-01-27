using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    /// 


    public partial class StartPage : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public StartPage()
        {
            InitializeComponent();
        }

        private void cbSiteToSite_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;

            cbRemoteAccess.IsChecked = false;

            if(cbSiteToSite.IsChecked == true)
            {
                mainWindow.btnNext.IsEnabled = true;
            }
            else
            {
                mainWindow.btnNext.IsEnabled = false;
            }
        }

        private void cbRemoteAccess_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;

            cbSiteToSite.IsChecked = false;

            if (cbRemoteAccess.IsChecked == true)
            {
                mainWindow.btnNext.IsEnabled = true;
            }
            else
            {
                mainWindow.btnNext.IsEnabled = false;
            }
        }
    }
}
