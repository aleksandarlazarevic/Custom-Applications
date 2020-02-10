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
        int port;
        int bufferSize = 1024;
        byte[] buffer = null;
        byte[] header = null;
        private TcpClient tcpClient;
        private string IPAddress;
        bool read = true;

        public Client()
        {
            port = 777;
            IPAddress = "localhost";
        }

        public void StartClient()
        {
            tcpClient = new TcpClient(IPAddress, port);
            tcpClient.SendTimeout = 600000;
            tcpClient.ReceiveTimeout = 600000;
        }

        public void SendFile(string fileToSend, string plantedFileLocation)
        {
            FileStream fs = new FileStream(fileToSend, FileMode.Open);
            int bufferCount = Convert.ToInt32(Math.Ceiling((double)fs.Length / (double)bufferSize));
            string headerStr = "Content-length:" + fs.Length.ToString() + "\r\nFilename:" + plantedFileLocation +"\r\n";
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
    }
}
