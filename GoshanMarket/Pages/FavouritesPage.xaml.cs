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
using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
using GoshanMarket.Models;
using MaterialDesignThemes.Wpf;

namespace GoshanMarket.Pages
{
    public partial class FavouritesPage : Page
    {
        Basket basket;
        public FavouritesPage()
        {
            InitializeComponent();
            basket = new Basket();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
        }
        List<Favorites> FavoritesList = new List<Favorites>();

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
                FavoritesList = DB.entities.Favorites.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
                FavoritesListView.ItemsSource = FavoritesList;
                if (FavoritesListView.HasItems == true)
                {
                    EmptyFavoritesPanel.Visibility = Visibility.Hidden;
                    FavoritesList = DB.entities.Favorites.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
                    FavoritesListView.ItemsSource = FavoritesList;
                }
                else
                {
                    EmptyFavoritesPanel.Visibility = Visibility.Visible;
                    MenuPanel.Visibility = Visibility.Hidden;
                    FavouritesTextBlock.Visibility = Visibility.Hidden;
                }
            }
        }

        private void DeleteFavouriteButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Favorites CurrentFavorites = ((Favorites)((PackIcon)sender).DataContext);
            DB.entities.Favorites.Remove(CurrentFavorites);
            DB.entities.SaveChanges();
            FavoritesList = DB.entities.Favorites.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
            FavoritesListView.ItemsSource = FavoritesList;
            if (FavoritesListView.HasItems == true)
            {
                EmptyFavoritesPanel.Visibility = Visibility.Hidden;
                FavoritesList = DB.entities.Favorites.Where(c => c.ClientID == CurrentClient.client.ClientID).ToList();
                FavoritesListView.ItemsSource = FavoritesList;
            }
            else
            {
                EmptyFavoritesPanel.Visibility = Visibility.Visible;
                MenuPanel.Visibility = Visibility.Hidden;
                FavouritesTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void MyProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage(CurrentClient.client));
        }

        private void FavouritesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new FavouritesPage());
        }

        private void MyOrdersButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ClientOrdersPage());
        }

        private void MyProfileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MyProfileButton.Background = Brushes.White;
        }

        private void MyProfileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MyProfileButton.Background = Brushes.WhiteSmoke;
        }

        private void MyOrdersButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MyOrdersButton.Background = Brushes.White;
        }

        private void MyOrdersButton_MouseLeave(object sender, MouseEventArgs e)
        {
            MyOrdersButton.Background = Brushes.WhiteSmoke;
        }

        private void InBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentClient.client == null)
            {
                NavigationService.Navigate(new AuthtorizationPage());
            }
            else
            {
                Favorites CurrentProduct = ((Favorites)((Button)sender).DataContext);
                basket.ProductID = (long)CurrentProduct.ProductID;
                basket.ClientID = CurrentClient.client.ClientID;
                DB.entities.Basket.Add(basket);
                DB.entities.SaveChanges();
            }
        }
    }
}
