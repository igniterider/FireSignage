using System;
using CommunityToolkit.Mvvm.Input;
using Realms;
using CommunityToolkit.Maui;

using Realms.Sync;
using User = FireSignage.Models.User;

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



        public ObservableCollection<User> _getUsers = new ObservableCollection<User>();

        public ObservableCollection<User> GetUsers
        {

            get
            {
                return _getUsers;
            }
            set
            {
                _getUsers = value;
                OnPropertyChanged("_getUsers");

            }
        }

        


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
                _getUsers = new ObservableCollection<User>(userRealm.All<User>().Where(t => t.Id == App.realmApp.CurrentUser.Id));

                FName = userinfo.Firstname;
                LName = userinfo.Lastname;
                Console.WriteLine(FName);
                Console.WriteLine(userinfo.Firstname);

                //Console.WriteLine("realmappID = ", App.realmApp.CurrentUser.Id.ToString());
                //Console.WriteLine("realm user = ", App.realmApp.CurrentUser.ToString());
                //Console.WriteLine(fname);
                //Console.WriteLine(lname);

                //var device = new UserDeviceInfo()
                //{
                //    DeviceManuf = manu,
                //    Devicename = name,
                //    DeviceOS = OS,
                //    Devicetype = idiom,
                //    Devicescreensize = screen,
                //    OwnerId = partid



                //};

                //userRealm.Write(() =>
                //{

                //    userinfo.Firstname = fname;
                //    userinfo.Lastname = lname;
                //    userinfo.Licenseplate = licplate;

                //    userinfo.Userdeviceinfo.Add(device);

                //});

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

            Console.WriteLine(idiom);
            Console.WriteLine(OS);
            Console.WriteLine(osVersion);
            Console.WriteLine(manu);
            Console.WriteLine(model);
            Console.WriteLine(name);
            Console.WriteLine(screen);

            

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

