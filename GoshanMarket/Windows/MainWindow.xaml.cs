using GoshanMarket.Classes;
using GoshanMarket.Models;
using GoshanMarket.Pages;
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
using GoshanMarket.MessageBox;
using System.IO;

namespace GoshanMarket
{
    public partial class MainWindow : Window
    {
        public ProdusctsPage ProdusctsPage;
        public MainWindow()
        {
            InitializeComponent();
            ProdusctsPage = new ProdusctsPage(SearchTextBox); 
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWin.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MyProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage(CurrentClient.client));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.EmailUser = "";
            Properties.Settings.Default.PasswordUser = "";
            Properties.Settings.Default.Save();
            CurrentClient.client = null;
            NameUser.Visibility = Visibility.Collapsed;
            Authtorization.Visibility = Visibility.Visible;
            DB.entities.SaveChanges();
            MainFrame.Navigate(new MainPage());
        }

        private void AuthtorizationButton_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentClient.client != null)
            {
                ContextMenuUser.Visibility = Visibility.Visible;
            }
            else
            {
                ContextMenuUser.Visibility = Visibility.Hidden;
            }
        }

        private void AuthtorizationButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentClient.client == null)
            {
                MainFrame.Navigate(new AuthtorizationPage());
            }
        }

        private void BasketButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new BasketPage());
        }

        private void FavoritesButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new FavouritesPage());
        }

        private void BackButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainFrame.CanGoBack == true)
            {
                SearchTextBox.Text = string.Empty;
                Keyboard.ClearFocus();
                MainFrame.GoBack();
            }
        }
        public static async Task<bool> GetIdle(TextBox txb)
        {
            string txt = txb.Text;
            await Task.Delay(500);
            return txt == txb.Text;
        }

        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(await GetIdle(SearchTextBox))
            {
                if(SearchTextBox.Text != string.Empty)
                {
                    ProdusctsPage.Page_Loaded(sender,e);
                    MainFrame.Navigate(ProdusctsPage);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            string EmailAddress = Properties.Settings.Default.EmailUser;
            string Password = Properties.Settings.Default.PasswordUser;
            Client ClientAuthtorization = DB.entities.Client.FirstOrDefault(c => c.EmailAddress == EmailAddress && c.PasswordClient == Password);
            if (ClientAuthtorization != null)
            {
                CurrentClient.client = ClientAuthtorization;
                mainWindow.Authtorization.Visibility = Visibility.Collapsed;
                mainWindow.NameUser.Visibility = Visibility.Visible;
                mainWindow.NameUser.Text = CurrentClient.client.FirstName;
                MainFrame.Navigate(new MainPage());
            }           
            else
            {
                MainFrame.Navigate(new MainPage());
            }
        }

        private void MyOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ClientOrdersPage());
        }

        private void MainPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Directory.Delete("1234");
        }
    }
}
