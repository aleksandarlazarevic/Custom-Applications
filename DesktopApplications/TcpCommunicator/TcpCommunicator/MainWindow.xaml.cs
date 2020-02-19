using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using TcpClientApp;
using TcpServerApp;

namespace TcpCommunicator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> logMessages = new ObservableCollection<string>();
        public Client tcpClient;
        public Server tcpServer;
        public ObservableCollection<string> LogMessages
        {
            get { return this.logMessages; }
            set { this.SetProperty(ref this.logMessages, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void SetProperty<T>(
                       ref T member,
                       T val,
                       [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;
            member = val;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            tcpClient = new Client();
            tcpServer = new Server();
        }

        private void StartClientButton_Click(object sender, RoutedEventArgs e)
        {
            tcpClient.StartClient();
            LogMessages.Add("TCP Client started...");
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(2000).ContinueWith(t => tcpServer.StartServer());
            LogMessages.Add("TCP Server started...");
        }

        private void SendFileButton_Click(object sender, RoutedEventArgs e)
        {
            tcpClient.SendFile(@"C:\FileToSend\bomb.txt", @"C:\PlantedFile\bomb.txt");
            LogMessages.Add("Malicious file sent!");
        }
    }
}
