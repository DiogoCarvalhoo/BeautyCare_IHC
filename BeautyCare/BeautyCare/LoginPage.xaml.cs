using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


namespace BeautyCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        public static UtilizadorInfo currentUser;

        public LoginPage()
        {
            InitializeComponent();
            ShowsNavigationUI = false;
            EmailBox.Document.Blocks.Clear();
            EmailBox.Document.Blocks.Add(new Paragraph(new Run("Email")) { Foreground = Brushes.Gray, FontSize = 12 });
            //PasswordBox.Document.Blocks.Clear();
            //PasswordBox.Document.Blocks.Add(new Paragraph(new Run("Password")) { Foreground = Brushes.Gray, FontSize = 12 });
        }

        public UtilizadorInfo getCurrentUser()
        {
            login(currentUser.email, currentUser.password);
            return currentUser;
        }



        private void EmailBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            AddHandler();
            string richText = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            richText = richText.Replace(Environment.NewLine, "");

            if (richText.Equals("Email"))
            {
                EmailBox.Document.Blocks.Clear();
                Keyboard.Focus(EmailBox);
            }
        }

        private void PasswordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            AddHandler();
            string passText = PasswordBox.Password.ToString();
            passText = passText.Replace(Environment.NewLine, "");
            if (passText.Equals("Password"))
            {
                //PasswordBox.Document.Blocks.Clear();
                Keyboard.Focus(PasswordBox);
            }
        }

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(HandleClickOutsideOfControl), true);
        }

        private void HandleClickOutsideOfControl(object sender, MouseButtonEventArgs e)
        {
            string passText = PasswordBox.Password.ToString();
            passText = passText.Replace(Environment.NewLine, "");
            if (passText.Equals(""))
            {
                LabelShowPassword.Visibility = Visibility.Visible;
                //PasswordBox.Document.Blocks.Clear();
                //PasswordBox.Document.Blocks.Add(new Paragraph(new Run("Password")) { Foreground = Brushes.Gray, FontSize = 12 });
            }

            string richText = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            richText = richText.Replace(Environment.NewLine, "");

            if (richText.Equals(""))
            {
                EmailBox.Document.Blocks.Clear();
                EmailBox.Document.Blocks.Add(new Paragraph(new Run("Email")) { Foreground = Brushes.Gray, FontSize = 12 });
            }
            ReleaseMouseCapture();
        }


        private Boolean login(string email, string password)
        {
            List<UtilizadorInfo> utilizadores = new List<UtilizadorInfo>();
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
                        if (int.TryParse(s, out _)) {
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
                    utilizadores.Add(utilizador);
                }
            }
            foreach (UtilizadorInfo u in utilizadores)
            {
                if (u.email.Equals(email) && u.password.Equals(password))
                {
                    currentUser = u;
                    return true;
                }
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            email = email.Replace(Environment.NewLine, "");
            string password = PasswordBox.Password.ToString();
            password = password.Replace(Environment.NewLine, "");
            bool valido = login(email, password);
            if (valido)
            {
                if (currentUser.tipoConta == 2)
                {
                    ChooseAccountType chooseaccountPage = new ChooseAccountType();
                    this.NavigationService.Navigate(chooseaccountPage);
                } else
                {
                    HomePage_Client homeclientPage = new HomePage_Client();
                    this.NavigationService.Navigate(homeclientPage);
                }
            }
            else
            {
                MessageBox.Show("Email e/ou password são inválidos");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            this.NavigationService.Navigate(registerPage);
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            AddHandler();
            string richText = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            richText = richText.Replace(Environment.NewLine, "");

            if (richText.Equals("Email"))
            {
                EmailBox.Document.Blocks.Clear();
                Keyboard.Focus(EmailBox);
            }
        }

        private void LabelShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LabelShowPassword.Visibility = Visibility.Collapsed;
            Keyboard.Focus(PasswordBox);
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LabelShowPassword.Visibility = Visibility.Collapsed;
        }
    }
}

public class UtilizadorInfo
{
    public UtilizadorInfo()
    {
        favoritos = new List<int>();
        reservas = new List<String>();
        user_institutos_id = new List<int>();
    }
    public int id { get; set; }
    public int tipoConta { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string localidade { get; set; }
    public string foto { get; set; }

    public List<int> favoritos { get; set; }

    public List<String> reservas { get; set; }

    public List<int> user_institutos_id { get; set; }
    public override string ToString()
    {
        return "Utilizador: " + id + " " + tipoConta + " " + email + " " + username + " " + password + " " + localidade + foto + favoritos + reservas +";";
    }
}