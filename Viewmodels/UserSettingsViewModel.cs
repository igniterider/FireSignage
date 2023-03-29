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
using FireSignage.Services;

namespace FireSignage.Viewmodels
{
	public partial class UserSettingsViewModel : BaseViewModel
	{
        
        
        private string idiom;
        private string OS;
        private string osVersion;
        private string manu;
        private string model;
        private string name;
        private string screen;
        
        private string dname;
        private string userid;

        private string fname;
        private string lname;
        private string email;
        private string business;
        private string licplate;

        public User VMUser { get;  }
        RealmService realmservice;

        public UserSettingsViewModel()
        {
           // Title = "User Settings";
            realmservice = new RealmService();
            VMUser = realmservice.MyInfo;
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

        public string Business1
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

        public event EventHandler<EventArgs> OperationCompeleted;

        private void HandleFailure()
        {
            App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
            //throw new NotImplementedException();
        }

        public ObservableCollection<User> GetUsers { get; } = new ObservableCollection<User>();

        [RelayCommand]
        async Task GetUsersInfo()
        {
            var userinfo = await realmservice.UsersInfo();
           
            foreach (var user in userinfo)
            {
                GetUsers.Add(user);
                
                //FirstName = user.Firstname;
                //LastName = user.Lastname;
                //Business1 = user.Business;
                //LicensePlate = user.Licenseplate;
                //Email = user.Email;

            }
           
        }

        [RelayCommand]
        public Task ChangeUsersInfoVM()
        {
            realmservice.ChangeUsersInfo(FirstName, LastName, LicensePlate, Business1);
            return Task.CompletedTask;
        }

    }
}


