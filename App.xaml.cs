using FireSignage.Views.LoginFlow;
using Newtonsoft.Json;
using Realms;
using Realms.Sync;
using System;

namespace FireSignage;

public partial class App : Application
{

    public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

    private string appId = "signdisplays-awkrz";
    private string baseUrl = "https://realm.mongodb.com";
    public static Realms.Sync.App realmApp;
    private Page page;
    

    public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU0Nzg0QDMyMzAyZTMxMmUzMGFVWGs5V1VvTHZ6UVVjbHFxbURYdDdhYTB4RElISS9wZFJpQlUrRDJ6Nzg9");

        InitializeComponent();
        OnStart();
        if (realmApp.CurrentUser == null)
        {
            MainPage = new TabbedLogin();
        }
        else
        {
            MainPage = new AppShell();
        }
    }

    public static string ImageServerPath { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

    

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new MauiWindow(MainPage);
    }

    protected override void OnStart()
    {
        try
        {
            
            var appConfiguration = new Realms.Sync.AppConfiguration(appId)
            {
                BaseUri = new Uri(baseUrl)
            };
            realmApp = Realms.Sync.App.Create(appConfiguration);

           
            

        }
        catch (Exception ex)
        {
            // A Exception occurs if:
            // 1. the config file does not exist, or
            // 2. the config does not contain an "appId" or "baseUrl" element.

            // If the appId value is incorrect, we handle that
            // exception in the Login page.

            Debug.WriteLine(ex.ToString());
        }
        
    }

    protected override void OnSleep()
    {
        base.OnSleep();
    }


}

   


