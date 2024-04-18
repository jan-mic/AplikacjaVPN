using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
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
using static VpnApplication.RemoteAccessPage2;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for SiteToSitePage3.xaml
    /// </summary>
    public partial class SiteToSitePage3 : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        //string scriptRouter1="";
        //string scriptRouter2="";
 
        public SiteToSitePage3()
        {
            InitializeComponent();
        }

        public void omg()
        {
            SerialPort serialPort = new SerialPort("COM1", 1920, Parity.None, 8, StopBits.One);
            serialPort.Handshake = Handshake.None;
            //serialPort.DataReceived += new SerialDataReceivedEventHandler(spDataReceived);
        }
        

        public void generateScript()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            SiteToSitePage2 siteToSite2 = mainWindow.siteToSitePage2;
            tBoxScript.Text = "";
            //scriptRouter1 = "";

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
            addCommand("crypto isakmp key " + siteToSite1.tBoxPreSharedKey.Text + " address " + generateIP(siteToSite1.tBoxDestinationPeerIP1, siteToSite1.tBoxDestinationPeerIP2, siteToSite1.tBoxDestinationPeerIP3, siteToSite1.tBoxDestinationPeerIP4));
            addCommand("crypto ipsec transform-set " + startPage.tBoxRuleName.Text + " " + siteToSite2.generateTransformSet());
            addCommand("crypto ipsec security-association lifetime seconds " + siteToSite2.tBoxLifeTime.Text);
            addCommand("ip access-list extended " + startPage.tBoxRuleName.Text);
            addCommand("permit ip " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4) + " " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4));
            addCommand("permit icmp " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4) + " " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4));
            addCommand("exit");
            addCommand("crypto map " + startPage.tBoxRuleName.Text + " " + siteToSite1.tBoxPriority.Text + " ipsec-isakmp");
            addCommand("match address " + startPage.tBoxRuleName.Text);
            addCommand("set peer " + generateIP(siteToSite1.tBoxDestinationPeerIP1, siteToSite1.tBoxDestinationPeerIP2, siteToSite1.tBoxDestinationPeerIP3, siteToSite1.tBoxDestinationPeerIP4));
            addCommand("set pfs " + siteToSite2.generateDiffieHellmanGroup());
            addCommand("set transform-set " + startPage.tBoxRuleName.Text);
            addCommand("exit");
            addCommand("interface " + siteToSite2.generateInterface());
            addCommand("crypto map " + startPage.tBoxRuleName.Text);
            addCommand("end");


        }

        private void addCommand(string text)
        {
            tBoxScript.Text += text;
            tBoxScript.Text += "\n";

            //scriptRouter1 += text;
            //scriptRouter1 += "\n";
        }

        public void generateScript2()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            SiteToSitePage2 siteToSite2 = mainWindow.siteToSitePage2;
            tBoxScript2.Text = "";
            //scriptRouter2 = "";

            addCommand2("enable");
            addCommand2("configure terminal");
            addCommand2("crypto isakmp enable");
            addCommand2("crypto isakmp policy " + siteToSite1.tBoxPriority.Text);
            addCommand2("authentication pre-share");
            addCommand2("encryption " + siteToSite1.generateEncryptionAlgorithm());
            addCommand2("group " + siteToSite1.generateDiffieHellmanGroup());
            addCommand2("hash " + siteToSite1.generateAuthenticationAlgorithm());
            addCommand2("lifetime " + siteToSite1.tBoxLifeTime.Text);
            addCommand2("exit");
            addCommand2("crypto isakmp key " + siteToSite1.tBoxPreSharedKey.Text + " address " + generateIP(siteToSite1.tBoxSourcePeerIP1, siteToSite1.tBoxSourcePeerIP2, siteToSite1.tBoxSourcePeerIP3, siteToSite1.tBoxSourcePeerIP4));
            addCommand2("crypto ipsec transform-set " + startPage.tBoxRuleName.Text + " " + siteToSite2.generateTransformSet());
            addCommand2("crypto ipsec security-association lifetime seconds " + siteToSite2.tBoxLifeTime.Text);
            addCommand2("ip access-list extended " + startPage.tBoxRuleName.Text);
            addCommand2("permit ip " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4) + " " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4));
            addCommand2("permit icmp " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4) + " " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4));
            addCommand2("exit");
            addCommand2("crypto map " + startPage.tBoxRuleName.Text + " " + siteToSite1.tBoxPriority.Text + " ipsec-isakmp");
            addCommand2("match address " + startPage.tBoxRuleName.Text);
            addCommand2("set peer " + generateIP(siteToSite1.tBoxSourcePeerIP1, siteToSite1.tBoxSourcePeerIP2, siteToSite1.tBoxSourcePeerIP3, siteToSite1.tBoxSourcePeerIP4));
            addCommand2("set pfs " + siteToSite2.generateDiffieHellmanGroup());
            addCommand2("set transform-set " + startPage.tBoxRuleName.Text);
            addCommand2("exit");
            addCommand2("interface " + siteToSite2.generateInterface());
            addCommand2("crypto map " + startPage.tBoxRuleName.Text);
            addCommand("end");
        }

        private void addCommand2(string text)
        {
            tBoxScript2.Text += text;
            tBoxScript2.Text += "\n";

            //scriptRouter2 += text;
            //scriptRouter2 += "\n";
        }

        private string generateIP(TextBox ip1, TextBox ip2, TextBox ip3, TextBox ip4)
        {
            return ip1.Text + "." + ip2.Text + "." + ip3.Text + "." + ip4.Text;
        }

        private string generateReversedMask(TextBox mask1, TextBox mask2, TextBox mask3, TextBox mask4)
        {
            int number1=0;
            int number2=0;
            int number3 = 0;
            int number4 = 0;
            if (mask1.Text.Length > 0)
            {
                number1 = 255 - int.Parse(mask1.Text);
                number2 = 255 - int.Parse(mask2.Text);
                number3 = 255 - int.Parse(mask3.Text);
                number4 = 255 - int.Parse(mask4.Text);
            }
            string a=number1.ToString();
            

            return number1 + "." + number2 + "." + number3 + "." + number4;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tBoxScript.Text);
        }

        private void btnUploadConfiguration_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCopy2_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tBoxScript2.Text);
        }

        private void btnUploadConfiguration2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "ConfigurationScript1";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(dialog.FileName, tBoxScript.Text);
            }           
        }

        private void btnSave2_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "ConfigurationScript2";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(dialog.FileName, tBoxScript2.Text);
            }
        }
    }
}
