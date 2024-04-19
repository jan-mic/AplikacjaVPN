using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using System.Windows.Shapes;

namespace VpnApplication
{
    /// <summary>
    /// Interaction logic for SerialPortSelection.xaml
    /// </summary>
    public partial class SerialPortSelection : Window
    {
        public string[] SerialPortNames { get; set; } = SerialPort.GetPortNames().Where(p => p.StartsWith("COM")).ToArray();
        public string selectedVpnType = "";
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

        public SerialPortSelection()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            if (selectedVpnType == "Site to site")
            {
                SiteToSitePage3 siteToSitePage3 = mainWindow.siteToSitePage3;

                if(siteToSitePage3.selectedRouter == 1)
                {
                    siteToSitePage3.uploadToRouter1(cbSelectSerialPort.Text);
                }
                else
                {
                    siteToSitePage3.uploadToRouter2(cbSelectSerialPort.Text);
                }
            }
            
            if(selectedVpnType == "Remote Access")
            {                
                RemoteAccessPage3 remoteAccessPage3 = mainWindow.remoteAccessPage3;
                remoteAccessPage3.uploadToRouter(cbSelectSerialPort.Text);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
