namespace FireSignage.Views.Settings;

public partial class UserView : ContentPage
{
	UserSettingsViewModel viewModel;
	public UserView()
	{
		InitializeComponent();
		viewModel = new UserSettingsViewModel();
		BindingContext = viewModel;
	}

    
}