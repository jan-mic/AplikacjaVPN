using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for RemoteAccessPage1.xaml
    /// </summary>
    public partial class RemoteAccessPage1 : Page
    {
        List<string> Commands = new();
        public string[] EncryptionAlgorithms { get; set; } = { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
        public string[] AuthenticationAlgorithms { get; set; } = { "SHA", "MD5" };
        public string[] DiffieHellmanGroup { get; set; } = { "DH 1", "DH 2", "DH 5" };
        public string[] InterfaceTypes { get; set; } = { "Fast Ethernet", "Gigabit Ethernet", "Serial" };

        public RemoteAccessPage1()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string generateEncryptionAlgorithm()
        {
            Commands = new List<string>
            {
                "des",
                "3des",
                "aes 128",
                "aes 192",
                "aes 256"
            };

            return Commands[cbEncryptionAlgorithm.SelectedIndex];
        }

        public string generateDiffieHellmanGroup()
        {
            Commands = new List<string>
            {
                "1",
                "2",
                "5"
            };

            return Commands[cbDiffieHellman.SelectedIndex];
        }

        public string generateAuthenticationAlgorithm()
        {
            Commands = new List<string>
            {
                "sha",
                "md5"
            };

            return Commands[cbAuthenticationAlgorithm.SelectedIndex];
        }

        public string generateInterface()
        {
            Commands = new List<string>
            {
                "fastEthernet",
                "gigabitEthernet",
                "serial"
            };

            return Commands[cbInterface.SelectedIndex] + tBoxInterface.Text;
        }

        /*private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //RemoteAccessPage remoteAccessPage = new RemoteAccessPage();
            //this.Content = remoteAccessPage;
            //MainWindow mainWindow = new MainWindow();
            //this.Content=
            //this.Content=mainWindow.Content;
            //Content = MainWindow

        }*/


    }
}
