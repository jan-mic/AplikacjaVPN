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
    /// Interaction logic for SiteToSitePage3.xaml
    /// </summary>
    public partial class SiteToSitePage3 : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
 
        public SiteToSitePage3()
        {
            InitializeComponent();

            //mainWindow = (MainWindow)Application.Current.MainWindow;

            //generateScript();

        }

        public void generateScript()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            tBoxScript.Text = "";

            addCommand("enable");
            addCommand("configure terminal");
            addCommand("crypto isakmp enable");
            addCommand("crypto isakmp policy " + siteToSite1.tBoxPriority.Text);
            addCommand("authentication pre-share");
            addCommand("encryption " + siteToSite1.generateEncryptionAlgorithm());
            addCommand("group " + siteToSite1.generateDiffieHellmanGroup());
            addCommand("hash " + siteToSite1.generateAuthenticationAlgorithm());
            addCommand("lifetime " + siteToSite1.tBoxLifeTime.Text);
            addCommand("exit");
            addCommand("crypto isakmp key " + siteToSite1.tBoxPreSharedKey.Text + " address " + generateIP(siteToSite1.tBoxPeerIP1, siteToSite1.tBoxPeerIP2, siteToSite1.tBoxPeerIP3, siteToSite1.tBoxPeerIP4));

        }

        private void addCommand(string text)
        {
            tBoxScript.Text += text;
            tBoxScript.Text += "\n";
        }

        private string generateIP(TextBox ip1, TextBox ip2, TextBox ip3, TextBox ip4)
        {
            return ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
