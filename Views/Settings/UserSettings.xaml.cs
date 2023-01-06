namespace FireSignage.Views.Settings;

public partial class UserSettings : ContentPage
{
	UserSettingsViewModel vm;
	public UserSettings()
	{
		InitializeComponent();
		vm = new UserSettingsViewModel();
		BindingContext = vm;
	}
}
