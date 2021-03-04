using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Interaction logic for servicos.xaml
    /// </summary>
    public partial class servicossearch : Page
    {
        public static List<ServiceInfo> listaServicos = new List<ServiceInfo>();
        public static List<InstitutoInfo> listaInstitutos = new List<InstitutoInfo>();
        private static UtilizadorInfo currentUser;

        public servicossearch()
        {
            InitializeComponent();
            this.loadInfo();

            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();

            foreach (ServiceInfo s in listaServicos)
            {
                Grid novaGrid = new Grid();
                novaGrid.Height = 40;
                ColumnDefinition colDef0 = new ColumnDefinition();
                colDef0.Width = new GridLength(15, GridUnitType.Star);
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = new GridLength(30, GridUnitType.Star);
                ColumnDefinition colDef2 = new ColumnDefinition();
                colDef2.Width = new GridLength(160, GridUnitType.Star);
                ColumnDefinition colDef3 = new ColumnDefinition();
                colDef3.Width = new GridLength(85, GridUnitType.Star);
                ColumnDefinition colDef4 = new ColumnDefinition();
                colDef4.Width = new GridLength(50, GridUnitType.Star);
                novaGrid.ColumnDefinitions.Add(colDef0);
                novaGrid.ColumnDefinitions.Add(colDef1);
                novaGrid.ColumnDefinitions.Add(colDef2);
                novaGrid.ColumnDefinitions.Add(colDef3);
                novaGrid.ColumnDefinitions.Add(colDef4);
                var icon = new nova { Kind = PackIconKind.StarBorder };
                foreach (int i in currentUser.favoritos)
                {
                    if (i == s.id)
                    {
                        icon = new nova { Kind = PackIconKind.Star };
                    }
                }
                icon.Width = 30;
                icon.Height = 45;
                icon.SetValue(Grid.ColumnProperty, 1);
                icon.Name = "seta" + s.id;
                icon.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                nova2 novo = new nova2();
                novo.SetValue(Grid.ColumnProperty, 1);
                novo.Width = 30;
                novo.Height = 45;
                novo.Opacity = 0;
                novaGrid.Children.Add(novo);
                novo.Name = "seta" + s.id;
                novo.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                TextBlock newTextBox = new TextBlock();
                newTextBox.Text = s.tipo;
                newTextBox.VerticalAlignment = VerticalAlignment.Center;
                newTextBox.FontSize = 18;
                newTextBox.Margin = new Thickness(10, 10, 0, 0);
                newTextBox.SetValue(Grid.ColumnProperty, 2);
                TextBlock newTextBox2 = new TextBlock();
                newTextBox2.Text = s.preco + "  €";
                newTextBox2.FontSize = 18;
                newTextBox2.Margin = new Thickness(10, 10, 0, 0);
                newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                newTextBox2.SetValue(Grid.ColumnProperty, 3);
                var icon2 = new nova { Kind = PackIconKind.ArrowRight };
                icon2.Width = 25;
                icon2.Height = 25;
                icon2.VerticalAlignment = VerticalAlignment.Center;
                icon2.HorizontalAlignment = HorizontalAlignment.Left;
                icon2.SetValue(Grid.ColumnProperty, 4);
                icon2.Name = "seta" + s.id;
                icon2.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                painel.Children.Add(novaGrid);
                novaGrid.Children.Add(newTextBox);
                novaGrid.Children.Add(newTextBox2);
                novaGrid.Children.Add(icon);
                novaGrid.Children.Add(icon2);
            }
        }


        private void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string id = sender.ToString().Replace("seta", "");
            servicePage paginaservico = new servicePage(Convert.ToInt32(id), this);
            this.NavigationService.Navigate(paginaservico);
        }


        private void FavIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(sender);
            int id = Convert.ToInt32(sender.ToString().Replace("seta", ""));
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            List<String> novoConteudoFicheiro = new List<String>();
            RegisterPage registerpageobject = new RegisterPage();
            List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
            bool adicionar = true;
            foreach (UtilizadorInfo u in utilizadores)
            {
                if (u.id == currentUser.id)
                {
                    foreach (int i in u.favoritos)
                    {
                        if (i == id)
                        {
                            adicionar = false;
                        }
                    }
                    if (adicionar == true)
                    {
                        u.favoritos.Add(id);
                    }
                    else
                    {
                        u.favoritos.Remove(id);
                    }
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
            currentUser = logPage.getCurrentUser();
            painel.Children.Clear();
            bool valido = true;
            string nomeservico;
            try
            {
                nomeservico = tiposervico.SelectedItem.ToString();
            }
            catch
            {
                nomeservico = "";
            }
            string localizacao = localizacaoBox.Text;
            double precomin = 0;
            if (!(minpreco.Text == ""))
            {
                try
                {
                    precomin = Convert.ToDouble(minpreco.Text);
                }
                catch
                {
                    valido = false;
                }
            }
            double precomax = 999999;
            if (!(maxpreco.Text == ""))
            {
                try
                {
                    precomax = Convert.ToDouble(maxpreco.Text);
                }
                catch
                {
                    valido = false;
                }
            }
            double precoservico = -1;
            loadInfo();
            foreach (ServiceInfo s in listaServicos)
            {
                try
                {
                    precoservico = Convert.ToDouble(s.preco);
                }
                catch
                {
                    valido = false;
                }
                if (valido == true)
                {
                    if ((s.tipo.Equals(nomeservico) || nomeservico.Equals("")) && precomin <= precoservico && precoservico <= precomax)
                    {
                        foreach (InstitutoInfo i in listaInstitutos)
                        {
                            if (i.id == s.instituto_id && i.localizacao.StartsWith(localizacao))
                            {
                                Grid novaGrid = new Grid();
                                novaGrid.Height = 40;
                                ColumnDefinition colDef0 = new ColumnDefinition();
                                colDef0.Width = new GridLength(15, GridUnitType.Star);
                                ColumnDefinition colDef1 = new ColumnDefinition();
                                colDef1.Width = new GridLength(30, GridUnitType.Star);
                                ColumnDefinition colDef2 = new ColumnDefinition();
                                colDef2.Width = new GridLength(160, GridUnitType.Star);
                                ColumnDefinition colDef3 = new ColumnDefinition();
                                colDef3.Width = new GridLength(85, GridUnitType.Star);
                                ColumnDefinition colDef4 = new ColumnDefinition();
                                colDef4.Width = new GridLength(50, GridUnitType.Star);
                                novaGrid.ColumnDefinitions.Add(colDef0);
                                novaGrid.ColumnDefinitions.Add(colDef1);
                                novaGrid.ColumnDefinitions.Add(colDef2);
                                novaGrid.ColumnDefinitions.Add(colDef3);
                                novaGrid.ColumnDefinitions.Add(colDef4);
                                var icon = new nova { Kind = PackIconKind.StarBorder };
                                foreach (int l in currentUser.favoritos)
                                {
                                    if (l == s.id)
                                    {
                                        icon = new nova { Kind = PackIconKind.Star };
                                    }
                                }
                                icon.Width = 30;
                                icon.Height = 45;
                                icon.HorizontalAlignment = HorizontalAlignment.Center;
                                icon.SetValue(Grid.ColumnProperty, 1);
                                icon.Name = "seta" + s.id;
                                icon.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                                nova2 novo = new nova2();
                                novo.SetValue(Grid.ColumnProperty, 1);
                                novo.Width = 30;
                                novo.Height = 45;
                                novo.Opacity = 0;
                                novaGrid.Children.Add(novo);
                                novo.Name = "seta" + s.id;
                                novo.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                                TextBlock newTextBox = new TextBlock();
                                newTextBox.Text = s.tipo;
                                newTextBox.VerticalAlignment = VerticalAlignment.Center;
                                newTextBox.Margin = new Thickness(10, 10, 0, 0);
                                newTextBox.FontSize = 18;
                                newTextBox.SetValue(Grid.ColumnProperty, 2);
                                TextBlock newTextBox2 = new TextBlock();
                                newTextBox2.Text = s.preco + "  €";
                                newTextBox2.FontSize = 18;
                                newTextBox2.Margin = new Thickness(10, 10, 0, 0);
                                newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                                newTextBox2.SetValue(Grid.ColumnProperty, 3);
                                var icon2 = new nova { Kind = PackIconKind.ArrowRight };
                                icon2.Width = 25;
                                icon2.Height = 25;
                                icon2.VerticalAlignment = VerticalAlignment.Center;
                                icon2.HorizontalAlignment = HorizontalAlignment.Left;
                                icon2.SetValue(Grid.ColumnProperty, 4);
                                icon2.Name = "seta" + s.id;
                                icon2.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                                painel.Children.Add(novaGrid);
                                novaGrid.Children.Add(newTextBox);
                                novaGrid.Children.Add(newTextBox2);
                                novaGrid.Children.Add(icon);
                                novaGrid.Children.Add(icon2);
                            }
                        }
                    }
                }
            }
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


        public void loadInfo()
        {
            servicePage servicoPagina = new servicePage(-1, this);
            List<ServiceInfo> servicos = new List<ServiceInfo>();
            institutoPage institutoPagina = new institutoPage(-1, this);
            List<InstitutoInfo> institutos = new List<InstitutoInfo>();
            servicos = servicoPagina.readService();
            institutos = institutoPagina.readInstituto();
            listaServicos = servicos;
            listaInstitutos = institutos;
            foreach (ServiceInfo s in listaServicos)
            {
                if (tiposervico.Items.Contains(s.tipo) == false)
                {
                    tiposervico.Items.Add(s.tipo);
                }
            }
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (filtros.Visibility == Visibility.Collapsed)
            {
                filtros.Visibility = Visibility.Visible;
                scrollViewer.Margin = new Thickness(0, 350, 10, 44);
                tabelatop_filtro.Visibility = Visibility.Visible;
            }
            else
            {
                filtros.Visibility = Visibility.Collapsed;
                scrollViewer.Margin = new Thickness(0, 0, 10, 44);
                tabelatop_filtro.Visibility = Visibility.Collapsed;
            }
        }



        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            HomePage_Client homeclientPage = new HomePage_Client();
            this.NavigationService.Navigate(homeclientPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e2)
        {
            painel.Children.Clear();
            tabelatop_filtro.Visibility = Visibility.Collapsed;
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            bool valido = true;
            string nomeservico;
            try
            {
                nomeservico = tiposervico.SelectedItem.ToString();
            }
            catch
            {
                nomeservico = "";
            }
            string localizacao = localizacaoBox.Text;
            double precomin = 0;
            if (!(minpreco.Text == ""))
            {
                try
                {
                    precomin = Convert.ToDouble(minpreco.Text);
                }
                catch
                {
                    MessageBox.Show("Please provide valid price input value (Number)!");
                    valido = false;
                }
            }
            double precomax = 999999;
            if (!(maxpreco.Text == ""))
            {
                try
                {
                    precomax = Convert.ToDouble(maxpreco.Text);
                }
                catch
                {
                    MessageBox.Show("Please provide valid price input value (Number)!");
                    valido = false;
                }
            }
            double precoservico = -1;
            loadInfo();
            foreach (ServiceInfo s in listaServicos)
            {
                valido = true;
                try
                {
                    precoservico = Convert.ToDouble(s.preco);
                }
                catch
                {
                    valido = false;
                }

                if (valido == true)
                {
                    if ((s.tipo.Equals(nomeservico) || nomeservico.Equals("")) && precomin <= precoservico && precoservico <= precomax)
                    {
                        foreach (InstitutoInfo i in listaInstitutos)
                        {
                            if (i.id == s.instituto_id && i.localizacao.StartsWith(localizacao))
                            {
                                Grid novaGrid = new Grid();
                                novaGrid.Height = 40;
                                ColumnDefinition colDef0 = new ColumnDefinition();
                                colDef0.Width = new GridLength(15, GridUnitType.Star);
                                ColumnDefinition colDef1 = new ColumnDefinition();
                                colDef1.Width = new GridLength(30, GridUnitType.Star);
                                ColumnDefinition colDef2 = new ColumnDefinition();
                                colDef2.Width = new GridLength(160, GridUnitType.Star);
                                ColumnDefinition colDef3 = new ColumnDefinition();
                                colDef3.Width = new GridLength(85, GridUnitType.Star);
                                ColumnDefinition colDef4 = new ColumnDefinition();
                                colDef4.Width = new GridLength(50, GridUnitType.Star);
                                novaGrid.ColumnDefinitions.Add(colDef0);
                                novaGrid.ColumnDefinitions.Add(colDef1);
                                novaGrid.ColumnDefinitions.Add(colDef2);
                                novaGrid.ColumnDefinitions.Add(colDef3);
                                novaGrid.ColumnDefinitions.Add(colDef4);
                                var icon = new nova { Kind = PackIconKind.StarBorder };
                                foreach (int l in currentUser.favoritos)
                                {
                                    if (l == s.id)
                                    {
                                        icon = new nova { Kind = PackIconKind.Star };
                                    }
                                }
                                icon.Width = 30;
                                icon.Height = 45;
                                icon.HorizontalAlignment = HorizontalAlignment.Center;
                                icon.SetValue(Grid.ColumnProperty, 1);
                                icon.Name = "seta" + s.id;
                                icon.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                                nova2 novo = new nova2();
                                novo.SetValue(Grid.ColumnProperty, 1);
                                novo.Width = 30;
                                novo.Height = 45;
                                novo.Opacity = 0;
                                novaGrid.Children.Add(novo);
                                novo.Name = "seta" + s.id;
                                novo.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                                TextBlock newTextBox = new TextBlock();
                                newTextBox.Text = s.tipo;
                                newTextBox.VerticalAlignment = VerticalAlignment.Center;
                                newTextBox.Margin = new Thickness(10, 10, 0, 0);
                                newTextBox.FontSize = 18;
                                newTextBox.SetValue(Grid.ColumnProperty, 2);
                                TextBlock newTextBox2 = new TextBlock();
                                newTextBox2.Text = s.preco + "  €";
                                newTextBox2.FontSize = 18;
                                newTextBox2.Margin = new Thickness(10, 10, 0, 0);
                                newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                                newTextBox2.SetValue(Grid.ColumnProperty, 3);
                                var icon2 = new nova { Kind = PackIconKind.ArrowRight };
                                icon2.Width = 25;
                                icon2.Height = 25;
                                icon2.VerticalAlignment = VerticalAlignment.Center;
                                icon2.HorizontalAlignment = HorizontalAlignment.Left;
                                icon2.SetValue(Grid.ColumnProperty, 4);
                                icon2.Name = "seta" + s.id;
                                icon2.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                                painel.Children.Add(novaGrid);
                                novaGrid.Children.Add(newTextBox);
                                novaGrid.Children.Add(newTextBox2);
                                novaGrid.Children.Add(icon);
                                novaGrid.Children.Add(icon2);
                            }
                        }
                    }
                }
            }
            filtros.Visibility = Visibility.Collapsed;
            scrollViewer.Margin = new Thickness(0, 0, 10, 44);
            minpreco.Text = "";
            maxpreco.Text = "";
            localizacaoBox.Text = "";
            tiposervico.SelectedIndex = -1;
        }
    }
}

