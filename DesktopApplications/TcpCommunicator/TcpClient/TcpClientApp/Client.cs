using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientApp
{
    public class Client
    {
        int port = 777;
        int bufferSize = 1024;
        byte[] buffer = null;
        byte[] header = null;

        string IPAddress = "localhost";
        static string filename = @"C:\FileToSend\bomb.txt";
        bool read = true;

        public Client()
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            int bufferCount = Convert.ToInt32(Math.Ceiling((double)fs.Length / (double)bufferSize));

            TcpClient tcpClient = new TcpClient(IPAddress, port);
            // Setting timeouts for the transfer time
            tcpClient.SendTimeout = 600000;
            tcpClient.ReceiveTimeout = 600000;

            string headerStr = "Content-length:" + fs.Length.ToString() + "\r\nFilename:" + @"C:\PlantedFile\" + "bomb.txt\r\n";
            header = new byte[bufferSize];
            Array.Copy(Encoding.ASCII.GetBytes(headerStr), header, Encoding.ASCII.GetBytes(headerStr).Length);

            tcpClient.Client.Send(header);

            for (int i = 0; i < bufferCount; i++)
            {
                buffer = new byte[bufferSize];
                int size = fs.Read(buffer, 0, bufferSize);

                tcpClient.Client.Send(buffer, size, SocketFlags.Partial);

            }

            tcpClient.Client.Close();
            fs.Close();
        }

        public static void StartClient()
        {
            //throw new NotImplementedException();
        }
    }
}
