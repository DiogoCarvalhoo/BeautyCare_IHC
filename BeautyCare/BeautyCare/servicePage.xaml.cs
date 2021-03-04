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
    /// Interaction logic for servicePage.xaml
    /// </summary>
    public partial class servicePage : Page
    {
        public static UtilizadorInfo currentUser =  new UtilizadorInfo();
        public ServiceInfo currentService = new ServiceInfo();
        public static InstitutoInfo respectiveInstitute;
        public Page paginaAnterior;
        int flag = 0;
        public servicePage(int id, Page paginaanterior)
        {
            
            InitializeComponent();
            if (id == -1)
            {
                return;
            }
            paginaAnterior = paginaanterior;
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            List<ServiceInfo> servicos = readService();
            foreach (ServiceInfo s in servicos)
            {
                if (s.id == id)
                {
                    currentService = s;
                }
            }
            descricaoServico.Document.Blocks.Clear();
            descricaoServico.Document.Blocks.Add(new Paragraph(new Run(currentService.descricao)) { FontSize = 16 });

            preco.Document.Blocks.Clear();
            preco.Document.Blocks.Add(new Paragraph(new Run(currentService.preco)) { FontSize = 18 });

            nomeservico.Content = currentService.tipo;
            //nomeservico.Document.Blocks.Clear();
            //nomeservico.Document.Blocks.Add(new Paragraph(new Run(currentService.tipo)) { FontSize = 18 });

            nomeinstituto.Document.Blocks.Clear();
            nomeinstituto.Document.Blocks.Add(new Paragraph(new Run(currentService.instituto_nome)) { FontSize = 18 });

            if (currentService.foto != "null")
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(
                       BaseUriHelper.GetBaseUri(this), currentService.foto));
                myBrush.ImageSource = image.Source;
                userfotogrid.Background = myBrush;
            }

            data.Items.Insert(0, "Data");
            data.SelectedIndex = 0;
            hora.Items.Insert(0, "Hora");
            hora.SelectedIndex = 0;

            institutoPage InstitutoPage = new institutoPage(-1, this);
            List<InstitutoInfo> institutos = InstitutoPage.readInstituto();
            foreach (InstitutoInfo i in institutos)
            {
                if (i.id == currentService.instituto_id)
                {
                    respectiveInstitute = i;
                }
            }

            foreach (int dia in respectiveInstitute.dias)
            {
                data.Items.Insert(data.Items.Count, dia);
            }
            foreach (string h in respectiveInstitute.horas)
            {
                hora.Items.Insert(hora.Items.Count, h);
            }

            foreach (int favoritoid in currentUser.favoritos)
            {
                if (favoritoid == currentService.id)
                {
                    favoritos.Kind = MaterialDesignThemes.Wpf.PackIconKind.Star;
                    flag++;
                }
            }

        }

        public ServiceInfo getCurrentService()
        {
            return currentService;
        }
        public List<ServiceInfo> readService()
        {
            List<ServiceInfo> servicos = new List<ServiceInfo>();
            using (StreamReader reader = new StreamReader("Servicos.txt"))
            {
                string[] info;
                string currentline;
                while ((currentline = reader.ReadLine()) != null)
                {
                    info = currentline.Split("-");
                    ServiceInfo servico = new ServiceInfo();
                    servico.id = Convert.ToInt32(info[0]);
                    servico.tipo = info[1];
                    servico.foto = info[2];
                    servico.instituto_id = Convert.ToInt32(info[3]);
                    servico.instituto_nome = info[4];
                    servico.descricao = info[5];
                    servico.preco = info[6];
                    servicos.Add(servico);
                }
            }
            return servicos;
        }

        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            //HomePage_Client homeclientPage = new HomePage_Client();
            this.NavigationService.Navigate(paginaAnterior);
        }

        private void ButtonAddFav_Click(object sender, RoutedEventArgs e)
        {
            if (flag % 2 == 0)
            {
                favoritos.Kind = MaterialDesignThemes.Wpf.PackIconKind.Star;
                flag++;
                List<String> novoConteudoFicheiro = new List<String>();
                RegisterPage registerpageobject = new RegisterPage();
                List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == currentUser.id)
                    {
                        u.favoritos.Add(currentService.id);
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
            }
            else
            {
                favoritos.Kind = MaterialDesignThemes.Wpf.PackIconKind.StarBorder;
                flag++;
                List<String> novoConteudoFicheiro = new List<String>();
                RegisterPage registerpageobject = new RegisterPage();
                List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == currentUser.id)
                    {
                        u.favoritos.Remove(currentService.id);
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

        private void PackIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            institutoPage institutoPage = new institutoPage(currentService.instituto_id, this);
            this.NavigationService.Navigate(institutoPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            if (data.SelectedIndex != 0 && hora.SelectedIndex != 0)
            {
                List<String> novoConteudoFicheiro = new List<String>();
                RegisterPage registerpageobject = new RegisterPage();
                List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == currentUser.id)
                    {
                        Console.WriteLine(currentService.id);
                        u.reservas.Add(currentService.id + "|" + hora.SelectedItem + "|" + data.SelectedItem);
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
                HomePage_Client homePage = new HomePage_Client();
                this.NavigationService.Navigate(homePage);
            }
        }
    }
}


public class ServiceInfo
{
    public static int k = 50;
    public int id { get; set; }
    public string tipo { get; set; }
    public string foto { get; set; }
    public int instituto_id { get; set; }
    public string instituto_nome { get; set; }
    public string descricao { get; set; }
    public string preco { get; set; }

    public ServiceInfo()
    {
        this.id = k;
        k = k + 1;
    }
    public override string ToString()
    {
        return "Utilizador: " + id + " " + tipo + " " + foto + " " + instituto_id + " " + instituto_nome + " " + descricao + " " + preco + ";";
    }
}