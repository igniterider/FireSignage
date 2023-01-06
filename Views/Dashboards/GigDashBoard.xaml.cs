namespace FireSignage.Views.Dashboards;

public partial class GigDashBoard : ContentPage
{
	GigViewModel vm;

	public GigDashBoard()
	{
		InitializeComponent();
		vm = new GigViewModel();
		BindingContext = vm;
		
	}

   
}