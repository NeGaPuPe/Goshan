using System;
using System.Collections.Generic;
using System.Linq;
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
using GoshanMarket.Models;
using System.IO;
using GoshanMarket.MessageBox;

namespace GoshanMarket.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
        }
        private void Category_PreviewMouseLeftButton(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProdusctsPage(((Border)sender).Tag));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Hidden;
            mainWindow.SearchTextBox.Text = string.Empty;
        }
    }
}
