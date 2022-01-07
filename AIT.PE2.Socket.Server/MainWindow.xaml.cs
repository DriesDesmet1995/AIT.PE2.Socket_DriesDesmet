using AIT.PE2.Socket.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Threading;

namespace AIT.PE2.Socket.Server
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
        DirectoryService directoryService;
        System.Net.Sockets.Socket serverSocket;
        IPEndPoint serverEndpoint;
        bool serverOnline = false;
        #endregion
        private void CmbIPs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SaveConfig();

        }

        private void CmbPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SaveConfig();

        }

        private void txtPath_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //SaveConfig();

        }

        private void BtnStartServer_Click(object sender, RoutedEventArgs e)
        {
            btnStartServer.Visibility = Visibility.Hidden;
            btnStopServer.Visibility = Visibility.Visible;
            cmbIPs.IsEnabled = false;
            cmbPorts.IsEnabled = false;
            txtPath.IsEnabled = false;
            grdUsers.ItemsSource = null;
            //DisplayData();

            //StartTheServer();
            //StartListening();
        }

        private void BtnStopServer_Click(object sender, RoutedEventArgs e)
        {
            btnStartServer.Visibility = Visibility.Visible;
            btnStopServer.Visibility = Visibility.Hidden;
            cmbIPs.IsEnabled = true;
            cmbPorts.IsEnabled = true;
            txtPath.IsEnabled = true;

            //CloseTheServer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            directoryService = new DirectoryService();
            //StartupConfig();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BtnStopServer_Click(null, null);
        }
    }
}
