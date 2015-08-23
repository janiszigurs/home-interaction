using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using WebApiServer.Models;

namespace WebApiServer.Components
{
    public class ReminderManager
    {
        public List<Reminder> Reminders = new List<Reminder>();

        public List<Reminder> LoadReminders(string Reminderfile)
        {
            using (StreamWriter w = File.AppendText(Reminderfile))
            {
                //empty body. this creates file if its not yet created.
            };

            string json;
            using (StreamReader reader = new StreamReader(Reminderfile))
            {
                json = reader.ReadToEnd();
                reader.Close();
            }
            if (json.Length == 0)
            {
                this.Reminders = new List<Reminder>();
            }
            else
            {
                this.Reminders = JsonConvert.DeserializeObject<List<Reminder>>(json);
            }

            return this.Reminders;
        }

        public bool AddReminder()
        {
            //false methond for now
            return true;
        }

        public bool AddReminder(string owner)
        {
            //false methond for now
            return true;
        }

        public void DeleteReminder(Guid reminderid)
        {
            var delreminder = this.Reminders.Single(r => r.id == reminderid);
            this.Reminders.Remove(delreminder);
            SaveReminders();
        }

        public void InitializeEnvironment()
        {
            //crate config file if it doesnt exist. This prevents json empty file error.
        }

        public void SaveReminders()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"d:\reminders.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    string output = JsonConvert.SerializeObject(Reminders);
                    serializer.Serialize(writer, Reminders);
                }
            }
        }

        public void ListReminder()
        {
            //lists all the Reminders...
        }
    }
}