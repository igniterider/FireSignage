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
    
    private Realm alldataRealm;
    private Realms.Sync.User _user;

    public User GetUser { get; set; }
    public UserDevices GetUserDevices { get; set; }

    public User MyInfo { get; set; }

    public RealmService()
    {
        

    }

    async Task GetRealms()
    {
        if (alldataRealm == null)
            {
                _user = App.realmApp.CurrentUser;
                var syncConfig = new FlexibleSyncConfiguration(_user);


                AddSubscriptionsToRealm();

                alldataRealm = await Realm.GetInstanceAsync(syncConfig);


                syncConfig.OnSessionError = (sender, e) =>
                {
                    //handle errors here
                    Console.WriteLine(e.Message);
                };
            MyInfo = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);

        }


        else
        {
            var config = new FlexibleSyncConfiguration(App.realmApp.CurrentUser);
            alldataRealm = Realm.GetInstance(config);
            alldataRealm.Subscriptions.Update(() =>
            {
                var usersData = alldataRealm.All<User>().Where(n => n.OwnerId == _user.Id);
                alldataRealm.Subscriptions.Add(usersData, new SubscriptionOptions() { Name = "AllUserInfo" });

                var userDeviceData = alldataRealm.All<UserDevices>().Where(n => n.OwnerId == _user.Id);
                alldataRealm.Subscriptions.Add(userDeviceData, new SubscriptionOptions() { Name = "AllUserDevices" });
            });
            
            await alldataRealm.Subscriptions.WaitForSynchronizationAsync();
            MyInfo = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);

        }

        

    }


    private void AddSubscriptionsToRealm()
    {
        var syncConfig = new FlexibleSyncConfiguration(_user)
        {
            PopulateInitialSubscriptions = (alldataRealm) =>
            {
                var usersData = alldataRealm.All<User>().Where(n => n.OwnerId == _user.Id);
                alldataRealm.Subscriptions.Add(usersData, new SubscriptionOptions() { Name = "AllUserInfo"});

                var userDeviceData = alldataRealm.All<UserDevices>().Where(n => n.OwnerId == _user.Id);
                alldataRealm.Subscriptions.Add(userDeviceData, new SubscriptionOptions() { Name = "AllUserDevices" });

            }


        };
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



   List<User> userinfo = new();

   public async Task<List<User>> UsersInfo()
    {
        await GetRealms();
        GetUser = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);
        userinfo = alldataRealm.All<User>().Where(e => e.OwnerId == _user.Id).ToList();
       // MyInfo = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);
        return userinfo;
    }



    public Task ChangeUsersInfo(string F, string L, string Lic, string busi)
    {
        alldataRealm.Write(() =>
        {
            var myuser = alldataRealm.All<User>().FirstOrDefault(e => e.OwnerId == _user.Id);
            myuser.Firstname = F;
            myuser.Lastname = L;
            myuser.Licenseplate = Lic;
            myuser.Business = busi;

        });
          
        return Task.CompletedTask;

    }

}
