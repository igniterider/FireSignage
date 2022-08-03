namespace FireSignage;

public partial class MainPage : ContentPage
{
	

	public MainPage(PremadeViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

	
}


