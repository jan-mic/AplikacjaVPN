using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for RemoteAccessPage3.xaml
    /// </summary>
    public partial class RemoteAccessPage3 : Page
    {
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

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
            //scriptRouter1 = "";

            addCommand("enable");
            addCommand("configure terminal");
            addCommand("aaa new-model");
            addCommand("aaa authentication login " + startPage.tBoxRuleName.Text + "local");
            addCommand("aaa authorization network " + startPage.tBoxRuleName.Text + "local");

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
            addCommand("crypto isakmp client configuration group " + remoteAccessPage1.tbGroupName.Text);
            addCommand("key " + remoteAccessPage1.tbGroupKey.Text);
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

            //scriptRouter1 += text;
            //scriptRouter1 += "\n";
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

        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(tBoxScript.Text);
        }
    }
}
