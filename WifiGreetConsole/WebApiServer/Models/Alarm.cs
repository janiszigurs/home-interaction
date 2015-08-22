using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServer.Models
{
    public class Alarm
    {
        public int id { get; set; }
        public bool isRepeatable { get; set; }
        public DateTime AlarmTime { get; set; }
        public int SnoozeCount { get; set; }
        public bool[] weekdays { get; set; }
        public string AlarmTuneLocation { get; set; }
        public string AlarmText { get; set; }
        public string Owner { get; set; }
        public DateTime AlarmCreated { get; set; }
    }
}