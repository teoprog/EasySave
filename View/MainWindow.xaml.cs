using System;
using System.Windows;
using System.Windows.Input;
using View.MVVM.View;
using View;
using System.Net.Sockets;
using System.Net;
using System.Text;
using EasySaveApp;
using ServeurClient;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Serveur _serveur;
        private Client _client;
        public MainWindow()
        {
            InitializeComponent();

            // Instancier le serveur avec le port 8000
            _serveur = new Serveur(8000);

            // Démarrer le serveur
            _serveur.Start();

            // Instancier le client avec l'adresse IP du serveur et le port 8000
            _client = new Client("127.0.0.1", 8000);

            // Envoyer des données au serveur
            _client.SendData("Bonjour, serveur !");

        }

        //public object CompleteSaveObject { get; set; }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
