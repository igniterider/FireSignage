using CommunityToolkit.Mvvm.Input;
using FireSignage.Models;
using FireSignage.Services;


namespace FireSignage.Viewmodels;



	public partial class PremadeViewModel : BaseViewModel
	{


		PremadeService premadeService;
		
		public ObservableCollection<PreMadeSigns> GetSigns { get; } = new();

		public ObservableCollection<CategoriesList> SignCategory { get; set; } = new();
		public ObservableCollection<PreMadeSigns> Rideshare { get; set; } = new();
		public ObservableCollection<PreMadeSigns> Business { get; set; } = new();
		public ObservableCollection<PreMadeSigns> Holiday { get; set; } = new();


    private string categoryname;

		public PremadeViewModel()
		{
			Title = "Premade Signs";
			premadeService = new PremadeService();

		
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

			if(GetSigns.Count != 0)
			
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
				 await GetRideshareAsync();

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

           

            SignCategory.Add(new CategoriesList("Rideshare", Rideshare));
            //SignCategory.Add(new CategoriesList("Business", Business));
            //SignCategory.Add(new CategoriesList("Holiday", Holiday));


        }


        //foreach (var cat in signs)
        //{
        //	categoryname = cat.Category;
        //	SignCategory.Add(new CategoriesList(categoryname, new List<PreMadeSigns>
        //	{
        //		new PreMadeSigns
        //		{
        //			Category = cat.Category,
        //			Displaytext = cat.Displaytext,
        //			Displaytext2 = cat.Displaytext2,
        //			Textcolor = cat.Textcolor,
        //			Textcolor2 = cat.Textcolor2,
        //			Business = cat.Business

        //		}



        //	}));




        //var ride = signs.Where(n => n.Category == "Rideshare");

        //foreach (var rides in ride)
        //{
        //	GetRideshare.Add(rides);

        //}




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


}

