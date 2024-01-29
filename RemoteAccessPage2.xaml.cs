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

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for RemoteAccessPage2.xaml
    /// </summary>
    public partial class RemoteAccessPage2 : Page
    {
        List<User> users = new List<User>();

        public RemoteAccessPage2()
        {
            InitializeComponent();
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

            }
        }
    }
}
