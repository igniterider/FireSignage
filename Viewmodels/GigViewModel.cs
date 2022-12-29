using CommunityToolkit.Mvvm.Input;
using FireSignage.Services;
using FireSignage.Views.Settings;
using FireSignage.Views.SignDisplay;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User = FireSignage.Models.User;

namespace FireSignage.Viewmodels
{
    public partial class GigViewModel : BaseViewModel
    {

       

        private string idiom;
        private string OS;
        private string osVersion;
        private string manu;
        private string model;
        private string name;
        private string screen;
        public string dname;
        private bool isdevicesign;

        PremadeService premadeService;

       

        public ObservableCollection<PreMadeSigns> GetSigns { get; } = new();
        public ObservableCollection<String> GetDevices { get; } = new();
        

        public List<CategoriesList> SignCategory { get; set; } = new();
        public List<PreMadeSigns> Rides { get; set; } = new();
        
        private string categoryname;
        private int devicecount = 0;

        readonly IReadOnlyDictionary<string, Color> colors = typeof(Colors)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .ToDictionary(c => c.Name, c => (Color)(c.GetValue(null) ?? throw new InvalidOperationException()));

        private string selectedItem;
        private Color selectedColor;
        private Color selectedBackColor;
        private string selectedbackItem;
        private List<String> colors2 = new List<string>();


        public GigViewModel()
        {
            Title = "Dashboard";
            premadeService = new PremadeService();
            MyColors = colors.Keys.ToList();
            CreateUserRealm();


        }


        public List<String> MyColors
        {
            get
            {
                return colors2;
            }
            set
            {
                colors2 = value;
                OnPropertyChanged("Colors");
            }
        }

        public Color SelectedColor
        {
            get
            {
                if (SelectedItem != null)
                    return (Color)typeof(Color).GetField(SelectedItem).GetValue(this);
                return SelectedColor;
            }
            internal set
            {
                selectedColor = value;

                //took out colorchange method


            }
        }

        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("SelectedColor");




            }


        }

        public string SelectedBackItem
        {
            get
            {
                return selectedbackItem;
            }
            set
            {
                selectedbackItem = value;
                OnPropertyChanged("SelectedBackItem");
                OnPropertyChanged("SelectedBackColor");




            }

        }

        public Color SelectedBackColor
        {
            get
            {
                if (SelectedBackItem != null)
                    return (Color)typeof(Color).GetField(SelectedBackItem).GetValue(this);
                return SelectedBackColor;
            }
            internal set
            {
                selectedColor = value;


            }
        }


        //Icommand auto creates instead of writing code for the command and in constructor booo yahhh
        

        [RelayCommand]
        async Task GetSignsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var signs = await premadeService.GetPreMadeSigns();

                if (GetSigns.Count != 0)

                    GetSigns.Clear();

                foreach (var sign in signs)
                    GetSigns.Add(sign);



            }


            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Signs: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }


            finally //gets called no matter what with try or exception
            {
                IsBusy = false;

            }
        }




        [RelayCommand]
        async Task GetRideshareAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var signs = await premadeService.GetPreMadeSigns();
                var ride = signs.Where(n => n.Category == "Rideshare");
                

                foreach (var rides in ride)
                {
                    Rides.Add(rides);

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Signs: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }


            finally //gets called no matter what with try or exception
            {


                IsBusy = false;


            }
        }
        
        public Realm alldataRealm;
        private Realms.Sync.User _user;

        public IQueryable<User> UsersData { get; private set; }
        private string selectedDevice;

        public string SelectedDevice
        {
            get
            {
                return selectedDevice;
            }
            set
            {
                selectedDevice = value;
                OnPropertyChanged("SelectedDevice");
                
                //Set Current Sign to View and Change
                Console.WriteLine("Device is set to" +  selectedDevice);


            }


        }

        [RelayCommand]
        public void CreateUserRealm()
        {
            
                _user = App.realmApp.CurrentUser;
                var syncConfig = new FlexibleSyncConfiguration(_user)
                {
                    PopulateInitialSubscriptions = (alldataRealm) =>
                    {
                        var usersData = alldataRealm.All<User>().Where(n => n.OwnerId == _user.Id);
                        alldataRealm.Subscriptions.Add(usersData);

                    }


                };

                alldataRealm = Realm.GetInstance(syncConfig);

            UsersData = alldataRealm.All<User>().Where(t => t.OwnerId == _user.Id);

            var deviceinfo = UsersData;
                foreach (var dev in deviceinfo)
                {
                    foreach (var d in dev.Userdeviceinfo)
                    {
                        dname = d.Devicename;
                        
                        GetDevices.Add(dname);
                        Console.WriteLine(dname);

                       
                    }
                   

                }



                syncConfig.OnSessionError = (sender, e) =>
                {
                    //handle errors here
                    Console.WriteLine(e.Message);
                };

            

        }


        private void AddSubscriptionsToRealm()
        {
            var subscriptions = alldataRealm.Subscriptions;
            subscriptions.Update(() =>
            {
                var defaultSubscription = alldataRealm.All<User>()
                    .Where(t => t.OwnerId == _user.Id);
                subscriptions.Add(defaultSubscription);
            });
        }

        static EventHandler<Realms.ErrorEventArgs> SessionErrorHandler()
        {
            return (session, errorArgs) =>
            {
                var sessionException = (SessionException)errorArgs.Exception;
                switch (sessionException.ErrorCode)
                {
                    case ErrorCode.AccessTokenExpired:
                    case ErrorCode.BadUserAuthentication:
                        // Ask user for credentials
                        break;
                    case ErrorCode.PermissionDenied:
                        // Tell the user they don't have permissions to work with that Realm
                        break;
                    default:
                        // We have another error. Check the application log for
                        // details and/or add another `case` statement.
                        break;
                }
            };
        }


    }   

    }

   