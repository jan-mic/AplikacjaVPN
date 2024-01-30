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
    /// Interaction logic for SiteToSitePage2.xaml
    /// </summary>
    public partial class SiteToSitePage2 : Page
    {
        List<string> Commands = new();
        public string[] ProtocolTypes { get; set; } = { "ESP", "AH" };
        public string[] EncryptionAlgorithms { get; set; } = { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
        public string[] AuthenticationAlgorithms { get; set; } = { "SHA", "MD5" };
        public string[] DiffieHellmanGroup { get; set; } = { "DH 1", "DH 2", "DH 5" };
        public string[] InterfaceTypes { get; set; } = { "Fast Ethernet", "Gigabit Ethernet", "Serial" };

        public SiteToSitePage2()
        {
            InitializeComponent();

            DataContext = this;
        }

        public string generateTransformSet()
        {
            string command = "";

            if (cbProtocol.Text == "ESP")
            {
                command += "esp-";
                command += generateEncryptionAlgorithm();
                command += " esp-";
                command += generateAuthenticationAlgorithm();
                command += "-hmac";
            }
            else
            {
                command += "ah-";
                command += generateAuthenticationAlgorithm();
                command += "-hmac ";
                command += "esp-";
                command += generateEncryptionAlgorithm();
                command += " esp-";
                command += generateAuthenticationAlgorithm();
                command += "-hmac";
            }

            return command;
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

        public string generateAuthenticationAlgorithm()
        {
            Commands = new List<string>
            {
                "sha",
                "md5"
            };

            return Commands[cbAuthenticationAlgorithm.SelectedIndex];
        }
    }
}
