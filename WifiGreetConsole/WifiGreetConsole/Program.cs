using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Configuration;

namespace WifiGreetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Alarm.AlarmManager Manager = new Alarm.AlarmManager();
            Manager.LoadAlarms(@"d:\json.txt");
            Manager.AddAlarm(20,15);
            Manager.AddAlarm(20,19);
            Manager.AddAlarm(21,15);
            Console.Write(Manager.Alarms.Count);
            //new Thread(delegate () { Manager.StartAlarmClock(); }).Start();

            // Asynchronous
            //synthesizer.SpeakAsync("Hello World");*/

            //fetched from settings
            Console.WriteLine(ConfigurationManager.AppSettings["testkey"]);
            Console.WriteLine(ConfigurationManager.AppSettings["a"]);


            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            path = path.Remove(path.Length - 9);
            path = path + @"executables\";
            Console.WriteLine(path);

            PingIP pingip = new PingIP();
            Process proc = pingip.StartPingingProcess(path);
            

            IPAddress ip = IPAddress.Parse("192.168.1.6");

            Console.WriteLine(pingip.PingIPAddress(ip,proc));
            Console.ReadKey();
         }
    }
}
