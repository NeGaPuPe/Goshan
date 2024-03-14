using GoshanMarket.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoshanMarket.Windows
{
    public partial class LoadScreen : Window
    {
        public LoadScreen()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_Dowork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }
        void worker_Dowork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                DB.entities.Client.Where(c => true).ToListAsync().Wait();
                Thread.Sleep(17);
            }
        }
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadProgressBar.Value = e.ProgressPercentage;
            LoadPercent.Text = LoadProgressBar.Value.ToString();
            if (LoadProgressBar.Value == 100)
            {
                MainWindow mainWindow = new MainWindow();
                Close();
                mainWindow.ShowDialog();
            }
        }
    }
}
