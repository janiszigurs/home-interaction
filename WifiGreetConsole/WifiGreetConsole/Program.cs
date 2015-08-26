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
            /*MacListener maclistener = new MacListener();
            maclistener.StartToListen();

            wSoundPlayer player = new wSoundPlayer();
            player.LoadSound("alarm_test.wav");
            player.PlaySound();
            Console.ReadKey();
            maclistener.StopToListen();
            Console.ReadKey();*/


            //example of how to work with settings





            Console.WriteLine(ConfigurationManager.AppSettings["testkey"]);

            string input;

            bool alreadyStarted = false;

            while(!_StopMainThread)
            {
                Console.WriteLine("Select Option");
                input = Console.ReadLine();
                switch (input)
                {
                    case "a_start":
                        if (alreadyStarted == false)
                        {
                            alreadyStarted = true;
                            Manager.LoadAlarms();
                            new Thread(delegate () { Manager.StartAlarmClock(); }).Start();
                        }
                        else
                        {
                            Console.WriteLine("Already started!");
                        }
                        break;

                    case "a_add":
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

                    case "a_count":
                        Console.WriteLine("Currently you have:"+Manager.Alarms.Count());
                        break;

                    case "a_stop":
                        Manager._stopThread = true;
                        break;

                    default:
                        _StopMainThread = true;
                        break;
                }
            }
        }
    }
}
