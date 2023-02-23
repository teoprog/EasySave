using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServeurClient
{
    public class Client
    {
        public TcpClient client;

        public NetworkStream _stream;
        
        public Client(string serverIp, int port)
        {
            client = new TcpClient(serverIp, port);
            _stream = client.GetStream();
        }

        public string ReceiveData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return data;
        }

        public void Close()
        {
            client.Close();
        }
        
        public double GetProgress()
        {
            byte[] buffer = new byte[8];
            _stream.Read(buffer, 0, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

    }
}
