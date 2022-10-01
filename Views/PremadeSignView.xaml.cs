namespace FireSignage.Views;

public partial class PremadeSignView : ContentPage
{
	

	public PremadeSignView()
	{
		InitializeComponent();
		BindingContext = new PremadeViewModel();
	}
}
