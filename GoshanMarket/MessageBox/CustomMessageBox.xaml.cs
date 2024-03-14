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

namespace GoshanMarket.MessageBox
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }
        static CustomMessageBox CMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public enum CMessageBoxButton
        {
            OK,
            Отмена,
            Нет,
            Да
        }
        public enum CMessageBoxTitle
        {
            Ошибка,
            Сообщение,
            Внимание
        }
        public static DialogResult Show(string message, CMessageBoxTitle title, CMessageBoxButton ButtonOk, CMessageBoxButton ButtonNo)
        {
            CMessageBox = new CustomMessageBox();
            CMessageBox.MessageText.Text = message;
            CMessageBox.ButtonOk.Content = CMessageBox.GetMessageButton(ButtonOk);
            CMessageBox.ButtonCancel.Content = CMessageBox.GetMessageButton(ButtonNo);
            CMessageBox.TextTitle.Text = CMessageBox.GetTitle(title);


            switch (title)
            {
                case CMessageBoxTitle.Ошибка:
                    CMessageBox.MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
                    CMessageBox.MessageIcon.Foreground = Brushes.IndianRed;
                    CMessageBox.ButtonCancel.Visibility = Visibility.Collapsed;
                    CMessageBox.ButtonOk.SetValue(Grid.ColumnSpanProperty, 2);
                    CMessageBox.ButtonOk.Width = 80;
                    break;
                case CMessageBoxTitle.Сообщение:
                    CMessageBox.MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.InformationVariantCircleOutline;
                    CMessageBox.MessageIcon.Foreground = Brushes.DodgerBlue;
                    CMessageBox.ButtonCancel.Visibility = Visibility.Collapsed;
                    CMessageBox.ButtonOk.SetValue(Grid.ColumnSpanProperty, 2);
                    CMessageBox.ButtonOk.Width = 80;
                    break;
                case CMessageBoxTitle.Внимание:
                    CMessageBox.MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertOctagram;
                    CMessageBox.MessageIcon.Foreground = Brushes.OrangeRed;
                    break;
            }
            CMessageBox.ShowDialog();
            return result;
        }
        public string GetTitle(CMessageBoxTitle value)
        {
            return Enum.GetName(typeof(CMessageBoxTitle), value);

        }

        public string GetMessageButton(CMessageBoxButton value)
        {
            return Enum.GetName(typeof(CMessageBoxButton), value);
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            CMessageBox.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            CMessageBox.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
