﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Speech.Synthesis;
using Newtonsoft.Json;
using System.IO;

namespace WifiGreetConsole.Alarm
{
    class AlarmManager
    {
        Guid a = Guid.NewGuid();
        public bool _stopThread = false; //stop alarm thread
        public List<Alarm> Alarms = new List<Alarm>();

        public bool LoadAlarms(string AlarmConfigFileName)
        {
            using (StreamWriter w = File.AppendText(AlarmConfigFileName))
            {
                //empty body. this creates file if its not yet created.
            };

                string json;
            using (StreamReader reader = new StreamReader(AlarmConfigFileName))
            {
                json = reader.ReadToEnd();
                reader.Close();
            }
            if (json.Length == 0)
            {
                this.Alarms = new List<Alarm>();
            }
            else
            {
                this.Alarms = JsonConvert.DeserializeObject<List<Alarm>>(json);
            }

            return true;
        }

        public bool AddAlarm(int hours, int minutes)
        {
            Console.WriteLine(this.Alarms.Count());
            Alarm currAlarm = new Alarm();
            currAlarm.id = Guid.NewGuid();
            currAlarm.SnoozeCount = 2;
            currAlarm.AlarmTime =  new DateTime().AddHours(hours).AddMinutes(minutes);
            Console.Write(currAlarm.AlarmTime.ToString());
            this.Alarms.Add(currAlarm);
            SaveAlarms();
            return true;
        }

        public bool AddAlarm(int hours, int minutes, int snoozecount)
        {
            Console.WriteLine(this.Alarms.Count());
            Alarm currAlarm = new Alarm();
            currAlarm.id = Guid.NewGuid(); ;
            currAlarm.SnoozeCount = snoozecount;
            currAlarm.AlarmTime = new DateTime().AddHours(hours).AddMinutes(minutes);
            Console.Write(currAlarm.AlarmTime.ToString());
            this.Alarms.Add(currAlarm);
            SaveAlarms();
            return true;
        }

        public void AwakePerson()
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10
            //synthesizer.SetOutputToDefaultAudioDevice();
            string sup = "Arthurs, its time to wake up! It's already " + DateTime.Now.ToString("h:mm tt");
            synthesizer.Speak(sup);
            //playTune
        }

        public void DeleteAlarm(Guid alarmid)
        {
            var AlarmToDelete = this.Alarms.Single(r => r.id == alarmid);
            this.Alarms.Remove(AlarmToDelete);
        }

        public void InitializeEnvironment()
        {
            //crate config file if it doesnt exist. This prevents json empty file error.
        }

        public void SaveAlarms()
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(@"d:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                string output = JsonConvert.SerializeObject(Alarms);
                serializer.Serialize(writer, Alarms);
            }
        }

        public void ListAlarms()
        {
            //lists all alarms
        }

        public void StartAlarmClock()
        {
            while (!this._stopThread)
            {
                Thread.Sleep(31000);
                foreach (Alarm CurrAlarm in Alarms)
                {
                    if (CurrAlarm.AlarmTime.Minute == DateTime.Now.Minute && CurrAlarm.AlarmTime.Hour == DateTime.Now.Hour)
                    {
                        if (CurrAlarm.weekdays[(int)DateTime.Now.DayOfWeek] == true)
                        {
                            Console.WriteLine("Alarm called!");
                            this.AwakePerson();
                            Thread.Sleep(1000);
                            //this._stopThread = true;
                        }
                    }
                }
            }
        }
    }
}
