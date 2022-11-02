using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Realms;
using CommunityToolkit.Maui;
using MongoDB;
using MongoDB.Driver;
using Realms.Sync;
using User = FireSignage.Models.User;

namespace FireSignage.Services
{
    public class CheckDeviceService
    {
        private Realm deviceRealm;
        private string idiom;
        private string OS;
        private string osVersion;
        private string manu;
        private string model;
        private string name;
        private string screen;
        private string dname;

        public List<String> userDeviceList = new List<String>();

        public CheckDeviceService()
        {


        }

        private void GetDeviceInfo()
        {
            idiom = DeviceInfo.Idiom.ToString();
            OS = DeviceInfo.Platform.ToString();
            osVersion = DeviceInfo.Version.ToString();
            manu = DeviceInfo.Manufacturer;
            model = DeviceInfo.Model;
            name = DeviceInfo.Name;
            screen = DeviceDisplay.MainDisplayInfo.ToString();



        }



        private async Task WriteDeviceInfo()
        {
            var device = new UserDeviceInfo()
            {
                DeviceManuf = manu,
                Devicename = name,
                DeviceOS = OS,
                Devicetype = idiom,
                Devicescreensize = screen,
                OwnerId = App.realmApp.CurrentUser.Id



            };

            var user = App.realmApp.CurrentUser;
            var partid = App.realmApp.CurrentUser.Id;
            var config = new PartitionSyncConfiguration(partid, App.realmApp.CurrentUser);
            deviceRealm = await Realm.GetInstanceAsync(config);
            await deviceRealm.SyncSession.WaitForDownloadAsync();


            var userinfo = deviceRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);

            deviceRealm.Write(() =>
            {
                userinfo.Userdeviceinfo.Add(device);

            });


        }

        //private async Task CheckDeviceInfo()
        //{
        //    var userinfo = deviceRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
        //    var getdevicename = userinfo.Userdeviceinfo.Where<UserDeviceInfo>(t => t.Devicename == DeviceInfo.Name);

        //    if (getdevicename == null)
        //    {
        //        GetDeviceInfo();


        //    }

        //    else if (getdevicename != null)
        //    {
        //        foreach (var device in getdevicename)
        //        {
        //            userDeviceList.Add(device);
        //        }


        //    }


        //}


    }
}
