using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SSHLib
{
    public static class MySqlHandler
    {
        public static String[] MySqlSelectData(String command, String connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(command, conn);

            List<String> data = new List<String>();

            if (!command.ToLower().Contains("select")){
                Console.WriteLine("Use MySqlNoQueryData() for no 'Select' commands");
                return null;
            }

            int coloumnCount = command.Remove(command.ToLower().IndexOf("from"), command.Length - command.ToLower().IndexOf("from")).Count(f => f == ',') + 1;

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                int counter = 0;
                while (reader.Read())
                {
                    String dataString = String.Empty;
                    for(int i = 0; i < coloumnCount; i++)
                    {
                        dataString += reader[i].ToString() + " ";
                    }
                    data.Add(dataString.Trim());
                    counter++;
                }
                foreach(String dat in data)
                {
                    dat.Trim();
                }
                return data.ToArray();
            }
            catch(MySqlException e)
            {
                Console.WriteLine("GetMySqlData from: \"" + command + "\" failed:");
                Console.Error.WriteLine(e);
                return null;
            }
        }

        public static void MySqlNoQueryData(String command, String connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(command, conn);

            List<String> data = new List<String>();

            if (command.ToLower().Contains("select")){
                Console.WriteLine("Use MySqlSelectData() for no Select commands");
                return;
            }

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                Console.WriteLine("MySqlNoQueryData from: \"" + command + "\" failed:");
                Console.Error.WriteLine(e);
                return;
            }
        }
    }
}
