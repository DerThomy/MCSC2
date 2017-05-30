using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using Renci.SshNet;

namespace SSHLib
{
    public static class IPHandler
    {
        public static String getInternalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString().Trim();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public static String getExternalIP()
        {
            String IP = new WebClient().DownloadString("http://icanhazip.com");
            return IP.Trim();
        }

        public static String getHostEntry(String url)
        {
            return Dns.GetHostEntry(url).AddressList.First().ToString().Trim();
        }

        public static String getIPOfLocation(String url, String localIP = "")
        {
            if(getHostEntry(url) == getExternalIP())
            {
                return localIP == "" ? getInternalIP() : localIP;
            }
            else
            {
                return getExternalIP();
            }
        }
    }
}
