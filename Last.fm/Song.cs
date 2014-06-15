using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Last.fm
{
    public class Song
    {
        public string Artist { set; get; }
        public string Track { set; get; }
        public string Album { set; get; }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(Album))
                return Artist + " - " + Track;
            else return Artist + " - " + Track + " - " + Album;
        }

    }
}
