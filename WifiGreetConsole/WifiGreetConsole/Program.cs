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
using WifiGreetConsole.Backbone;

namespace WifiGreetConsole
{
    class Program
    {
        static bool _StopMainThread = false;

        static void Main(string[] args)
        {
            Alarm.AlarmManager Manager = new Alarm.AlarmManager();
            MacListener maclistener = new MacListener();
            maclistener.RunMainThread();



            /*
            //example of how to work with settings
            Console.WriteLine(ConfigurationManager.AppSettings["testkey"]);

            string input;

            while(!_StopMainThread)
            {
                Console.WriteLine("Select Option");
                input = Console.ReadLine();
                switch (input)
                {
                    case "start alarms":
                        Manager.LoadAlarms("StoredAlarms.json");
                        new Thread(delegate () { Manager.StartAlarmClock(); }).Start();
                        break;

                    case "add alarm":
                        Console.WriteLine("Please provide Hours");
                        int hours = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please provide Minutes");
                        int minutes = Convert.ToInt32(Console.ReadLine());
                        Manager.AddAlarm(hours, minutes);
                        Console.WriteLine("Alarm added");
                        #if DEBUG
                            new Logger("DEBUG", "Alarm added");
                        #endif
                        break;

                    case "alarm count":
                        Console.WriteLine("Currently you have:"+Manager.Alarms.Count());
                        break;

                    case "stop alarms":
                        Manager._stopThread = true;
                        break;

                    default:
                        _StopMainThread = true;
                        break;
                }
            }*/
        }
    }
}
