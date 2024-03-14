using Azure;
using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
using GoshanMarket.Models;
using GoshanMarket.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ProdusctsPage : Page
    {
        int id = 0;
        private static bool messageShown = false;
        TextBox search = new TextBox();
        Favorites favorites;
        Basket basket;
        public ProdusctsPage(TextBox search)
        {
            InitializeComponent();
            favorites = new Favorites();
            basket = new Basket();
            this.search = search;
        }
        public ProdusctsPage(object category)
        {
            InitializeComponent();
            basket = new Basket();
            favorites = new Favorites();
            id = int.Parse(category.ToString());
            var Category = DB.entities.Category.FirstOrDefault(c => c.CategoryID == id);
            ProductCategoryTextBlock.Text = Category.CategoryName;
        }
        List<Product> ProductList = new List<Product>();
        public async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!messageShown)
            {
                if (id == 20)
                {
                    ProductCategoryTextBlock.Visibility = Visibility.Hidden;
                    System.Windows.Forms.DialogResult result = CustomMessageBox.Show("Вам есть 18 лет?", CustomMessageBox.CMessageBoxTitle.Внимание, CustomMessageBox.CMessageBoxButton.Да, CustomMessageBox.CMessageBoxButton.Нет);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        ProductList = DB.entities.Product.Where(c => (id == 0 || c.Category.CategoryID == id) && (search.Text == "" ? true : c.ProductName.Contains(search.Text))).ToList();
                        ProductsListView.ItemsSource = ProductList;
                        messageShown = true;
                    }
                    else
                    {
                        NavigationService.GoBack();
                    }
                }
            }
            EmptySearchPanel.Visibility = Visibility.Hidden;
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Visible;
            ProductList = DB.entities.Product.Where(c => (id == 0 || c.Category.CategoryID == id) && (search.Text == "" ? true : c.ProductName.Contains(search.Text))).ToList();
            ProductsListView.ItemsSource = ProductList;
            LoadDataPanel.Visibility = Visibility.Visible;
            ProductsPanel.Visibility = Visibility.Collapsed;
            ProductCategoryTextBlock.Visibility = Visibility.Collapsed;
            await Task.Delay(1000);
            LoadDataPanel.Visibility = Visibility.Collapsed;
            ProductsPanel.Visibility = Visibility.Visible;
            ProductCategoryTextBlock.Visibility = Visibility.Visible;
            if (ProductsListView.HasItems == true)
            {
                EmptySearchPanel.Visibility = Visibility.Hidden;
                ProductList = DB.entities.Product.Where(c => (id == 0 || c.Category.CategoryID == id) && (search.Text == "" ? true : c.ProductName.Contains(search.Text))).ToList();
                ProductsListView.ItemsSource = ProductList;
            }
            else if (SpaceCheck.Check(search.Text))
            {
                ProductList = DB.entities.Product.ToList();
                ProductsListView.ItemsSource = ProductList;
            }
            else
            {
                EmptySearchPanel.Visibility = Visibility.Visible;
                ProductCategoryTextBlock.Visibility = Visibility.Collapsed;
                LoadDataPanel.Visibility = Visibility.Hidden;
            }
        }
        private void AddInFavorites_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavouritesPage());
        }

        private void ProductBorder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2)
            {
                Product CurrentProduct = (Product)ProductsListView.SelectedItem;
                NavigationService.Navigate(new ProductsDescriptionPage(CurrentProduct));
            }
        }

        private void AddFavouriteButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Product CurrentProduct = ((Product)((PackIcon)sender).DataContext);
            var FavouritesProduct = CurrentProduct.Favorites.FirstOrDefault(c => c.ClientID == CurrentClient.client.ClientID);
            if (FavouritesProduct != null)
            {
                CustomMessageBox.Show("Данный товар уже находится в избранном.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                if (CurrentProduct != null && CurrentClient.client != null)
                {
                    favorites.ProductID = CurrentProduct.ProductID;
                    favorites.ClientID = CurrentClient.client.ClientID;
                    DB.entities.Favorites.Add(favorites);
                    DB.entities.SaveChanges();
                    CustomMessageBox.Show("Товар добавлен в избранное.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
                }
                else
                {
                    NavigationService.Navigate(new AuthtorizationPage());
                }
            }
        }

        private void InBasketButton_Click(object sender, RoutedEventArgs e)
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
                CustomMessageBox.Show("Товар добавлен в корзину.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
        }
    }
}
