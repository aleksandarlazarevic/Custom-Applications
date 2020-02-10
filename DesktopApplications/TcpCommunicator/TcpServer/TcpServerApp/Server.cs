using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerApp
{
    public class Server
    {

        int port = 777;
        int bufferSize = 1024;
        byte[] buffer = null;
        byte[] header = null;

        string headerStr = "";
        string filename = "";
        int filesize = 0;

        public Server()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Socket socket = listener.AcceptSocket();
            header = new byte[bufferSize];
            socket.Receive(header);
            headerStr = Encoding.ASCII.GetString(header);

            string[] splitted = headerStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            Dictionary<string, string> headers = new Dictionary<string, string>();
            foreach (string s in splitted)
            {
                if (s.Contains(":"))
                {
                    headers.Add(s.Substring(0, s.IndexOf(":")), s.Substring(s.IndexOf(":") + 1));
                }

            }

            //Get filesize from header
            filesize = Convert.ToInt32(headers["Content-length"]);
            //Get filename from header
            filename = headers["Filename"];

            int bufferCount = Convert.ToInt32(Math.Ceiling((double)filesize / (double)bufferSize));
            CreateFolderIfItDoesNotExist(filename);
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);

            while (filesize > 0)
            {
                buffer = new byte[bufferSize];

                int size = socket.Receive(buffer, SocketFlags.Partial);

                fs.Write(buffer, 0, size);

                filesize -= size;
            }

            fs.Close();
        }

        private void CreateFolderIfItDoesNotExist(string filename)
        {
            (new FileInfo(filename)).Directory.Create();
        }

        public static void StartServer()
        {
            //throw new NotImplementedException();
        }
    }
}

