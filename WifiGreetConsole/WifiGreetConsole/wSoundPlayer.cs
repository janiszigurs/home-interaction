using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiGreetConsole
{
    public class wSoundPlayer
    {
        private WMPLib.WindowsMediaPlayer player;
        WMPLib.WMPPlayState state;

        public string ReturnSoundLocation(string songname) //returns location of sound location and adds its name to location for full path 
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            path = path.Remove(path.Length - 9);
            path = path + @"Media\" + songname;

            return path;
        }

        public wSoundPlayer()
        {
            player = new WMPLib.WindowsMediaPlayer();
            state = player.playState;
        }

        public void LoadSound(string soundName) //loads sound
        {           
            player.URL = ReturnSoundLocation(soundName);
            Console.WriteLine("Sound loaded!");
        }

        public void PlaySound() //plays song once and stops when song fully played (if not stopped)
        {
            player.controls.play();
            Console.WriteLine("Plays sound!");
        }

        public void PlaySoundLooping() //plays song looping until it's stopped (if not stopped)
        {
            // TODO : do
        }

        public void PlaySoundSpecificTime(int milliseconds) //plays sound for X milliseconds (if not stopped)
        {
            // TODO : do
        }

        public void StopSound(WMPLib.WindowsMediaPlayer player)
        {
            player.controls.stop();
        }
    }
}

