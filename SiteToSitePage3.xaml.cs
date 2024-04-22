using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
        public int selectedRouter;
        bool errorOccurred = false;
        string errorMessage = "";

        public SiteToSitePage3()
        {
            InitializeComponent();
        }      

        public void generateScript()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            SiteToSitePage2 siteToSite2 = mainWindow.siteToSitePage2;
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
        }

        public void generateScript2()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            SiteToSitePage2 siteToSite2 = mainWindow.siteToSitePage2;
            tBoxScript2.Text = "";

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
            addCommand2("interface " + siteToSite2.generateInterfaceDestination());
            addCommand2("crypto map " + startPage.tBoxRuleName.Text);
            addCommand2("end");
        }

        private void addCommand2(string text)
        {
            tBoxScript2.Text += text;
            tBoxScript2.Text += "\n";
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
            //string a=number1.ToString();
            return number1 + "." + number2 + "." + number3 + "." + number4;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tBoxScript.Text);
        }

        private void btnCopy2_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tBoxScript2.Text);
        }
        private void btnUploadConfiguration_Click(object sender, RoutedEventArgs e)
        {
            selectedRouter = 1;
            SerialPortSelection serialPortSelection = new SerialPortSelection();
            serialPortSelection.selectedVpnType = "Site to site";
            serialPortSelection.ShowDialog();
        }

        private void btnUploadConfiguration2_Click(object sender, RoutedEventArgs e)
        {
            selectedRouter = 2;
            SerialPortSelection serialPortSelection = new SerialPortSelection();
            serialPortSelection.selectedVpnType = "Site to site";
            serialPortSelection.ShowDialog();
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

        public void uploadToRouter1(string portName)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            SiteToSitePage2 siteToSite2 = mainWindow.siteToSitePage2;
            SerialPortError serialPortError = new SerialPortError();
            SerialPort serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = 3000;
            serialPort.WriteTimeout = 3000;
            serialPort.Handshake = Handshake.None;
            serialPort.NewLine = "\n";
            serialPort.Open();

            if (serialPort.IsOpen)
            {
                bool isConnected = true;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                string entryValue = "";
                while (entryValue.Length == 0 || entryValue[entryValue.Length - 1] != '>')
                {
                    try
                    {
                        serialPort.Write("\n");
                        Thread.Sleep(1);
                        entryValue = serialPort.ReadExisting();
                        Thread.Sleep(1);
                    }
                    catch { }

                    watch.Stop();
                    if (watch.ElapsedMilliseconds > 2000)
                    {
                        isConnected = false;
                        break;
                    }
                    watch.Start();
                }

                if (isConnected)
                {
                    uploadCommand(serialPort, "enable");
                    uploadCommand(serialPort, "configure terminal");
                    uploadCommand(serialPort, "crypto isakmp enable");
                    uploadCommand(serialPort, "crypto isakmp policy " + siteToSite1.tBoxPriority.Text);
                    uploadCommand(serialPort, "authentication pre-share");
                    uploadCommand(serialPort, "encryption " + siteToSite1.generateEncryptionAlgorithm());
                    uploadCommand(serialPort, "group " + siteToSite1.generateDiffieHellmanGroup());
                    uploadCommand(serialPort, "hash " + siteToSite1.generateAuthenticationAlgorithm());
                    uploadCommand(serialPort, "lifetime " + siteToSite1.tBoxLifeTime.Text);
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "crypto isakmp key " + siteToSite1.tBoxPreSharedKey.Text + " address " + generateIP(siteToSite1.tBoxDestinationPeerIP1, siteToSite1.tBoxDestinationPeerIP2, siteToSite1.tBoxDestinationPeerIP3, siteToSite1.tBoxDestinationPeerIP4));
                    uploadCommand(serialPort, "crypto ipsec transform-set " + startPage.tBoxRuleName.Text + " " + siteToSite2.generateTransformSet());
                    uploadCommand(serialPort, "crypto ipsec security-association lifetime seconds " + siteToSite2.tBoxLifeTime.Text);
                    uploadCommand(serialPort, "ip access-list extended " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "permit ip " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4) + " " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4));
                    uploadCommand(serialPort, "permit icmp " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4) + " " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4));
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text + " " + siteToSite1.tBoxPriority.Text + " ipsec-isakmp");
                    uploadCommand(serialPort, "match address " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "set peer " + generateIP(siteToSite1.tBoxDestinationPeerIP1, siteToSite1.tBoxDestinationPeerIP2, siteToSite1.tBoxDestinationPeerIP3, siteToSite1.tBoxDestinationPeerIP4));
                    uploadCommand(serialPort, "set pfs " + siteToSite2.generateDiffieHellmanGroup());
                    uploadCommand(serialPort, "set transform-set " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "interface " + siteToSite2.generateInterface());
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "end");

                    if (errorOccurred)
                    {
                        serialPortError.tbError.Text = "Error occurred during configuration:";
                        serialPortError.tBoxError.Text = errorMessage;
                        serialPortError.tBoxError.Visibility = System.Windows.Visibility.Visible;
                        serialPortError.ShowDialog();
                    }
                    else
                    {
                        serialPortError.tbError.Text = "Configuration uploaded successfully!";
                        serialPortError.tBoxError.Visibility = System.Windows.Visibility.Hidden;
                        serialPortError.ShowDialog();
                    }
                }
                else
                {
                    serialPortError.tbError.Text = "Failed to establish connection with serial port.";
                    serialPortError.tBoxError.Visibility = System.Windows.Visibility.Hidden;
                    serialPortError.ShowDialog();
                }
            }
            serialPort.Close();
        }

        public void uploadToRouter2(string portName)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            SiteToSitePage1 siteToSite1 = mainWindow.siteToSitePage1;
            SiteToSitePage2 siteToSite2 = mainWindow.siteToSitePage2;
            SerialPortError serialPortError = new SerialPortError();
            SerialPort serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = 3000;
            serialPort.WriteTimeout = 3000;
            serialPort.Handshake = Handshake.None;
            serialPort.NewLine = "\n";
            serialPort.Open();

            if (serialPort.IsOpen)
            {
                bool isConnected = true;
                var watch = System.Diagnostics.Stopwatch.StartNew();
                string entryValue = "";
                while (entryValue.Length == 0 || entryValue[entryValue.Length - 1] != '>')
                {
                    try
                    {
                        serialPort.Write("\n");
                        Thread.Sleep(1);
                        entryValue = serialPort.ReadExisting();
                        Thread.Sleep(1);
                    }
                    catch { }

                    watch.Stop();
                    if (watch.ElapsedMilliseconds > 2000)
                    {
                        isConnected = false;
                        break;
                    }
                    watch.Start();
                }

                if (isConnected)
                {
                    uploadCommand(serialPort, "enable");
                    uploadCommand(serialPort, "configure terminal");
                    uploadCommand(serialPort, "crypto isakmp enable");
                    uploadCommand(serialPort, "crypto isakmp policy " + siteToSite1.tBoxPriority.Text);
                    uploadCommand(serialPort, "authentication pre-share");
                    uploadCommand(serialPort, "encryption " + siteToSite1.generateEncryptionAlgorithm());
                    uploadCommand(serialPort, "group " + siteToSite1.generateDiffieHellmanGroup());
                    uploadCommand(serialPort, "hash " + siteToSite1.generateAuthenticationAlgorithm());
                    uploadCommand(serialPort, "lifetime " + siteToSite1.tBoxLifeTime.Text);
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "crypto isakmp key " + siteToSite1.tBoxPreSharedKey.Text + " address " + generateIP(siteToSite1.tBoxSourcePeerIP1, siteToSite1.tBoxSourcePeerIP2, siteToSite1.tBoxSourcePeerIP3, siteToSite1.tBoxSourcePeerIP4));
                    uploadCommand(serialPort, "crypto ipsec transform-set " + startPage.tBoxRuleName.Text + " " + siteToSite2.generateTransformSet());
                    uploadCommand(serialPort, "crypto ipsec security-association lifetime seconds " + siteToSite2.tBoxLifeTime.Text);
                    uploadCommand(serialPort, "ip access-list extended " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "permit ip " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4) + " " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4));
                    uploadCommand(serialPort, "permit icmp " + generateIP(siteToSite2.tBoxDestinationIP1, siteToSite2.tBoxDestinationIP2, siteToSite2.tBoxDestinationIP3, siteToSite2.tBoxDestinationIP4) + " " + generateReversedMask(siteToSite2.tBoxDestinationMask1, siteToSite2.tBoxDestinationMask2, siteToSite2.tBoxDestinationMask3, siteToSite2.tBoxDestinationMask4) + " " + generateIP(siteToSite2.tBoxSourceIP1, siteToSite2.tBoxSourceIP2, siteToSite2.tBoxSourceIP3, siteToSite2.tBoxSourceIP4) + " " + generateReversedMask(siteToSite2.tBoxSourceMask1, siteToSite2.tBoxSourceMask2, siteToSite2.tBoxSourceMask3, siteToSite2.tBoxSourceMask4));
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text + " " + siteToSite1.tBoxPriority.Text + " ipsec-isakmp");
                    uploadCommand(serialPort, "match address " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "set peer " + generateIP(siteToSite1.tBoxSourcePeerIP1, siteToSite1.tBoxSourcePeerIP2, siteToSite1.tBoxSourcePeerIP3, siteToSite1.tBoxSourcePeerIP4));
                    uploadCommand(serialPort, "set pfs " + siteToSite2.generateDiffieHellmanGroup());
                    uploadCommand(serialPort, "set transform-set " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "interface " + siteToSite2.generateInterfaceDestination());
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "end");

                    if (errorOccurred)
                    {
                        serialPortError.tbError.Text = "Error occurred during configuration:";
                        serialPortError.tBoxError.Text = errorMessage;
                        serialPortError.tBoxError.Visibility = System.Windows.Visibility.Visible;
                        serialPortError.ShowDialog();
                    }
                    else
                    {
                        serialPortError.tbError.Text = "Configuration uploaded successfully!";
                        serialPortError.tBoxError.Visibility = System.Windows.Visibility.Hidden;
                        serialPortError.ShowDialog();
                    }
                }
                else
                {
                    serialPortError.tbError.Text = "Failed to establish connection with serial port.";
                    serialPortError.tBoxError.Visibility = System.Windows.Visibility.Hidden;
                    serialPortError.ShowDialog();
                }
            }
            serialPort.Close();
        }

        void uploadCommand(SerialPort serialPort, string command)
        {
            if (!errorOccurred)
            {
                try
                {
                    serialPort.Write(command + "\n");
                    Thread.Sleep(1);
                }
                catch { }
                checkResponse(serialPort);
            }
        }

        void checkResponse(SerialPort serialPort)
        {
            string response = "";
            try
            {
                response = serialPort.ReadExisting();
                Thread.Sleep(1);
            }
            catch { }
            if (response.Length > 0 && response[response.Length - 1] != '>' && response[response.Length - 1] != '#' && response[1] == ' ')
            {
                errorOccurred = true;
                errorMessage = response;
            }
        }
    }
}
