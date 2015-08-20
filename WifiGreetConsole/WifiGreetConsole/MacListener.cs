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
        UserManager manager;
        PingIP pingip;
        List<PhysicalAddress> mac_addreses;
        List<IPAddress> IP_addreses;
        string filepath;
        Process process;
        ProcessStartInfo procStartInfo;

        public string ReturnLocation()
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
            mac_list.RemoveRange(0,mac_list.Count);
        }

        public void ResetIPList(List<IPAddress> ip_list)
        {
            ip_list.RemoveRange(0, ip_list.Count);
        }


        public string[] SplitString(string string_to_split) //shouldn't be in this class
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

        public void RunMainThread()
        {
            ProcessStartInfo PSI = SetARPProcessStartInfo();
            

            Thread main = new Thread(HandleConnections);
            //main.IsBackground = true;
            main.Start();
        }


        public void HandleConnections() //used for thread
        {
            while (true)
            {
                Console.WriteLine("Entering HandleConnections main loop");
                Process proc = StartARPProcess();
                //SendARPTableRequest(process);
            
                Console.WriteLine("Getting IP and MAC from ARP"); //gets error when StreamWriter used
                GetAndAddAddresses(process, IP_addreses, mac_addreses); //gets mac and ip addresses from ARP -a
                
                foreach (IPAddress ip in IP_addreses) //go through each IP in ARP table and pings it
                {
                    pingip.PingIPAddress(ip);
                }

                Console.WriteLine("Checking users");

                foreach (PhysicalAddress mac in mac_addreses)
                {
                    manager.AddUserIfNecessary(manager.CheckIfUserIsConnectedAndExists(mac), mac, filepath); //cheks and adds user if necessary
                }

                Console.WriteLine("Going to disconnect users");
                manager.UserStatusDisconnect(mac_addreses);
                Console.WriteLine("Disconnected users");
                ResetMACList(mac_addreses);
                ResetIPList(IP_addreses);
                Console.WriteLine("Exiting HandleConnection loop");
                Thread.Sleep(5000);
            }
        }

        /*public void SendARPTableRequest(Process process)
        {
            Console.WriteLine("Sending ARP table request");

            StreamWriter command_writer = process.StandardInput;
            command_writer.WriteLine("cls");
            command_writer.WriteLine("arp -a");
            command_writer.Close();
        }*/

    }
}
