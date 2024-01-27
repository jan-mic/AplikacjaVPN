using System;
using System.Windows;
using System.Windows.Controls;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //info
        public string currentPage = "startPage";

        StartPage startPage = new StartPage();
        RemoteAccessPage remoteAccessPage = new RemoteAccessPage();
        SiteToSitePage siteToSidePage = new SiteToSitePage();
        SiteToSitePage2 siteToSitePage2 = new SiteToSitePage2();

        public MainWindow()
        {
            InitializeComponent();

            btnBack.Visibility = Visibility.Hidden;
            mainFrame.Content= startPage;

            btnNext.IsEnabled = false;



        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //RemoteAccessPage remoteAccessPage = new RemoteAccessPage();
            //this.Content = remoteAccessPage;
            //if(VpnApplication.StartPage.)

            //if(startPage.cbSiteToSite.IsChecked.Value)
            //bool omg = startPage.cbSiteToSite.IsChecked.Value;
            //MessageBox.Show(startPage.cbSiteToSite.IsChecked.ToString);

            if(currentPage == "startPage")
            {
                if (startPage.cbSiteToSite.IsChecked == true)
                {
                    currentPage = "siteToSite";
                    mainFrame.Content = siteToSidePage;
                    btnBack.Visibility = Visibility.Visible;
                    
                }
                else if (startPage.cbRemoteAccess.IsChecked == true)
                {
                    currentPage = "remoteAccess";
                    mainFrame.Content = remoteAccessPage;
                    btnBack.Visibility = Visibility.Visible;
                }
            }
            else if (currentPage == "siteToSite")
            {
                currentPage = "siteToSite2";
                mainFrame.Content = siteToSitePage2;
            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage == "siteToSite" || currentPage == "remoteAccess")
            {
                btnBack.Visibility = Visibility.Hidden;
                currentPage = "startPage";
            }
            else if(currentPage == "siteToSite2")
            {
                currentPage = "siteToSite";
            }


            if (mainFrame.NavigationService.CanGoBack)
            {
                mainFrame.NavigationService.GoBack();
            }



        }

        /*private void btnToggleRun_Click(object sender, RoutedEventArgs e)
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
        }*/
    }
}
