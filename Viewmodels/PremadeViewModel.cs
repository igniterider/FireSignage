using System.Linq;
using System.Reflection;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using FireSignage.Models;
using FireSignage.Services;
using FireSignage.Views.Settings;
using FireSignage.Views.SignDisplay;
using Microsoft.Maui.Controls;
using Realms;
using Realms.Sync;
using User = FireSignage.Models.User;

namespace FireSignage.Viewmodels;



public partial class PremadeViewModel : BaseViewModel
{
   

    
    
    private bool isdevicesign;

    PremadeService premadeService;

	public ObservableCollection<PreMadeSigns> GetSigns { get; } = new();
    

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

    public PremadeViewModel()
	{
		Title = "Dashboard";
		premadeService = new PremadeService();
		MyColors = colors.Keys.ToList();
       
        

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


    //public Realm alldataRealm;
    //private Realms.Sync.User _user;
    //public UserDevices GetUserDevices { get; set; }

    //public IQueryable<User> UsersData { get; private set; }

    //public List<String> userDeviceList = new List<String>();


    //[RelayCommand]
    //public void CreateUserRealm()
    //{

    //    _user = App.realmApp.CurrentUser;
    //    var syncConfig = new FlexibleSyncConfiguration(_user)
    //    {
    //        PopulateInitialSubscriptions = (alldataRealm) =>
    //        {
    //            var usersData = alldataRealm.All<User>().Where(n => n.OwnerId == _user.Id);
    //            alldataRealm.Subscriptions.Add(usersData);

    //        }


    //    };

    //    alldataRealm = Realm.GetInstance(syncConfig);

    //    UsersData = alldataRealm.All<User>().Where(t => t.OwnerId == _user.Id);
    //    GetUserDevices = alldataRealm.All<UserDevices>().FirstOrDefault(e => e.OwnerId == _user.Id);




    //    syncConfig.OnSessionError = (sender, e) =>
    //    {
    //        //handle errors here
    //        Console.WriteLine(e.Message);
    //    };



    //}

    

   
       

           
        //    foreach (var dev in deviceinfo.Userdeviceinfo)
        //    {
        //        dname = dev.Devicename;
        //        userDeviceList.Add(dname);
        //        Console.WriteLine(dname);

        //        if (name == dname)
        //        {
        //            isdevicesign = dev.Deviceissign ?? false;
        //            devicecount++;
        //            Console.WriteLine(name);
                    
        //        }

        //    }

        //    if (devicecount <= 0)
        //    {
        //        bool answer = await Shell.Current.DisplayAlert("Device Not Registered", "Register Now?", "Yes", "No");
        //        if (answer == true)
        //        {
        //            await Shell.Current.GoToAsync($"//{nameof(DeviceSettingsPage)}");
        //            return;
        //        }
        //    }
        

        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.ToString());
        //    throw;
        //}

        //DeviceUse();
        
    

	//private async void DeviceUse()
 //   {
 //       if(isdevicesign == true)
 //       {
 //           await Shell.Current.GoToAsync($"/{nameof(SignDisplayMain)}");

 //       }

 //       else if(isdevicesign == false)
 //       {
 //           await Shell.Current.GoToAsync($"//{nameof(MainPage)}");

 //       }

 //   }

    
    //private void print()
    //{
    //    //string nav = Shell.NavigationProperty.DefaultValue.ToString();
    //    //Console.WriteLine("Nav Default Value = " + nav);


    //    string main = Microsoft.Maui.Controls.Page.NavigationProperty.PropertyName.ToString();
    //    Console.WriteLine("ShellNav = " + main);

       

    //}

}

