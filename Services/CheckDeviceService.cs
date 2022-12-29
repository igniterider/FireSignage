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
using FireSignage.Views.Settings;

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


        // can get rid of
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



        public async Task <List<string>> CheckDeviceInfo()
        {
            

            
            var user = App.realmApp.CurrentUser;
            var partid = App.realmApp.CurrentUser.Id;
            var config = new FlexibleSyncConfiguration(App.realmApp.CurrentUser);
            deviceRealm = await Realm.GetInstanceAsync(config);
            await deviceRealm.SyncSession.WaitForDownloadAsync();


            var deviceinfo = deviceRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
            foreach (var dev in deviceinfo.Userdeviceinfo)
            {
                dname = dev.Devicename;
                userDeviceList.Add(dname);
                Console.WriteLine(dname);
                
            }

            
            return userDeviceList;

        }

        


    }
}
