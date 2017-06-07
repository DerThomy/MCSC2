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

            BackgroundWorkHandler BwH = new BackgroundWorkHandler();

            BackgroundWorker Main = new BackgroundWorker();
            BwH.SetupBW(ref Main, false, false);

            Main.DoWork += new DoWorkEventHandler(Main_DoWork);

            Main.DoWork += BwH.DoWork(new Action(()=> {
                Dispatcher.Invoke(new Action(() =>
                {
                    updateGui();
                }),DispatcherPriority.ContextIdle);
                Thread.Sleep(500);
            }));
            Main.RunWorkerAsync();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }



        public virtual void Main_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    LabelIP.Content = IPHandler.getIPOfLocation("altmuensterkoehler.hopto.org", "10.0.0.200");
                    ToolBox.DelayAction(500, new Action(() => { }));
                }
            }
        }

        private void updateGui()
        {
            this.LabelIP.Content = IPHandler.getIPOfLocation("altmuensterkoehler.hopto.org", "10.0.0.200");
        }
    }
}
