using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BeautyCare
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            EmailBox.Document.Blocks.Clear();
            EmailBox.Document.Blocks.Add(new Paragraph(new Run("Email")) { Foreground = Brushes.Gray, FontSize = 12 });
            //PasswordBox.Document.Blocks.Clear();
            //PasswordBox.Document.Blocks.Add(new Paragraph(new Run("Password")) { Foreground = Brushes.Gray, FontSize = 12 });
            UsernameBox.Document.Blocks.Clear();
            UsernameBox.Document.Blocks.Add(new Paragraph(new Run("Username")) { Foreground = Brushes.Gray, FontSize = 12 });
            //ConfirmPasswordBox.Document.Blocks.Clear();
            //ConfirmPasswordBox.Document.Blocks.Add(new Paragraph(new Run("Confirme Password")) { Foreground = Brushes.Gray, FontSize = 12 });
        }

        private void UsernameBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            AddHandler();
            string richText = new TextRange(UsernameBox.Document.ContentStart, UsernameBox.Document.ContentEnd).Text;
            richText = richText.Replace(Environment.NewLine, "");

            if (richText.Equals("Username"))
            {
                UsernameBox.Document.Blocks.Clear();
                Keyboard.Focus(UsernameBox);
            }
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
            string password = PasswordBox.Password.ToString();
            password = password.Replace(Environment.NewLine, "");
            Console.WriteLine(password);
            if (password.Equals("Password"))
            {
                //PasswordBox.Document.Blocks.Clear();
                Keyboard.Focus(PasswordBox);
            }
        }

        private void ConfirmPasswordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            AddHandler();
            string cPasswordBox = ConfirmPasswordBox.Password.ToString();
            cPasswordBox = cPasswordBox.Replace(Environment.NewLine, "");

            if (cPasswordBox.Equals("Confirme Password"))
            {
                //ConfirmPasswordBox.Document.Blocks.Clear();
                Keyboard.Focus(ConfirmPasswordBox);
            }
        }

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(HandleClickOutsideOfControl), true);
        }

        private void HandleClickOutsideOfControl(object sender, MouseButtonEventArgs e)
        {
            string password = PasswordBox.Password.ToString();
            password = password.Replace(Environment.NewLine, "");
            if (password.Equals(""))
            {
                LabelShowPassword.Visibility = Visibility.Visible;
                //PasswordBox.Document.Blocks.Clear();
                //PasswordBox.Document.Blocks.Add(new Paragraph(new Run("Password")) { Foreground = Brushes.Gray, FontSize = 12 });
            }
            string email = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            email = email.Replace(Environment.NewLine, "");

            if (email.Equals(""))
            {
                EmailBox.Document.Blocks.Clear();
                EmailBox.Document.Blocks.Add(new Paragraph(new Run("Email")) { Foreground = Brushes.Gray, FontSize = 12 });
            }

            string user = new TextRange(UsernameBox.Document.ContentStart, UsernameBox.Document.ContentEnd).Text;
            user = user.Replace(Environment.NewLine, "");

            if (user.Equals(""))
            {
                UsernameBox.Document.Blocks.Clear();
                UsernameBox.Document.Blocks.Add(new Paragraph(new Run("Username")) { Foreground = Brushes.Gray, FontSize = 12 });
            }

            string cpassword = ConfirmPasswordBox.Password.ToString();
            cpassword = cpassword.Replace(Environment.NewLine, "");

            if (cpassword.Equals(""))
            {
                LabelCShowPassword.Visibility = Visibility.Visible;
                //ConfirmPasswordBox.Document.Blocks.Clear();
                //ConfirmPasswordBox.Document.Blocks.Add(new Paragraph(new Run("Confirme Password")) { Foreground = Brushes.Gray, FontSize = 12 });
            }
            ReleaseMouseCapture();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = new TextRange(EmailBox.Document.ContentStart, EmailBox.Document.ContentEnd).Text;
            email = email.Replace(Environment.NewLine, "");
            string password = PasswordBox.Password.ToString();
            password = password.Replace(Environment.NewLine, "");
            string username = new TextRange(UsernameBox.Document.ContentStart, UsernameBox.Document.ContentEnd).Text;
            username = username.Replace(Environment.NewLine, "");
            string confirmpassword = ConfirmPasswordBox.Password.ToString();
            confirmpassword = confirmpassword.Replace(Environment.NewLine, "");
            bool valido;
            int numeroUtilizadores;
            (valido, numeroUtilizadores) = validarRegisto(username, email, password, confirmpassword);
            if (valido)
            {
                if (FirstRadioButton.IsChecked == true)
                {
                    registarRegisto(numeroUtilizadores + 1, username, email, password, "1");
                }
                else
                {
                    registarRegisto(numeroUtilizadores + 1, username, email, password, "2");
                }
                LoginPage LoginPage = new LoginPage();
                this.NavigationService.Navigate(LoginPage);
            }
        }

        private (Boolean, int) validarRegisto(string username, string email, string password, string confirmpassword)
        {
            List<UtilizadorInfo> utilizadores = readRegisto();
            if (username.Length > 15 || username.Length < 3)
            {
                MessageBox.Show("Username is invalid");
                return (false, -1);
            }

            if (email.Length < 3 || email.Contains('@') == false || email.Contains('.') == false)
            {
                MessageBox.Show("Email is invalid");
                return (false, -1);
            }
            else
            {
                foreach (UtilizadorInfo u in utilizadores)
                {
                    if (u.email.Equals(email))
                    {
                        MessageBox.Show("Email already used");
                        return (false, -1);
                    }
                }
            }

            if (password.Length < 6 || Regex.Matches(password, @"[0-9]").Count == 0)
            {
                MessageBox.Show("Password is invalid! Must have at least 6 characters and contain numbers.");
                return (false, -1);
            }

            if (password.Equals(confirmpassword) == false)
            {
                MessageBox.Show("Passwords are different");
                return (false, -1);
            }

            if (FirstRadioButton.IsChecked == false && SecondRadioButton.IsChecked == false)
            {
                MessageBox.Show("Select Account Type Please!");
                return (false, -1);
            }
            return (true, utilizadores.Count);
        }

        private void registarRegisto(int id, string username, string email, string password, string accounttype)
        {
            using (StreamWriter sw = File.AppendText("Registos.txt"))
            {
                string registo = id + "-" + accounttype + "-" + email + "-" + username + "-" + password + "-null-null-{}-{}-{}";
                sw.WriteLine(registo);
            }
        }

        public List<UtilizadorInfo> readRegisto()
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
                    utilizadores.Add(utilizador);
                }
            }
            return utilizadores;
        }

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }

        private void UsernameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(this, CaptureMode.SubTree);
            AddHandler();
            string richText = new TextRange(UsernameBox.Document.ContentStart, UsernameBox.Document.ContentEnd).Text;
            richText = richText.Replace(Environment.NewLine, "");

            if (richText.Equals("Username"))
            {
                UsernameBox.Document.Blocks.Clear();
                Keyboard.Focus(UsernameBox);
            }
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

        private void LabelCShowPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LabelCShowPassword.Visibility = Visibility.Collapsed;
            Keyboard.Focus(ConfirmPasswordBox);
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LabelShowPassword.Visibility = Visibility.Collapsed;
            Keyboard.Focus(PasswordBox);
        }

        private void ConfirmPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LabelCShowPassword.Visibility = Visibility.Collapsed;
            Keyboard.Focus(ConfirmPasswordBox);
        }
    }
}

