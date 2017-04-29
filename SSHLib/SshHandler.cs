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

        public virtual void ExecuteCommandWithoutOutput(String command)
        {
            try
            {
                client.Connect();

                var cmd = client.CreateCommand(command);

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

        public virtual String ExecuteCommandWithOutput(String command)
        {
            try
            {
                client.Connect();

                var cmd = client.CreateCommand(command);

                var output = cmd.Execute();

                return output.ToString();
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Couldn't execute command: " + command + ": " + e);

                return null;
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
