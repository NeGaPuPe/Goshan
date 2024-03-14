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
using GoshanMarket.MessageBox;

namespace GoshanMarket.Pages
{
    public partial class ProductInOrderPage : Page
    {
        public ProductInOrderPage(Order orders)
        {
            InitializeComponent();
            orderLists = DB.entities.OrderList.Where(c => c.Order.OrderID == orders.OrderID).ToList();
            ProductsInOrderListView.ItemsSource = orderLists;
        }
        List<OrderList> orderLists = new List<OrderList>();

        private void MyOrdersButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ClientOrdersPage());
        }

        private void FavouritesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new FavouritesPage());
        }

        private void ProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage(CurrentClient.client));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentClient.client == null)
            {
                Menu.Visibility = Visibility.Collapsed;

            }
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Visible;
        }
    }
}
