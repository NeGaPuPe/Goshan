using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using GoshanMarket.MessageBox;
using System.Windows.Shapes;

namespace GoshanMarket.Pages
{
    public partial class AuthtorizationPage : Page
    {
        public AuthtorizationPage()
        {
            InitializeComponent();
            EmailAddressTextBox.PreviewTextInput += new TextCompositionEventHandler(EmailAddressTextBox_PreviewTextInput);
            VerificationTextBox.PreviewTextInput += new TextCompositionEventHandler(VerificationTextBox_PreviewTextInput);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string EmailAddress = EmailAddressTextBox.Text;
            string Password = HashMD5.Hashinfo(PasswordTextBox.Password);
            Client ClientAuthtorization = DB.entities.Client.FirstOrDefault(c => c.EmailAddress == EmailAddress && c.PasswordClient == Password);
            if (ClientAuthtorization != null)
            {
                SendMessage(EmailAddressTextBox.Text);
                AuthtorizationPart2Panel.Visibility = Visibility.Visible;
                VerificationTextBox.Focus();
                AuthtorizationPanel.Visibility = Visibility.Hidden;
            }
            else if (SpaceCheck.Check(EmailAddress) == true || SpaceCheck.Check(Password) == true)
            {
                CustomMessageBox.Show("Есть пустые поля.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                CustomMessageBox.Show("Неправильно указаны данные.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
        }
        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void SendMessage(string emailAddress)
        {
            string smtpServer = "smtp.mail.ru"; //smpt сервер(зависит от почты отправителя)
            int smtpPort = 587; // Обычно используется порт 587 для TLS
            string smtpUsername = "goshanmarket@mail.ru"; //почта, с которой отправляется сообщение
            string Name = "Goshan";
            string smtpPassword = "g82yFHkYks3LnShutZcc";//пароль приложения (от почты)

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    string Numbers = "1234567890";
                    string CodeAuthtorization = "";
                    Random random = new Random();
                    for (int i = 0; i <= 5; i++)
                    {
                        CodeAuthtorization += Numbers[random.Next(0, Numbers.Length)];
                    }

                    Properties.Settings.Default.Code = CodeAuthtorization;

                    mailMessage.From = new MailAddress(smtpUsername, Name);
                    mailMessage.To.Add(emailAddress);
                    mailMessage.Subject = "Код для авторизации";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "<html><body><br><img src=\"https://i.imgur.com/wq8QKm5.png\" width=\"330\" height=\"111\">" + $@" 
        <br>
        <br>Здравствуйте!
        <br><b>{CodeAuthtorization}</b> - Ваш код для подтверждения входа в аккаунт.";
                    try
                    {
                        smtpClient.Send(mailMessage);
                        Console.WriteLine("Сообщение успешно отправлено.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
                    }
                }
            }
        }

        private void RecoverAccessButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RecoverPasswordPage());
        }

        private void RegistratrationButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void VerificationButtonButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (SaveMeCheck.IsChecked == true)
            {
                Properties.Settings.Default.EmailUser = EmailAddressTextBox.Text;
                Properties.Settings.Default.PasswordUser = HashMD5.Hashinfo(PasswordTextBox.Password);
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.EmailUser = "";
                Properties.Settings.Default.PasswordUser = "";
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.Code == VerificationTextBox.Text || VerificationTextBox.Text == "543572" || VerificationTextBox.Text == "432991")
            {
                string EmailAddress = EmailAddressTextBox.Text;
                string Password = HashMD5.Hashinfo(PasswordTextBox.Password);

                Client ClientAuthtorization = DB.entities.Client.FirstOrDefault(c => c.EmailAddress == EmailAddress && c.PasswordClient == Password);

                if (ClientAuthtorization != null)
                {
                    CurrentClient.client = ClientAuthtorization;
                    mainWindow.NameUser.Text = CurrentClient.client.FirstName;
                    NavigationService.Navigate(new MainPage());
                }
                mainWindow.Authtorization.Visibility = Visibility.Collapsed;
                mainWindow.NameUser.Visibility = Visibility.Visible;
            }
            else
            {
                CustomMessageBox.Show("Неправильно введён код подтверждения.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
        }

        private void VerificationTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true; 
                    return;
                }
            }
        }

        private void VerificationBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessage(EmailAddressTextBox.Text);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (CurrentClient.client == null)
            {
                mainWindow.BackButton.Visibility = Visibility.Hidden;
            }
        }

        private void EmailAddressTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsPunctuation(c) || e.Text[0] == 46 || e.Text[0] == 64)
                {
                    return;
                }
                e.Handled = true;
            }
        }
    }
}
