using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautyCare
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : Page
    {
        public Contacts()
        {
            InitializeComponent();
        }

        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            HomePage_Client homeclientPage = new HomePage_Client();
            this.NavigationService.Navigate(homeclientPage);
        }
    }
}
