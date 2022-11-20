using Realms.Sync;
using Realms;
using Realms.Sync.Exceptions;
using ErrorEventArgs = Realms.ErrorEventArgs;

namespace FireSignage.Views.Testing;

public partial class Test1 : ContentPage
{
    public IEnumerable<DisplaySign> Items { get; }

    private Realm _itemsRealm;
    private Realms.Sync.User _user;

    public Test1()
    {
        InitializeComponent();
        _user = App.realmApp.CurrentUser;
        var config = new FlexibleSyncConfiguration(_user);
        _itemsRealm = Realm.GetInstance(config);
        AddSubscriptionsToRealm();
        config.OnSessionError = (sender, e) =>
        {
            //handle errors here
        };
       
        Items = (IEnumerable<DisplaySign>)_itemsRealm.All<DisplaySign>();
        BindingContext = this;
       
    }

    private void AddSubscriptionsToRealm()
    {
        var subscriptions = _itemsRealm.Subscriptions;
        subscriptions.Update(() =>
        {
            var defaultSubscription = _itemsRealm.All<DisplaySign>()
                .Where(t => t.OwnerId == _user.Id);
            subscriptions.Add(defaultSubscription);
        });
    }

    /// <summary>
    /// Handle Sync errors that might occur. This is only a subset
    /// of possible errors. See Realms.Sync.Exceptions.ErrorCode
    /// for the complete enumeration.
    /// </summary>
    /// <returns></returns>
    static EventHandler<ErrorEventArgs> SessionErrorHandler()
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