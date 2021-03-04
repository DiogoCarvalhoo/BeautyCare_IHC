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
    /// Interaction logic for MinhasReservas.xaml
    /// </summary>
    public partial class MinhasReservas : Page
    {
        public static UtilizadorInfo currentUser;
        public MinhasReservas()
        {
            InitializeComponent();
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            int flag = 0;
            foreach (String reserva in currentUser.reservas)
            {
                if (reserva != "")
                {
                    int servico_id = -1;
                    string data = "";
                    string[] cadaparte = reserva.Split("|");
                    servico_id = Convert.ToInt32(cadaparte[0]);
                    data = cadaparte[2] + "           " + cadaparte[1];
                    servicePage ServicePage = new servicePage(servico_id, this);
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

                    var binicon = new nova { Kind = PackIconKind.Bin };
                    binicon.Width = 25;
                    binicon.Height = 25;
                    binicon.VerticalAlignment = VerticalAlignment.Center;
                    binicon.HorizontalAlignment = HorizontalAlignment.Left;
                    binicon.SetValue(Grid.ColumnProperty, 1);
                    string hora = cadaparte[1].Split(":")[0];
                    string minutos = cadaparte[1].Split(":")[1];
                    binicon.Name = "bin" + servico_id + "separador" + hora  + "separador" + minutos + "separador" + cadaparte[2];
                    binicon.PreviewMouseLeftButtonDown += BinIcon_PreviewMouseLeftButtonDown;
                    TextBlock newTextBox = new TextBlock();
                    newTextBox.Text = servico.tipo;
                    newTextBox.VerticalAlignment = VerticalAlignment.Center;
                    newTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                    newTextBox.FontSize = 16;
                    newTextBox.SetValue(Grid.ColumnProperty, 2);
                    TextBlock newTextBox2 = new TextBlock();
                    newTextBox2.Text = data;
                    newTextBox2.FontSize = 16;
                    newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                    newTextBox2.HorizontalAlignment = HorizontalAlignment.Left;
                    newTextBox2.SetValue(Grid.ColumnProperty, 3);
                    var icon = new nova { Kind = PackIconKind.ArrowRight };
                    icon.Width = 25;
                    icon.Height = 25;
                    icon.VerticalAlignment = VerticalAlignment.Center;
                    icon.HorizontalAlignment = HorizontalAlignment.Right;
                    icon.SetValue(Grid.ColumnProperty, 4);
                    icon.Name = "seta" + servico_id;
                    icon.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                    painel.Children.Add(novaGrid);
                    novaGrid.Children.Add(binicon);
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
        }

        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            HomePage_Client homeclientPage = new HomePage_Client();
            this.NavigationService.Navigate(homeclientPage);
        }


        private void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string id = sender.ToString().Replace("seta", "");
            servicePage paginaservico = new servicePage(Convert.ToInt32(id), this);
            this.NavigationService.Navigate(paginaservico);
        }


        private void BinIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string[] partes = sender.ToString().Replace("bin", "").Split("separador");
            string aremover = partes[0] + "|" + partes[1] + ":" + partes[2] + "|" + partes[3];
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            List<String> novoConteudoFicheiro = new List<String>();
            RegisterPage registerpageobject = new RegisterPage();
            List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
            foreach (UtilizadorInfo u in utilizadores)
            {
                if (u.id == currentUser.id)
                {
                    u.reservas.Remove(aremover);
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
            MinhasReservas nova = new  MinhasReservas();
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
    }

}
