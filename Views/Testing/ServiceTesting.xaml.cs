using FireSignage.Services;
using Realms;
using Realms.Sync;

namespace FireSignage.Views.Testing;

public partial class ServiceTesting : ContentPage
{
	

    private Realm alldataRealm;

    public ServiceTesting()
	{
		InitializeComponent();
		
		PrintUserInfo();
		GetRealm();
	}

	private void PrintUserInfo()
	{
		var a = App.realmApp.CurrentUser.State.ToString();
		Console.WriteLine("UserstateServiceTest = " + a);

		var b = App.realmApp.GetType().Name;
		Console.WriteLine("App Type = " +  b);
	}

	private void GetRealm()
	{
		var _user = App.realmApp.CurrentUser;
        var config = new FlexibleSyncConfiguration(_user);
        var realm = Realm.GetInstance(config);

		var subs = realm.Subscriptions.Count();
		Console.WriteLine("Subs Count = " + subs);

		var subslist = realm.Subscriptions.ToList();
		foreach (var a in subslist)
		{
			Console.WriteLine("Sub List = " + a);
		}

		alldataRealm = Realm.GetInstance(config);
		var privsubs = alldataRealm.Subscriptions.Count();
		var privsubslist = alldataRealm.Subscriptions.ToList();

		Console.WriteLine("Private Realm Count = " + privsubs);
		foreach(var a in privsubslist)
		{
			Console.WriteLine("Private Realm Count = " + a);
        }
    }
}