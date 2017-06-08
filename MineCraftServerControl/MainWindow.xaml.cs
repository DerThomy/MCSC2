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

            int i = 0;

            BackgroundWorkHandler BwH = new BackgroundWorkHandler();

            BackgroundWorker Main = new BackgroundWorker();

            Main.DoWork += BwH.DoWork(new Action(()=> {
                Dispatcher.InvokeAsync(new Action(() =>
                {
                    this.LabelIP.Content = IPHandler.getIPOfLocation("altmuensterkoehler.hopto.org", "10.0.0.200");
                    this.Debug.Text += i;
                    i++;
                }),DispatcherPriority.ContextIdle);
            }));
            Main.RunWorkerAsync();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
