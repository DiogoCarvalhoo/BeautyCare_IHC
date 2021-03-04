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
    /// Interaction logic for reservasDonos.xaml
    /// </summary>
    public partial class reservasDonos : Page
    {
        private static UtilizadorInfo currentUser;
        private List<ServiceInfo> listaServicos = new List<ServiceInfo>();
        private List<InstitutoInfo> listaInstitutos = new List<InstitutoInfo>();
        private List<UtilizadorInfo> listaUtilizadores = new List<UtilizadorInfo>();
        public reservasDonos()
        {
            InitializeComponent();
            loadInfo();
        }

        public void getListaUtitilizadores ()
        {
            using (StreamReader reader = new StreamReader("Registos.txt"))
            {
                string[] info;
                string currentline;
                while ((currentline = reader.ReadLine()) != null)
                {
                    info = currentline.Split("-");
                    UtilizadorInfo utilizador = new UtilizadorInfo();
                    utilizador.id = Convert.ToInt32(info[0]);
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
                            utilizador.favoritos.Add(Convert.ToInt32(s));
                        }
                    }
                    info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    foreach (string s in info[8].Split(","))
                    {
                        if (s != "")
                        {
                            utilizador.reservas.Add(s);
                        }
                    }
                    info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    foreach (string s in info[9].Split(","))
                    {
                        if (int.TryParse(s, out _))
                        {
                            utilizador.user_institutos_id.Add(Convert.ToInt32(s));
                        }
                    }
                    listaUtilizadores.Add(utilizador);
                }
            }
        }

        public void atualizarLista(String nomeInstituto)
        {
            painel.Children.Clear();
            foreach (UtilizadorInfo u in listaUtilizadores)
            {
                foreach (String s in u.reservas)
                {
                    int service_id = Convert.ToInt32(s.Split("|")[0]);
                    String horas = s.Split("|")[1];
                    String dia = s.Split("|")[2];
                    foreach (InstitutoInfo i in listaInstitutos)
                    {           
                        if ((currentUser.user_institutos_id.Contains(i.id) == true) && (i.servicos_id.Contains(service_id) == true) && (i.nome.Equals(nomeInstituto) || i.nome.Equals("")))
                        {
                            String servico_tipo = "";
                            foreach (ServiceInfo servico in listaServicos)
                            {
                                if (servico.id == service_id)
                                {
                                    servico_tipo = servico.tipo;
                                }
                            }
                            Grid firstGrid = new Grid();
                            firstGrid.Height = 70;
                            RowDefinition rowDef0 = new RowDefinition();
                            rowDef0.Height = new GridLength(40, GridUnitType.Star);
                            RowDefinition rowDef1 = new RowDefinition();
                            rowDef1.Height = new GridLength(20, GridUnitType.Star);
                            firstGrid.RowDefinitions.Add(rowDef0);
                            firstGrid.RowDefinitions.Add(rowDef1);

                            Grid secondGrid = new Grid();
                            ColumnDefinition colDef0 = new ColumnDefinition();
                            colDef0.Width = new GridLength(60, GridUnitType.Star);
                            ColumnDefinition colDef1 = new ColumnDefinition();
                            colDef1.Width = new GridLength(230, GridUnitType.Star);
                            ColumnDefinition colDef2 = new ColumnDefinition();
                            colDef2.Width = new GridLength(50, GridUnitType.Star);
                            ColumnDefinition colDef3 = new ColumnDefinition();
                            colDef3.Width = new GridLength(50, GridUnitType.Star);
                            ColumnDefinition colDef4 = new ColumnDefinition();
                            colDef4.Width = new GridLength(20, GridUnitType.Star);
                            secondGrid.ColumnDefinitions.Add(colDef0);
                            secondGrid.ColumnDefinitions.Add(colDef1);
                            secondGrid.ColumnDefinitions.Add(colDef2);
                            secondGrid.ColumnDefinitions.Add(colDef3);
                            secondGrid.ColumnDefinitions.Add(colDef4);
                            secondGrid.SetValue(Grid.RowProperty, 0);

                            TextBlock newTextBox = new TextBlock();
                            newTextBox.Text = servico_tipo;
                            newTextBox.VerticalAlignment = VerticalAlignment.Center;
                            newTextBox.SetValue(Grid.ColumnProperty, 1);
                            newTextBox.FontSize = 20;

                            TextBlock newTextBox1 = new TextBlock();
                            newTextBox1.Text = dia;
                            newTextBox1.VerticalAlignment = VerticalAlignment.Center;
                            newTextBox1.SetValue(Grid.ColumnProperty, 2);
                            newTextBox1.FontSize = 20;

                            TextBlock newTextBox2 = new TextBlock();
                            newTextBox2.Text = horas;
                            newTextBox2.VerticalAlignment = VerticalAlignment.Center;
                            newTextBox2.SetValue(Grid.ColumnProperty, 3);
                            newTextBox2.FontSize = 20;

                            secondGrid.Children.Add(newTextBox);
                            secondGrid.Children.Add(newTextBox1);
                            secondGrid.Children.Add(newTextBox2);


                            Grid thirdGrid = new Grid();
                            ColumnDefinition colDef00 = new ColumnDefinition();
                            colDef00.Width = new GridLength(60, GridUnitType.Star);
                            ColumnDefinition colDef01 = new ColumnDefinition();
                            colDef01.Width = new GridLength(280, GridUnitType.Star);
                            ColumnDefinition colDef02 = new ColumnDefinition();
                            colDef02.Width = new GridLength(50, GridUnitType.Star);
                            ColumnDefinition colDef03 = new ColumnDefinition();
                            colDef03.Width = new GridLength(20, GridUnitType.Star);
                            thirdGrid.ColumnDefinitions.Add(colDef00);
                            thirdGrid.ColumnDefinitions.Add(colDef01);
                            thirdGrid.ColumnDefinitions.Add(colDef02);
                            thirdGrid.ColumnDefinitions.Add(colDef03);
                            thirdGrid.SetValue(Grid.RowProperty, 1);

                            TextBlock newTextBox3 = new TextBlock();
                            newTextBox3.Text = u.username;
                            newTextBox3.HorizontalAlignment = HorizontalAlignment.Right;
                            newTextBox3.VerticalAlignment = VerticalAlignment.Center;
                            newTextBox3.SetValue(Grid.ColumnProperty, 1);
                            newTextBox3.FontSize = 16;

                            var converter = new System.Windows.Media.BrushConverter();
                            var brush = (Brush)converter.ConvertFromString("#FF60C4E5");
                            var icon = new nova { Kind = PackIconKind.ArrowRight };
                            icon.Width = 25;
                            icon.Height = 25;
                            icon.HorizontalAlignment = HorizontalAlignment.Center;
                            icon.VerticalAlignment = VerticalAlignment.Center;
                            icon.SetValue(Grid.ColumnProperty, 2);
                            icon.Name = "seta"+u.id;
                            icon.PreviewMouseLeftButtonDown += Icon_PreviewMouseLeftButtonDown;

                            thirdGrid.Children.Add(icon);
                            thirdGrid.Children.Add(newTextBox3);

                            painel.Children.Add(firstGrid);
                            firstGrid.Children.Add(secondGrid);
                            firstGrid.Children.Add(thirdGrid);
                        }
                    }
                }

            }
        }

        private void Icon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int id = Convert.ToInt32(sender.ToString().Split("seta")[1]);
            PersonalInformation pagina = new PersonalInformation(1,id);
            this.NavigationService.Navigate(pagina);
        }


            public void loadInfo()
        {
            LoginPage loginPagina = new LoginPage();
            currentUser = loginPagina.getCurrentUser();
            servicePage servicoPagina = new servicePage(-1, this);
            institutoPage institutoPagina = new institutoPage(-1, this);
            listaServicos = servicoPagina.readService();
            listaInstitutos = institutoPagina.readInstituto();
            foreach (InstitutoInfo i in listaInstitutos)
            {
                if (currentUser.user_institutos_id.Contains(i.id) && institutosBox.Items.Contains(i.nome) == false)
                {
                    institutosBox.Items.Add(i.nome);
                }
            }
            this.getListaUtitilizadores();
        }

        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            meusInstitutos pagina = new meusInstitutos();
            this.NavigationService.Navigate(pagina);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nomeInstituto = "";
            try
            {
                nomeInstituto = institutosBox.SelectedItem.ToString();
            }
            catch
            {
                nomeInstituto = "";
            }
            atualizarLista(nomeInstituto.Replace(Environment.NewLine, ""));
        }
    }
}
