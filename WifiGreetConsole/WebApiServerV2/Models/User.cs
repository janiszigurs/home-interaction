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
        public string Pwd { get; set; }
        public DateTime created { get; set; }
        public DateTime lastlogin { get; set; }
        public bool AlarmsUpdated { get; set; }
        public bool RemindersUpdated { get; set; }
        public bool CalendarUpdated { get; set; }
        //Settings for user to edit CSS classes on frontend
        public bool IsOnline { get; set; }
        public string BackgroundColor { get; set; }
        public string CardColor { get; set; }
        public string HeaderFooterColor { get; set; }
    }
}