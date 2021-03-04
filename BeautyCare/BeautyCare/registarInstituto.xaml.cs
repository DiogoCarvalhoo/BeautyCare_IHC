using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
    /// Interaction logic for registarInstituto.xaml
    /// </summary>
    public partial class registarInstituto : Page
    {
        private static UtilizadorInfo currentUser;
        private List<ServiceInfo> listaServicosInseridos = new List<ServiceInfo>();
        private List<ServiceInfo> listaServicos = new List<ServiceInfo>();
        private List<InstitutoInfo> listaInstitutos = new List<InstitutoInfo>();
        private string photopath = "";
        private int idflag = -1;

        public void loadInfo()
        {
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            institutoPage pagina = new institutoPage(-1, this);
            listaInstitutos = pagina.readInstituto();
            servicePage pagina2 = new servicePage(-1, this);
            listaServicos = pagina2.readService();
        }
        public registarInstituto(int id)
        {
            InitializeComponent();
            loadInfo();
            if (id > 0)
            {
                idflag = id;
                foreach (InstitutoInfo i in listaInstitutos)
                {
                    if (i.id == id)
                    {
                        nomeBox.Document.Blocks.Clear();
                        nomeBox.Document.Blocks.Add(new Paragraph(new Run(i.nome)) { Foreground = Brushes.Gray, FontSize = 12 });
                        localizacaoBox.Document.Blocks.Clear();
                        localizacaoBox.Document.Blocks.Add(new Paragraph(new Run(i.localizacao)) { Foreground = Brushes.Gray, FontSize = 12 });
                        contactoBox.Document.Blocks.Clear();
                        contactoBox.Document.Blocks.Add(new Paragraph(new Run(i.telefone)) { Foreground = Brushes.Gray, FontSize = 12 });
                        descricaoBox.Document.Blocks.Clear();
                        descricaoBox.Document.Blocks.Add(new Paragraph(new Run(i.descricao)) { Foreground = Brushes.Gray, FontSize = 12 });
                        if (i.foto != "null")
                        {
                            ImageBrush myBrush = new ImageBrush();
                            Image image = new Image();
                            image.Source = new BitmapImage(
                                new Uri(
                                   BaseUriHelper.GetBaseUri(this), i.foto));
                            myBrush.ImageSource = image.Source;
                            institutofotogrid.Background = myBrush;
                        }
                        foreach (int servico_id in i.servicos_id)
                        {
                            foreach (ServiceInfo s in listaServicos)
                            {
                                if (s.id == servico_id)
                                {
                                    this.listaServicosInseridos.Add(s);
                                }
                            }
                        }
                    }
                }
                atualizarLista();
            }

        }

        public void atualizarLista()
        {
            painel.Children.Clear();
            foreach (ServiceInfo s in listaServicosInseridos)
            {
                Grid novaGrid = new Grid();
                novaGrid.Height = 40;
                ColumnDefinition colDef0 = new ColumnDefinition();
                colDef0.Width = new GridLength(216, GridUnitType.Star);
                ColumnDefinition colDef1 = new ColumnDefinition();
                colDef1.Width = new GridLength(50, GridUnitType.Star);
                ColumnDefinition colDef2 = new ColumnDefinition();
                colDef2.Width = new GridLength(50, GridUnitType.Star);
                novaGrid.ColumnDefinitions.Add(colDef0);
                novaGrid.ColumnDefinitions.Add(colDef1);
                novaGrid.ColumnDefinitions.Add(colDef2);
                TextBlock newTextBox = new TextBlock();
                newTextBox.Text = s.tipo;
                newTextBox.HorizontalAlignment = HorizontalAlignment.Left;
                newTextBox.VerticalAlignment = VerticalAlignment.Center;
                newTextBox.SetValue(Grid.ColumnProperty, 0);
                newTextBox.FontSize = 15;

                nova2 botaoeditar = new nova2();
                botaoeditar.SetValue(Grid.ColumnProperty, 1);
                botaoeditar.Width = 35;
                botaoeditar.VerticalAlignment = VerticalAlignment.Center;
                botaoeditar.HorizontalAlignment = HorizontalAlignment.Center;
                var converter1 = new System.Windows.Media.BrushConverter();
                var b = (Brush)converter1.ConvertFromString("#FF60C4E5");
                botaoeditar.Background = b;
                StackPanel stackpanel1 = new StackPanel();
                stackpanel1.Orientation = Orientation.Horizontal;
                var icon1 = new nova { Kind = PackIconKind.Edit };
                var b2 = (Brush)converter1.ConvertFromString("White");
                icon1.Foreground = b2;
                string indices1 = "";
                char[] array1 = s.tipo.ToCharArray();
                int contador1 = 0;
                foreach (char c in array1)
                {
                    if (c.Equals(' '))
                    {
                        indices1 = indices1 + contador1 + "separador";
                    }
                    contador1 = contador1 + 1;
                }
                icon1.Name = s.tipo.Replace(" ", "") + "firstseparador" + indices1;
                icon1.PreviewMouseLeftButtonDown += EditIcon_PreviewMouseLeftButtonDown;
                icon1.Width = 18;
                icon1.Height = 18;
                botaoeditar.Name = s.tipo.Replace(" ", "") + "firstseparador" + indices1;
                botaoeditar.PreviewMouseLeftButtonDown += EditIcon_PreviewMouseLeftButtonDown;
                stackpanel1.Children.Add(icon1);
                botaoeditar.Content = stackpanel1;

                nova2 botao = new nova2();
                botao.SetValue(Grid.ColumnProperty, 2);
                botao.Width = 35;
                botao.VerticalAlignment = VerticalAlignment.Center;
                botao.HorizontalAlignment = HorizontalAlignment.Center;
                var converter = new System.Windows.Media.BrushConverter();
                var brush = (Brush)converter.ConvertFromString("#FF60C4E5");
                botao.Background = brush;
                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                var icon = new nova { Kind = PackIconKind.Delete };
                var brush2 = (Brush)converter.ConvertFromString("White");
                icon.Foreground = brush2;
                string indices = "";
                char[] array = s.tipo.ToCharArray();
                int contador = 0;
                foreach (char c in array)
                {
                    if (c.Equals(' '))
                    {
                        indices = indices + contador + "separador";
                    }
                    contador = contador + 1;
                }
                icon.Name = s.tipo.Replace(" ", "") + "firstseparador" + indices;
                icon.PreviewMouseLeftButtonDown += BinIcon_PreviewMouseLeftButtonDown;
                icon.Width = 18;
                icon.Height = 18;
                botao.Name = s.tipo.Replace(" ", "") + "firstseparador" + indices;
                botao.PreviewMouseLeftButtonDown += BinIcon_PreviewMouseLeftButtonDown;
                stackpanel.Children.Add(icon);
                botao.Content = stackpanel;
                painel.Children.Add(novaGrid);

                novaGrid.Children.Add(newTextBox);
                novaGrid.Children.Add(botao);
                novaGrid.Children.Add(botaoeditar);
            }
        }

        private void EditIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string tipoServicoRemover = sender.ToString().Replace(Environment.NewLine, "");
            string inicial = tipoServicoRemover.Split("firstseparador")[0];
            string[] indices = tipoServicoRemover.Split("firstseparador")[1].Split("separador");
            foreach (string s in indices)
            {
                try
                {
                    int.TryParse(s, out _);
                    inicial = inicial.Insert(Convert.ToInt32(s), " ");
                }
                catch
                {
                    continue;
                }

            }
            adicionarServico addPage = new adicionarServico(this, idflag, inicial);
            this.NavigationService.Navigate(addPage);
        }


        private void AddServ_Click(object sender, RoutedEventArgs e)
        {
            adicionarServico addPage = new adicionarServico(this, -1, "null");
            this.NavigationService.Navigate(addPage);
        }

        private void AddServ_Click(object sender, MouseButtonEventArgs e)
        {
            adicionarServico addPage = new adicionarServico(this, -1, "null");
            this.NavigationService.Navigate(addPage);
        }

        public void AddServico(String tipo, String fotoPath, String descricao, String preco)
        {
            ServiceInfo novoServico = new ServiceInfo();
            novoServico.descricao = descricao;
            novoServico.tipo = tipo;
            novoServico.preco = preco;
            novoServico.foto = fotoPath;
            this.listaServicosInseridos.Add(novoServico);
            atualizarLista();
        }

        public void AddServico2(ServiceInfo servico_enviado)
        {
            foreach (ServiceInfo s in listaServicosInseridos)
            {
                if (s.instituto_id == idflag && s.id == servico_enviado.id)
                {
                    s.tipo = servico_enviado.tipo;
                    s.preco = servico_enviado.preco;
                    s.descricao = servico_enviado.descricao;
                }
            }
            atualizarLista();
        }

        private void BinIcon_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string tipoServicoRemover = sender.ToString().Replace(Environment.NewLine, "");
            string inicial = tipoServicoRemover.Split("firstseparador")[0];
            string[] indices = tipoServicoRemover.Split("firstseparador")[1].Split("separador");
            foreach (string s in indices)
            {
                try
                {
                    int.TryParse(s, out _);
                    inicial = inicial.Insert(Convert.ToInt32(s), " ");
                }
                catch
                {
                    continue;
                }

            }
            if (idflag < 0)
            {
                foreach (ServiceInfo s in this.listaServicosInseridos.ToArray())
                {
                    if (s.tipo.Equals(inicial))
                    {
                        this.listaServicosInseridos.Remove(s);
                    }
                }
            } 
            else
            {
                foreach (ServiceInfo s in this.listaServicosInseridos.ToArray())
                {
                    if (s.tipo.Equals(inicial))
                    {
                        this.listaServicosInseridos.Remove(s);
                    }
                }
                int servico_a_remover = -1;
                List<String> novoConteudoFicheiro = new List<String>();
                foreach (ServiceInfo s in listaServicos)
                {
                    if (s.tipo.Equals(inicial) && s.instituto_id == idflag)
                    {
                        servico_a_remover = s.id;
                    }
                }
                List<InstitutoInfo> institutos = new List<InstitutoInfo>();
                using (StreamReader reader = new StreamReader("Institutos.txt"))
                {
                    string[] info;
                    string currentline;
                    while ((currentline = reader.ReadLine()) != null)
                    {
                        info = currentline.Split("-");
                        InstitutoInfo institutoLido = new InstitutoInfo();
                        institutoLido.id = Convert.ToInt32(info[0]);
                        if (institutoLido.id == idflag)
                        {
                            info[7] = info[7].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string ii in (info[7].Split(",")))
                            {
                                if (int.TryParse(ii, out _))
                                {
                                    if (Convert.ToInt32(ii) != servico_a_remover)
                                    {
                                        institutoLido.servicos_id.Add(Convert.ToInt32(ii));
                                    }
                                }
                            }
                        }
                        else
                        {
                            info[7] = info[7].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string ii in (info[7].Split(",")))
                            {
                                if (int.TryParse(ii, out _))
                                {                                    
                                        institutoLido.servicos_id.Add(Convert.ToInt32(ii));
                                }
                            }
                        }
                            institutoLido.rating = info[1];
                            institutoLido.foto = info[2];
                            institutoLido.nome = info[3];
                            institutoLido.descricao = info[4];
                            institutoLido.localizacao = info[5];
                            institutoLido.telefone = info[6];
                            info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string s in (info[8].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.dias.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[9].Split(",")))
                            {
                                if (s != "")
                                {
                                    institutoLido.horas.Add(s);
                                }
                            }
                            string registo = institutoLido.id + "-" + institutoLido.rating + "-" + institutoLido.foto + "-" + institutoLido.nome + "-" + institutoLido.descricao + "-" + institutoLido.localizacao + "-" + institutoLido.telefone + "-{";
                            foreach (int aa in institutoLido.servicos_id)
                            {
                                Console.WriteLine(aa);
                                registo = registo + aa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (int aaa in institutoLido.dias)
                            {
                                registo = registo + aaa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (String ss in institutoLido.horas)
                            {
                                registo = registo + ss + ",";
                            }
                            registo = registo + "}";
                            novoConteudoFicheiro.Add(registo);
                    }
                }
                registarAlteracoesInstitutos(novoConteudoFicheiro);

                novoConteudoFicheiro.Clear();
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
                        if (servico.id != servico_a_remover)
                        {
                            servico.tipo = info[1];
                            servico.foto = info[2];
                            servico.instituto_id = Convert.ToInt32(info[3]);
                            servico.instituto_nome = info[4];
                            servico.descricao = info[5];
                            servico.preco = info[6];
                            servicos.Add(servico);
                            string registo = servico.id + "-" + servico.tipo + "-" + servico.foto + "-" + servico.instituto_id + "-" + servico.instituto_nome + "-" + servico.descricao + "-" + servico.preco;
                            novoConteudoFicheiro.Add(registo);
                        }
                    }
                }
                registarAlteracoesServicos(novoConteudoFicheiro);

                novoConteudoFicheiro.Clear();
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
                        info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                        foreach (string s in info[9].Split(","))
                        {
                            if (int.TryParse(s, out _))
                                {
                                    utilizador.user_institutos_id.Add(Convert.ToInt32(s));
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
                                if (!(servico_a_remover == Convert.ToInt32(s)))
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
                                if (!(servico_a_remover == Convert.ToInt32(s.Split("|")[0])))
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
                        novoConteudoFicheiro.Add(registo);

                    }
                }
                registarUtilizadores(novoConteudoFicheiro);
            }
            atualizarLista();
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
        private void ChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                string sFileName = openFileDialog.FileName;
                string fileName = sFileName.Split("\\")[sFileName.Split("\\").Length - 1];
                string destPath = System.AppDomain.CurrentDomain.BaseDirectory;
                destPath = destPath.Replace("\\bin\\Debug\\netcoreapp3.1\\", "\\Images\\institutophotos");
                System.IO.File.Copy(sFileName, System.IO.Path.Combine(destPath, fileName), true);
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(sFileName));
                myBrush.ImageSource = image.Source;
                this.photopath = "Images/institutophotos/" + fileName;
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            meusInstitutos page = new meusInstitutos();
            this.NavigationService.Navigate(page);
        }

        private void CancelarButton_Click(object sender, MouseButtonEventArgs e)
        {
            meusInstitutos page = new meusInstitutos();
            this.NavigationService.Navigate(page);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            loadInfo();
            List<String> novoConteudoFicheiro = new List<String>();
            if (idflag < 0)
            {
                novoConteudoFicheiro.Clear();
                InstitutoInfo instituto = new InstitutoInfo();
                var contacto = new TextRange(contactoBox.Document.ContentStart, contactoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var doublevar = 1.1;
                if (contacto.Length == 9)
                {
                    try
                    {
                        doublevar = Convert.ToDouble(contacto);
                    }
                    catch
                    {
                        MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                    return;
                }
                instituto.rating = "2,5/5";
                var nome = new TextRange(nomeBox.Document.ContentStart, nomeBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var loca = new TextRange(localizacaoBox.Document.ContentStart, localizacaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var desc = new TextRange(descricaoBox.Document.ContentStart, descricaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                if (nome.Equals("") || loca.Equals("") || desc.Equals(""))
                {
                    MessageBox.Show("Name, location and description should be filled");
                    return;
                }
                else
                {
                    instituto.nome = nome;
                    instituto.localizacao = loca;
                    instituto.descricao = desc;
                    instituto.telefone = contacto;
                }
                if (photopath.Equals(""))
                {
                    photopath = "null";
                }
                instituto.foto = photopath;
                foreach (ServiceInfo s in listaServicosInseridos)
                {
                    s.instituto_id = instituto.id;
                    s.instituto_nome = instituto.nome;
                    instituto.servicos_id.Add(s.id);
                    using (StreamWriter sw = File.AppendText("Servicos.txt"))
                    {
                        string registo = s.id + "-" + s.tipo + "-" + s.foto + "-" + s.instituto_id + "-" + s.instituto_nome + "-" + s.descricao + "-" + s.preco;
                        sw.WriteLine(registo);
                    }
                }
                instituto.horas.Add("12:00");
                instituto.horas.Add("13:00");
                instituto.horas.Add("14:00");
                instituto.horas.Add("15:00");
                instituto.horas.Add("16:00");
                instituto.horas.Add("17:00");
                instituto.dias.Add(05);
                instituto.dias.Add(06);
                instituto.dias.Add(07);
                instituto.dias.Add(08);
                instituto.dias.Add(09);
                using (StreamWriter sw = File.AppendText("Institutos.txt"))
                {
                    string registo = instituto.id + "-" + instituto.rating + "-" + instituto.foto + "-" + instituto.nome + "-" + instituto.descricao + "-" + instituto.localizacao + "-" + instituto.telefone + "-{";
                    foreach (int i in instituto.servicos_id)
                    {
                        registo = registo + i + ",";
                    }
                    registo = registo + "}-{";
                    foreach (int s in instituto.dias)
                    {
                        registo = registo + s + ",";
                    }
                    registo = registo + "}-{";
                    foreach (String s in instituto.horas)
                    {
                        registo = registo + s + ",";
                    }
                    registo = registo + "}";
                    sw.WriteLine(registo);
                }
                RegisterPage registerpageobject = new RegisterPage();
                List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == currentUser.id)
                    {
                        u.user_institutos_id.Add(Convert.ToInt32(instituto.id));
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
                novoConteudoFicheiro.Clear();
            }
            else
            {
                novoConteudoFicheiro.Clear();
                InstitutoInfo instituto = new InstitutoInfo();
                foreach (InstitutoInfo i in listaInstitutos)
                {
                    if (i.id == idflag)
                    {
                        instituto = i;
                    }
                }
                var contacto = new TextRange(contactoBox.Document.ContentStart, contactoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var doublevar = 1.1;
                if (contacto.Length == 9)
                {
                    try
                    {
                        doublevar = Convert.ToDouble(contacto);
                    }
                    catch
                    {
                        MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                    return;
                }
                instituto.rating = "2,5/5";
                var nome = new TextRange(nomeBox.Document.ContentStart, nomeBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var loca = new TextRange(localizacaoBox.Document.ContentStart, localizacaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var desc = new TextRange(descricaoBox.Document.ContentStart, descricaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                if (nome.Equals("") || loca.Equals("") || desc.Equals(""))
                {
                    MessageBox.Show("Name, location and description should be filled");
                    return;
                }
                else
                {
                    instituto.nome = nome;
                    instituto.localizacao = loca;
                    instituto.descricao = desc;
                    instituto.telefone = contacto;
                }

                foreach (ServiceInfo s in listaServicosInseridos)
                {
                    bool novo = true;
                    foreach (ServiceInfo s2 in listaServicos)
                    {
                        if (s2.id == s.id)
                        {
                            novo = false;
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
                                    if (servico.id == s.id)
                                    {
                                        servico.tipo = s.tipo;
                                        servico.foto = s.foto;
                                        servico.instituto_id = s.instituto_id;
                                        servico.instituto_nome = s.instituto_nome;
                                        servico.descricao = s.descricao;
                                        servico.preco = s.preco;
                                        servicos.Add(servico);
                                    }
                                    else
                                    {
                                        servico.tipo = info[1];
                                        servico.foto = info[2];
                                        servico.instituto_id = Convert.ToInt32(info[3]);
                                        servico.instituto_nome = info[4];
                                        servico.descricao = info[5];
                                        servico.preco = info[6];
                                        servicos.Add(servico);
                                    }
                                    string registo = servico.id + "-" + servico.tipo + "-" + servico.foto + "-" + servico.instituto_id + "-" + servico.instituto_nome + "-" + servico.descricao + "-" + servico.preco;
                                    novoConteudoFicheiro.Add(registo);
                                }
                            }
                            registarAlteracoesServicos(novoConteudoFicheiro);
                            novoConteudoFicheiro.Clear();
                        }
                    }
                    if (novo == true)
                    {
                        s.instituto_id = instituto.id;
                        s.instituto_nome = instituto.nome;
                        instituto.servicos_id.Add(s.id);
                        using (StreamWriter sw = File.AppendText("Servicos.txt"))
                        {
                            string registo = s.id + "-" + s.tipo + "-" + s.foto + "-" + s.instituto_id + "-" + s.instituto_nome + "-" + s.descricao + "-" + s.preco;
                            sw.WriteLine(registo);
                        }
                    }
                }
                novoConteudoFicheiro.Clear();
                List<InstitutoInfo> institutos = new List<InstitutoInfo>();
                using (StreamReader reader = new StreamReader("Institutos.txt"))
                {
                    string[] info;
                    string currentline;
                    while ((currentline = reader.ReadLine()) != null)
                    {
                        info = currentline.Split("-");
                        InstitutoInfo institutoLido = new InstitutoInfo();
                        institutoLido.id = Convert.ToInt32(info[0]);
                        if (institutoLido.id == instituto.id)
                        {
                            institutoLido.rating = info[1];
                            institutoLido.foto = instituto.foto;
                            institutoLido.nome = instituto.nome;
                            institutoLido.descricao = instituto.descricao;
                            institutoLido.localizacao = instituto.localizacao;
                            institutoLido.telefone = instituto.telefone;
                            institutoLido.servicos_id = instituto.servicos_id;
                            info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string s in (info[8].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.dias.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[9].Split(",")))
                            {
                                if (s != "")
                                {
                                    institutoLido.horas.Add(s);
                                }
                            }
                            string registo = institutoLido.id + "-" + institutoLido.rating + "-" + institutoLido.foto + "-" + institutoLido.nome + "-" + institutoLido.descricao + "-" + institutoLido.localizacao + "-" + institutoLido.telefone + "-{";
                            foreach (int aa in institutoLido.servicos_id)
                            {
                                registo = registo + aa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (int aaa in institutoLido.dias)
                            {
                                registo = registo + aaa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (String ss in institutoLido.horas)
                            {
                                registo = registo + ss + ",";
                            }
                            registo = registo + "}";
                            novoConteudoFicheiro.Add(registo);
                        }
                        else
                        {
                            institutoLido.rating = info[1];
                            institutoLido.foto = info[2];
                            institutoLido.nome = info[3];
                            institutoLido.descricao = info[4];
                            institutoLido.localizacao = info[5];
                            institutoLido.telefone = info[6];
                            info[7] = info[7].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string s in (info[7].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.servicos_id.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[8].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.dias.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[9].Split(",")))
                            {
                                if (s != "")
                                {
                                    institutoLido.horas.Add(s);
                                }
                            }
                            string registo = institutoLido.id + "-" + institutoLido.rating + "-" + institutoLido.foto + "-" + institutoLido.nome + "-" + institutoLido.descricao + "-" + institutoLido.localizacao + "-" + institutoLido.telefone + "-{";
                            foreach (int aa in institutoLido.servicos_id)
                            {
                                registo = registo + aa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (int aaa in institutoLido.dias)
                            {
                                registo = registo + aaa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (String ss in institutoLido.horas)
                            {
                                registo = registo + ss + ",";
                            }
                            registo = registo + "}";
                            novoConteudoFicheiro.Add(registo);
                        }
                    }
                }
                registarAlteracoesInstitutos(novoConteudoFicheiro);

            }
            meusInstitutos pagina = new meusInstitutos();
            this.NavigationService.Navigate(pagina);
        }

        private void ConfirmButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loadInfo();
            List<String> novoConteudoFicheiro = new List<String>();
            if (idflag < 0)
            {
                novoConteudoFicheiro.Clear();
                InstitutoInfo instituto = new InstitutoInfo();
                var contacto = new TextRange(contactoBox.Document.ContentStart, contactoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var doublevar = 1.1;
                if (contacto.Length == 9)
                {
                    try
                    {
                        doublevar = Convert.ToDouble(contacto);
                    }
                    catch
                    {
                        MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                        return;
                    }
                } 
                else
                {
                    MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                    return;
                }
                instituto.rating = "2,5/5";
                var nome = new TextRange(nomeBox.Document.ContentStart, nomeBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var loca = new TextRange(localizacaoBox.Document.ContentStart, localizacaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var desc = new TextRange(descricaoBox.Document.ContentStart, descricaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                if (nome.Equals("") || loca.Equals("") || desc.Equals(""))
                {
                    MessageBox.Show("Name, location and description should be field");
                    return;
                }
                else
                {
                    instituto.nome = nome;
                    instituto.localizacao = loca;
                    instituto.descricao = desc;
                    instituto.telefone = contacto;
                }


                if (photopath.Equals(""))
                {
                    photopath = "null";
                }
                instituto.foto = photopath;
                foreach (ServiceInfo s in listaServicosInseridos)
                {
                    s.instituto_id = instituto.id;
                    s.instituto_nome = instituto.nome;
                    instituto.servicos_id.Add(s.id);
                    using (StreamWriter sw = File.AppendText("Servicos.txt"))
                    {
                        string registo = s.id + "-" + s.tipo + "-" + s.foto + "-" + s.instituto_id + "-" + s.instituto_nome + "-" + s.descricao + "-" + s.preco;
                        sw.WriteLine(registo);
                    }
                }
                instituto.horas.Add("12:00");
                instituto.horas.Add("13:00");
                instituto.horas.Add("14:00");
                instituto.horas.Add("15:00");
                instituto.horas.Add("16:00");
                instituto.horas.Add("17:00");
                instituto.dias.Add(05);
                instituto.dias.Add(06);
                instituto.dias.Add(07);
                instituto.dias.Add(08);
                instituto.dias.Add(09);
                using (StreamWriter sw = File.AppendText("Institutos.txt"))
                {
                    string registo = instituto.id + "-" + instituto.rating + "-" + instituto.foto + "-" + instituto.nome + "-" + instituto.descricao + "-" + instituto.localizacao + "-" + instituto.telefone + "-{";
                    foreach (int i in instituto.servicos_id)
                    {
                        registo = registo + i + ",";
                    }
                    registo = registo + "}-{";
                    foreach (int s in instituto.dias)
                    {
                        registo = registo + s + ",";
                    }
                    registo = registo + "}-{";
                    foreach (String s in instituto.horas)
                    {
                        registo = registo + s + ",";
                    }
                    registo = registo + "}";
                    sw.WriteLine(registo);
                }
                RegisterPage registerpageobject = new RegisterPage();
                List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == currentUser.id)
                    {
                        u.user_institutos_id.Add(Convert.ToInt32(instituto.id));
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
                novoConteudoFicheiro.Clear();
            }
            else
            {
                novoConteudoFicheiro.Clear();
                InstitutoInfo instituto = new InstitutoInfo();
                foreach (InstitutoInfo i in listaInstitutos)
                {
                    if (i.id == idflag)
                    {
                        instituto = i;
                    }
                }
                var contacto = new TextRange(contactoBox.Document.ContentStart, contactoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var doublevar = 1.1;
                if (contacto.Length == 9)
                {
                    try
                    {
                        doublevar = Convert.ToDouble(contacto);
                    }
                    catch
                    {
                        MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please provide valid telefone input value (9 Digit Number)!");
                    return;
                }
                instituto.rating = "2,5/5";
                var nome = new TextRange(nomeBox.Document.ContentStart, nomeBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var loca = new TextRange(localizacaoBox.Document.ContentStart, localizacaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                var desc = new TextRange(descricaoBox.Document.ContentStart, descricaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
                if (nome.Equals("") || loca.Equals("") || desc.Equals(""))
                {
                    MessageBox.Show("Name, location and description should be field");
                    return;
                }
                else
                {
                    instituto.nome = nome;
                    instituto.localizacao = loca;
                    instituto.descricao = desc;
                    instituto.telefone = contacto;
                }
                foreach (ServiceInfo s in listaServicosInseridos)
                {
                    bool novo = true;
                    foreach (ServiceInfo s2 in listaServicos)
                    {
                        if (s2.id == s.id)
                        {
                            novo = false;
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
                                    if (servico.id == s.id)
                                    {
                                        servico.tipo = s.tipo;
                                        servico.foto = s.foto;
                                        servico.instituto_id = s.instituto_id;
                                        servico.instituto_nome = s.instituto_nome;
                                        servico.descricao = s.descricao;
                                        servico.preco = s.preco;
                                        servicos.Add(servico);
                                    }
                                    else
                                    {
                                        servico.tipo = info[1];
                                        servico.foto = info[2];
                                        servico.instituto_id = Convert.ToInt32(info[3]);
                                        servico.instituto_nome = info[4];
                                        servico.descricao = info[5];
                                        servico.preco = info[6];
                                        servicos.Add(servico);
                                    }
                                    string registo = servico.id + "-" + servico.tipo + "-" + servico.foto + "-" + servico.instituto_id + "-" + servico.instituto_nome + "-" + servico.descricao + "-" + servico.preco;
                                    novoConteudoFicheiro.Add(registo);
                                }
                            }
                            registarAlteracoesServicos(novoConteudoFicheiro);
                            novoConteudoFicheiro.Clear();
                        }
                    }
                    if (novo == true)
                    {
                        s.instituto_id = instituto.id;
                        s.instituto_nome = instituto.nome;
                        instituto.servicos_id.Add(s.id);
                        using (StreamWriter sw = File.AppendText("Servicos.txt"))
                        {
                            string registo = s.id + "-" + s.tipo + "-" + s.foto + "-" + s.instituto_id + "-" + s.instituto_nome + "-" + s.descricao + "-" + s.preco;
                            sw.WriteLine(registo);
                        }
                    }
                }
                novoConteudoFicheiro.Clear();
                List<InstitutoInfo> institutos = new List<InstitutoInfo>();
                using (StreamReader reader = new StreamReader("Institutos.txt"))
                {
                    string[] info;
                    string currentline;
                    while ((currentline = reader.ReadLine()) != null)
                    {
                        info = currentline.Split("-");
                        InstitutoInfo institutoLido = new InstitutoInfo();
                        institutoLido.id = Convert.ToInt32(info[0]);
                        if (institutoLido.id == instituto.id)
                        {
                            institutoLido.rating = info[1];
                            institutoLido.foto = instituto.foto;
                            institutoLido.nome = instituto.nome;
                            institutoLido.descricao = instituto.descricao;
                            institutoLido.localizacao = instituto.localizacao;
                            institutoLido.telefone = instituto.telefone;
                            institutoLido.servicos_id = instituto.servicos_id;
                            info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string s in (info[8].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.dias.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[9].Split(",")))
                            {
                                if (s != "")
                                {
                                    institutoLido.horas.Add(s);
                                }
                            }
                            string registo = institutoLido.id + "-" + institutoLido.rating + "-" + institutoLido.foto + "-" + institutoLido.nome + "-" + institutoLido.descricao + "-" + institutoLido.localizacao + "-" + institutoLido.telefone + "-{";
                            foreach (int aa in institutoLido.servicos_id)
                            {
                                registo = registo + aa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (int aaa in institutoLido.dias)
                            {
                                registo = registo + aaa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (String ss in institutoLido.horas)
                            {
                                registo = registo + ss + ",";
                            }
                            registo = registo + "}";
                            novoConteudoFicheiro.Add(registo);
                        }
                        else
                        {
                            institutoLido.rating = info[1];
                            institutoLido.foto = info[2];
                            institutoLido.nome = info[3];
                            institutoLido.descricao = info[4];
                            institutoLido.localizacao = info[5];
                            institutoLido.telefone = info[6];
                            info[7] = info[7].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[8] = info[8].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            info[9] = info[9].Replace("{", "").Replace("}", "").Replace(Environment.NewLine, "");
                            foreach (string s in (info[7].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.servicos_id.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[8].Split(",")))
                            {
                                if (int.TryParse(s, out _))
                                {
                                    institutoLido.dias.Add(Convert.ToInt32(s));
                                }
                            }
                            foreach (string s in (info[9].Split(",")))
                            {
                                if (s != "")
                                {
                                    institutoLido.horas.Add(s);
                                }
                            }
                            string registo = institutoLido.id + "-" + institutoLido.rating + "-" + institutoLido.foto + "-" + institutoLido.nome + "-" + institutoLido.descricao + "-" + institutoLido.localizacao + "-" + institutoLido.telefone + "-{";
                            foreach (int aa in institutoLido.servicos_id)
                            {
                                registo = registo + aa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (int aaa in institutoLido.dias)
                            {
                                registo = registo + aaa + ",";
                            }
                            registo = registo + "}-{";
                            foreach (String ss in institutoLido.horas)
                            {
                                registo = registo + ss + ",";
                            }
                            registo = registo + "}";
                            novoConteudoFicheiro.Add(registo);
                        }
                    }
                }
                registarAlteracoesInstitutos(novoConteudoFicheiro);

            }
            meusInstitutos pagina = new meusInstitutos();
            this.NavigationService.Navigate(pagina);
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

        public void registarAlteracoesServicos(List<String> servicos)
        {
            using (StreamWriter sw = File.CreateText("Servicos.txt"))
            {
                foreach (String s in servicos)
                {
                    sw.WriteLine(s);
                }
            }
        }

        public void registarAlteracoesInstitutos(List<String> institutos)
        {
            using (StreamWriter sw = File.CreateText("Institutos.txt"))
            {
                foreach (String s in institutos)
                {
                    sw.WriteLine(s);
                }
            }
        }
    }
}

