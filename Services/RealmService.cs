using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSignage.Models;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;

namespace FireSignage.Services;

public class RealmService
{
    
    public Realm alldataRealm;
    private Realms.Sync.User _user;

    public IEnumerable<DisplaySign> DisplaySigns { get; private set; }

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

}
