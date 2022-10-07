using Syncfusion.Maui.DataSource;

namespace FireSignage.Views;

public partial class PremadeSignView : ContentPage
{
	

	public PremadeSignView()
	{
		InitializeComponent();
		BindingContext = new PremadeViewModel();
        

        //listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
        //{
        //    PropertyName = "Category",
        //});
    }


}
