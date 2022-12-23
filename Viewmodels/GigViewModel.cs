using CommunityToolkit.Mvvm.Input;
using FireSignage.Services;
using FireSignage.Views.Settings;
using FireSignage.Views.SignDisplay;
using Realms;
using Realms.Sync;
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


        private Realm newRealm;
        private string idiom;
        private string OS;
        private string osVersion;
        private string manu;
        private string model;
        private string name;
        private string screen;
        private string dname;
        private bool isdevicesign;

        PremadeService premadeService;

        public ObservableCollection<PreMadeSigns> GetSigns { get; } = new();
        public ObservableCollection<String> GetDevices { get; } = new();

        public List<CategoriesList> SignCategory { get; set; } = new();
        public List<PreMadeSigns> Rides { get; set; } = new();
        public List<PreMadeSigns> Business { get; set; } = new();
        public List<PreMadeSigns> Holiday { get; set; } = new();
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
            GetDeviceInfo();
            

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
                var business = signs.Where(n => n.Category == "Business");
                var holiday = signs.Where(n => n.Category == "Holiday");

                foreach (var rides in ride)
                {
                    Rides.Add(rides);

                }

                foreach (var hol in holiday)
                {

                    Holiday.Add(hol);
                }


                foreach (var bus in business)
                {
                    Business.Add(bus);
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

        public List<String> userDeviceList = new List<String>();

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

        [RelayCommand]
        async Task CheckDevices()
        {

            try
            {

                var user = App.realmApp.CurrentUser;
                var partid = App.realmApp.CurrentUser.Id;
                var config = new PartitionSyncConfiguration(partid, App.realmApp.CurrentUser);
                newRealm = await Realm.GetInstanceAsync(config);
                await newRealm.SyncSession.WaitForDownloadAsync();


                var deviceinfo = newRealm.All<User>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
                foreach (var dev in deviceinfo.Userdeviceinfo)
                {
                    dname = dev.Devicename;
                    userDeviceList.Add(dname);
                    Console.WriteLine(dname);

                    if (name == dname)
                    {
                        isdevicesign = dev.Deviceissign ?? false;
                        devicecount++;
                        Console.WriteLine(name);

                    }

                }

                if (devicecount <= 0)
                {
                    bool answer = await Shell.Current.DisplayAlert("Device Not Registered", "Register Now?", "Yes", "No");
                    if (answer == true)
                    {
                        await Shell.Current.GoToAsync($"//{nameof(DeviceSettingsPage)}");
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            DeviceUse();

        }

        private async void DeviceUse()
        {
            if (isdevicesign == true)
            {
                await Shell.Current.GoToAsync($"/{nameof(SignDisplayMain)}");

            }

            else if (isdevicesign == false)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

            }

        }


    }   

    }

   