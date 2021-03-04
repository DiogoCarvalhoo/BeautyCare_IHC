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
    /// Interaction logic for meusInstitutos.xaml
    /// </summary>
    public partial class meusInstitutos : Page
    {
        public UtilizadorInfo currentUser;
        public static List<InstitutoInfo> listaInstiutos = new List<InstitutoInfo>();
        public static List<ServiceInfo> listaServicos = new List<ServiceInfo>();
        public meusInstitutos()
        {
            InitializeComponent();
            loadInfo();
            foreach (int instituto_id in currentUser.user_institutos_id)
            {
                foreach (InstitutoInfo i in listaInstiutos)
                {
                    if (instituto_id == i.id)
                    {
                        Grid novaGrid = new Grid();
                        novaGrid.Height = 80;
                        ColumnDefinition colDef0 = new ColumnDefinition();
                        colDef0.Width = new GridLength(50, GridUnitType.Star);
                        ColumnDefinition colDef1 = new ColumnDefinition();
                        colDef1.Width = new GridLength(225, GridUnitType.Star);
                        ColumnDefinition colDef2 = new ColumnDefinition();
                        colDef2.Width = new GridLength(100, GridUnitType.Star);
                        novaGrid.ColumnDefinitions.Add(colDef0);
                        novaGrid.ColumnDefinitions.Add(colDef1);
                        novaGrid.ColumnDefinitions.Add(colDef2);
                        TextBlock newTextBox = new TextBlock();
                        newTextBox.Text = i.nome;
                        newTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                        newTextBox.VerticalAlignment = VerticalAlignment.Top;
                        newTextBox.FontSize = 20;
                        newTextBox.Margin = new Thickness(25, 25, 0, 0);
                        newTextBox.SetValue(Grid.ColumnProperty, 1);
                        nova2 botao = new nova2();
                        botao.SetValue(Grid.ColumnProperty, 0);
                        botao.VerticalAlignment = VerticalAlignment.Top;
                        botao.HorizontalAlignment = HorizontalAlignment.Right;
                        botao.Margin = new Thickness(0, 8, 0, 0);
                        var converter = new System.Windows.Media.BrushConverter();
                        var brush = (Brush)converter.ConvertFromString("#FF60C4E5");
                        botao.Background = brush;
                        StackPanel stackpanel = new StackPanel();
                        stackpanel.Orientation = Orientation.Horizontal;
                        var icon = new nova { Kind = PackIconKind.Edit };
                        var b1 = (Brush)converter.ConvertFromString("White");
                        icon.Name = "Edit" + i.id;
                        icon.Width = 25;
                        icon.Height = 25;
                        icon.PreviewMouseLeftButtonDown += EditIcon_PreviewMouseLeftButtonDown;
                        icon.Foreground = b1;
                        stackpanel.Children.Add(icon);
                        botao.Content = stackpanel;
                        botao.Name = "Edit" + i.id;
                        botao.PreviewMouseLeftButtonDown += EditIcon_PreviewMouseLeftButtonDown;
                        nova2 botao2 = new nova2();
                        botao2.SetValue(Grid.ColumnProperty, 0);
                        botao2.VerticalAlignment = VerticalAlignment.Bottom;
                        botao2.HorizontalAlignment = HorizontalAlignment.Right;
                        botao2.Margin = new Thickness(0, 0, 0, 3);
                        botao2.Background = brush;
                        StackPanel stackpanel2 = new StackPanel();
                        stackpanel2.Orientation = Orientation.Horizontal;
                        var icon2 = new nova { Kind = PackIconKind.Delete };
                        icon2.Width = 25;
                        icon2.Height = 25;
                        icon2.PreviewMouseLeftButtonDown += BinIcon_PreviewMouseLeftButtonDown;
                        icon2.Name = "Delete" + Convert.ToString(i.id);
                        icon2.Foreground = b1;
                        stackpanel2.Children.Add(icon2);
                        botao2.Content = stackpanel2;
                        botao2.Name = "Delete" + Convert.ToString(i.id);
                        botao2.PreviewMouseLeftButtonDown += BinIcon_PreviewMouseLeftButtonDown;
                        Grid novaGrid2 = new Grid();
                        novaGrid2.SetValue(Grid.ColumnProperty, 2);
                        novaGrid2.Width = 35;
                        novaGrid2.Height = 32;
                        novaGrid2.VerticalAlignment = VerticalAlignment.Center;
                        novaGrid2.HorizontalAlignment = HorizontalAlignment.Center;
                        Label label = new Label();
                        label.FontSize = 18;
                        label.Margin = new Thickness(-25, 0, 0, -6);
                        label.Content = i.rating;
                        var icon3 = new nova { Kind = PackIconKind.Star };
                        icon3.Width = 30;
                        icon3.Height = 28;
                        icon3.Margin = new Thickness(24, 2, -19, 2);
                        var brush2 = (Brush)converter.ConvertFromString("#FFFFD700");
                        icon3.Foreground = brush2;
                        novaGrid2.Children.Add(label);
                        novaGrid2.Children.Add(icon3);
                        painel.Children.Add(novaGrid);
                        novaGrid.Children.Add(newTextBox);
                        novaGrid.Children.Add(botao);
                        novaGrid.Children.Add(botao2);
                        novaGrid.Children.Add(novaGrid2);


                    }
                }
            }
        }

        private void EditIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int id = Convert.ToInt32(sender.ToString().Split("Edit")[1]);
            registarInstituto pagina = new registarInstituto(id);
            this.NavigationService.Navigate(pagina);
        }

        private void BinIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loadInfo();
            int inst_id = Convert.ToInt32(sender.ToString().Split("Delete")[1]);
            List<int> service_removed = new List<int>();
            foreach (InstitutoInfo i in listaInstiutos.ToArray())
            {
                if (i.id == inst_id)
                {
                    listaInstiutos.Remove(i);
                }

            }

            foreach (ServiceInfo s in listaServicos.ToArray())
            {
                if (s.instituto_id == inst_id)
                {
                    service_removed.Add(s.id);
                    listaServicos.Remove(s);
                }
            }

            List<String> novoConteudoFicheiro = new List<String>();
            foreach (InstitutoInfo i3 in listaInstiutos)
            {
                string conteudo = i3.id + "-" + i3.rating + "-" + i3.foto + "-" + i3.nome + "-" + i3.descricao + "-" + i3.localizacao + "-" + i3.telefone + "-{";
                foreach (int i2 in i3.servicos_id)
                {
                    conteudo = conteudo + i2 + ",";
                }
                conteudo = conteudo + "}-{";
                foreach (int i2 in i3.dias)
                {
                    conteudo = conteudo + i2 + ",";
                }
                conteudo = conteudo + "}-{";
                foreach (String s in i3.horas)
                {
                    conteudo = conteudo + s + ",";
                }
                conteudo = conteudo + "}";
                novoConteudoFicheiro.Add(conteudo);
            }
            registarInstitutos(novoConteudoFicheiro);

            List<String> novoConteudoFicheiro2 = new List<String>();
            foreach (ServiceInfo s in listaServicos)
            {
                string conteudo = s.id + "-" + s.tipo + "-" + s.foto + "-" + s.instituto_id + "-" + s.instituto_nome + "-" + s.descricao + "-" + s.preco;
                novoConteudoFicheiro2.Add(conteudo);
            }
            registarServicos(novoConteudoFicheiro2);



            novoConteudoFicheiro2.Clear();
            List<UtilizadorInfo> listaUtilizadores = new List<UtilizadorInfo>();
            RegisterPage paginaa = new RegisterPage();
            listaUtilizadores = paginaa.readRegisto();
            using (StreamReader reader = new StreamReader("Registos.txt"))
            {
                string[] info;
                string currentline;
                while ((currentline = reader.ReadLine()) != null)
                {
                    info = currentline.Split("-");
                    UtilizadorInfo utilizador = new UtilizadorInfo();
                    utilizador.id = Convert.ToInt32(info[0]);
                    if (utilizador.id == currentUser.id)
                    {
                        info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                        foreach (string s in info[9].Split(","))
                        {
                            if (int.TryParse(s, out _))
                            {
                                if (Convert.ToInt32(s) != inst_id)
                                {
                                    utilizador.user_institutos_id.Add(Convert.ToInt32(s));
                                }
                            }
                        }
                    }
                    else
                    {
                        info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                        foreach (string s in info[9].Split(","))
                        {
                            if (int.TryParse(s, out _))
                            {
                                utilizador.user_institutos_id.Add(Convert.ToInt32(s));
                            }
                        }
                    }
                    utilizador.tipoConta = Convert.ToInt32(info[1]);
                    utilizador.email = info[2];
                    utilizador.username = info[3];
                    utilizador.password = info[4];
                    utilizador.localidade = info[5];
                    utilizador.foto = info[6];
                    info[7] = info[7].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    foreach (string s in info[7].Split(","))
                    {
                        if (int.TryParse(s, out _))
                        {
                            if (!service_removed.Contains(Convert.ToInt32(s)))
                            {
                                utilizador.favoritos.Add(Convert.ToInt32(s));
                            }
                        }
                    }
                    info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    foreach (string s in info[8].Split(","))
                    {
                        if (s != "")
                        {
                            if (!service_removed.Contains(Convert.ToInt32(s.Split("|")[0])))
                            {
                                utilizador.reservas.Add(s);
                            }
                        }
                    }
                    info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    string registo = utilizador.id + "-" + utilizador.tipoConta + "-" + utilizador.email + "-" + utilizador.username + "-" + utilizador.password + "-" + utilizador.localidade + "-" + utilizador.foto + "-{";
                    foreach (int iii in utilizador.favoritos)
                    {
                        registo = registo + iii + ",";
                    }
                    registo = registo + "}-{";
                    foreach (string sss in utilizador.reservas)
                    {
                        registo = registo + sss + ",";
                    }
                    registo = registo + "}-{";
                    foreach (int iii in utilizador.user_institutos_id)
                    {
                        registo = registo + iii + ",";
                    }
                    registo = registo + "}";
                    novoConteudoFicheiro2.Add(registo);

                }
            }
            registarUtilizadores(novoConteudoFicheiro2);
            meusInstitutos pagina = new meusInstitutos();
            this.NavigationService.Navigate(pagina);


        }

        public void registarUtilizadores(List<String> users)
        {
            using (StreamWriter sw = File.CreateText("Registos.txt"))
            {
                foreach (String s in users)
                {
                    sw.WriteLine(s);
                }
            }
        }

        public void registarInstitutos(List<String> institutos)
        {
            using (StreamWriter sw = File.CreateText("Institutos.txt"))
            {
                foreach (String s in institutos)
                {
                    sw.WriteLine(s);
                }
            }
        }

        public void registarServicos(List<String> servicos)
        {
            using (StreamWriter sw = File.CreateText("Servicos.txt"))
            {
                foreach (String s in servicos)
                {
                    sw.WriteLine(s);
                }
            }
        }

        private void loadInfo()
        {
            LoginPage pagina = new LoginPage();
            currentUser = pagina.getCurrentUser();
            institutoPage pagina2 = new institutoPage(-1, this);
            listaInstiutos = pagina2.readInstituto();
            servicePage pagina3 = new servicePage(-1, this);
            listaServicos = pagina3.readService();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ListMenu.Visibility = Visibility.Visible;
            homepage.IsEnabled = false;
            addInstituto.IsEnabled = false;
            fecharmenu2.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ListMenu.Visibility = Visibility.Collapsed;
            homepage.IsEnabled = true;
            addInstituto.IsEnabled = true;
            fecharmenu2.Visibility = Visibility.Collapsed;
        }

        private void Grid_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ListMenu.Visibility = Visibility.Collapsed;
            homepage.IsEnabled = true;
            addInstituto.IsEnabled = true;
            fecharmenu2.Visibility = Visibility.Collapsed;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PersonalInformation page = new PersonalInformation(1, -1);
            this.NavigationService.Navigate(page);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ChooseAccountType page = new ChooseAccountType();
            this.NavigationService.Navigate(page);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            LoginPage page = new LoginPage();
            this.NavigationService.Navigate(page);
        }

        private void addInstituto_Click(object sender, RoutedEventArgs e)
        {
            registarInstituto pagina = new registarInstituto(-1);
            this.NavigationService.Navigate(pagina);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            reservasDonos pagina = new reservasDonos();
            this.NavigationService.Navigate(pagina);
        }
    }
}
