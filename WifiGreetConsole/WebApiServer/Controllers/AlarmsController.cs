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
        Alarm[] alarmsList = new Alarm[]
        {
            new Alarm { isRepeatable = true, AlarmText = "Alarm one" },
            new Alarm { isRepeatable = true, AlarmText = "Alarm two"}
        };

        [Route("alarms/alarmsforuser")]
        public IEnumerable<Alarm> GetAllAlarms()
        {
            return alarmsList;
        }

        //[Route("customers/{customerId}/orders")]
        [Route("alarms/getalarm/{Id}")]
        public IHttpActionResult GetAlarms(int Id)
        {
            var alarm = alarmsList.FirstOrDefault((p) => p.id == Id);
            if (alarm == null)
            {
                return NotFound();
            }
            return Ok(alarm);
        }
    }
}
