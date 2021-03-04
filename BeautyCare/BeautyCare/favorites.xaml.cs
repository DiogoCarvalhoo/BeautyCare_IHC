using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for favoritesPage.xaml
    /// </summary>
    public partial class favoritesPage : Page
    {
        public static UtilizadorInfo currentUser;

        int flag = 0;
        public favoritesPage()
        {
            InitializeComponent();
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();


            foreach (int service_id in currentUser.favoritos)
            {
                servicePage ServicePage = new servicePage(service_id, this);
                ServiceInfo servico = ServicePage.getCurrentService();
                Grid novaGrid = new Grid();
                novaGrid.Height = 50;
                ColumnDefinition colDef0 = new ColumnDefinition();
                colDef0.Width = new GridLength(30, GridUnitType.Star);
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = new GridLength(40, GridUnitType.Star);
                ColumnDefinition colDef2 = new ColumnDefinition();
                colDef2.Width = new GridLength(130, GridUnitType.Star);
                ColumnDefinition colDef3 = new ColumnDefinition();
                colDef3.Width = new GridLength(100, GridUnitType.Star);
                ColumnDefinition colDef4 = new ColumnDefinition();
                colDef4.Width = new GridLength(40, GridUnitType.Star);
                ColumnDefinition colDef5 = new ColumnDefinition();
                colDef5.Width = new GridLength(30, GridUnitType.Star);
                novaGrid.ColumnDefinitions.Add(colDef0);
                novaGrid.ColumnDefinitions.Add(colDef1);
                novaGrid.ColumnDefinitions.Add(colDef2);
                novaGrid.ColumnDefinitions.Add(colDef3);
                novaGrid.ColumnDefinitions.Add(colDef4);
                novaGrid.ColumnDefinitions.Add(colDef5);
                var bin = new nova { Kind = PackIconKind.Bin };
                bin.Width = 25;
                bin.Height = 25;
                bin.VerticalAlignment = VerticalAlignment.Center;
                bin.HorizontalAlignment = HorizontalAlignment.Left;
                bin.SetValue(Grid.ColumnProperty, 1);
                bin.Name = "bin" + service_id;
                bin.PreviewMouseLeftButtonDown += BinIcon_PreviewMouseLeftButtonDown;
                TextBlock newTextBox = new TextBlock();
                newTextBox.Text = servico.tipo;
                newTextBox.VerticalAlignment = VerticalAlignment.Center;
                newTextBox.FontSize = 16;
                newTextBox.SetValue(Grid.ColumnProperty, 2);
                TextBlock newTextBox2 = new TextBlock();
                newTextBox2.Text = servico.instituto_nome;
                newTextBox2.FontSize = 16;
                newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                newTextBox2.SetValue(Grid.ColumnProperty, 3);
                var icon = new nova { Kind = PackIconKind.ArrowRight };
                icon.Width = 25;
                icon.Height = 25;
                icon.VerticalAlignment = VerticalAlignment.Center;
                icon.HorizontalAlignment = HorizontalAlignment.Right;
                icon.SetValue(Grid.ColumnProperty, 4);
                icon.Name = "seta" + service_id;
                icon.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                painel.Children.Add(novaGrid);
                novaGrid.Children.Add(bin);
                novaGrid.Children.Add(newTextBox);
                novaGrid.Children.Add(newTextBox2);
                novaGrid.Children.Add(icon);

                if (flag % 2 == 0)
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#FFD0EDED");
                    novaGrid.Background = brush;
                }
                else
                {
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#FFC2DCDC");
                    novaGrid.Background = brush;
                }
                flag++;
            }

        }

        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            HomePage_Client homepage = new HomePage_Client();
            this.NavigationService.Navigate(homepage);
        }


        private void BinIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string id = sender.ToString().Replace("bin", "");
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            List<String> novoConteudoFicheiro = new List<String>();
            RegisterPage registerpageobject = new RegisterPage();
            List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
            foreach (UtilizadorInfo u in utilizadores)
            {
                if (u.id == currentUser.id)
                {
                    u.favoritos.Remove(Convert.ToInt32(id));
                }
                string conteudo = u.id + "-" + u.tipoConta + "-" + u.email + "-" + u.username + "-" + u.password + "-" + u.localidade + "-" + u.foto + "-{";
                foreach (int i in u.favoritos)
                {
                    conteudo = conteudo + i + ",";
                }
                conteudo = conteudo + "}-{";
                foreach (string s in u.reservas)
                {
                    conteudo = conteudo + s + ",";
                }
                conteudo = conteudo + "}-{";
                foreach (int i in u.user_institutos_id)
                {
                    conteudo = conteudo + i + ",";
                }
                conteudo = conteudo + "}";
                novoConteudoFicheiro.Add(conteudo);
            }
            registarAlteracoes(novoConteudoFicheiro);
            favoritesPage nova = new favoritesPage();
            this.NavigationService.Navigate(nova);
        }

        public void registarAlteracoes(List<String> utilizadores)
        {
            using (StreamWriter sw = File.CreateText("Registos.txt"))
            {
                foreach (String s in utilizadores)
                {
                    sw.WriteLine(s);
                }
            }
        }

        private void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string id = sender.ToString().Replace("seta", "");
            servicePage paginaservico = new servicePage(Convert.ToInt32(id), this);
            this.NavigationService.Navigate(paginaservico);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Tou vivo");
        }
    }
}


public class nova:PackIcon
{

    public override string ToString()
    {
        return this.Name;
    }
}