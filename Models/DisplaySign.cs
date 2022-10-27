using System;
using Realms;
using MongoDB;

namespace FireSignage.Models
{

    // Class for changing sign information 

	public class DisplaySign : RealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        [Required]
        public string Id { get; set; }


        [MapTo("_backgroundcolor")]
        public string Backgroundcolor { get; set; }


        [MapTo("_backgroundcolor2")]
        public string Backgroundcolor2 { get; set; }

        [MapTo("_displaytext")]
        public string Displaytext { get; set; }

        [MapTo("_pagename")]
        public string Pagename { get; set; }

        [MapTo("_ridername")]
        public string Ridername { get; set; }

        [MapTo("_textcolor")]
        public string Textcolor { get; set; }

        [MapTo("_textcolor2")]
        public string Textcolor2 { get; set; }

        [MapTo("owner_id")]
        [Required]
        public string OwnerId { get; set; }
    }


}


