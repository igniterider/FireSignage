using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

namespace FireSignage.Models
{
    //User class model

    public class User : RealmObject
    {
        [MapTo("_id")]
        [PrimaryKey]
        [Required]
        public string Id { get; set; }

        [MapTo("_email")]
        public string Email { get; set; }

        [MapTo("_firstname")]
        public string Firstname { get; set; }

        [MapTo("_lastname")]
        public string Lastname { get; set; }

        [MapTo("_licenseplate")]
        public string Licenseplate { get; set; }

        [MapTo("_password")]
        public string Password { get; set; }

        [MapTo("personalsigns")]
        public IList<PersonalSigns> Personalsigns { get; }

        [MapTo("userdeviceinfo")]
        public IList<UserDeviceInfo> Userdeviceinfo { get; }

        
        [MapTo("owner_id")]
        [Required]
        public string OwnerId { get; set; }

        

    }



}
