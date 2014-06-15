using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Settings
{
    [Serializable]
    public class ApplicationSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Mode { get; set; }
        public bool Log { get; set; }
        public bool Clear { get; set; }
        public bool WithAlbum { set; get; }
        public bool SaveArtists { set; get; }
        public List<String> AutoFill { set; get; }
        public bool AutoSave { set; get; }
        public bool AutoClear { set; get; }

        /*public int Mode
        {
            set
            {
                value
            }
        }*/

        public string SessionKey { get; set; }



        public ApplicationSettings(string u, string p, string s, int mode, bool log, bool clear, bool saveA, bool autoS, bool autoC)
        {
            Username = u;
            Password = p;
            SaveArtists = saveA;
            SessionKey = s;
            Mode = mode;
            Log = log;
            AutoSave = autoS;
            AutoClear = autoC;
            Clear = clear;
            AutoFill = new List<string>();

        }

        

    }
}
