

namespace FireSignage;

public partial class App : Application
{

    public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

    private const string appId = "signdisplays-awkrz";
    public static Realms.Sync.App realmApp;

    public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU0Nzg0QDMyMzAyZTMxMmUzMGFVWGs5V1VvTHZ6UVVjbHFxbURYdDdhYTB4RElISS9wZFJpQlUrRDJ6Nzg9");

        InitializeComponent();

		MainPage = new AppShell();
	}

    public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

    protected override void OnStart()
    {
        realmApp = Realms.Sync.App.Create(appId);
        var current = Connectivity.NetworkAccess;



        if (current == NetworkAccess.Internet && App.realmApp.CurrentUser == null)
        {
            // Connection to internet is available
            // MainPage = new TabbedLogin();
            MainPage = new AppShell();

        }
        else
        {
            // MainPage = new TabbedLogin();//need to make a network error page or popup
        }


    }
}

