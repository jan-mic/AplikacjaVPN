using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for RemoteAccessPage3.xaml
    /// </summary>
    public partial class RemoteAccessPage3 : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        //bool routerUpload = false;
        bool errorOccurred = false;
        string errorMessage = "";

        public RemoteAccessPage3()
        {
            InitializeComponent();
        }

        public void generateScript()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            RemoteAccessPage1 remoteAccessPage1 = mainWindow.remoteAccessPage1;
            RemoteAccessPage2 remoteAccessPage2 = mainWindow.remoteAccessPage2;
            tBoxScript.Text = "";

            addCommand("enable");
            addCommand("configure terminal");
            addCommand("aaa new-model");
            addCommand("aaa authentication login " + startPage.tBoxRuleName.Text + " local");
            addCommand("aaa authorization network " + startPage.tBoxRuleName.Text + " local");
            foreach(RemoteAccessPage2.User user in remoteAccessPage2.users)
            {
                addCommand("username " + user.userName + " password " + user.userPassword);
            }
            addCommand("crypto isakmp policy " + remoteAccessPage1.tBoxPriority.Text);
            addCommand("encryption " + remoteAccessPage1.generateEncryptionAlgorithm());
            addCommand("hash " + remoteAccessPage1.generateAuthenticationAlgorithm());
            addCommand("group " + remoteAccessPage1.generateDiffieHellmanGroup());
            addCommand("exit");
            addCommand("ip local pool " + startPage.tBoxRuleName.Text + " " + generateIP(remoteAccessPage1.tBoxFirstIP1, remoteAccessPage1.tBoxFirstIP2, remoteAccessPage1.tBoxFirstIP3, remoteAccessPage1.tBoxFirstIP4) + " " + generateIP(remoteAccessPage1.tBoxLastIP1, remoteAccessPage1.tBoxLastIP2, remoteAccessPage1.tBoxLastIP3, remoteAccessPage1.tBoxLastIP4));
            addCommand("crypto isakmp client configuration group " + remoteAccessPage1.tBoxGroupName.Text);
            addCommand("key " + remoteAccessPage1.tBoxGroupKey.Text);
            addCommand("pool " + startPage.tBoxRuleName.Text);
            addCommand("exit");
            addCommand("crypto ipsec transform-set " + startPage.tBoxRuleName.Text + " " + remoteAccessPage2.generateTransformSet());
            addCommand("crypto dynamic-map " + startPage.tBoxRuleName.Text + "-dynamic" + " " + remoteAccessPage1.tBoxPriority.Text);
            addCommand("set transform-set " + startPage.tBoxRuleName.Text);
            addCommand("reverse-route");
            addCommand("exit");
            addCommand("crypto map " + startPage.tBoxRuleName.Text + " client configuration address respond");
            addCommand("crypto map " + startPage.tBoxRuleName.Text + " client authentication list " + startPage.tBoxRuleName.Text);
            addCommand("crypto map " + startPage.tBoxRuleName.Text + " isakmp authorization list " + startPage.tBoxRuleName.Text);
            addCommand("crypto map " + startPage.tBoxRuleName.Text + " " + remoteAccessPage1.tBoxPriority.Text + " ipsec-isakmp dynamic " + startPage.tBoxRuleName.Text + "-dynamic");
            addCommand("interface " + remoteAccessPage1.generateInterface());
            addCommand("crypto map " + startPage.tBoxRuleName.Text);
            addCommand("end");
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "ConfigurationScript";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(dialog.FileName, tBoxScript.Text);
            }
        }

        private void btnUploadConfiguration_Click(object sender, RoutedEventArgs e)
        {
            SerialPortSelection serialPortSelection = new SerialPortSelection();
            serialPortSelection.selectedVpnType = "Remote Access";
            serialPortSelection.ShowDialog();
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tBoxScript.Text);
        }

        public void uploadToRouter(string portName)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            StartPage startPage = mainWindow.startPage;
            RemoteAccessPage1 remoteAccessPage1 = mainWindow.remoteAccessPage1;
            RemoteAccessPage2 remoteAccessPage2 = mainWindow.remoteAccessPage2;
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

            if(serialPort.IsOpen)
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
                /////
                ///
                isConnected = true;
                //////
                if (isConnected)
                {
                    uploadCommand(serialPort, "enable");
                    uploadCommand(serialPort, "configure terminal");
                    uploadCommand(serialPort, "aaa new-model");
                    uploadCommand(serialPort, "aaa authentication login " + startPage.tBoxRuleName.Text + " local");
                    uploadCommand(serialPort, "aaa authorization network " + startPage.tBoxRuleName.Text + " local");
                    foreach (RemoteAccessPage2.User user in remoteAccessPage2.users)
                    {
                        uploadCommand(serialPort, "username " + user.userName + " password " + user.userPassword);
                    }
                    uploadCommand(serialPort, "crypto isakmp policy " + remoteAccessPage1.tBoxPriority.Text);
                    uploadCommand(serialPort, "encryption " + remoteAccessPage1.generateEncryptionAlgorithm());
                    uploadCommand(serialPort, "hash " + remoteAccessPage1.generateAuthenticationAlgorithm());
                    uploadCommand(serialPort, "group " + remoteAccessPage1.generateDiffieHellmanGroup());
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "ip local pool " + startPage.tBoxRuleName.Text + " " + generateIP(remoteAccessPage1.tBoxFirstIP1, remoteAccessPage1.tBoxFirstIP2, remoteAccessPage1.tBoxFirstIP3, remoteAccessPage1.tBoxFirstIP4) + " " + generateIP(remoteAccessPage1.tBoxLastIP1, remoteAccessPage1.tBoxLastIP2, remoteAccessPage1.tBoxLastIP3, remoteAccessPage1.tBoxLastIP4));
                    uploadCommand(serialPort, "crypto isakmp client configuration group " + remoteAccessPage1.tBoxGroupName.Text);
                    uploadCommand(serialPort, "key " + remoteAccessPage1.tBoxGroupKey.Text);
                    uploadCommand(serialPort, "pool " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "crypto ipsec transform-set " + startPage.tBoxRuleName.Text + " " + remoteAccessPage2.generateTransformSet());
                    uploadCommand(serialPort, "crypto dynamic-map " + startPage.tBoxRuleName.Text + "-dynamic" + " " + remoteAccessPage1.tBoxPriority.Text);
                    uploadCommand(serialPort, "set transform-set " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "reverse-route");
                    uploadCommand(serialPort, "exit");
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text + " client configuration address respond");
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text + " client authentication list " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text + " isakmp authorization list " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text + " " + remoteAccessPage1.tBoxPriority.Text + " ipsec-isakmp dynamic " + startPage.tBoxRuleName.Text + "-dynamic");
                    uploadCommand(serialPort, "interface " + remoteAccessPage1.generateInterface());
                    uploadCommand(serialPort, "crypto map " + startPage.tBoxRuleName.Text);
                    uploadCommand(serialPort, "end");

                    /////
                    ///
                    errorOccurred = false;
                    /////
                    if(errorOccurred)
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

        void uploadCommand (SerialPort serialPort, string command)
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
