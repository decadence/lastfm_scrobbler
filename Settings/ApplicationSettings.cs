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
        public bool Log { get; set; }
        public bool Clear { get; set; }
        public bool WithAlbum { set; get; }
        public bool SaveArtists { set; get; }
        public List<String> AutoFill { set; get; }
        public bool AutoSave { set; get; }
        public bool AutoClear { set; get; }
        public bool ExitConfirm { set; get; }

        public string SessionKey { get; set; }



        public ApplicationSettings(string u, string s, bool log, bool clear, bool saveA, bool autoS, bool autoC, bool exitConfirm)
        {
            Username = u;
            SaveArtists = saveA;
            SessionKey = s;
            Log = log;
            AutoSave = autoS;
            AutoClear = autoC;
            Clear = clear;
            ExitConfirm = exitConfirm;
            AutoFill = new List<string>();

        }

        

    }
}
