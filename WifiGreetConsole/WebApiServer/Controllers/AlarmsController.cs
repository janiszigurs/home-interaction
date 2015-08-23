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
    public class AlarmsController : ApiController
    {
        AlarmManager Am = new AlarmManager();
        List<Alarm> alarmsList = new List<Alarm>();


        [Route("allalarms")]
        public IEnumerable<Alarm> GetAllAlarms()
        {
            //return alarmsList;
            alarmsList = Am.LoadAlarms(@"d:\json.txt");
            return alarmsList;
        }

        [Route("alarms")]
        public IEnumerable<Alarm> GetAllAlarmsForUser(string user)
        {
            //return alarmsList;
            alarmsList = Am.LoadAlarms(@"d:\json.txt");
            var alarm = alarmsList.Where(p => p.Owner == user);
            if (alarm.Count() == 0)
            {
                //some error here should be applied.
            }
            return alarmsList;
        }


        [Route("alarms/get/{Id}")]
        public IHttpActionResult GetAlarm(Guid Id)
        {
            alarmsList = Am.LoadAlarms(@"d:\json.txt");
            var alarm = alarmsList.FirstOrDefault((p) => p.id == Id);
            if (alarmsList.Count == 0)
            {
                return Ok("NO ALARMS PRESENT ON SERVER, something went wrong");
            }
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok(alarm);
        }

        [Route("alarms/delete/{Id}")]
        public IHttpActionResult DelteAlarm(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Am.DeleteAlarm(Id);
            return Ok("Alarm succesfully delted!");
        }

        [System.Web.Http.HttpGet]
        [Route("alarms/add")]
        public IHttpActionResult AddStaticAlarm()
        {
            Alarm tmpAllarm = new Alarm();
            tmpAllarm.AlarmCreated = DateTime.Now;
            tmpAllarm.id = Guid.NewGuid();
            tmpAllarm.AlarmTuneLocation = "media/file/location/here/filename.txt";
            tmpAllarm.isRepeatable = true;
            tmpAllarm.SnoozeCount = 2;
            tmpAllarm.AlarmText = "Sample Alarm";
            tmpAllarm.Owner = "arturszigurs";
            if (tmpAllarm.Owner == "zigurs93")
            {
                return BadRequest("Alarm could not be added properly");
            }
            Am.LoadAlarms(@"d:\json.txt");
            Am.Alarms.Add(tmpAllarm);
            Am.SaveAlarms();
            return Ok("Alarm succesfully added");
        }

        [System.Web.Http.HttpGet]
        [Route("alarms/addstatic")]
        public IHttpActionResult AlarmAdd()
        {
            Alarm tmpAllarm = new Alarm();
            tmpAllarm.AlarmCreated = DateTime.Now;
            tmpAllarm.id = Guid.NewGuid();
            tmpAllarm.AlarmTuneLocation = "media/file/location/here/filename.txt";
            tmpAllarm.isRepeatable = true;
            tmpAllarm.SnoozeCount = 2;
            tmpAllarm.AlarmText = "Sample Alarm";
            tmpAllarm.Owner = "arturszigurs";
            if (tmpAllarm.Owner == "zigurs93")
            {
                return BadRequest("Alarm could not be added properly");
            }
            Am.LoadAlarms(@"d:\json.txt");
            Am.Alarms.Add(tmpAllarm);
            Am.SaveAlarms();
            return Ok("Static alarm succesfully added");
        }

        [System.Web.Http.HttpGet]
        [Route("alarms/add")]
        public IHttpActionResult Testquerry(int days, string tunelocation, bool ir, int snoozecount, string alarmtext, string owner, int hh, int mm)
        {
            string daysArray = days.ToString();
            if (daysArray.Length != 7) { return BadRequest("Wrong parameter: count of days in array"); }
            Alarm tmpAllarm = new Alarm();
            tmpAllarm.AlarmCreated = DateTime.Now;
            tmpAllarm.id = Guid.NewGuid();
            tmpAllarm.AlarmTuneLocation = tunelocation;
            tmpAllarm.isRepeatable = ir;
            tmpAllarm.SnoozeCount = snoozecount;
            tmpAllarm.AlarmText = alarmtext;
            tmpAllarm.Owner = owner;
            tmpAllarm.weekdays = new List<bool>();
            foreach (char t in daysArray)
            {
                if (t.ToString() == "a")
                {
                    tmpAllarm.weekdays.Add(true);
                }
                else
                {
                    tmpAllarm.weekdays.Add(false);
                }
            }
            Am.LoadAlarms(@"d:\json.txt");
            Am.Alarms.Add(tmpAllarm);
            Am.SaveAlarms();
            return Ok("Alarm succesfully added!");
        }
    }
}
