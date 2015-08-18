using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace WifiGreetConsole
{
    public class User
    {
        public int id { get; set; }
        public PhysicalAddress mac_address { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string gender { get; set; }
    }
}
