using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
using GoshanMarket.Models;

namespace GoshanMarket.Windows
{
    public partial class ExecuteOrderWindow : Window
    {
        Order order;
        public ExecuteOrderWindow(List<Basket> basketList)
        {
            InitializeComponent();
            order = new Order();
            order.OrderList = new List<OrderList>();
            foreach (var item in basketList)
            {
                order.OrderList.Add(new OrderList() { ProductID = item.ProductID });
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MapButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            YandexMapPanel.Visibility = Visibility.Visible;
            PostsPanel.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Hidden;
        }

        private void PostsButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            YandexMapPanel.Visibility = Visibility.Hidden;
            PostsPanel.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
        }

        private void MapButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MapI.Foreground = Brushes.Green;
            MapT.Foreground = Brushes.Green;
        }

        private void PostsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            PostsI.Foreground = Brushes.Green;
            PostsT.Foreground = Brushes.Green;
        }


        private void PostsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            PostsI.Foreground = Brushes.Black;
            PostsT.Foreground = Brushes.Black;
        }

        private void MapButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MapI.Foreground = Brushes.Black;
            MapT.Foreground = Brushes.Black;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            order.DateOrder = DateTime.Now;
            order.StatusID = 1;
            order.ClientID = CurrentClient.client.ClientID;
            
            if(GoshanSerpRadioButton.IsChecked == true)
            {
                order.PointID = 1;
            }
            else if(GoshanPyshinoRadioButton.IsChecked == true)
            {
                order.PointID = 2;
            }
            else if(GoshanIvankiRadioButton.IsChecked == true)
            {
                order.PointID = 3;
            }
            else
            {
                CustomMessageBox.Show("Выберите пункт самовывоза.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
                return;
            }     
            DB.entities.Order.Add(order);
            var sd = DB.entities.Basket.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
            DB.entities.Basket.RemoveRange(sd);
            DB.entities.SaveChanges();
            System.Windows.Forms.DialogResult result = CustomMessageBox.Show("Хотите получить электронную версию чека?", CustomMessageBox.CMessageBoxTitle.Внимание, CustomMessageBox.CMessageBoxButton.Да, CustomMessageBox.CMessageBoxButton.Нет);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                OrderIDForEmail.order = order;
                OrderIDForEmail.basket = sd;
                SendMessage(CurrentClient.client.EmailAddress);
                this.Close();
            }
            else
            {
                this.Close();
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
                    string ProductsOrder = "";
                    decimal totalPrice = 0;
                    foreach (var item in OrderIDForEmail.basket)
                    {
                        item.Product = DB.entities.Product.FirstOrDefault(c => c.ProductID == item.ProductID);
                        ProductsOrder += "<p>" + "- " + item.Product.ProductName + "<b>" + " "+ item.Product.Price + " ₽" + "</b></p>";
                        totalPrice += item.Product.Price;
                    }
                    mailMessage.From = new MailAddress(smtpUsername, Name);
                    mailMessage.To.Add(emailAddress);
                    mailMessage.Subject = "Чек ГОШАН";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body =  $"<html><body><br><img src=\"https://i.imgur.com/wq8QKm5.png\" width=\"330\" height=\"111\">" + $@" 
<br>Здравствуйте {CurrentClient.client.FirstName}!
<br>Чек вашей покупки через приложение ГОШАН.
<br>    
<br>Заказ№ <b>{OrderIDForEmail.order.OrderID}</b>
<br>Состав заказа:      " + ProductsOrder + @"
Итого к оплате:       <b>" + totalPrice + @" ₽ </b>
<br>
<br> Эл.адрес покупателя: <b>" + CurrentClient.client.EmailAddress + $@"</b>
<br> Дата покупки: <b>{DateTime.Now}</b> ";

                    CustomMessageBox.Show("Чек выслан вам на почту.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);

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
    }
}
