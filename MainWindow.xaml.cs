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

        public StartPage startPage = new StartPage();
        public RemoteAccessPage1 remoteAccessPage1 = new RemoteAccessPage1();
        public RemoteAccessPage2 remoteAccessPage2 = new RemoteAccessPage2();
        public SiteToSitePage1 siteToSitePage1 = new SiteToSitePage1();
        public SiteToSitePage2 siteToSitePage2 = new SiteToSitePage2();
        SiteToSitePage3 siteToSitePage3 = new SiteToSitePage3();

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
                    currentPage = "siteToSite1";
                    mainFrame.Content = siteToSitePage1;
                    btnBack.Visibility = Visibility.Visible;
                    
                }
                else if (startPage.cbRemoteAccess.IsChecked == true)
                {
                    currentPage = "remoteAccess1";
                    mainFrame.Content = remoteAccessPage1;
                    btnBack.Visibility = Visibility.Visible;
                }
            }
            else if (currentPage == "siteToSite1")
            {
                currentPage = "siteToSite2";
                mainFrame.Content = siteToSitePage2;
            }
            else if (currentPage == "siteToSite2")
            {
                siteToSitePage3.generateScript();

                currentPage = "siteToSite3";
                mainFrame.Content = siteToSitePage3;
            }
            else if(currentPage == "remoteAccess1")
            {
                currentPage = "remoteAccess2";
                mainFrame.Content = remoteAccessPage2;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage == "siteToSite1" || currentPage == "remoteAccess1")
            {
                btnBack.Visibility = Visibility.Hidden;
                currentPage = "startPage";
            }
            else if(currentPage == "siteToSite2")
            {
                currentPage = "siteToSite1";
            }
            else if (currentPage == "siteToSite3")
            {
                currentPage = "siteToSite2";
            }
            else if (currentPage == "remoteAccess2")
            {
                currentPage = "remoteAccess1";
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
