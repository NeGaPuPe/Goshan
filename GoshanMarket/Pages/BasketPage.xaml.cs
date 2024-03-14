using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
using GoshanMarket.Models;
using GoshanMarket.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoshanMarket.Pages
{
    public partial class BasketPage : Page
    {
        Favorites favorites;
        public BasketPage()
        {
            InitializeComponent();
            favorites = new Favorites();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
        }
        List<Basket> BasketList = new List<Basket>();
        private void CatalogButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Visible;
            if (CurrentClient.client == null)
            {
                NavigationService.Navigate(new AuthtorizationPage());
                mainWindow.BackButton.Visibility = Visibility.Hidden;
            }
            else
            {
                BasketList = DB.entities.Basket.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
                OrdersListView.ItemsSource = BasketList;
                if (OrdersListView.HasItems == true)
                {
                    EmptyBasketPanel.Visibility = Visibility.Hidden;
                    NotEmptyBasketPanel.Visibility = Visibility.Visible;
                    NotEmptyBasketPanel2.Visibility = Visibility.Visible;
                    decimal totalPrice = 0;
                    foreach (Basket basketList in OrdersListView.Items)
                    {
                        totalPrice += basketList.Product.Price;
                        PriceAllProducts.Text = totalPrice.ToString();
                    }
                }
                else
                {
                    EmptyBasketPanel.Visibility = Visibility.Visible;
                    NotEmptyBasketPanel.Visibility = Visibility.Collapsed;
                    NotEmptyBasketPanel2.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteOrderWindow executeOrderWindow = new ExecuteOrderWindow(BasketList);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Effect = new BlurEffect();
            executeOrderWindow.ShowDialog();
            Page_Loaded(sender, e);
        }
       
        private void DeleteProductOrder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Basket CurrentBasketItem = ((Basket)((PackIcon)sender).DataContext);
            DB.entities.Basket.Remove(CurrentBasketItem);
            DB.entities.SaveChanges();
            BasketList = DB.entities.Basket.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
            OrdersListView.ItemsSource = BasketList;
            if (OrdersListView.HasItems == true)
            {
                EmptyBasketPanel.Visibility = Visibility.Hidden;
                NotEmptyBasketPanel.Visibility = Visibility.Visible;
                NotEmptyBasketPanel2.Visibility = Visibility.Visible;
                decimal totalPrice = 0;
                foreach (Basket basketList in OrdersListView.Items)
                {
                    totalPrice += basketList.Product.Price;
                    PriceAllProducts.Text = totalPrice.ToString();
                }
            }
            else
            {
                EmptyBasketPanel.Visibility = Visibility.Visible;
                NotEmptyBasketPanel.Visibility = Visibility.Collapsed;
                NotEmptyBasketPanel2.Visibility = Visibility.Collapsed;
            }
        }

        private void Heartbutton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Basket CurrentBasketItem = ((Basket)((PackIcon)sender).DataContext);
            var FavouritesProduct = CurrentBasketItem.Product.Favorites.FirstOrDefault(c => c.ClientID == CurrentClient.client.ClientID);
            if (FavouritesProduct != null)
            {
                CustomMessageBox.Show("Данный товар уже находится в избранном.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                favorites.ProductID = (long)CurrentBasketItem.ProductID;
                favorites.ClientID = CurrentClient.client.ClientID;
                DB.entities.Favorites.Add(favorites);
                DB.entities.SaveChanges();
            }
        }
    }
}
