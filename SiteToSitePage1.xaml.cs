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
        List<string> Commands = new();
        public string[] EncryptionAlgorithms { get; set; } = { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
        public string[] AuthenticationAlgorithms { get; set; } = { "SHA", "MD5" };
        public string[] DiffieHellmanGroup { get; set; } = { "DH 1", "DH 2", "DH 5" };
        ValidationCheck validationCheck = new ValidationCheck();

        public SiteToSitePage1()
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

        private void tBoxPriority_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxPriority_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxPriority);
            int.TryParse(tBoxPriority.Text, out int numeric);
            if (numeric > 10000)
            {
                validationCheck.RemoveLastChar(tBoxPriority);
            }
        }      

        private void tBoxLifeTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxLifeTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxLifeTime);
            int.TryParse(tBoxLifeTime.Text, out int numeric);
            if(numeric > 86400)
            {
                validationCheck.RemoveLastChar(tBoxLifeTime);
            }
        }

        private void tBoxPreSharedKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxPreSharedKey);
        }

        private void tBoxSourcePeerIP1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourcePeerIP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourcePeerIP1);
        }

        private void tBoxSourcePeerIP2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourcePeerIP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourcePeerIP2);
        }

        private void tBoxSourcePeerIP3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourcePeerIP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourcePeerIP3);
        }

        private void tBoxSourcePeerIP4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourcePeerIP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourcePeerIP4);
        }

        private void tBoxDestinationPeerIP1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationPeerIP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationPeerIP1);
        }

        private void tBoxDestinationPeerIP2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationPeerIP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationPeerIP2);
        }

        private void tBoxDestinationPeerIP3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationPeerIP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationPeerIP3);
        }

        private void tBoxDestinationPeerIP4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationPeerIP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationPeerIP4);
        }
        
        private void ValidateIp(TextBox textBox)
        {
            validationCheck.RemoveSpaces(textBox);
            int.TryParse(textBox.Text, out int numeric);
            if (numeric > 255)
            {
                validationCheck.RemoveLastChar(textBox);
            }
        }

        private void tBoxPriority_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxLifeTime_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourcePeerIP1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourcePeerIP2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourcePeerIP3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourcePeerIP4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationPeerIP1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationPeerIP2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationPeerIP3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationPeerIP4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}
