using System.Reflection;
using CommunityToolkit.Mvvm.Input;
using FireSignage.Models;
using FireSignage.Services;


namespace FireSignage.Viewmodels;



public partial class PremadeViewModel : BaseViewModel
{

    
    PremadeService premadeService;

	public ObservableCollection<PreMadeSigns> GetSigns { get; } = new();

	public List<CategoriesList> SignCategory { get; set; } = new();
	public List<PreMadeSigns> Rides { get; set; } = new();
	public List<PreMadeSigns> Business { get; set; } = new();
	public List<PreMadeSigns> Holiday { get; set; } = new();
	private string categoryname;


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






		//var ride = signs.Where(n => n.Category == "Rideshare");







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



	
	void CreateCollection()
	{
		


		SignCategory.Add(new CategoriesList("Rideshare", Rides));
		


		SignCategory.Add(new CategoriesList("Business", new List<PreMadeSigns>
		{







		}));



		SignCategory.Add(new CategoriesList("Holiday", Holiday));

		foreach (var sign in Rides)
		{
			var dis = sign.Displaytext;
			Console.WriteLine(dis);

		}


    }

}

