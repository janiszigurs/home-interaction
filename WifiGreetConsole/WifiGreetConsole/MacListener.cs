using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace WifiGreetConsole
{
    public class MacListener
    {
        private UserManager manager;
        private PingIP pingip;
        private List<PhysicalAddress> mac_addreses;
        private List<IPAddress> IP_addreses;
        private string filepath;
        private Process process;
        private ProcessStartInfo procStartInfo;
        private Thread main;

        private int state;

        private string ReturnLocation()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            path = path.Remove(path.Length - 9);
            path = path + @"UserData\Users.txt";

            return path;
        }

        public MacListener()
        {
            filepath = ReturnLocation();
            manager = new UserManager(filepath);    //add filepath!
            pingip = new PingIP();
            mac_addreses = new List<PhysicalAddress>();
            IP_addreses = new List<IPAddress>();
            //main = new Thread(Listen);
        }


        public ProcessStartInfo SetARPProcessStartInfo()
        {
            procStartInfo = new ProcessStartInfo("cmd", "/C"+"ARP -a")
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = false,
                UseShellExecute = false,
                CreateNoWindow = true
                
            };
            return procStartInfo;     
        }



        public Process StartARPProcess()
        {
            process = new Process();
            process.StartInfo = procStartInfo;
            process.Start();      

            return process;
        }



        public void ResetMACList(List<PhysicalAddress> mac_list)
        {
            lock(mac_list) { mac_list.RemoveRange(0, mac_list.Count);}                       
        }

        public void ResetIPList(List<IPAddress> ip_list)
        {
            lock (ip_list) { ip_list.RemoveRange(0, ip_list.Count); }                        
        }


        private string[] SplitString(string string_to_split) //shouldn't be in this class
        {
            char[] charSeparators = { ',', ' ' };
            string[] strings = string_to_split.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

            return strings;
        }


        public void GetAndAddAddresses(Process process,List<IPAddress> ip_addr_list, List<PhysicalAddress> mac_addr_list)
        {
            string strOutput = process.StandardOutput.ReadToEnd();
            process.WaitForExit(50);

            string[] text = SplitString(strOutput);

            for (int k = 0; k < text.Length; k++)
            {
                if (text[k] == "dynamic")
                {
                    mac_addr_list.Add(PhysicalAddress.Parse(text[k-1].ToUpper()));
                    ip_addr_list.Add(IPAddress.Parse(text[k - 2].ToUpper()));
                    Console.WriteLine(text[k - 2]);
                }
            }
        }

        public void StartToListen()
        {
            ProcessStartInfo PSI = SetARPProcessStartInfo();
            state = 1;
            main = new Thread(Listen); //required because can't restart thread after stopping/aborting it
            if (((main.ThreadState & System.Threading.ThreadState.Stopped) == System.Threading.ThreadState.Stopped) || ((main.ThreadState & System.Threading.ThreadState.Unstarted) == System.Threading.ThreadState.Unstarted)) 
            {
                main.Start();
            }           
        }


        public void Listen() //used for thread
        {
            while (state == 1)
            {
                Console.WriteLine("Entering maclistener thread loop..");
                Process proc = StartARPProcess();
            
                GetAndAddAddresses(process, IP_addreses, mac_addreses); //gets mac and ip addresses from ARP -a
                
                /*foreach (IPAddress ip in IP_addreses) //go through each IP in ARP table and pings it
                {
                    pingip.PingIPAddress(ip);
                }*/

                foreach (PhysicalAddress mac in mac_addreses)
                {
                    manager.AddUserIfNecessary(manager.CheckIfUserIsConnectedAndExists(mac), mac, filepath); //cheks and adds user if necessary
                }

                manager.UserStatusDisconnect(mac_addreses);
                ResetMACList(mac_addreses);
                ResetIPList(IP_addreses);
                Thread.Sleep(5000);
            }
        }

        public void StopToListen()
        {
            state = 0;
            main.Abort();
        }

        public List<IPAddress> ReturnIPAddressList()
        {
            return IP_addreses;
        }

        public List<PhysicalAddress> ReturnMACAddressList()
        {
            return mac_addreses;
        }

        public List<User> ReturnUserList()
        {
            return manager.users;
        }
    }
}
