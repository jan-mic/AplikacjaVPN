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
using static VpnApplication.RemoteAccessPage2;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for SiteToSitePage1.xaml
    /// </summary>
    public partial class SiteToSitePage1 : Page
    {
        //public List<string> encryptionAlgorithms =new List<string>();
        public string[] EncryptionAlgorithms { get; set; }
        public string[] AuthenticationAlgorithms { get; set; }
        public List<string> asd = new List<string>();

        public SiteToSitePage1()
        {
            InitializeComponent();

            DataContext = this;

            EncryptionAlgorithms = new string[] { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
            AuthenticationAlgorithms = new string[] { "SHA", "MD5" };
            asd = EncryptionAlgorithms.ToList();

        }

        public string generateEncryptionAlgorithm()
        {
            EncryptionAlgorithms = new string[] { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
            asd = EncryptionAlgorithms.ToList();
            /*const string dh1 = "3DES";
            const string dh2 = "DH 2";
            const string dh5 = EncryptionAlgorithms[0];*/

            

            /*switch (cbAuthenticationAlgorithm.Text)
            {
                case asd.ElementAt(0):
                    return "1";
                case EncryptionAlgorithms[0]:
                    return "2";
                case dh5:
                    return "5";
                default:
                    return "";
            }*/

            foreach (string algorithm in asd)
            {
                if(algorithm == cbEncryptionAlgorithm.Text)
                {
                    return "command";
                }
            }
            return "";
        }

        public string generateDiffieHellmanGroup()
        {
            const string dh1 = "DH 1";
            const string dh2 = "DH 2";
            const string dh5 = "DH 5";

            switch (cbDiffieHellman.Text)
            {
                case dh1:
                    return "1";
                case dh2:
                    return "2";
                case dh5:
                    return "5";
                default:
                    return "";
            }
        }
    }
}
