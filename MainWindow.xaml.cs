using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        public RemoteAccessPage3 remoteAccessPage3 = new RemoteAccessPage3();
        public SiteToSitePage1 siteToSitePage1 = new SiteToSitePage1();
        public SiteToSitePage2 siteToSitePage2 = new SiteToSitePage2();
        public SiteToSitePage3 siteToSitePage3 = new SiteToSitePage3();
        //ValidationCheck validationCheck = new ValidationCheck();

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

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

            ValidationInfo validationInfo = new ValidationInfo();

            if (currentPage == "startPage")
            {
                if(startPage.tBoxRuleName.Text.Length > 0)
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
                else
                {
                    validationInfo.ShowDialog();
                }
              
            }
            else if (currentPage == "siteToSite1")
            {
                if (IsValidSiteToSite1())
                {
                    currentPage = "siteToSite2";
                    mainFrame.Content = siteToSitePage2;
                }
                else
                {                    
                    validationInfo.ShowDialog();
                }
            }
            else if (currentPage == "siteToSite2")
            {
                if(IsValidSiteToSite2())
                {
                    siteToSitePage3.generateScript();
                    siteToSitePage3.generateScript2();

                    btnNext.Content = "Finish";
                    currentPage = "siteToSite3";
                    mainFrame.Content = siteToSitePage3;
                }
                else
                {
                    validationInfo.ShowDialog();
                }
            }
            else if (currentPage == "siteToSite3")
            {
                System.Windows.Application.Current.Shutdown();
            }
            else if(currentPage == "remoteAccess1")
            {
                if(IsValidRemoteAccess1())
                {
                    currentPage = "remoteAccess2";
                    mainFrame.Content = remoteAccessPage2;
                }
                else
                {
                    validationInfo.ShowDialog();
                }
            }
            else if (currentPage == "remoteAccess2")
            {
                if(IsValidRemoteAccess2())
                {
                    remoteAccessPage3.generateScript();

                    btnNext.Content = "Finish";
                    currentPage = "remoteAccess3";
                    mainFrame.Content = remoteAccessPage3;
                }
                else
                {
                    validationInfo.ShowDialog();
                }             
            }
            else if (currentPage == "remoteAccess3")
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == "siteToSite1" || currentPage == "remoteAccess1")
            {
                btnBack.Visibility = Visibility.Hidden;
                currentPage = "startPage";
            }
            else if (currentPage == "siteToSite2")
            {
                currentPage = "siteToSite1";
            }
            else if (currentPage == "siteToSite3")
            {
                btnNext.Content = "Next";
                currentPage = "siteToSite2";
            }
            else if (currentPage == "remoteAccess2")
            {
                currentPage = "remoteAccess1";
            }
            else if (currentPage == "remoteAccess3")
            {
                btnNext.Content = "Next";
                currentPage = "remoteAccess2";
            }

            if (mainFrame.NavigationService.CanGoBack)
            {
                mainFrame.NavigationService.GoBack();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private bool IsValidSiteToSite1()
        {
            int.TryParse(siteToSitePage1.tBoxLifeTime.Text, out int numericLifeTime);
            if (numericLifeTime < 60 || siteToSitePage1.tBoxPriority.Text.Length == 0 || siteToSitePage1.tBoxPreSharedKey.Text.Length == 0 || siteToSitePage1.tBoxSourcePeerIP1.Text.Length == 0 || siteToSitePage1.tBoxSourcePeerIP2.Text.Length == 0 || siteToSitePage1.tBoxSourcePeerIP3.Text.Length == 0 || siteToSitePage1.tBoxSourcePeerIP4.Text.Length == 0 || siteToSitePage1.tBoxDestinationPeerIP1.Text.Length == 0 || siteToSitePage1.tBoxDestinationPeerIP2.Text.Length == 0 || siteToSitePage1.tBoxDestinationPeerIP3.Text.Length == 0 || siteToSitePage1.tBoxDestinationPeerIP4.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        private bool IsValidRemoteAccess1()
        {
            if (remoteAccessPage1.tBoxInterface.Text.Length < 5 || remoteAccessPage1.tBoxPriority.Text.Length == 0 || remoteAccessPage1.tBoxGroupName.Text.Length == 0 || remoteAccessPage1.tBoxGroupKey.Text.Length == 0 || remoteAccessPage1.tBoxFirstIP1.Text.Length == 0 || remoteAccessPage1.tBoxFirstIP2.Text.Length == 0 || remoteAccessPage1.tBoxFirstIP3.Text.Length == 0 || remoteAccessPage1.tBoxFirstIP4.Text.Length == 0 || remoteAccessPage1.tBoxLastIP1.Text.Length == 0 || remoteAccessPage1.tBoxLastIP2.Text.Length == 0 || remoteAccessPage1.tBoxLastIP3.Text.Length == 0 || remoteAccessPage1.tBoxLastIP4.Text.Length == 0)
            {
                return false;
            }

            if (!IsValidInterfaceFormat(remoteAccessPage1.tBoxInterface))
            {
                return false;
            }

            return true;
        }

        private bool IsValidSiteToSite2()
        {
            int.TryParse(siteToSitePage2.tBoxLifeTime.Text, out int numericLifeTime);
            if (numericLifeTime < 120 || siteToSitePage2.tBoxInterface.Text.Length < 5 || siteToSitePage2.tBoxInterfaceDestination.Text.Length < 5 || siteToSitePage2.tBoxLifeTime.Text.Length == 0 || siteToSitePage2.tBoxSourceIP1.Text.Length == 0 || siteToSitePage2.tBoxSourceIP2.Text.Length == 0 || siteToSitePage2.tBoxSourceIP3.Text.Length == 0 || siteToSitePage2.tBoxSourceIP4.Text.Length == 0 || siteToSitePage2.tBoxSourceMask1.Text.Length == 0 || siteToSitePage2.tBoxSourceMask2.Text.Length == 0 || siteToSitePage2.tBoxSourceMask3.Text.Length == 0 || siteToSitePage2.tBoxSourceMask4.Text.Length == 0 || siteToSitePage2.tBoxDestinationIP1.Text.Length == 0 || siteToSitePage2.tBoxDestinationIP2.Text.Length == 0 || siteToSitePage2.tBoxDestinationIP3.Text.Length == 0 || siteToSitePage2.tBoxDestinationIP4.Text.Length == 0 || siteToSitePage2.tBoxDestinationMask1.Text.Length == 0 || siteToSitePage2.tBoxDestinationMask2.Text.Length == 0 || siteToSitePage2.tBoxDestinationMask3.Text.Length == 0 || siteToSitePage2.tBoxDestinationMask4.Text.Length == 0)
            {
                return false;
            }

            if (!IsValidInterfaceFormat(siteToSitePage2.tBoxInterface))
            {
                return false;
            }
            if (!IsValidInterfaceFormat(siteToSitePage2.tBoxInterfaceDestination))
            {
                return false;
            }

            return true;
        }

        private bool IsValidRemoteAccess2()
        {
            if(remoteAccessPage2.users.Count == 0)
            {
                return false;
            }
            return true;
        }

        private bool IsValidInterfaceFormat(TextBox textBox)
        {
            string[] parts = Regex.Split(textBox.Text, @"/");

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "")
                {
                    return false;
                }
            }

            if (parts.Length == 3)
            {
                int.TryParse(parts[0], out int interface1);
                int.TryParse(parts[1], out int interface2);
                int.TryParse(parts[2], out int interface3);
                if (interface1 > 9 || interface2 > 24 || interface3 > 24)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
