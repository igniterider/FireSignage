using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

namespace FireSignage.Models
{
    public partial class UserDevices : IRealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();


        [MapTo("_deviceManuf")]
        public string DeviceManuf { get; set; }

        [MapTo("_deviceOS")]
        public string DeviceOS { get; set; }
        
        [MapTo("_deviceisonline")]
        public bool? Deviceisonline { get; set; }

        [MapTo("_devicename")]
        public string Devicename { get; set; }

        [MapTo("_devicescreensize")]
        public string Devicescreensize { get; set; }

        [MapTo("_devicetype")]
        public string Devicetype { get; set; }

        [MapTo("owner_id")]
        [Required]
        public string OwnerId { get; set; }

        public User DeviceOwner { get; set; }


    }
}
