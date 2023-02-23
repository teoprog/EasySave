using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveApp
{
    public class Serveur
    {
        private TcpListener _listener;
        private TcpClient _client;

        public Serveur(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            _listener.Start();

            while (true)
            {
                Console.WriteLine("En attente d'une connexion...");
                _client = _listener.AcceptTcpClient();

                var stream = _client.GetStream();
                var buffer = new byte[1024];
                int bytes;

                while ((bytes = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    var data = Encoding.UTF8.GetString(buffer, 0, bytes);
                    Console.WriteLine("Données reçues : " + data);

                    // Répondre au client avec une confirmation
                    var message = "Données reçues : " + data;
                    var response = Encoding.UTF8.GetBytes(message);
                    stream.Write(response, 0, response.Length);
                }

                _client.Close();
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}
