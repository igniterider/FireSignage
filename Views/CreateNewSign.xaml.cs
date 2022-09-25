namespace FireSignage.Views;

public partial class CreateNewSign : ContentPage
{
	
	public CreateNewSign()
	{
		InitializeComponent();
		BindingContext = new NewSignViewModel();
	}

    
}
