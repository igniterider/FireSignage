using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

namespace FireSignage.Models
{
    public class OnlineDevices : RealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        [Required]
        public string Id { get; set; }
        [MapTo("_deviceiscontrol")]
        public bool? Deviceiscontrol { get; set; }
        [MapTo("_deviceisonline")]
        public bool Deviceisonline { get; set; }
        [MapTo("_devicename")]
        public string Devicename { get; set; }
        [MapTo("_devicetype")]
        public string Devicetype { get; set; }
        [MapTo("owner_id")]
        [Required]
        public string OwnerId { get; set; }
    }


}

