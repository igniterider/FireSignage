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

    void OnEntryCompleted(object sender, EventArgs e)
    {
        string fname = ((Entry)sender).Text;


		
		
    }
}