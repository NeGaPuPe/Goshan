using GoshanMarket.Classes;
using GoshanMarket.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoshanMarket.ErrorReport
{
    public partial class ErrorReportWindow : Window
    {
        public ErrorReportWindow()
        {
            InitializeComponent();
        }

        static ErrorReportWindow ERWindow;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public enum ERWindowButton
        {
            Нет,
            Да
        }
        public static DialogResult Show(ERWindowButton ButtonYes, ERWindowButton ButtonNo)
        {
            ERWindow = new ErrorReportWindow();
            ERWindow.ButtonYes.Content = ERWindow.GetMessageButton(ButtonYes);
            ERWindow.ButtonNo.Content = ERWindow.GetMessageButton(ButtonNo);
            ERWindow.MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertOctagram;
            ERWindow.MessageIcon.Foreground = Brushes.OrangeRed;
            ERWindow.ShowDialog();
            return result;
        }

        public string GetMessageButton(ERWindowButton value)
        {
            return Enum.GetName(typeof(ERWindowButton), value);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Email.useremail = EmailAddressTextBox.Text;
            if (!EmailAddressTextBox.Text.Contains("@mail.ru") && !EmailAddressTextBox.Text.Contains("@gmail.com") && !EmailAddressTextBox.Text.Contains("@yandex.ru"))
            {
                CustomMessageBox.Show("Почта не указана или неверный формат почты.", CustomMessageBox.CMessageBoxTitle.Сообщение, CustomMessageBox.CMessageBoxButton.OK, CustomMessageBox.CMessageBoxButton.Отмена);
            }
            else
            {
                result = System.Windows.Forms.DialogResult.Yes;
                ERWindow.Close();
            }
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            ERWindow.Close();
        }
    }
}
