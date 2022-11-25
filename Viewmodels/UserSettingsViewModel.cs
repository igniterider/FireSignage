using System;
using CommunityToolkit.Mvvm.Input;
using Realms;
using CommunityToolkit.Maui;
using MongoDB;
using MongoDB.Driver;
using Realms.Sync;
using User = FireSignage.Models.User;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace FireSignage.Viewmodels
{
	public partial class UserSettingsViewModel : BaseViewModel
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
        private bool iscontrol;
        private bool issign;
        private string dname;

        public UserSettingsViewModel()
        {
            Title = "User Settings";
            GetDeviceInfo();
        }

        //Users
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

        //Devices
        public string DeviceName
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }

        public string Manuf
        {
            set { SetProperty(ref manu, value); }
            get { return manu; }
        }

        public string OperatingSystem
        {
            set { SetProperty(ref OS, value); }
            get { return OS; }
        }

        public string DeviceType
        {
            set { SetProperty(ref idiom, value); }
            get { return idiom; }
        }

        public bool ISSign
        {
            set { SetProperty(ref issign, value); }
            get { return issign; }
        }

        public bool ISControl
        { 
            set { SetProperty(ref iscontrol, value); }
            get { return iscontrol; } 
        
        }
        
        [RelayCommand]
        async Task AddUserInfo()
        {
            if(userRealm == null)
            {
                var user = App.realmApp.CurrentUser;
                var partid = App.realmApp.CurrentUser.Id;
                var config = new FlexibleSyncConfiguration(user);
                userRealm = await Realm.GetInstanceAsync(config);
                await userRealm.SyncSession.WaitForDownloadAsync();


                var userinfo = userRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);

                

                

                userRealm.Write(() =>
                {

                    userinfo.Firstname = fname;
                    userinfo.Lastname = lname;
                    userinfo.Licenseplate = licplate;

                    

                    });

                }

            else 
            {
                var userinfo = userRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
                userRealm.Write(() =>
                {

                    userinfo.Firstname = fname;
                    userinfo.Lastname = lname;
                    userinfo.Licenseplate = licplate;

                    

                });

            }

            return;
        }


        [RelayCommand]
        async Task GetUserInfo()
        {
            
                var user = App.realmApp.CurrentUser;
                var partid = App.realmApp.CurrentUser.Id;
                var config = new FlexibleSyncConfiguration(user);
                userRealm = await Realm.GetInstanceAsync(config);
                await userRealm.SyncSession.WaitForDownloadAsync();


            var userinfo = userRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);

            FName = userinfo.Firstname;
            LName = userinfo.Lastname;
            LicPlate = userinfo.Licenseplate;
            Email = userinfo.Email;

           

        }

        [RelayCommand]
        async Task AddDeviceInfo()
        {
            if (userRealm == null)
            {
                var user = App.realmApp.CurrentUser;
                var partid = App.realmApp.CurrentUser.Id;
                var config = new FlexibleSyncConfiguration(user);
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
                    OwnerId = App.realmApp.CurrentUser.Id,
                    Deviceiscontrol = iscontrol,
                    Deviceissign = issign


                };



                userRealm.Write(() =>
                {

                    
                    userinfo.Userdeviceinfo.Add(setdevice);


                });

            }

            else
            {
                HandleFailure();

            }

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
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

        public event EventHandler<EventArgs> OperationCompeleted;

        private void HandleFailure()
        {
            App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
            //throw new NotImplementedException();
        }
    }
}


