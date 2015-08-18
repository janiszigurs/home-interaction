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

        public MacListener()
        {
            manager = new UserManager();
            pingip = new PingIP();
            mac_addreses = new List<PhysicalAddress>();
            IP_addreses = new List<IPAddress>();
        }


        public ProcessStartInfo SetARPProcessStartInfo()
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/K")
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
                
            };
            return procStartInfo;     
        }



        public Process StartARPProcess(ProcessStartInfo procStartInfo)
        {
            Process process = new Process();
            process.StartInfo = procStartInfo;
            process.Start();      

            return process;
        }



        public void ResetMACList(List<PhysicalAddress> mac_list)
        {
            foreach (PhysicalAddress mac_address in mac_list)
            {
                mac_list.Remove(mac_address);
            }
        }



        public void SendARPTableRequest(Process process)
        {
            StreamWriter command_writer = process.StandardInput;
            command_writer.WriteLine("cls");
            command_writer.WriteLine("arp -a");
            command_writer.Close();
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
            process.WaitForExit();

            string[] text = SplitString(strOutput);

            for (int k = 0; k < text.Length; k++)
            {
                if (text[k] == "dynamic")
                {
                    mac_addr_list.Add(PhysicalAddress.Parse(text[k-1].ToUpper()));
                    ip_addr_list.Add(IPAddress.Parse(text[k - 2].ToUpper()));
                }
            }
        }



        public void HandleConnections(Process process,string filepath) //used for thread
        {
            SendARPTableRequest(process);
            GetAndAddAddresses(process, IP_addreses, mac_addreses);

            //TODO: ping ip addresses



            foreach (PhysicalAddress mac in mac_addreses)
            {
                manager.AddUserIfNecessary(manager.CheckIfUserIsConnectedAndExists(mac), mac,filepath);                             
            }


            manager.UserStatusDisconnect(mac_addreses);
            ResetMACList(mac_addreses);
        }
    }
}
