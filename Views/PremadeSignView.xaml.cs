namespace FireSignage.Views;

public partial class PremadeSignView : ContentPage
{
	public PremadeSignView(PremadeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
