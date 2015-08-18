using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace WifiGreetConsole
{
    public class PingIP
    {
        char[] delimiterChars = { ' ' };

        public string PingIPAddress(IPAddress address, Process process) //StartInfo.RedirectStandardInput = true; must be process startinfo..
        {
            StreamWriter cmd_writer = process.StandardInput;
            cmd_writer.WriteLine("arp-ping "+address.ToString()+" -n 1");

            string Output = process.StandardOutput.ReadToEnd();
            string[] strings = Output.Split(delimiterChars,StringSplitOptions.RemoveEmptyEntries);

            for (int i=0;i<strings.Length;i++)
            {
                if (strings[i+1] == "successful" & strings[i] == "0")
                {
                    return "address:" + address.ToString() + " NOT OK";
                }
            }

            cmd_writer.Close();
            return "address:"+address.ToString()+" OK";
        }
    }
}
