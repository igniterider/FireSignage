using System;
using MongoDB.Bson;
using Realms;
namespace FireSignage.Models
{
    public class UserDeviceInfo : EmbeddedObject
    {
        [MapTo("_deviceManuf")]
        public string DeviceManuf { get; set; }
        [MapTo("_deviceOS")]
        public string DeviceOS { get; set; }
        [MapTo("_devicename")]
        public string Devicename { get; set; }
        [MapTo("_devicescreensize")]
        public string Devicescreensize { get; set; }
        [MapTo("_devicetype")]
        public string Devicetype { get; set; }

        [MapTo("_deviceissign")]
        public bool DeviceIsSign { get; set; }

        [MapTo("_deviceiscontrol")]
        public bool DeviceIsControl { get; set; }

        [Indexed]
        [MapTo("owner_id")]
        public string OwnerId { get; set; }
    }

}

