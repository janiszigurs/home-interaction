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
            Alarm.AlarmManager Manager = new Alarm.AlarmManager("Alarmsconfigfile");
            Manager.AddAlarm(18,00);
            Manager.AddAlarm(18,10);
            Manager.AddAlarm(18,11);
            Manager.AddAlarm(18,12);
            new Thread(delegate () { Manager.StartAlarmClock(); }).Start();
            Console.Write("Thread Started");
            Thread.Sleep(10000000);
            
         //.Write(DateTime.Now.ToString("h and mm tt"));
            // Synchronous
            synthesizer.Speak(sup);

            // Asynchronous
            //synthesizer.SpeakAsync("Hello World");
        }
    }
}
