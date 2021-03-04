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
    /// Interaction logic for HomePage_Client.xaml
    /// </summary>
    public partial class HomePage_Client : Page
    {
        public static UtilizadorInfo currentUser;
        public HomePage_Client()
        {
            InitializeComponent();
            instituto1Descricao.Document.Blocks.Clear();
            instituto1Descricao.Document.Blocks.Add(new Paragraph(new Run("Alcance a beleza desejada com o Martina")) { FontSize = 14 });


            instituto2Descricao.Document.Blocks.Clear();
            instituto2Descricao.Document.Blocks.Add(new Paragraph(new Run("Instituto de Beleza preparado para dar o melhor dos cuidados às pessoas de Aveiro")) { FontSize = 14 });

            barbeariaDescricao.Document.Blocks.Clear();
            barbeariaDescricao.Document.Blocks.Add(new Paragraph(new Run("Barba aparada, conte connosco")) { FontSize = 12 });

            limpezaDescricao.Document.Blocks.Clear();
            limpezaDescricao.Document.Blocks.Add(new Paragraph(new Run("Para obter a pele que sempre desejou")) { FontSize = 12 });

            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            RegisterPage registerPage = new RegisterPage();
            List<UtilizadorInfo> utilizadores = registerPage.readRegisto();
            foreach (UtilizadorInfo u in utilizadores)
            {
                if (u.id == currentUser.id)
                {
                    currentUser.tipoConta = u.tipoConta;
                    currentUser.email = u.email;
                    currentUser.password = u.password;
                    currentUser.localidade = u.localidade;
                    currentUser.foto = u.foto;
                }
            }
            nomePessoa1.Text = currentUser.username;

            if (currentUser.tipoConta == 2)
            {
                mudarConta.Opacity = 1;
                mudarConta.IsEnabled = true;
            } else
            {
                mudarConta.Opacity = 0;
                mudarConta.IsEnabled = false;
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ListMenu.Visibility = Visibility.Visible;
            homepage.IsEnabled = false;
            Image1.Opacity = 0.3;
            Image2.Opacity = 0.3;
            Image3.Opacity = 0.3;
            Image4.Opacity = 0.3;
            ButtonPesquisa.IsEnabled = false;
            fecharmenu2.Visibility = Visibility.Visible;
            nomePessoa1.Visibility = Visibility.Visible;
            separador.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ListMenu.Visibility = Visibility.Collapsed;
            homepage.IsEnabled = true;
            Image1.Opacity = 1;
            Image2.Opacity = 1;
            Image3.Opacity = 1;
            Image4.Opacity = 1;
            ButtonPesquisa.IsEnabled = true;
            fecharmenu2.Visibility = Visibility.Collapsed;
            nomePessoa1.Visibility = Visibility.Collapsed;
            separador.Visibility = Visibility.Collapsed;
        }


        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PersonalInformation personalinfoPage = new PersonalInformation(-1, -1) ;
            this.NavigationService.Navigate(personalinfoPage);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            MinhasReservas reservasPage = new MinhasReservas();
            this.NavigationService.Navigate(reservasPage);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            favoritesPage fv = new favoritesPage();
            this.NavigationService.Navigate(fv);
        }

        private void Grid_PreviewMouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            servicePage servicePage = new servicePage(11, this);
            this.NavigationService.Navigate(servicePage);
        }

        private void Grid_PreviewMouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            servicePage servicePage = new servicePage(1, this);
            this.NavigationService.Navigate(servicePage);
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            institutoPage institutoPage = new institutoPage(2, this);
            this.NavigationService.Navigate(institutoPage);
        }

        private void Grid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            institutoPage institutoPage = new institutoPage(3, this);
            this.NavigationService.Navigate(institutoPage);
        }

        private void ButtonPesquisa_Click(object sender, RoutedEventArgs e)
        {
            servicossearch servicossearchPage = new servicossearch();
            this.NavigationService.Navigate(servicossearchPage);
        }

        private void ButtonPesquisaInstitutos_Click(object sender, RoutedEventArgs e)
        {
            institutossearch institutossearchPage = new institutossearch();
            this.NavigationService.Navigate(institutossearchPage);
        }

        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }

        private void Grid_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ListMenu.Visibility = Visibility.Collapsed;
            homepage.IsEnabled = true;
            Image1.Opacity = 1;
            Image2.Opacity = 1;
            Image3.Opacity = 1;
            Image4.Opacity = 1;
            ButtonPesquisa.IsEnabled = true;
            fecharmenu2.Visibility = Visibility.Collapsed;
            nomePessoa1.Visibility = Visibility.Collapsed;
            separador.Visibility = Visibility.Collapsed;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            Contacts contactos = new Contacts();
            this.NavigationService.Navigate(contactos);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            ChooseAccountType page = new ChooseAccountType();
            this.NavigationService.Navigate(page);
        }
    }
}



