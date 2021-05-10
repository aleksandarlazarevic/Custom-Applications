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

namespace RemovableMediaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {


                SecureFTP ftp = new SecureFTP();

                ftp.setDebug(true);
                ftp.setRemoteHost("192.168.0.13");

                //Connect to SSL Port (990)
                ftp.setRemotePort(990);
                //ftp.loginWithoutUser();

                //string cmd = "AUTH SSL";
                //ftp.sendCommand(cmd);

                ////Create SSL Stream
                //ftp.getSslStream();
                //ftp.setUseStream(true);


                //Login  FTP Secure
                ftp.setRemoteUser("test");
                ftp.setRemotePass("dvscorp08!");
                ftp.login();

                //Set ASCII Mode
                ftp.setBinaryMode(false);


                //Upload file

                // Send Argument if you want
                //cmd = "site arg1 arg2";
                //ftp.sendCommand(cmd);

                ftp.upload("", false);
                ftp.uploadSecure(@"Filepath", false);


                ftp.close();





            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Error :" + e.Message);
            }


        }
    }
}
