
using FireSignage.Views.LoginFlow;

namespace FireSignage.Controls;

public partial class FlyoutFooter : ContentView
{
    public FlyoutFooter()
    {
        InitializeComponent();
    }

    async void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
        await App.realmApp.CurrentUser.LogOutAsync();
      //  await Shell.Current.GoToAsync($"///{nameof(TabbedLogin)}");
        await Navigation.PushAsync(new TabbedLogin());
    }
}