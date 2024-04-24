using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

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
        ValidationCheck validationCheck = new ValidationCheck();

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

        private void tBoxPriority_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxPriority_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxPriority);
            if (tBoxPriority.Text.Length > 0)
            {
                int.TryParse(tBoxPriority.Text, out int numeric);
                if (numeric > 10000)
                {
                    validationCheck.RemoveLastChar(tBoxPriority);
                }
            }
            validationCheck.CheckFirstDigit(tBoxPriority);
        }

        private void tBoxGroupName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsLetterOrDigit(e.Text);
        }

        private void tBoxGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxGroupName);
        }

        private void tBoxGroupKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxGroupKey);
        }

        private void tBoxFirstIP1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxFirstIP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxFirstIP1);
        }

        private void tBoxFirstIP2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxFirstIP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxFirstIP2);
        }

        private void tBoxFirstIP3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxFirstIP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxFirstIP3);
        }

        private void tBoxFirstIP4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxFirstIP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxFirstIP4);
        }

        private void tBoxLastIP1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxLastIP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxLastIP1);
        }

        private void tBoxLastIP2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxLastIP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxLastIP2);
        }

        private void tBoxLastIP3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxLastIP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxLastIP3);
        }

        private void tBoxLastIP4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxLastIP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxLastIP4);
        }

        private void ValidateIp(TextBox textBox)
        {
            validationCheck.RemoveSpaces(textBox);
            if (textBox.Text.Length > 0)
            {
                int.TryParse(textBox.Text, out int numeric);
                if (numeric > 255)
                {
                    validationCheck.RemoveLastChar(textBox);
                }
                validationCheck.CheckIP(textBox);
            }
        }

        private void tBoxInterface_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[/]");
            e.Handled = validationCheck.IsDigit(e.Text) && !regex.IsMatch(e.Text);
        }

        private void tBoxInterface_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxInterface);
            validationCheck.CheckFirstDigitForInterface(tBoxInterface);
        }

        private void tBoxInterface_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxPriority_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxGroupName_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxFirstIP1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxFirstIP2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxFirstIP3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxFirstIP4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxLastIP1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxLastIP2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxLastIP3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxLastIP4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}
