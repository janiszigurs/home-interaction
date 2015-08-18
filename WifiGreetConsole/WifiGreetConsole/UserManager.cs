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
        int id;



        public void ReadUsers(string filepath)
        {
            int id = 0;
            string line = null;
            StreamReader user_reader = new StreamReader(filepath);

            while ((line = user_reader.ReadLine()) != null)
            {
                string[] strings = SplitString(line);
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
            user_reader.Close();
        }




        public string[] SplitString(string string_to_split) //shouldn't be in this class
        {
            char [] charSeparators = {',',' '};
            string [] strings = string_to_split.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

            return strings;
        }




        public string CheckIfUserIsConnectedAndExists(PhysicalAddress passed_mac_address)
        {
            foreach (User user in users)
            {
                if (user.mac_address == passed_mac_address)
                {                 
                    if (user.status == "Disconnected")
                    {
                        user.status = "Connected";
                        //TODO: Greetings!
                        return "Exists";
                    }
                    return "Exists";
                }
            }

            return "Does not exist";
        }




        public void AddUserIfNecessary(string DoesExist, PhysicalAddress passed_mac_address,string filepath)
        {
            if (DoesExist == "Does not exist")
            {
                File.AppendAllText(filepath, passed_mac_address.ToString() + ' ' + "Unknown" + ' ' + "Unknown" + Environment.NewLine);
                User user = new User();
                id++;
                user.id = id;
                user.mac_address = passed_mac_address;
                user.name = "Unknown";
                user.status = "Connected";
                user.gender = "Unknown";

                users.Add(user);

                //TODO: Greetings, stranger!
            }
        }


        public void UserStatusDisconnect(List<PhysicalAddress> passed_mac_list)
        {
            foreach (User user in users)
            {
                bool Disconnect = true;
                if (user.status == "Connected")
                {
                    foreach (PhysicalAddress mac in passed_mac_list)
                    {
                        if (user.mac_address == mac)
                        {
                            Disconnect = false;
                        }
                    }
                }
                if (Disconnect == true) { user.status = "Disconnected"; }
            }
        }


        public UserManager(string filepath)
        {
            //TODO: add ReadUsers call (once)
            ReadUsers(filepath);
        }
    }
}
