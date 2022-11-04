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
            GetDeviceInfo();

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



        public async Task CheckDeviceInfo()
        {
            

            
            var user = App.realmApp.CurrentUser;
            var partid = App.realmApp.CurrentUser.Id;
            var config = new PartitionSyncConfiguration(partid, App.realmApp.CurrentUser);
            deviceRealm = await Realm.GetInstanceAsync(config);
            await deviceRealm.SyncSession.WaitForDownloadAsync();


            var deviceinfo = deviceRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
            foreach (var dev in deviceinfo.Userdeviceinfo)
            {
                dname = dev.Devicename;
                userDeviceList.Add(dname);

                    //if (dname == name)
                    //{
                    //    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    //    return;
                         
                    //}
                    //else
                    //{
                    //  var answer =  await App.Current.MainPage.DisplayAlert("Device not Registered", "Register Device Now", "Yes", "No");
                    //    if(answer == true)
                    //    {
                    //        await Shell.Current.GoToAsync($"//{nameof(DeviceSettingsPage)}");
                    //    }
                    //    else
                    //    {
                    //        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    //    }
                    //}

                Console.WriteLine(dname);
            }

            


        }

        


    }
}
