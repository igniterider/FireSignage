using CommunityToolkit.Mvvm.Input;
using FireSignage.Models;
using FireSignage.Services;


namespace FireSignage.Viewmodels;



	public partial class PremadeViewModel : BaseViewModel
	{


		PremadeService premadeService;
		
		public ObservableCollection<PreMadeSigns> GetSigns { get; } = new();

    

		public PremadeViewModel(PremadeService premadeService)
		{
			Title = "Premade Signs";
			this.premadeService = premadeService;

		
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
            

			}
		}



	}

