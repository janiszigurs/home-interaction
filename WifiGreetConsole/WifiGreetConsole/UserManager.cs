using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Speech.Synthesis;

namespace WifiGreetConsole
{
    public class UserManager
    {
        List<User> users = new List<User>();
        int id = 0;
        public UserManager()
        {

            string line;
            string[] strings;
            char[] charSeparators = new char[] { ' ' };
            StreamReader sr = new StreamReader("data.txt");
            while ((line = sr.ReadLine()) != null)
            {
                strings = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                if (strings.Length > 0)
                {
                    User user = new User();
                    user.id = id;
                    user.mac_address = PhysicalAddress.Parse(strings[0]);
                    user.name = strings[1];
                    user.status = "Disconnected";
                    user.gender = strings[2];

                    users.Add(user);
                    id++;
                }
            }
            sr.Close();
        }

        public void ConnectUsers(PhysicalAddress address)
        {
            bool add = true;
            foreach (User user in users)
            {
                if (user.mac_address.ToString() == address.ToString())
                {
                    add = false;
                    if (user.status == "Disconnected")
                    {
                        user.status = "Connected";
                        //TODO : sasveicinaaties
                        //SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                        //synthesizer.Volume = 100;  // 0...100
                        //synthesizer.Rate = -2;     // -10...10
                        //                           //synthesizer.SetOutputToDefaultAudioDevice();
                        //string sup = "Greetings!";
                        //synthesizer.Speak(sup);
                    }               
                }
            }
            if (add == true)
            {
                File.AppendAllText("data.txt", address.ToString() + ' ' + "Unknown" + ' ' + "Unknown" + Environment.NewLine);
                User user = new User();
                id++;
                user.id = id;
                user.mac_address = address;
                user.name = "Unknown";
                user.status = "Connected";
                user.gender = "Unknown";
                //TODO : sasveicinaaties ar unknown
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;     // -10...10
                                           //synthesizer.SetOutputToDefaultAudioDevice();
                string sup = "Greetings!";
                synthesizer.Speak(sup);
            }
        }
        public void DisconnectUsers(List<PhysicalAddress> mac)
        {
            foreach (User user in users)
            {
                if (user.status == "Connected")
                {
                    bool disconnect = true;
                    foreach (PhysicalAddress address in mac)
                    {
                        if (address == user.mac_address)
                        {
                            disconnect = false;
                        }
                    }
                    if (disconnect == true) { user.status = "Disconnected"; }
                }
            }
        }



    }
}
