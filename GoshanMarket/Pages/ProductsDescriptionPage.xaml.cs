using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
using GoshanMarket.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class ProductsDescriptionPage : Page
    {
        Favorites favorites;
        Product products;
        Basket basket;
        public ProductsDescriptionPage()
        {
            InitializeComponent();
        }
        public ProductsDescriptionPage(Product product)
        {
            InitializeComponent();
            favorites = new Favorites();
            products = product;
            basket = new Basket();
            this.DataContext = products;
        }
        private void AddFavouritesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentClient.client == null)
            {
                NavigationService.Navigate(new AuthtorizationPage());
            }
            else
            {
                if (Heartbutton.Foreground == Brushes.Gray)
                {
                    Heartbutton.Foreground = Brushes.Red;
                    favorites.ProductID = products.ProductID;
                    favorites.ClientID = CurrentClient.client.ClientID;
                    DB.entities.Favorites.Add(favorites);
                    DB.entities.SaveChanges();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Visible;
            if (products.CategoryID == 4 || products.CategoryID == 5)
            {
                AlarmText.Visibility = Visibility.Visible;
                UnitText.Text = "кг.";
            }
            if (QuantityInStock2.Text == "0")
            {
                Icon.Visibility = Visibility.Collapsed;
                UnitText.Visibility = Visibility.Collapsed;
                QuantityInStock1.Visibility = Visibility.Collapsed;
                QuantityInStock2.Foreground = Brushes.Red;
                QuantityInStock2.Text = "Нет в наличии";
            }
            var FavouritesProduct = products.Favorites.FirstOrDefault(c => c.ClientID == CurrentClient.client.ClientID);
            if (FavouritesProduct != null)
            {
                Heartbutton.Foreground = Brushes.Red;
            }
            this.DataContext = products;
        }

        private void InBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentClient.client == null)
            {
                NavigationService.Navigate(new AuthtorizationPage());
            }
            else
            {
                if (QuantityInStock2.Text == "Нет в наличии")
                {
                    CustomMessageBox.Show("Данного товара нет в наличии.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
                }
                else
                {
                    if (CurrentClient.client == null)
                    {
                        NavigationService.Navigate(new AuthtorizationPage());
                    }
                    else
                    {
                        Product CurrentProduct = ((Product)((Button)sender).DataContext);
                        basket.ProductID = (long)CurrentProduct.ProductID;
                        basket.ClientID = CurrentClient.client.ClientID;
                        DB.entities.Basket.Add(basket);
                        DB.entities.SaveChanges();
                    }
                }
            }
        }
    }
}
