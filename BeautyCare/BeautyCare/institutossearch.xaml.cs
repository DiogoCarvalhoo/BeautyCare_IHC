using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for institutos.xaml
    /// </summary>
    public partial class institutossearch : Page
    {
        public static List<InstitutoInfo> listaInstitutos = new List<InstitutoInfo>();
        private static UtilizadorInfo currentUser;
        public institutossearch()
        {
            InitializeComponent();
            loadInfo();
            int flag = 0;
            foreach (InstitutoInfo i in listaInstitutos)
            {
                Grid novaGrid = new Grid();
                novaGrid.Height = 50;
                ColumnDefinition colDef0 = new ColumnDefinition();
                colDef0.Width = new GridLength(120, GridUnitType.Star);
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = new GridLength(180, GridUnitType.Star);
                ColumnDefinition colDef2 = new ColumnDefinition();
                colDef2.Width = new GridLength(50, GridUnitType.Star);
                ColumnDefinition colDef3 = new ColumnDefinition();
                novaGrid.ColumnDefinitions.Add(colDef0);
                novaGrid.ColumnDefinitions.Add(colDef1);
                novaGrid.ColumnDefinitions.Add(colDef2);
                novaGrid.ColumnDefinitions.Add(colDef3);
                StackPanel novoStackPanel = new StackPanel();
                novoStackPanel.SetValue(Grid.ColumnProperty, 0);
                var icon = new nova { Kind = PackIconKind.Star };
                var converter2 = new System.Windows.Media.BrushConverter();
                var brush2 = (Brush)converter2.ConvertFromString("#FFFFD700");
                icon.Foreground = brush2;
                icon.Width = 25;
                icon.Height = 25;
                icon.VerticalAlignment = VerticalAlignment.Top;
                icon.HorizontalAlignment = HorizontalAlignment.Center;
                icon.SetValue(Grid.ColumnProperty, 2);
                novoStackPanel.Children.Add(icon);
                TextBlock newTextBox = new TextBlock();
                newTextBox.Text = i.rating;
                newTextBox.VerticalAlignment = VerticalAlignment.Bottom;
                newTextBox.HorizontalAlignment = HorizontalAlignment.Center;
                newTextBox.FontSize = 16;
                novoStackPanel.Children.Add(newTextBox);
                TextBlock newTextBox2 = new TextBlock();
                newTextBox2.Text = i.nome;
                newTextBox2.FontSize = 16;
                newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                newTextBox2.SetValue(Grid.ColumnProperty, 1);
                var icon2 = new nova { Kind = PackIconKind.ArrowRight };
                icon2.Width = 25;
                icon2.Height = 25;
                icon2.VerticalAlignment = VerticalAlignment.Center;
                icon2.HorizontalAlignment = HorizontalAlignment.Left;
                icon2.SetValue(Grid.ColumnProperty, 2);
                icon2.Name = "seta" + i.id;
                icon2.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                painel.Children.Add(novaGrid);
                novaGrid.Children.Add(novoStackPanel);
                novaGrid.Children.Add(newTextBox2);
                novaGrid.Children.Add(icon2);

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

        private void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string id = sender.ToString().Replace("seta", "");
            institutoPage paginainstituto = new institutoPage(Convert.ToInt32(id), this);
            this.NavigationService.Navigate(paginainstituto);
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


        public void loadInfo()
        {
            institutoPage institutoPagina = new institutoPage(-1, this);
            List<InstitutoInfo> institutos = new List<InstitutoInfo>();
            institutos = institutoPagina.readInstituto();
            listaInstitutos = institutos;
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabelatop_filtro.Visibility = Visibility.Collapsed;
            painel.Children.Clear();
            bool valido = true;
            string nomeinstituto = nomeInstituto.Text;
            string localizacao = localizacaoBox.Text;
            double ratingmin = 0;
            if (!(minrating.Text == ""))
            {
                try
                {
                    ratingmin = Convert.ToDouble(minrating.Text);
                }
                catch
                {
                    MessageBox.Show("Please provide valid rating input value (Number)!");
                    valido = false;
                }
            }
            double ratingmax = 999999;
            if (!(maxrating.Text == ""))
            {
                try
                {
                    ratingmax = Convert.ToDouble(maxrating.Text);
                }
                catch
                {
                    MessageBox.Show("Please provide valid price input value (Number)!");
                    valido = false;
                }
            }
            double ratinginstituto = 0;
            loadInfo();
            int flag = 0;
            foreach (InstitutoInfo i in listaInstitutos)
            {
                valido = true;

                try
                {
                    ratinginstituto = Convert.ToDouble(i.rating.Split("/")[0].Replace(".", ","));
                }
                catch
                {
                    valido = false;
                }

                if (valido == true)
                {
                    Console.WriteLine(i);
                    if (ratingmin <= ratinginstituto && ratinginstituto <= ratingmax)
                    {

                        if (i.localizacao.StartsWith(localizacao) && i.nome.StartsWith(nomeinstituto))
                        {
                            Grid novaGrid = new Grid();
                            novaGrid.Height = 50;
                            ColumnDefinition colDef0 = new ColumnDefinition();
                            colDef0.Width = new GridLength(120, GridUnitType.Star);
                            ColumnDefinition colDef1 = new ColumnDefinition();
                            colDef1.Width = new GridLength(180, GridUnitType.Star);
                            ColumnDefinition colDef2 = new ColumnDefinition();
                            colDef2.Width = new GridLength(50, GridUnitType.Star);
                            ColumnDefinition colDef3 = new ColumnDefinition();
                            novaGrid.ColumnDefinitions.Add(colDef0);
                            novaGrid.ColumnDefinitions.Add(colDef1);
                            novaGrid.ColumnDefinitions.Add(colDef2);
                            novaGrid.ColumnDefinitions.Add(colDef3);
                            StackPanel novoStackPanel = new StackPanel();
                            novoStackPanel.SetValue(Grid.ColumnProperty, 0);
                            var icon = new nova { Kind = PackIconKind.Star };
                            var converter2 = new System.Windows.Media.BrushConverter();
                            var brush2 = (Brush)converter2.ConvertFromString("#FFFFD700");
                            icon.Foreground = brush2;
                            icon.Width = 25;
                            icon.Height = 25;
                            icon.VerticalAlignment = VerticalAlignment.Top;
                            icon.HorizontalAlignment = HorizontalAlignment.Center;
                            icon.SetValue(Grid.ColumnProperty, 2);
                            novoStackPanel.Children.Add(icon);
                            TextBlock newTextBox = new TextBlock();
                            newTextBox.Text = i.rating;
                            newTextBox.VerticalAlignment = VerticalAlignment.Bottom;
                            newTextBox.HorizontalAlignment = HorizontalAlignment.Center;
                            newTextBox.FontSize = 16;
                            novoStackPanel.Children.Add(newTextBox);
                            TextBlock newTextBox2 = new TextBlock();
                            newTextBox2.Text = i.nome;
                            newTextBox2.FontSize = 16;
                            newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                            newTextBox2.SetValue(Grid.ColumnProperty, 1);
                            var icon2 = new nova { Kind = PackIconKind.ArrowRight };
                            icon2.Width = 25;
                            icon2.Height = 25;
                            icon2.VerticalAlignment = VerticalAlignment.Center;
                            icon2.HorizontalAlignment = HorizontalAlignment.Left;
                            icon2.SetValue(Grid.ColumnProperty, 2);
                            icon2.Name = "seta" + i.id;
                            icon2.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                            painel.Children.Add(novaGrid);
                            novaGrid.Children.Add(novoStackPanel);
                            novaGrid.Children.Add(newTextBox2);
                            novaGrid.Children.Add(icon2);

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
            }
            filtros.Visibility = Visibility.Collapsed;
            scrollViewer.Margin = new Thickness(0, 0, 10, 44);
            minrating.Text = "";
            maxrating.Text = "";
            localizacaoBox.Text = "";
            nomeInstituto.Text = "";
        }
    }


}