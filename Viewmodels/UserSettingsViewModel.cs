using System;
using CommunityToolkit.Mvvm.Input;
using Realms;
using CommunityToolkit.Maui;
using MongoDB;
using MongoDB.Driver;
using Realms.Sync;
using User = FireSignage.Models.User;
using System.Linq;

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
        private string dname;

        public List<String> userDeviceList = new List<String>();

        public UserSettingsViewModel()
        {
            Title = "User Settings";
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
            if(userRealm == null)
            {
                var user = App.realmApp.CurrentUser;
                var partid = App.realmApp.CurrentUser.Id;
                var config = new PartitionSyncConfiguration(partid, App.realmApp.CurrentUser);
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
                var config = new PartitionSyncConfiguration(partid, App.realmApp.CurrentUser);
                userRealm = await Realm.GetInstanceAsync(config);
                await userRealm.SyncSession.WaitForDownloadAsync();


            var userinfo = userRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);

            FName = userinfo.Firstname;
            LName = userinfo.Lastname;
            LicPlate = userinfo.Licenseplate;
            Email = userinfo.Email;

            foreach (var dev in userinfo.Userdeviceinfo)
            {
                dname = dev.Devicename;
                userDeviceList.Add(dname);
                Console.WriteLine(dname);
            }
           

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
            userRealm = await Realm.GetInstanceAsync(config);
            await userRealm.SyncSession.WaitForDownloadAsync();


            var userinfo = userRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);

            userRealm.Write(() =>
            {
                userinfo.Userdeviceinfo.Add(device);

            });

            
        }

        public event EventHandler<EventArgs> OperationCompeleted;

        private void HandleFailure()
        {
            App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
            //throw new NotImplementedException();
        }

        //private void ChangeName(object obj)
        //{
        //    CheckLogin();
        //    var allPages = controllerRealm.All<DisplayChanges>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
        //    controllerRealm.Write(() =>
        //    {


        //        allPages.Id = App.realmApp.CurrentUser.Id;
        //        allPages.OwnerId = App.realmApp.CurrentUser.Id;
        //        allPages.Pagename = pagename;
        //        allPages.Textcolor = "White";


        //    });


        //}


    }
}


