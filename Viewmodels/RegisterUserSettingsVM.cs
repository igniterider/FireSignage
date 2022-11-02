using CommunityToolkit.Mvvm.Input;
using Realms;
using Realms.Sync;
using SampleBrowser.Maui.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = FireSignage.Models.User;

namespace FireSignage.Viewmodels
{
   
    public partial class RegisterUserSettingsVM : BaseViewModel
    {
        private Realm userRealm;

        private string password;
        private string fname;
        private string lname;
        private string licplate;
        private string email;
        private string idiom;
        private string OS;
        private string osVersion;
        private string manu;
        private string model;
        private string name;
        private string screen;
        private string dname;

        private bool is_controller;
        private bool is_sign;

        public RegisterUserSettingsVM()
        {
            Title = "Register User Settings";
            GetDeviceInfo();

        }

        public string FName
        {
            set { SetProperty(ref fname, value); }
            get { return fname; }

        }

        public string LName
        {
            set { SetProperty(ref lname, value); }
            get { return lname; }

        }

        public string LicPlate
        {
            set { SetProperty(ref licplate, value); }
            get { return licplate; }

        }

        public string Email
        {
            set { SetProperty(ref email, value); }
            get { return email; }
        }

        [RelayCommand]
        async Task AddUserInfo()
        {
            if (userRealm == null)
            {
                var user = App.realmApp.CurrentUser;
                var partid = App.realmApp.CurrentUser.Id;
                var config = new PartitionSyncConfiguration(partid, App.realmApp.CurrentUser);
                userRealm = await Realm.GetInstanceAsync(config);
                await userRealm.SyncSession.WaitForDownloadAsync();


                var userinfo = userRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);

                var setdevice = new UserDeviceInfo()
                {
                    DeviceManuf = manu,
                    Devicename = name,
                    DeviceOS = OS,
                    Devicetype = idiom,
                    Devicescreensize = screen,
                    OwnerId = App.realmApp.CurrentUser.Id



                };



                userRealm.Write(() =>
                {

                    userinfo.Firstname = fname;
                    userinfo.Lastname = lname;
                    userinfo.Licenseplate = licplate;
                    userinfo.Userdeviceinfo.Add(setdevice);


                });

            }

            else
            {
              

            }

            return;
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


    }




}
