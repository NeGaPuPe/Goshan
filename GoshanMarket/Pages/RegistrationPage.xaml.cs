using GoshanMarket.Entities;
using GoshanMarket.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using GoshanMarket.Classes;
using GoshanMarket.MessageBox;

namespace GoshanMarket.Pages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            NameTextBox.PreviewTextInput += new TextCompositionEventHandler(NameTextBox_PreviewTextInput);
            EmailAddressTextBox.PreviewTextInput += new TextCompositionEventHandler(EmailAddressTextBox_PreviewTextInput);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
        }

        private void VereficationButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpaceCheck.Check(NameTextBox.Text) == true || SpaceCheck.Check(EmailAddressTextBox.Text) == true || SpaceCheck.Check(PasswordTextBox.Password) == true)
            {
                CustomMessageBox.Show("Есть пустые поля.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else if (!EmailAddressTextBox.Text.Contains("@mail.ru") && !EmailAddressTextBox.Text.Contains("@gmail.com") && !EmailAddressTextBox.Text.Contains("@yandex.ru"))
            {
                CustomMessageBox.Show("Неверный формат эл. почты.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else if (DB.entities.Client.FirstOrDefault(c => EmailAddressTextBox.Text.Trim() == c.EmailAddress) != null)
            {
                CustomMessageBox.Show("Такая почта уже зарегистрирована", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                Client client = new Client();
                ClientReg.client = client;


                ClientReg.client.FirstName = NameTextBox.Text;
                ClientReg.client.EmailAddress = EmailAddressTextBox.Text;
                ClientReg.client.PasswordClient = HashMD5.Hashinfo(PasswordTextBox.Password);
                SendMessage(EmailAddressTextBox.Text);
                NavigationService.Navigate(new RegistrationPart2Page());
            }
        }
        private void SendMessage(string emailAddress)
        {
            string smtpServer = "smtp.mail.ru"; //smpt сервер(зависит от почты отправителя)
            int smtpPort = 587; // Обычно используется порт 587 для TLS
            string smtpUsername = "goshanmarket@mail.ru"; //почта, с которой отправляется сообщение
            string smtpPassword = "g82yFHkYks3LnShutZcc";//пароль приложения (от почты)
            string Name = "Goshan";

            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {

                    string Numbers = "1234567890";
                    string CodeRegistration = "";
                    Random random = new Random();
                    for (int i = 0; i <= 5; i++)
                    {
                        CodeRegistration += Numbers[random.Next(0, Numbers.Length)];
                    }

                    Properties.Settings.Default.CodeReg = CodeRegistration;

                    mailMessage.From = new MailAddress(smtpUsername, Name);
                    mailMessage.To.Add(emailAddress);
                    mailMessage.Subject = "Код для регистрации";
                    mailMessage.Body = $"Здравствуйте! \r\n{CodeRegistration} - ваш код для завершения регистрации.";

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
        private void NameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
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
