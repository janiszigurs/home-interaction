using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServerV2.Models
{
    public class Alarm
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRepeatable { get; set; }
        public DateTime AlarmTime { get; set; }
        public DateTime AlarmCreated { get; set; }
        public DateTime AlarmLastCalled { get; set; }
        public int SnoozeCount { get; set; }
        public List<bool> Weekdays { get; set; }
        public string AlarmTuneLocation { get; set; }
        public string AlarmText { get; set; }
        public string Owner { get; set; }
        public bool Enabled { get; set; }
        public string AlarmDeviceType{ get; set; }
    }
}