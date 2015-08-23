using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiServer.Models;
using WebApiServer.Components;

namespace WebApiServer.Controllers
{
    public class ReminderController : ApiController
    {
        ReminderManager Rm = new ReminderManager();
        List<Reminder> reminderList = new List<Reminder>();


        [Route("allreminders")]
        public IEnumerable<Reminder> GetAllReminders()
        {
            //return alarmsList;
            reminderList = Rm.LoadReminders(@"d:\reminders.json");
            return reminderList;
        }

        [Route("reminders")]
        public IEnumerable<Reminder> GetAllRemindersForUser(string user)
        {
            //return alarmsList;
            reminderList = Rm.LoadReminders(@"d:\reminders.json");
            var reminder = reminderList.Where(p => p.Owner == user);
            if (reminder.Count() == 0)
            {
                //some error here should be applied.
            }
            return reminder;
        }


        [Route("alarms/get/{Id}")]
        public IHttpActionResult GetAlarm(Guid Id)
        {
            reminderList = Rm.LoadReminders(@"d:\reminders.json");
            var alarm = reminderList.FirstOrDefault((p) => p.id == Id);
            if (reminderList.Count == 0)
            {
                return Ok("NO REMINDERS PRESENT ON SERVER, something went wrong");
            }
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok(alarm);
        }

        [System.Web.Http.HttpGet]
        [Route("reminders/delete/{Id}")]
        public IHttpActionResult DelteAlarm(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Rm.DeleteReminder(Id);
            return Ok("Reminder succesfully delted!");
        }

        [System.Web.Http.HttpGet]
        [Route("reminders/addstatic")]
        public IHttpActionResult AddStaticAlarm()
        {
            Reminder tmpRem = new Reminder();
            tmpRem.id = Guid.NewGuid();
            tmpRem.ReminderCreated = DateTime.Now;
            tmpRem.RemindTime = DateTime.Now.AddHours(10);
            tmpRem.Priority = 1;
            tmpRem.Owner = "arturszigurs";
            tmpRem.RemindText = "YoloSwagnStuff";
            //adds and saves the Reminder;
            Rm.LoadReminders(@"d:\reminders.json");
            Rm.Reminders.Add(tmpRem);
            Rm.SaveReminders();
            return Ok("Reminder succesfully added");
        }

        [System.Web.Http.HttpGet]
        [Route("reminders/add")]
        public IHttpActionResult AddReminderWithParams(string owner, int priority, string text, int hh, int mm, string date)
        {
            Reminder tmpRem = new Reminder();
            tmpRem.id = Guid.NewGuid();
            tmpRem.ReminderCreated = DateTime.Now;
            tmpRem.RemindTime = DateTime.Now.AddHours(10);
            tmpRem.Priority = priority;
            tmpRem.Owner = owner;
            tmpRem.RemindText = text;
            //adds and saves the Reminder;
            Rm.LoadReminders(@"d:\reminders.json");
            Rm.Reminders.Add(tmpRem);
            Rm.SaveReminders();
            return Ok("Reminder succesfully added");
        }
    }
}
