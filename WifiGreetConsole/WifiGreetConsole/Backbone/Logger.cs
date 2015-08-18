using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WifiGreetConsole.Backbone
{
    class Logger
    {
        public Logger(string level, string log)
        {
            string location = ConfigurationManager.AppSettings["loglocation"];

            System.IO.StreamWriter file = new System.IO.StreamWriter(location, true);
            file.WriteLine(level + " : " + log+"  |  "+DateTime.Now.ToString());
            file.Close();
        }
    }
}
