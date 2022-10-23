using System;
using Realms;

namespace FireSignage.Viewmodels
{
	public class UserSettingsViewModel : BaseViewModel
	{
        private Realm controllerRealm;

        private string password;
        private string fname;
        private string lname;
        private string licplate;




        public string FName
        {
            get
            {
                return this.fname;
            }

            set
            {
                if (this.fname == value)
                {
                    return;
                }
                this.fname = value;
                this.OnPropertyChanged();
            }

        }

        public string LName
        {
            get
            {
                return this.lname;
            }

            set
            {
                if (this.lname == value)
                {
                    return;
                }
                this.lname = value;
                this.OnPropertyChanged();
            }

        }

        public string LicPlate
        {
            get
            {
                return this.licplate;
            }

            set
            {
                if (this.licplate == value)
                {
                    return;
                }
                this.licplate = value;
                this.OnPropertyChanged();
            }

        }


        private async void AddUserInfo(object obj)
        {


            var userinfo = controllerRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
            controllerRealm.Write(() =>
            {
                userinfo.Id = App.realmApp.CurrentUser.Id;
                userinfo.OwnerId = App.realmApp.CurrentUser.Id;
                userinfo.Firstname = fname;
                userinfo.Lastname = lname;
                userinfo.Licenseplate = licplate;
                userinfo.Email = App.realmApp.CurrentUser.Profile.Email;
                userinfo.Password = password;


            });

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
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

