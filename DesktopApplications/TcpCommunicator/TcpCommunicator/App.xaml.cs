using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace TcpCommunicator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length == 2)
            {
                List<string> lowercaseArguments = e.Args.ToList().ConvertAll(x => x.ToLower());

                if (AttachConsole(Attach_Parent_Process))
                {
                    string IpAddress = lowercaseArguments[0];
                    string portNumbe = lowercaseArguments[1];
 
                    SendKeys.SendWait("{ENTER}");
                    FreeConsole();
                }
                this.Shutdown();

            }
            else if (e.Args.Length == 1 || e.Args.Length > 2)
            {
                AttachConsole(Attach_Parent_Process);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Error: Arguments are incorrect.");
                Console.WriteLine("Please run this application with the following arguments: ");

                Console.WriteLine(string.Empty);
                SendKeys.SendWait("{ENTER}");
                this.Shutdown();

            }
        }

        private const int Attach_Parent_Process = -1;

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AttachConsole(int processId);

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
