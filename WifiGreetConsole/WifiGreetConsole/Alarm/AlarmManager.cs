using System;
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
        public bool _stopThread = false; //stop alarm thread
        public List<Alarm> Alarms = new List<Alarm>();

        public AlarmManager(string AlarmConfigFileName)
        {
            Alarms = new List<Alarm>();
            LoadAlarms(AlarmConfigFileName);      
        }

        public bool LoadAlarms(string AlarmConfigFileName)
        {
            string json;
            using (StreamReader reader = new StreamReader(AlarmConfigFileName))
            {
                json = reader.ReadToEnd();
                reader.Close();
            }
            this.Alarms = JsonConvert.DeserializeObject<List<Alarm>>(json);
            return true;
        }
        public bool AddAlarm()
        {
            Alarm currAlarm = new Alarm();
            currAlarm.ID = 1;
            currAlarm.SnoozeCount = 2;
            string time;
            Console.WriteLine("Please provide Time to set alarm");
            time = Console.ReadLine();
            currAlarm.AlarmTime = DateTime.ParseExact(time, @"h\:m", CultureInfo.InvariantCulture);
            this.Alarms.Add(currAlarm);
            //save alarms to file
            return true;
        }

        public bool AddAlarm(int hours, int minutes)
        {
            Console.WriteLine(this.Alarms.Count());
            Alarm currAlarm = new Alarm();
            currAlarm.ID = 1;
            currAlarm.SnoozeCount = 2;
            currAlarm.AlarmTime =  new DateTime().AddHours(hours).AddMinutes(minutes);
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

        public void DeleteAlarm(int id)
        {
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
            while (!_stopThread)
            {
                Thread.Sleep(31000);
                foreach (Alarm CurrAlarm in Alarms)
                {
                    if (CurrAlarm.AlarmTime.Minute == DateTime.Now.Minute && CurrAlarm.AlarmTime.Hour == DateTime.Now.Hour)
                    {
                        if (CurrAlarm.weekdays[(int)DateTime.Now.DayOfWeek] == true)
                        {
                            Console.Write("Alarm called/n");
                            this.AwakePerson();
                            Thread.Sleep(1000);
                            //_stopThread = true;
                        }
                    }
                }
            }
        }
    }
}
