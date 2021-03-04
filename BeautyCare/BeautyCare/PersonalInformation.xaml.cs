using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for PersonalInformation.xaml
    /// </summary>
    public partial class PersonalInformation : Page
    {
        public static UtilizadorInfo currentUser = new UtilizadorInfo();
        public int tipo;

        public PersonalInformation(int tipo, int id)
        {
            Console.WriteLine(id);
            this.tipo = tipo;
            InitializeComponent();
            LoginPage logPage = new LoginPage();
            RegisterPage registerPage = new RegisterPage();
            List<UtilizadorInfo> utilizadores = registerPage.readRegisto();
            if (id == -1)
            {
                currentUser = logPage.getCurrentUser();
            } else
            {
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == id)
                    {
                        Console.WriteLine("entrei");
                        Console.WriteLine(u.id);
                        Console.WriteLine(u);
                        currentUser = u;
                    }
                }
            }
            nomeUtilizador.Content = currentUser.username;
            EmailBox.Document.Blocks.Clear();
            EmailBox.Document.Blocks.Add(new Paragraph(new Run(currentUser.email)));
            Localidade.Document.Blocks.Clear();
            if (currentUser.localidade != "null")
            {
                Localidade.Document.Blocks.Add(new Paragraph(new Run(currentUser.localidade)));
            }
            if (currentUser.foto != "null")
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(
                       BaseUriHelper.GetBaseUri(this), currentUser.foto));
                myBrush.ImageSource = image.Source;
                userfotoelipse.Fill = myBrush;
            }
        }


        private void ButtonVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (this.tipo == -1)
            {
                HomePage_Client homeclientPage = new HomePage_Client();
                this.NavigationService.Navigate(homeclientPage);
            } else
            {
                meusInstitutos Page = new meusInstitutos();
                this.NavigationService.Navigate(Page);
            }
        }

        public void loadInformation()
        {
            LoginPage logPage = new LoginPage();
            currentUser = logPage.getCurrentUser();
            RegisterPage registerPage = new RegisterPage();
            List<UtilizadorInfo> utilizadores = registerPage.readRegisto();
            foreach (UtilizadorInfo u in utilizadores)
            {
                if (u.id == currentUser.id)
                {
                    currentUser.tipoConta = u.tipoConta;
                    currentUser.email = u.email;
                    currentUser.password = u.password;
                    currentUser.localidade = u.localidade;
                    currentUser.foto = u.foto;
                }
            }
            nomeUtilizador.Content = currentUser.username;
            EmailBox.Document.Blocks.Clear();
            EmailBox.Document.Blocks.Add(new Paragraph(new Run(currentUser.email)));
            Localidade.Document.Blocks.Clear();
            if (currentUser.localidade != "null")
            {
                Localidade.Document.Blocks.Add(new Paragraph(new Run(currentUser.localidade)));
            }
            if (currentUser.foto != "null")
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(
                       BaseUriHelper.GetBaseUri(this), currentUser.foto));
                myBrush.ImageSource = image.Source;
                userfotoelipse.Fill = myBrush;
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            EditarButton.Visibility = Visibility.Visible;
            ConfirmarButton.Visibility = Visibility.Collapsed;
            CancelarButton.Visibility = Visibility.Collapsed;

            loadInformation();
            EmailBox.IsReadOnly = true;
            Localidade.IsReadOnly = true;
        }

        private void ConfirmarButton_Click(object sender, RoutedEventArgs e)
        {
            EditarButton.Visibility = Visibility.Visible;
            ConfirmarButton.Visibility = Visibility.Collapsed;
            CancelarButton.Visibility = Visibility.Collapsed;

            bool valido = true;
            EmailBox.IsReadOnly = true;
            Localidade.IsReadOnly = true;
            string novoEmail = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            novoEmail = novoEmail.Replace(Environment.NewLine, "");
            string novaLocalidade = new TextRange(Localidade.Document.ContentStart, Localidade.Document.ContentEnd).Text;
            novaLocalidade = novaLocalidade.Replace(Environment.NewLine, "");
            List<String> novoConteudoFicheiro = new List<String>();

            if (novoEmail.Equals(currentUser.email))
            {
                valido = false;
                if (novaLocalidade.Equals(currentUser.localidade) == false)
                {
                    RegisterPage registerpageobject = new RegisterPage();
                    List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                    foreach (UtilizadorInfo u in utilizadores)
                    {
                        if (u.id == currentUser.id)
                        {
                            string conteudo = u.id + "-" + u.tipoConta + "-" + u.email + "-" + u.username + "-" + u.password + "-" + novaLocalidade + "-" + u.foto + "-{";
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
                        else
                        {
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
                    }
                    registarAlteracoes(novoConteudoFicheiro);
                }
            }
            else
            {
                if (novoEmail.Length < 3 || novoEmail.Contains('@') == false || novoEmail.Contains('.') == false)
                {
                    valido = false;
                    MessageBox.Show("Email is invalid");
                }
                else
                {
                    RegisterPage registerpageobject = new RegisterPage();
                    List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                    foreach (UtilizadorInfo u in utilizadores)
                    {
                        if (u.email.Equals(novoEmail))
                        {
                            valido = false;
                            MessageBox.Show("Email já em uso!");
                        }
                    }
                }
            }

            if (valido)
            {
                if (novaLocalidade.Equals(currentUser.localidade))
                {
                    RegisterPage registerpageobject = new RegisterPage();
                    List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                    foreach (UtilizadorInfo u in utilizadores)
                    {
                        if (u.id == currentUser.id)
                        {
                            string conteudo = u.id + "-" + u.tipoConta + "-" + novoEmail + "-" + u.username + "-" + u.password + "-" + u.localidade + "-" + u.foto + "-{";
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
                        else
                        {
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
                    }
                    registarAlteracoes(novoConteudoFicheiro);
                }
                else
                {
                    RegisterPage registerpageobject = new RegisterPage();
                    List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                    foreach (UtilizadorInfo u in utilizadores)
                    {
                        if (u.id == currentUser.id)
                        {
                            string conteudo = u.id + "-" + u.tipoConta + "-" + novoEmail + "-" + u.username + "-" + u.password + "-" + novaLocalidade + "-" + u.foto + "-{";
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
                        else
                        {
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
                    }
                    registarAlteracoes(novoConteudoFicheiro);
                }
            }
            EmailBox.IsReadOnly = true;
            Localidade.IsReadOnly = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditarButton.Visibility = Visibility.Collapsed;
            ConfirmarButton.Visibility = Visibility.Visible;
            CancelarButton.Visibility = Visibility.Visible;
            EmailBox.IsReadOnly = false;
            Localidade.IsReadOnly = false;

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

        private void EmailBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EditarButton.Content.Equals("Confirmar"))
            {
                Keyboard.Focus(EmailBox);
            }
        }

        private void Localidade_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EditarButton.Content.Equals("Confirmar"))
            {
                Keyboard.Focus(Localidade);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                string sFileName = openFileDialog.FileName;
                string fileName = sFileName.Split("\\")[sFileName.Split("\\").Length - 1];
                string destPath = System.AppDomain.CurrentDomain.BaseDirectory;
                destPath = destPath.Replace("\\bin\\Debug\\netcoreapp3.1\\", "\\Images\\userphotos");
                System.IO.File.Copy(sFileName, System.IO.Path.Combine(destPath, fileName), true);
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(sFileName));
                myBrush.ImageSource = image.Source;
                userfotoelipse.Fill = myBrush;

                List<String> novoConteudoFicheiro = new List<String>();
                RegisterPage registerpageobject = new RegisterPage();
                List<UtilizadorInfo> utilizadores = registerpageobject.readRegisto();
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.id == currentUser.id)
                    {
                        string conteudo = u.id + "-" + u.tipoConta + "-" + u.email + "-" + u.username + "-" + u.password + "-" + u.localidade + "-" + "Images/userphotos/" + fileName + "-{";
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
                    else
                    {
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
                }
                registarAlteracoes(novoConteudoFicheiro);
            }
        }


    }
}