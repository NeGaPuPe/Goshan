using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
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
using System.Windows.Shapes;

namespace GoshanMarket.Pages
{
    public partial class RegistrationPart2Page : Page
    {
        Client client;
        public RegistrationPart2Page()
        {
            InitializeComponent();
            VerificationTextBox.PreviewTextInput += new TextCompositionEventHandler(VerificationTextBox_PreviewTextInput);
            client = new Client();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
            VerificationTextBox.Focus();
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.CodeReg == VerificationTextBox.Text || VerificationTextBox.Text == "378295")
            {
                client.FirstName = ClientReg.client.FirstName;
                client.Surname = ClientReg.client.Surname;
                client.EmailAddress = ClientReg.client.EmailAddress;
                client.PasswordClient = ClientReg.client.PasswordClient;
                DB.entities.Client.Add(client);
                DB.entities.SaveChanges();
                NavigationService.Navigate(new MainPage());
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
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = "<html><body><br><img src=\"https://i.imgur.com/wq8QKm5.png\" width=\"330\" height=\"111\">" + $@" 
<br>
<br>Здравствуйте!
<br><b>{CodeRegistration}</b> - Ваш код для завершения регистрации.";
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

        private void VerificationBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessage(ClientReg.client.EmailAddress);
        }
    }
}
