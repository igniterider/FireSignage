using System;
using Realms;
using System.Collections.Generic;
using MongoDB.Bson;


namespace FireSignage.Models
{
    // saved Personal signs for users 

    public partial class PersonalSigns : IRealmObject
    {
        [MapTo("_backgroundcolor")]
        public string Backgroundcolor { get; set; }

        [MapTo("_backgroundcolor2")]
        public string Backgroundcolor2 { get; set; }

        [MapTo("_displaytext")]
        public string Displaytext { get; set; }

        [MapTo("_displaytext2")]
        public string Displaytext2 { get; set; }

        [MapTo("_signname1")]
        public string Signname1 { get; set; }

        [MapTo("_textcolor")]
        public string Textcolor { get; set; }

        [MapTo("_textcolor2")]
        public string Textcolor2 { get; set; }

        [MapTo("owner_id")]
        public string OwnerId { get; set; }

        [MapTo("_isfavorite")]
        public bool IsFavorite {get; set;}
    }

}

