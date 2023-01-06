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
using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.Specialized;

namespace FireSignage.Viewmodels
{
	public partial class UserSettingsViewModel : BaseViewModel
	{
        
        public User GetUser { get; set; }
        public UserDevices GetUserDevices { get; set; }
        private string idiom;
        private string OS;
        private string osVersion;
        private string manu;
        private string model;
        private string name;
        private string screen;
        
        private string dname;

        private string fname;
        private string lname;
        private string email;
        private string business;
        private string licplate;

        public UserSettingsViewModel()
        {
            Title = "User Settings";
            SetRealms();
            GetDeviceInfo();
        }

        //User Info

        public string FirstName
        {
            set { SetProperty(ref fname, value); }
            get { return fname; }
        }

        public string LastName
        {
            set { SetProperty(ref lname, value); }
            get { return lname; }
        }

        public string Email
        { 
            set { SetProperty(ref email, value);}
            get { return email; } }

        public string Business
        { 
            set { SetProperty(ref business, value); }
            get { return business; } }

        public string LicensePlate
        { 
            set { SetProperty(ref licplate, value); }
            get { return licplate; } }

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

       
        
       

        private Realm alldataRealm;
        private Realms.Sync.User _user;

       
        

        [RelayCommand]
        public void SetRealms()
        {
            _user = App.realmApp.CurrentUser;
            var syncConfig = new FlexibleSyncConfiguration(_user)
            {
                PopulateInitialSubscriptions = (alldataRealm) =>
                {
                    var usersData = alldataRealm.All<User>().Where(n => n.OwnerId == _user.Id);
                    var devicedata = alldataRealm.All<UserDevices>().Where(n => n.OwnerId == _user.Id);
                    alldataRealm.Subscriptions.Add(usersData);
                    alldataRealm.Subscriptions.Add(devicedata);

                }


            };

            alldataRealm =  Realm.GetInstance(syncConfig);
            
            GetUser = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);
            GetUserDevices = alldataRealm.All<UserDevices>().FirstOrDefault(e => e.OwnerId == _user.Id);

            syncConfig.OnSessionError = (sender, e) =>
            {
                //handle errors here
                Console.WriteLine(e.Message);
            };

            

        }

        private void UpdateUser()
        {
            alldataRealm.Write(() =>
            {
                var user = alldataRealm.All<User>().FirstOrDefault(n => n.OwnerId == App.realmApp.CurrentUser.Id);
                user.Firstname = FirstName; 
                user.Lastname = LastName;
                user.Licenseplate = LicensePlate;
                user.Business = Business;

            });

            Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        private Task DeviceCheck()
        {
            if(GetUserDevices == null)
            {
                AddDeviceInfo();
                return Task.CompletedTask;
            }
            else if(GetUserDevices.Devicename == name && GetUserDevices.DeviceManuf == manu)
            {
                return Task.CompletedTask;

            }
            else
            {
                AddDeviceInfo();
                return Task.CompletedTask;
            }

        }
        
        private void AddDeviceInfo()
        {
            alldataRealm.Write(() =>
            {

                var device = new UserDevices
                {
                    DeviceManuf = manu,
                    Devicename = name,
                    DeviceOS = OS,
                    Devicetype = idiom,
                    Deviceisonline = true,
                    OwnerId = App.realmApp.CurrentUser.Id,
                    DeviceOwner = GetUser,
                    Devicescreensize = screen

                };

                alldataRealm.Add(device);

            });

           

            Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            
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


