using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServer.Models
{
    public class Reminder
    {
        public Guid id { get; set; }
        public int Priority { get; set; }
        public DateTime RemindTime { get; set; }
        public string RemindText { get; set; }
        public string Owner { get; set; }
        public DateTime ReminderCreated { get; set; }
    }
}