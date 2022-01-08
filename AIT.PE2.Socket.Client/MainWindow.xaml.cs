using AIT.PE2.Socket.Core.Helpers;
using AIT.PE2.Socket.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace AIT.PE2.Socket.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Global variables
        DirectoryClientService directoryService;
        System.Net.Sockets.Socket serverSocket;
        IPEndPoint serverEndpoint;
        #endregion
        private void btnStartServer_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == null)
            {
                txtUserName.Focus();
                return;
            }
            SaveConfig();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            directoryService = new DirectoryClientService();
            grpFts.Visibility = Visibility.Hidden;
            StartupConfig();
        }

        private void btnNavigate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbFolders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartupConfig()
        {
            cmbIPs.ItemsSource = IPv4Helper.GetActiveIP4s();
            for (int port = 49200; port <= 49500; port++)
            {
                cmbPorts.Items.Add(port);
            }
            AppConfig.GetConfig(out string savedIP, out int savedPort, out string savedPath);
            try
            {
                cmbIPs.SelectedItem = savedIP;
            }
            catch
            {
                cmbIPs.SelectedItem = "127.0.0.1";
            }
            try
            {
                cmbPorts.SelectedItem = savedPort;
            }
            catch
            {
                cmbPorts.SelectedItem = 49200;
            }
            try
            {
                txtUserName.Text = savedPath;
            }
            catch
            {
                txtUserName.Text = "C:\\howest";
            }
        }
        private void SaveConfig()
        {
            if (cmbIPs.SelectedItem == null) return;
            if (cmbPorts.SelectedItem == null) return;
            if (txtUserName.Text == null) return;

            string ip = cmbIPs.SelectedItem.ToString();
            int port = int.Parse(cmbPorts.SelectedItem.ToString());
            string path = txtUserName.Text;
            AppConfig.WriteConfig(ip, port, path);
        }
    }
}
