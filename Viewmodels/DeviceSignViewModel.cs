using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSignage.Viewmodels
{
    public partial class DeviceSignViewModel : BaseViewModel
    {
        private Realm _deviceissignrealm;
        string NameofPage;
        public Command DisplaySetupCommand { get; }

        public string backcolor;
        private string backcolor2;
        public string displaytext;
        private string displaytext2;
        private string ridername;
        private string textcolor;
        private string textcolor2;

        private string devicename;

        public DeviceSignViewModel()
        {
            DisplaySetupCommand = new Command(Watching);

        }

        private async void Watching()
        {
            var part = App.realmApp.CurrentUser.Id;

            if (_deviceissignrealm == null)
            {


                var syncConfig = new PartitionSyncConfiguration(part, App.realmApp.CurrentUser);

                _deviceissignrealm = await Realm.GetInstanceAsync(syncConfig);

            }

            var newpage = _deviceissignrealm.All<DisplaySign>().FirstOrDefault(t => t.Id == App.realmApp.CurrentUser.Id);
            newpage.PropertyChanged += async (sender, eventArgs) =>  //changed from no async
            {
                // pulls current value

                Debug.WriteLine("Pull value =" + newpage.Pagename);
                NameofPage = newpage.Pagename;
                backcolor = newpage.Backgroundcolor;
                backcolor2 = newpage.Backgroundcolor2;
                displaytext = newpage.Displaytext;
                displaytext2 = newpage.Displaytext2;
                ridername = newpage.Ridername;
                textcolor = newpage.Textcolor;
                textcolor2 = newpage.Textcolor2;
                devicename = newpage.Devicename2;

               
            };


            // Later, when you no longer wish to receive notifications
            //watch.Dispose();


        }

        public async Task ChangeSign()
        {


        }

        private async Task ChangePage(string page)
        {

            await AppShell.Current.GoToAsync("//" + page);
        }


    }
}
