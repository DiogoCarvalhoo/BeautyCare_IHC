using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautyCare
{
    /// <summary>
    /// Interaction logic for ChooseAccountType_Page.xaml
    /// </summary>
    public partial class ChooseAccountType : Page
    {
        public ChooseAccountType()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomePage_Client homeclientPage = new HomePage_Client();
            this.NavigationService.Navigate(homeclientPage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            meusInstitutos pagina = new meusInstitutos();
            this.NavigationService.Navigate(pagina);
        }
    }
}
