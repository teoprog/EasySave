using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Windows.Threading;

namespace ServeurClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client _client;
        public MainWindow()
        {
            InitializeComponent();
            
            // Connexion au serveur
            _client = new Client("127.0.0.1", 12345);

            // Envoi de données au serveur
            Task.Run(async () =>
            {
                while (true)
                {
                    double progress = await Task.Run(() => _client.GetProgress());
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = progress;
                    });
                    MessageBox.Show(_client.GetProgress().ToString());
                    await Task.Delay(100);
                }
            });

        }
        
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
