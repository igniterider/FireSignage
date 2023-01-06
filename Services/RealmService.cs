using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FireSignage.Models;
using FireSignage.Views.Settings;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;
using User = FireSignage.Models.User;

namespace FireSignage.Services;

public partial class RealmService
{
    
    public Realm alldataRealm;
    private Realms.Sync.User _user;

    public Models.User GetUser { get; set; }
    public UserDevices GetUserDevices { get; set; }

    public IEnumerable<DisplaySign> DisplaySigns { get; private set; }

    public RealmService()
    {


    }

    public void GetDisplayRealm()
    {

        if (alldataRealm == null) 
        {
            _user = App.realmApp.CurrentUser;
            var syncConfig = new FlexibleSyncConfiguration(_user);

            alldataRealm = Realm.GetInstance(syncConfig);
            AddSubscriptionsToRealm();
            DisplaySigns = alldataRealm.All<DisplaySign>();
            syncConfig.OnSessionError = (sender, e) =>
            {
                //handle errors here
                Console.WriteLine(e.Message);
            };
        }

       
    }


    private void AddSubscriptionsToRealm()
    {
        var subscriptions = alldataRealm.Subscriptions;
        subscriptions.Update(() =>
        {
            var defaultSubscription = alldataRealm.All<DisplaySign>()
                .Where(t => t.OwnerId == _user.Id);
            subscriptions.Add(defaultSubscription);
        });
    }

    static EventHandler<Realms.ErrorEventArgs> SessionErrorHandler()
    {
        return (session, errorArgs) =>
        {
            var sessionException = (SessionException)errorArgs.Exception;
            switch (sessionException.ErrorCode)
            {
                case ErrorCode.AccessTokenExpired:
                case ErrorCode.BadUserAuthentication:
                    // Ask user for credentials
                    break;
                case ErrorCode.PermissionDenied:
                    // Tell the user they don't have permissions to work with that Realm
                    break;
                default:
                    // We have another error. Check the application log for
                    // details and/or add another `case` statement.
                    break;
            }
        };
    }

    public event EventHandler<EventArgs> OperationCompeleted;

    

    private void HandleFailure()
    {
        App.Current.MainPage.DisplayAlert("Login Failed", "HitOk", "OK");
        //throw new NotImplementedException();
    }


    [RelayCommand]
    public void GetUserInfo()
    {
        _user = App.realmApp.CurrentUser;
        var syncConfig = new FlexibleSyncConfiguration(_user)
        {
            PopulateInitialSubscriptions = (alldataRealm) =>
            {
                var usersData = alldataRealm.All<User>().Where(n => n.OwnerId == _user.Id);
                alldataRealm.Subscriptions.Add(usersData);

            }


        };

        alldataRealm = Realm.GetInstance(syncConfig);
        GetUser = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);





        syncConfig.OnSessionError = (sender, e) =>
        {
            //handle errors here
            Console.WriteLine(e.Message);
        };

    }

}
