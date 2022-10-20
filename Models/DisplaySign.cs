using System;
using Realms;
using MongoDB;

namespace FireSignage.Models
{

    // Class for changing sign information 

	public class DisplaySign : RealmObject
    {

        [MapTo("owner_id")]
        public string OwnerId { get; set; }


        [MapTo("_devicename")]
        public string DeviceName { get; set; }

        [MapTo("_backgroundcolors")]
        public string Backgroundcolors { get; set; }

        [MapTo("_backgroundcolors2")]
        public string Backgroundcolors2 { get; set; }

        [MapTo("_displaytext")]
        public string Displaytext { get; set; }

        [MapTo("_textcolor")]
        public string Textcolor { get; set; }

        [MapTo("_textcolor2")]
        public string Textcolor2 { get; set; }


        [MapTo("_displaytext2")]
        public string Displaytext2 { get; set; }

     

    }
}

