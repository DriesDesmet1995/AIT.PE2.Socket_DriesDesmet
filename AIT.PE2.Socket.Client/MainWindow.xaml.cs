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
using System.Net.Sockets;
using AIT.PE2.Socket.Core.Entities;
using Newtonsoft.Json;
using System.IO;

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
        bool serverOnline = false;
        #endregion
        private void btnStartServer_Click(object sender, RoutedEventArgs e)
        {
            if(serverOnline==false)
            {
                if (txtUserName.Text == null)
                {
                    txtUserName.Focus();
                    return;
                }
                lblDirectory.Content = "C:\\howest"; //TestCode
                serverOnline = true;
                SaveConfig();
                SendLocalInformation("CONNECT");
                GetAllFolders(lblDirectory.Content.ToString());
            }

            if(serverOnline==true)
            {
                serverOnline = false;
                grpFts.Visibility = Visibility.Hidden;
                SendLocalInformation("CLOSE");

            }


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
            string path = lbFolders.SelectedItem.ToString();
            string parent = System.IO.Directory.GetParent(path).FullName;
            lblFolderName.Content = path.Substring(path.LastIndexOf('\\') + 1); ;
            lblPath.Content = path;
            lblParent.Content = parent;
            lbFiles.Items.Clear();
            GetAllFiles(path);
            SendLocalInformation("PUT");

        }

        private void lbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = lbFiles.SelectedItem.ToString();
            long length = new System.IO.FileInfo(path).Length;
            

            lblFileName.Content = path.Substring(path.LastIndexOf('\\') + 1); ;
            lblFileSize.Content = length.ToString() + " KB";
            lblDate.Content= File.GetCreationTime(path).ToString();
            lblPathFile.Content = path;
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

        private string SendMessageToServer(string message)
        {
            if (serverSocket == null)
            {
                IPAddress serverIP = IPAddress.Parse(cmbIPs.Text);
                int serverPort = int.Parse(cmbPorts.SelectedItem.ToString());
                serverEndpoint = new IPEndPoint(serverIP, serverPort);
                serverSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            try
            {
                serverSocket.Connect(serverEndpoint);
                byte[] outMessage = Encoding.ASCII.GetBytes(message);
                byte[] inMessage = new byte[1024];

                serverSocket.Send(outMessage);
                string response = "";
                while (true)
                {
                    int responseLength = serverSocket.Receive(inMessage);
                    response += Encoding.ASCII.GetString(inMessage, 0, responseLength).ToUpper();
                    if (response.IndexOf("##EOM") > -1)
                        break;
                }
                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();
                serverSocket = null;
                return response;
            }
            catch (Exception fout)
            {
                serverSocket = null;
                return "";
            }
        }

        private void SendLocalInformation(string requestString)
        {
            string foldername = lblFolderName.Content.ToString();
            string folderPath = lblPath.Content.ToString();
            string parentPath = lblParent.Content.ToString();
            ICollection<FTFile> filesInFolder = new List<FTFile>();
            string message = "";

            FTFolder fTFolder = new FTFolder(foldername,folderPath,parentPath,filesInFolder);

            string json = JsonConvert.SerializeObject(fTFolder);
            if (requestString == "CONNECT")
            {
                message = "CONNECT|" + json + "##EOM";
            }
            if (requestString == "CLOSE")
            {
                message = "CLOSE|" + json + "##EOM";
            }
            if (requestString == "PUT")
            {
                message = "PUT|" + json + "##EOM";
            }
            if (requestString == "GET")
            {
                message = "GET|" + json + "##EOM";
            }
            if (requestString == "MKDIR")
            {
                message = "MKDIR|" + json + "##EOM";
            }
            if (requestString == "CDUP")
            {
                message = "CDUP|" + json + "##EOM";
            }
            if (requestString == "CDDIR")
            {
                message = "CDDIR|" + json + "##EOM";
            }
            string response = SendMessageToServer(message);
            if (response != "")
            {
                response = response.Replace("##EOM", "").Trim().ToUpper();
                directoryService.Folders = JsonConvert.DeserializeObject<List<FTFolder>>(response);

                DoVisuals(true);
            }
            else
            {
                MessageBox.Show("Server unreachable");
                DoVisuals(false);
            }
        }

        private void DoVisuals(bool isConnected)
        {
            if (isConnected)
            {
                grpFts.Visibility = Visibility.Visible;
                btnStartServer.Content = "Disconnect";


            }
            else
            {
                grpFts.Visibility = Visibility.Hidden;
                btnStartServer.Content = "Connect";

            }
        }

        public void GetAllFiles(string path)

        {
            string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (var file in allfiles)
            {
                FileInfo info = new FileInfo(file);
                lbFiles.Items.Add(file);
            }
        }

        public void GetAllFolders(string path)

        {
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);


            foreach (var dir in dirs)
            {
                FileInfo info = new FileInfo(dir);
                lbFolders.Items.Add(dir);
            }
            GetAllFiles(lblDirectory.Content.ToString());

        }

        private void lbFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(lblPathFile.Content.ToString(),lblFileName.Content.ToString());
            }
        }
    }
}
