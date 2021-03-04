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
    /// Interaction logic for institutoPage.xaml
    /// </summary>
    public partial class institutoPage : Page
    {
        public static InstitutoInfo currentInstituto;
        public static UtilizadorInfo currentUser;
        public Page paginaAnterior;
        public institutoPage(int id, Page paginaanterior)
        {
            if (id == -1)
            {
                return;
            }
            InitializeComponent();
            paginaAnterior = paginaanterior;
            Console.WriteLine(paginaAnterior);
            List<InstitutoInfo> institutos = readInstituto();
            foreach (InstitutoInfo i in institutos)
            {
                if (i.id == id)
                {
                    currentInstituto = i;
                }
            }

            nomeInstituto.Content = currentInstituto.nome;
            rating.Content = currentInstituto.rating;

            descricaoServico.Document.Blocks.Clear();
            descricaoServico.Document.Blocks.Add(new Paragraph(new Run(currentInstituto.descricao)) { FontSize = 18 });

            if (currentInstituto.foto != "null")
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(
                       BaseUriHelper.GetBaseUri(this), currentInstituto.foto));
                myBrush.ImageSource = image.Source;
                userfotogrid.Background = myBrush;
            }

            int flag = 0;

            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();

            foreach (int service_id in currentInstituto.servicos_id)
            {
                servicePage ServicePage = new servicePage(service_id,paginaAnterior);
                ServiceInfo servico = ServicePage.getCurrentService();
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
                    if (i == service_id)
                    {
                        icon = new nova { Kind = PackIconKind.Star };
                    }
                }
                icon.Width = 30;
                icon.Height = 45;
                icon.SetValue(Grid.ColumnProperty, 1);
                icon.Name = "seta" + service_id;
                icon.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                nova2 novo = new nova2();
                novo.SetValue(Grid.ColumnProperty, 1);
                novo.Width = 30;
                novo.Height = 45;
                novo.Opacity = 0;
                novaGrid.Children.Add(novo);
                novo.Name = "seta" + service_id;
                novo.PreviewMouseLeftButtonDown += FavIcon_PreviewMouseLeftButtonDown;
                TextBlock newTextBox = new TextBlock();
                newTextBox.Text = servico.tipo;
                newTextBox.VerticalAlignment = VerticalAlignment.Center;
                newTextBox.FontSize = 18;
                newTextBox.Margin = new Thickness(10, 10, 0, 0);
                newTextBox.SetValue(Grid.ColumnProperty, 2);
                TextBlock newTextBox2 = new TextBlock();
                newTextBox2.Text = servico.preco + "  €";
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
                icon2.Name = "seta" + service_id;
                icon2.PreviewMouseLeftButtonDown += PackIcon_PreviewMouseLeftButtonDown;
                painel.Children.Add(novaGrid);
                novaGrid.Children.Add(newTextBox);
                novaGrid.Children.Add(newTextBox2);
                novaGrid.Children.Add(icon);
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
            Console.WriteLine(paginaAnterior);
        }



        private void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string id = sender.ToString().Replace("seta", "");
            servicePage paginaservico = new servicePage(Convert.ToInt32(id), this);
            this.NavigationService.Navigate(paginaservico);
        }


        private void FavIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
                        if (i==id)
                        {
                            adicionar = false;
                        }
                    }
                    if (adicionar == true)
                    {
                        u.favoritos.Add(id);
                    } else
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
            institutoPage nova = new institutoPage(currentInstituto.id, paginaAnterior);
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


        public List<InstitutoInfo> readInstituto()
        {
            List<InstitutoInfo> institutos = new List<InstitutoInfo>();
            using (StreamReader reader = new StreamReader("Institutos.txt"))
            {
                string[] info;
                string currentline;
                while ((currentline = reader.ReadLine()) != null)
                {
                    info = currentline.Split("-");
                    InstitutoInfo instituto = new InstitutoInfo();
                    instituto.id = Convert.ToInt32(info[0]);
                    instituto.rating = info[1];
                    instituto.foto = info[2];
                    instituto.nome = info[3];
                    instituto.descricao = info[4];
                    instituto.localizacao = info[5];
                    instituto.telefone = info[6];
                    info[7] = info[7].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                    foreach (string s in (info[7].Split(",")))
                    {
                        if (int.TryParse(s, out _))
                        {
                            instituto.servicos_id.Add(Convert.ToInt32(s));
                        }
                    }
                    foreach (string s in (info[8].Split(",")))
                    {
                        if (int.TryParse(s, out _))
                        {
                            instituto.dias.Add(Convert.ToInt32(s));
                        }
                    }
                    foreach (string s in (info[9].Split(",")))
                    {
                        if (s != "")
                        {
                            instituto.horas.Add(s);
                        }
                    }
                    institutos.Add(instituto);
                }
            }
            return institutos;
        }





        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            //HomePage_Client homeclientPage = new HomePage_Client();
            this.NavigationService.Navigate(paginaAnterior);
        }


    }
}

public class InstitutoInfo
{
    public InstitutoInfo()
    {
        this.servicos_id = new List<int>();
        this.dias = new List<int>();
        this.horas = new List<string>();
        this.id = k;
        k = k + 1;
    }
    public static int k = 50;
    public int id { get; set; }
    public string rating { get; set; }
    public string foto { get; set; }
    public string nome { get; set; }
    public string descricao { get; set; }
    public string localizacao { get; set; }
    public string telefone { get; set; }
    public List<int> servicos_id { get; set; }
    public List<int> dias { get; set; }
    public List<string> horas { get; set; }
    public override string ToString()
    {
        return "Utilizador: " + id + " " + rating + " " + foto + " " + nome + " " + descricao + " " + localizacao + " " + telefone + ";";
    }
}


public class nova2:Button
{
    public override string ToString()
    {
        return this.Name;
    }
}