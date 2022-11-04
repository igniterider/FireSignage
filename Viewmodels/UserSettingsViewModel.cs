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
        


        public UserSettingsViewModel()
        {
            Title = "User Settings";

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

           

        }

        public event EventHandler<EventArgs> OperationCompeleted;

        private void HandleFailure()
        {
            App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
            //throw new NotImplementedException();
        }
    }
}


