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

        SshHandler ServerSsHHandler;
        List<Server> ServerList;
        BackgroundWorkHandler BwH;
        BackgroundWorker GUI;
        String oldIP = null;
        String newIP = null;
        String IPServer = IPHandler.getIPOfLocation("altmuensterkoehler.hopto.org", "10.0.0.20");
        String IPRes = IPHandler.getIPOfLocation("altmuensterkoehler.hopto.org", "10.0.0.200");
        bool ServerStatus;


        public MainWindow()
        {
            InitializeComponent();

            ServerSsHHandler = new SshHandler(IPRes, "root", "Anakankoe99");
            BwH = new BackgroundWorkHandler();
            GUI = new BackgroundWorker();

            ServerList = new List<Server>()
            {
                new Server() { StartCommand = "./ARK/startserver", SshHanlder = ServerSsHHandler, Name = "ARKServer", SessionName = "ark_server", ControlPort=7777}
            };
            ServerListBox.ItemsSource = ServerList.Select(n => n.Name);
            ServerListBox.SelectedIndex = 0;

            BwH.SetupBW(ref GUI, false, false);
            GUI.DoWork += BwH.DoWork(new Action(()=> {
                Dispatcher.InvokeAsync(new Action(() =>
                {
                    updateGui();
                }),DispatcherPriority.ContextIdle);
                Thread.Sleep(1000);
            }));
            GUI.RunWorkerAsync();
        }

        private void updateGui()
        {
            if(ServerListBox.SelectedIndex != -1)
            {
                ServerStatus = Convert.ToInt32(ServerSsHHandler
                    .ExecuteCommandWithOutput("./checkserver "+IPServer+" "+ServerList[ServerListBox.SelectedIndex].ControlPort)) == 0 ? false : true;
                if(!ServerStatus){
                    LabelStatus.Content = "Down";
                    LabelStatus.Foreground = Brushes.Red;
                }else
                {
                    LabelStatus.Content = "Up";
                    LabelStatus.Foreground = Brushes.Green;
                }
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServerList[ServerListBox.SelectedIndex].StartServer();
            }
            catch(Exception t)
            {
                Debug.Text += "\nDu musst einen Server Auswählen ... falls du dies bereits getan hast kontaktiere mich bitte";
                Debug.Text += "\nFehler: " + t;
            }  
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            ServerList[ServerListBox.SelectedIndex].StopServer();
        }
    }

    public class Server
    {
        public String StartCommand { get; set; }
        public String Name { get; set; }
        public String SessionName { get; set; }
        public SshHandler SshHanlder { get; set; }
        public int ControlPort { get; set; }

        public void StartServer()
        {
            SshHanlder.ExecuteCommandWithoutOutput("sshpass -p Anakankoe99 ssh thomy@10.0.0.20 '"+ StartCommand +"'");
        }

        public void StopServer()
        {
            SshHanlder.ExecuteCommandWithoutOutput("sshpass -p Anakankoe99 ssh thomy@10.0.0.20 'tmux kill-session -t"+ SessionName +"'");
        }
    }
}
