using GoshanMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
using GoshanMarket.Classes;
using MaterialDesignThemes.Wpf;
using GoshanMarket.MessageBox;
using System.IO;

namespace GoshanMarket.Pages
{
    public partial class ClientOrdersPage : Page
    {
        public ClientOrdersPage()
        {
            InitializeComponent();
            OrdersList = DB.entities.Order.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
            OrdersClientListView.ItemsSource = OrdersList;
        }
        List<Order> OrdersList = new List<Order>();

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Order CurrentOrder = ((Order)((Border)sender).DataContext);
            NavigationService.Navigate(new ProductInOrderPage(CurrentOrder));
        }

        private void MyOrdersButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ClientOrdersPage());
        }

        private void FavouritesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new FavouritesPage());
        }

        private void MyProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage(CurrentClient.client));
        }

        private void MyProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MyProfileButton.Background = Brushes.White;
        }

        private void MyProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MyProfileButton.Background = Brushes.WhiteSmoke;
        }

        private void FavouritesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            FavouritesButton.Background = Brushes.White;
        }

        private void FavouritesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            FavouritesButton.Background = Brushes.WhiteSmoke;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Directory.Delete("t435");
        }
    }
}
