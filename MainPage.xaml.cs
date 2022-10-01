namespace FireSignage;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
        
    }

    void SwipeContainer_Swipe(System.Object sender, Microsoft.Maui.Controls.SwipedEventArgs e)
    {

        switch (e.Direction)
        {
            case SwipeDirection.Left:
                // Handle the swipe
                BackgroundColor = Colors.Red;

                break;
            case SwipeDirection.Right:
                // Handle the swipe

                BackgroundColor = Colors.Green;

                break;
            case SwipeDirection.Up:
                // Handle the swipe
                break;
            case SwipeDirection.Down:
                // Handle the swipe
                break;
        }


    }

}


