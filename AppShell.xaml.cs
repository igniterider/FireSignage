using System.Windows.Input;
using FireSignage.Views.Dashboards;
using FireSignage.Views.LoginFlow;
using FireSignage.Views.Settings;
using FireSignage.Views.SignDisplay;

namespace FireSignage;

public partial class AppShell : Shell
{

    public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
    public ICommand HelpCommand => new Microsoft.Maui.Controls.Command<string>(async (url) => await Launcher.OpenAsync(url));

    
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
        BindingContext = this;
        
    }

    void RegisterRoutes()
    {

        Routes.Add("SignDisplayMain", typeof(SignDisplayMain));
        //Routes.Add("TabbedLogin", typeof(TabbedLogin));
       // Routes.Add("RegisterUserSettings", typeof(RegisterUserSettings));

        foreach (var item in Routes)
        {
            Routing.RegisterRoute(item.Key, item.Value);
        }

    }


}

