using Microsoft.Win32;
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
    /// Interaction logic for adicionarServico.xaml
    /// </summary>
    public partial class adicionarServico : Page
    {
        private List<ServiceInfo> listaServicos = new List<ServiceInfo>();
        private List<InstitutoInfo> listaInstitutos = new List<InstitutoInfo>();
        private int id_instituto = -1;
        private string nomeServico = "null";
        private ServiceInfo currentServico = new ServiceInfo();

        private registarInstituto institutoPage;
        private string photopath = "";
        private string tipoServico;
        private string descricao;
        private string preco;

        public void loadInfo()
        {
            institutoPage pagina = new institutoPage(-1, this);
            listaInstitutos = pagina.readInstituto();
            servicePage pagina2 = new servicePage(-1, this);
            listaServicos = pagina2.readService();

        }
        public adicionarServico(registarInstituto pagina, int id_instituto, string nomeServico)
        {
            InitializeComponent();
            institutoPage = pagina;
            loadInfo();
            if (id_instituto > 0)
            {
                this.id_instituto = id_instituto;
                this.nomeServico = nomeServico;
                foreach (InstitutoInfo i in listaInstitutos)
                {
                    if (i.id == id_instituto)
                    {
                        foreach (ServiceInfo s in listaServicos)
                        {
                            if (s.tipo.Equals(nomeServico) && s.instituto_id == id_instituto)
                            {
                                currentServico = s;
                                nomeBox.Document.Blocks.Clear();
                                nomeBox.Document.Blocks.Add(new Paragraph(new Run(currentServico.tipo)) { Foreground = Brushes.Gray, FontSize = 12 });
                                precoBox.Document.Blocks.Clear();
                                precoBox.Document.Blocks.Add(new Paragraph(new Run(currentServico.preco)) { Foreground = Brushes.Gray, FontSize = 12 });
                                descricaoBox.Document.Blocks.Clear();
                                descricaoBox.Document.Blocks.Add(new Paragraph(new Run(currentServico.descricao)) { Foreground = Brushes.Gray, FontSize = 12 });
                                if (currentServico.foto != "null")
                                {
                                    ImageBrush myBrush = new ImageBrush();
                                    Image image = new Image();
                                    image.Source = new BitmapImage(
                                        new Uri(
                                           BaseUriHelper.GetBaseUri(this), currentServico.foto));
                                    myBrush.ImageSource = image.Source;
                                    servicofotogrid.Background = myBrush;
                                }
                            }
                        }
                    }
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
                destPath = destPath.Replace("\\bin\\Debug\\netcoreapp3.1\\", "\\Images\\servicephotos");
                System.IO.File.Copy(sFileName, System.IO.Path.Combine(destPath, fileName), true);
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(sFileName));
                myBrush.ImageSource = image.Source;
                this.photopath = "Images/servicephotos/" + fileName;
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)                                          
        {
            var tipo = new TextRange(nomeBox.Document.ContentStart, nomeBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
            var pre = new  TextRange(precoBox.Document.ContentStart, precoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
            var desc = new TextRange(descricaoBox.Document.ContentStart, descricaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
            if (tipo.Equals("") || pre.Equals("") || desc.Equals(""))
            {
                MessageBox.Show("Type, Price and description should be filled");
                return;
            }
            else
            {
                this.tipoServico = tipo;
                this.preco = pre;
                this.descricao = desc;
            }

            if (photopath.Equals(""))
            {
                photopath = "null";
            }
            if (id_instituto > 0)
            {
                currentServico.tipo = tipoServico;
                currentServico.preco = preco;
                currentServico.descricao = descricao;
                institutoPage.AddServico2(currentServico);
            } else
            {
                institutoPage.AddServico(this.tipoServico, this.photopath, this.descricao, this.preco);
            }
            this.NavigationService.Navigate(institutoPage);
        }

        private void ConfirmButton_Click(object sender, MouseButtonEventArgs e)
        {
            var tipo = new TextRange(nomeBox.Document.ContentStart, nomeBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
            var pre = new TextRange(precoBox.Document.ContentStart, precoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
            var desc = new TextRange(descricaoBox.Document.ContentStart, descricaoBox.Document.ContentEnd).Text.Replace(Environment.NewLine, "");
            if (tipo.Equals("") || pre.Equals("") || desc.Equals(""))
            {
                MessageBox.Show("Type, Price and description should be filled");
                return;
            }
            else
            {
                this.tipoServico = tipo;
                this.preco = pre;
                this.descricao = desc;
            }

            if (photopath.Equals(""))
            {
                photopath = "null";
            }
            if (id_instituto > 0)
            {
                currentServico.tipo = tipoServico;
                currentServico.preco = preco;
                currentServico.descricao = descricao;
                institutoPage.AddServico2(currentServico);
            }
            else
            {
                institutoPage.AddServico(this.tipoServico, this.photopath, this.descricao, this.preco);
            }
            this.NavigationService.Navigate(institutoPage);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(institutoPage);
        }

        private void CancelButton_Click(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(institutoPage);
        }

    }
}
