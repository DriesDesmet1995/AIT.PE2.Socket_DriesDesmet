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

        }

        private void CmbPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtPath_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStartServer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStopServer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
