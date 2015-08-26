using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiGreetConsole.Alarm
{
    public class Alarm
    {
        public Guid id { get; set; }
        public bool isRepeatable { get; set; }
        public DateTime AlarmTime { get; set; }
        public int SnoozeCount { get; set; }
        public List<bool> weekdays { get; set; }
        public string AlarmTuneLocation { get; set; }
        public string AlarmText { get; set; }
        public string Owner { get; set; }
        public DateTime AlarmCreated { get; set; }
    }
}
