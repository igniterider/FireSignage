using FireSignage.ViewModels;

namespace FireSignage.Views.Settings;

public partial class SomethingWentWrongPage : ContentPage
{
	public SomethingWentWrongPage()
	{
		InitializeComponent();
        this.BindingContext = SomethingWentWrongPageViewModel.BindingContext;
    }
}
