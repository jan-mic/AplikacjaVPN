using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static VpnApplication.RemoteAccessPage2;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for SiteToSitePage1.xaml
    /// </summary>
    public partial class SiteToSitePage1 : Page
    {
        //public List<string> encryptionAlgorithms =new List<string>();
        List<string> Commands = new();
        public string[] EncryptionAlgorithms { get; set; } = { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
        public string[] AuthenticationAlgorithms { get; set; } = { "SHA", "MD5" };
        public string[] DiffieHellmanGroup { get; set; } = { "DH 1", "DH 2", "DH 5" };
        //public List<string> asd = new List<string>();

        public SiteToSitePage1()
        {
            InitializeComponent();

            DataContext = this;

            //EncryptionAlgorithms = new string[] { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
            //AuthenticationAlgorithms = new string[] { "SHA", "MD5" };
            //asd = EncryptionAlgorithms.ToList();

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
    }
}
