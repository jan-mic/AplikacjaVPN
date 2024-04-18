using System;
using System.Collections.Generic;
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
    /// Interaction logic for RemoteAccessPage2.xaml
    /// </summary>
    public partial class RemoteAccessPage2 : Page
    {
        public List<User> users = new List<User>();
        List<string> Commands = new();
        public string[] ProtocolTypes { get; set; } = { "ESP", "AH" };
        public string[] EncryptionAlgorithms { get; set; } = { "DES", "3DES", "AES-128", "AES-192", "AES-256" };
        public string[] AuthenticationAlgorithms { get; set; } = { "SHA", "MD5" };

        public RemoteAccessPage2()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class User
        {
            public int userID { get; set; }
            public string userName { get; set; }
            public string userPassword { get; set; }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.userID = users.Count() + 1;
            user.userName=tBoxUsername.Text;
            user.userPassword=tBoxPassword.Text;
            users.Add(user);

            dgUsers.Items.Add(user);

            tBoxUsername.Clear();
            tBoxPassword.Clear();

        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if(users.Count() > 0)
            {
                dgUsers.Items.Remove(users.Last());
                users.Remove(users.Last());
                //dgUsers.Items.RemoveAt(index);
            }
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
