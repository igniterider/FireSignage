namespace FireSignage.Views.Dashboards;

public partial class GigDashBoard : ContentPage
{
	GigViewModel gigviewmodel;

	public GigDashBoard()
	{
		InitializeComponent();
        gigviewmodel = new GigViewModel();
		BindingContext = gigviewmodel;
	}
   

}