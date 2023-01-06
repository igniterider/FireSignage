namespace FireSignage.Views.Settings;

public partial class DeviceSettingsPage : ContentPage
{
	UserSettingsViewModel vm;
	public DeviceSettingsPage()
	{
		InitializeComponent();
		vm = new UserSettingsViewModel();
		BindingContext = vm;
	}
}