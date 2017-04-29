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
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String host = "10.0.0.200";
            String username = "root";
            String pwd = "Anakankoe99";
            SshHandler SshRaspRoot = new SshHandler(host, username, pwd);

            Debug.Text = SshRaspRoot.ExecuteCommandWithOutput("ls");
        }
    }
}
