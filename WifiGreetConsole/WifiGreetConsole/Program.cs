using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Threading;

namespace WifiGreetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Alarm.AlarmManager Manager = new Alarm.AlarmManager(@"d:\json.txt");
            //Manager.AddAlarm(19,15);
            Console.Write(Manager.Alarms.Count);
            new Thread(delegate () { Manager.StartAlarmClock(); }).Start();
            Console.Write("Thread Started /n");
            Thread.Sleep(1000);

            // Asynchronous
            //synthesizer.SpeakAsync("Hello World");
        }
    }
}
