using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiGreetConsole.Alarm
{
    class Alarm
    {
        //by default snooze could be implemented with 5 minutes...
        public Guid id { get; set; }
        public bool isRepeatable { get; set; }
        public DateTime AlarmTime { get; set; }
        public int SnoozeCount { get; set; }
        public bool[] weekdays{ get; set; } 
        public AlarmTune Tune { get; set; }
        public Alarm()
        {
            weekdays = new bool[8];
            for (int i=0; i<=7; i++)
            {
                this.weekdays[i] = true;
            }
        }
    }
}
