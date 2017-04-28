using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace SSHLib
{
    public class SshHandler
    {
        private SshClient client;

        public SshHandler(String host, String username, String password)
        {
            client = new SshClient(host, username, password);
        }

        public virtual void ExecuteCommand(String command)
        {
            var cmd = client.CreateCommand(command);

            try
            {
                client.Connect();

                cmd.Execute();
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Couldn't execute command: " + command + ": " + e);
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void setClient(String host, String username, String password)
        {
            client = new SshClient(host, username, password);
        }
    }
}
