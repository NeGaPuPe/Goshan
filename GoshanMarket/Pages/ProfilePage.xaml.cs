using GoshanMarket.Classes;
using GoshanMarket.Entities;
using GoshanMarket.MessageBox;
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

namespace GoshanMarket.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage(Client client)
        {
            InitializeComponent();
            NameTextBox.PreviewTextInput += new TextCompositionEventHandler(TextBox_PreviewTextInput);
            SurNameTextBox.PreviewTextInput += new TextCompositionEventHandler(TextBox_PreviewTextInput);
            PatronymicTextBox.PreviewTextInput += new TextCompositionEventHandler(TextBox_PreviewTextInput);
            EmailTextBox.PreviewTextInput += new TextCompositionEventHandler(EmailTextBox_PreviewTextInput);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.SearchTextBox.Text = string.Empty;
            Keyboard.ClearFocus();
            this.DataContext = client;
            ProfileClientList = DB.entities.Client.ToList();
            if (CurrentClient.client.GenderID == 1)
            {
                MaleRButton.IsChecked = true;
            }
            else
            {
                FemaleRButton.IsChecked = true;
            }
        }
        List<Client> ProfileClientList = new List<Client>();

        private void SaveButtonButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpaceCheck.Check(NameTextBox.Text) == true || SpaceCheck.Check(EmailTextBox.Text) == true)
            {
                CustomMessageBox.Show("Обязательные поля не заполнены.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else if (!EmailTextBox.Text.Contains("@mail.ru") && !EmailTextBox.Text.Contains("@gmail.com") && !EmailTextBox.Text.Contains("@yandex.ru"))
            {
                CustomMessageBox.Show("Неверный формат эл. почты.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else if (DateBirthPicker.SelectedDate >= DateTime.Now)
            {
                CustomMessageBox.Show("Неверно указана дата рождения.", CustomMessageBox.CMessageBoxTitle.Ошибка, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                if (MaleRButton.IsChecked == true)
                {
                    CurrentClient.client.GenderID = 1;
                }
                else
                {
                    CurrentClient.client.GenderID = 2;
                }
                CurrentClient.client.DateBirth = DateBirthPicker.SelectedDate;
                DB.entities.SaveChanges();
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow.NameUser.Text = CurrentClient.client.FirstName;
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

        private void FavouritesButton_MouseEnter(object sender, MouseEventArgs e)
        {
            FavouritesButton.Background = Brushes.White;
        }

        private void FavouritesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            FavouritesButton.Background = Brushes.WhiteSmoke;
        }

        private void MyOrdersButton_MouseEnter(object sender, MouseEventArgs e)
        {
            MyOrdersButton.Background = Brushes.White;
        }

        private void MyOrdersButton_MouseLeave(object sender, MouseEventArgs e)
        {
           MyOrdersButton.Background = Brushes.WhiteSmoke;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.BackButton.Visibility = Visibility.Visible;
        }

        private void EmailTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsPunctuation(c) || e.Text[0] == 46 || e.Text[0] == 64)
                {
                    return;
                }
                e.Handled = true;
            }
        }
        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
