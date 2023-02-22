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
using EasySave;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour BlackListSoftware.xaml
    /// </summary>
    public partial class BlackListSoftware : UserControl
    {
        public BusinessSoftware Software = new BusinessSoftware();

        public BlackListSoftware()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(SoftwareBox.Text))
            {
                ErrorTargetLabel.Content = "Veuillez entrer un logiciel Metier";
                ErrorTargetLabel.Visibility = Visibility.Visible;
            }

            if (Software.Add(SoftwareBox.Text))
            {
                SuccesLabel.Content = "Le Logiciel Metier a bien été Ajouté";
                SuccesLabel.Visibility = Visibility.Visible;

            }
            else
            {
                ErrorTargetLabel.Content = "Le Logiciel metier que vous avez rentré n'existe pas ou existe déja";
                ErrorTargetLabel.Visibility = Visibility.Visible;
            }
        }

        private void SoftwareBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorTargetLabel.Visibility = Visibility.Collapsed;
            SuccesLabel.Visibility = Visibility.Collapsed;
        }
    }
}
