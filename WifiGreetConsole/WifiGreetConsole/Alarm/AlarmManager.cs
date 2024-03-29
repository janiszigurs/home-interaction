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
using System.Net.Http;
using System.Net.Http.Headers;

namespace WifiGreetConsole.Alarm
{
    class AlarmManager
    {
        public bool _stopThread = false;
        public List<Alarm> Alarms = new List<Alarm>();
        public bool retrieved = false;


        public List<Alarm> LoadAlarms()
        {
            /*if (Alarms.Count > 0)
            {
                Alarms.RemoveRange(0, Alarms.Count);
            }    */       
            RetrieveAlarms().Wait();
            return Alarms;
        }

        public async Task RetrieveAlarms()
        {
            if (retrieved == false)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://84.237.250.21:55000/");
                    string a = "alarms?user=zigurs93";
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // HTTP GET
                    HttpResponseMessage response = await client.GetAsync(a);
                    var c = response.Content;
                    if (response.IsSuccessStatusCode)
                    {
                        Alarms = await response.Content.ReadAsAsync<List<Alarm>>();
                        ///////////////////////////////// temporary
                        foreach (Alarm alarm in Alarms)
                        {
                            alarm.isRepeatable = false;
                        }
                        retrieved = true;
                        ///////////////////////////////// temporary
                    }
                }
            }
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
                            if(CurrAlarm.isRepeatable == false) ///////////////////////////////// temporary
                            { ///////////////////////////////// temporary
                                Console.WriteLine("Alarm called!");
                                this.AwakePerson();
                                CurrAlarm.isRepeatable = true;
                                Thread.Sleep(1000);
                                //this._stopThread = true;
                                
                            } ///////////////////////////////// temporary
                        }
                    }
                }
            }
        }
    }
}
