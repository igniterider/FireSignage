using System;
using System.Collections.Generic;
using Realms;
using MongoDB.Bson;

namespace FireSignage.Models
{
    //User class model

    public partial class User : IRealmObject
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

        [MapTo("_business")]
        public string Business { get; set; }

        [MapTo("owner_id")]
        [Required]
        public string OwnerId { get; set; }

        //[Backlink(nameof(UserDevices.DeviceOwner))]
        //public IQueryable<UserDevices> DevicesOwned { get; }

    }



}
