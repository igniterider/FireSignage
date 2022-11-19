using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Realms.Sync;

namespace FireSignage.Services;

public class RealmService
{
    
    public Realm alldataRealm;
    private Realms.Sync.User _user;

    public async Task GetDisplayRealm()
    {

        if (alldataRealm == null) 
        {
            var partition = App.realmApp.CurrentUser.Id;
            var syncConfig = new PartitionSyncConfiguration(partition, App.realmApp.CurrentUser);

            alldataRealm = await Realm.GetInstanceAsync(syncConfig);
            
        }
    }

}
