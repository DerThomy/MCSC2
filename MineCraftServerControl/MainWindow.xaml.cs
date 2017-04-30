using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using Renci.SshNet;
using SSHLib;

namespace MineCraftServerControl
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BackgroundWorkerHandler BwH = new BackgroundWorkerHandler();

            BackgroundWorker Main = new BackgroundWorker();
            BwH.InitializeBW(Main, true, true);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }

    public class BackgroundWorkerHandler
    {
        public virtual void InitializeBW(BackgroundWorker bw, bool WorkerSupportsCancellation, bool WorkerReportsProgress)
        {
            if (WorkerSupportsCancellation == true)
            {
                bw.WorkerSupportsCancellation = true;
            }

            if (WorkerReportsProgress == true)
            {
                bw.WorkerReportsProgress = true;
            }
        }
    }

    public class BackgroundFunctions : MainWindow{
        public virtual void UpdateIP(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    //!!!!!!!!!!
                    System.Threading.Thread.Sleep(500);
                }
            }
        }
    }
}
