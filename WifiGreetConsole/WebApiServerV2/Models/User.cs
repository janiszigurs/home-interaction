using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiServerV2.Models
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public string Uname { get; set; }
        public string Rname { get; set; }
        public string Sname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }
        public DateTime Lastlogin { get; set; }
        public DateTime LastHeartBeat { get; set; }
        public bool AlarmsUpdated { get; set; }
        public bool RemindersUpdated { get; set; }
        public bool CalendarUpdated { get; set; }
        public bool IsOnline { get; set; }
        //Settings for use
        public string BackgroundColor { get; set; }
        public string CardColor { get; set; }
        public string HeaderFooterColor { get; set; }
    }
}