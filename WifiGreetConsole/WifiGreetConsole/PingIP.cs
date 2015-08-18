﻿using System;
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
        char[] delimiterChars = { ' ',',' };

        public string PingIPAddress(IPAddress address, Process process)
        {
            StreamWriter cmd_writer = process.StandardInput;
            cmd_writer.WriteLine("cls");
            cmd_writer.WriteLine("arp-ping "+address.ToString()+" -n 2 -x");
            cmd_writer.Close();

            string Output = process.StandardOutput.ReadToEnd();
            string[] strings = Output.Split(delimiterChars,StringSplitOptions.RemoveEmptyEntries);

            for (int i=1;i<strings.Length;i++)
            {
                if (strings[i] == "successful" & strings[i-1] == "0")
                {
                    return "address:" + address.ToString() + " NOT OK";
                }
            }

            return "address:"+address.ToString()+" OK";
        }

        public Process StartPingingProcess(string path)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/K"+"cd "+path) //actually setting process
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process proc = new Process(); 
            proc.StartInfo = procStartInfo;
            proc.Start();               //actually this is where pinging process is started

            return proc;
        }
    }
}
