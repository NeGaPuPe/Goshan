using GoshanMarket.Classes;
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
using System.Windows.Shapes;
using GoshanMarket.Entities;
using GoshanMarket.Models;
using GoshanMarket.MessageBox;

namespace GoshanMarket.Pages
{
    public partial class RecoverPasswordPage : Page
    {
        public RecoverPasswordPage()
        {
            InitializeComponent();
            EmailAddressTextBox.PreviewTextInput += new TextCompositionEventHandler(EmailAddressTextBox_PreviewTextInput);
            CodeTextBox.PreviewTextInput += new TextCompositionEventHandler(CodeTextBox_PreviewTextInput);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
        }

        private void GetCodeButton_Click(object sender, RoutedEventArgs e)
        {
            string EmailAddress = EmailAddressTextBox.Text;
            Client ClientEmailRecover = DB.entities.Client.FirstOrDefault(c => c.EmailAddress == EmailAddress);
            if (ClientEmailRecover != null)
            {
                CurrentClientRecoverAccess.client = ClientEmailRecover;
                SendMessage(EmailAddressTextBox.Text);
                GetCodePanel.Visibility = Visibility.Hidden;
                RecoverPasswordPanel.Visibility = Visibility.Visible;
                CodeTextBox.Focus();
            }
            else if (SpaceCheck.Check(EmailAddress) == true)
            {
                CustomMessageBox.Show("Укажите вашу электронную почту.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else if (!EmailAddressTextBox.Text.Contains("@mail.ru") && !EmailAddressTextBox.Text.Contains("@gmail.com") && !EmailAddressTextBox.Text.Contains("@yandex.ru"))
            {
                CustomMessageBox.Show("Неверный формат эл. почты.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                CustomMessageBox.Show("Пользователь не найден.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
        }

        private void RecoverPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.CodeRec == CodeTextBox.Text)
            {
                RecoverPasswordPanel.Visibility = Visibility.Hidden;
                SavePasswordPanel.Visibility = Visibility.Visible;
            }
            else
            {
                CustomMessageBox.Show("Неправильно введён код подтверждения.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
        }

        private void SavePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentClientRecoverAccess.client.PasswordClient = HashMD5.Hashinfo(PasswordTextBox.Password);
            if (HashMD5.Hashinfo(PasswordTextBox.Password) == HashMD5.Hashinfo(AcceptPasswordTextBox.Password))
            {
                DB.entities.SaveChanges();
                CustomMessageBox.Show("Пароль успешно изменён.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                CustomMessageBox.Show("Пароли не совпадают.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
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
                    string CodeRecover = "";
                    Random random = new Random();
                    for (int i = 0; i <= 5; i++)
                    {
                        CodeRecover += Numbers[random.Next(0, Numbers.Length)];
                    }

                    Properties.Settings.Default.CodeRec = CodeRecover;

                    mailMessage.From = new MailAddress(smtpUsername, Name);
                    mailMessage.To.Add(emailAddress);
                    mailMessage.Subject = "Восстановление доступа";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "<html><body><br><img src=\"https://i.imgur.com/wq8QKm5.png\" width=\"330\" height=\"111\">" + $@" 
<br>
<br>Здравствуйте!
<br><b>{CodeRecover}</b> - Ваш код для восстановления дуступа к аккаунту.";
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

        private void CodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void VerificationBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessage(EmailAddressTextBox.Text);
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
