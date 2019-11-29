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
using TcpServer;

namespace TcpCommunicator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> logMessages = new ObservableCollection<string>();
        public ObservableCollection<string> LogMessages
        {
            get { return this.logMessages; }
            set { this.SetProperty(ref this.logMessages, value); }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void SetProperty<T>(
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

        }

        private void StartClientButton_Click(object sender, RoutedEventArgs e)
        {
            LogMessages.Add("TCP Client started...");
            Client tcpClient = new Client();
            Client.StartClient();
        }

        private void StartServerButton_Click(object sender, RoutedEventArgs e)
        {
            LogMessages.Add("TCP Server started...");
            Server tcpServer = new Server();
            Server.StartServer();
        }

        private void SendFileButton_Click(object sender, RoutedEventArgs e)
        {
            LogMessages.Add("Malicious file sent!");
            //SendFile(@"C:\FileToSend\bomb.txt");
        }

        private void SendFile(string fileToSend)
        {

        }
    }
}
