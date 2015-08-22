using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiServer.Models;

namespace WebApiServer.Controllers
{
    public class AlarmsController : ApiController
    {
        /*Alarm[] alarmsList = new Alarm[]
        {
            new Alarm { isRepeatable = true, AlarmText = "Alarm one" },
            new Alarm { isRepeatable = true, AlarmText = "Alarm two"}
        };*/

        List<Alarm> alarmsList = new List<Alarm>();
        

        [Route("alarms/alarmsforuser")]
        public IEnumerable<Alarm> GetAllAlarms()
        {
            //return alarmsList;
            return alarmsList;
        }

        [Route("alarms/getalarm/{Id}")]
        public IHttpActionResult GetAlarm(int Id)
        {
            var alarm = alarmsList.FirstOrDefault((p) => p.id == Id);
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok(alarm);
        }

        [Route("alarms/addalarm")]
        public IHttpActionResult AlarmAdd(int Id)
        {
            //for now creates alarm just to see that interface works
            Alarm tmpAllarm = new Alarm();
            tmpAllarm.AlarmCreated = DateTime.Now;
            tmpAllarm.id = 67;
            tmpAllarm.AlarmTuneLocation = "media/file/location/here/filename.txt";
            tmpAllarm.isRepeatable = true;
            tmpAllarm.SnoozeCount = 2;
            tmpAllarm.AlarmText = "Sample Alarm";
            tmpAllarm.Owner = "arturszigurs";
            if (tmpAllarm.Owner == "zigurs93")
                {
                return BadRequest("Alarm could not be added properly");
                }
            alarmsList.Add(tmpAllarm);
            return Ok("Alarm succesfully added");        
        }
    }
}
