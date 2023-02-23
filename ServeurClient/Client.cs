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
        private TcpClient _client;

        public Client(string serverIp, int port)
        {
            _client = new TcpClient(serverIp, port);
        }

        public void SendData(string data)
        {
            var stream = _client.GetStream();
            var buffer = Encoding.UTF8.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);

            buffer = new byte[1024];
            var response = new StringBuilder();
            int bytes;

            while ((bytes = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                response.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            }

            MessageBox.Show("Réponse du serveur : " + response.ToString());
        }

        public void Close()
        {
            _client.Close();
        }
    }
}
