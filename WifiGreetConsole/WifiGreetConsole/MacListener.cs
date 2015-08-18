using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace WifiGreetConsole
{
    public class MacListener
    {
        UserManager manager;
        public MacListener()
        {
            manager = new UserManager();
        }
        public void ListenForMacs()
        {
            while (true)
            {
                System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
                pProcess.StartInfo.FileName = @"C:\Windows\System32\ARP.EXE";
                pProcess.StartInfo.Arguments = "-a";
                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.Start();

                string strOutput = pProcess.StandardOutput.ReadToEnd();

                pProcess.WaitForExit();
                char[] charSeparators = new char[] { ' ' };
                string[] strings = strOutput.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                List<PhysicalAddress> mac = new List<PhysicalAddress>();

                for (int k = 0; k < strings.Length; k++)
                {
                    if (strings[k] == "dynamic")
                    {
                        string str_addr = strings[k - 1];
                        mac.Add(PhysicalAddress.Parse(str_addr.ToUpper()));
                        manager.ConnectUsers(PhysicalAddress.Parse(str_addr.ToUpper()));
                    }
                }
                manager.DisconnectUsers(mac);
                Thread.Sleep(5000);
            }
        }  
         
    }
}
