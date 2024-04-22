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
        public string[] InterfaceTypesDestination { get; set; } = { "Fast Ethernet", "Gigabit Ethernet", "Serial" };
        ValidationCheck validationCheck = new ValidationCheck();

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

        public string generateDiffieHellmanGroup()
        {
            Commands = new List<string>
            {
                "group1",
                "group2",
                "group5"
            };

            return Commands[cbDiffieHellman.SelectedIndex];
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

        public string generateInterfaceDestination()
        {
            Commands = new List<string>
            {
                "fastEthernet",
                "gigabitEthernet",
                "serial"
            };

            return Commands[cbInterfaceDestination.SelectedIndex] + tBoxInterfaceDestination.Text;
        }

        private void tBoxLifeTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxLifeTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxLifeTime);
            int.TryParse(tBoxLifeTime.Text, out int numeric);
            if (numeric > 86400)
            {
                validationCheck.RemoveLastChar(tBoxLifeTime);
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
        }

        private void tBoxSourceIP1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceIP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceIP1);
        }

        private void tBoxSourceIP2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceIP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceIP2);
        }

        private void tBoxSourceIP3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceIP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceIP3);
        }

        private void tBoxSourceIP4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceIP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceIP4);
        }

        private void tBoxSourceMask1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceMask1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceMask1);
        }

        private void tBoxSourceMask2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceMask2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceMask2);
        }

        private void tBoxSourceMask3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceMask3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceMask3);
        }

        private void tBoxSourceMask4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxSourceMask4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxSourceMask4);
        }

        private void tBoxDestinationIP1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationIP1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationIP1);
        }

        private void tBoxDestinationIP2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationIP2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationIP2);
        }

        private void tBoxDestinationIP3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationIP3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationIP3);
        }

        private void tBoxDestinationIP4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationIP4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationIP4);
        }

        private void tBoxDestinationMask1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationMask1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationMask1);
        }

        private void tBoxDestinationMask2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationMask2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationMask2);
        }

        private void tBoxDestinationMask3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationMask3_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationMask3);
        }

        private void tBoxDestinationMask4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = validationCheck.IsDigit(e.Text);
        }

        private void tBoxDestinationMask4_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateIp(tBoxDestinationMask4);
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
        private void tBoxInterface_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxLifeTime_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceIP1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceIP2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceIP3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceIP4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceMask1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceMask2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceMask3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxSourceMask4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationIP1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationIP2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationIP3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationIP4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationMask1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationMask2_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationMask3_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxDestinationMask4_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private void tBoxInterfaceDestination_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[/]");
            e.Handled = validationCheck.IsDigit(e.Text) && !regex.IsMatch(e.Text);
        }

        private void tBoxInterfaceDestination_TextChanged(object sender, TextChangedEventArgs e)
        {
            validationCheck.RemoveSpaces(tBoxInterfaceDestination);
        }

        private void tBoxInterfaceDestination_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
    }
}
