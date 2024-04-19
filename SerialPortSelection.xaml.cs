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
        //public string[] SerialPortNames;
        public SerialPortSelection()
        {
            InitializeComponent();
            DataContext = this;
            //SerialPortNames = SerialPort.GetPortNames();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
