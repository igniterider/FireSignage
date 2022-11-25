using CommunityToolkit.Mvvm.Input;
using Realms;
using Realms.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireSignage.Viewmodels
{
    public partial class DeviceSignViewModel : BaseViewModel
    {
        private Realm signrealm;
        string NameofPage;
       
        public DisplaySign getsign { get; }

        private string backcolor;
        private string backcolor2;
        private string displaytext;
        private string displaytext2;
        private string ridername;
        private string textcolor;
        private string textcolor2;

        private string devicename;

        public DeviceSignViewModel()
        {
            
            
        }


        public string DisplayText
        {
            set { SetProperty(ref displaytext, value); }
            get { return displaytext; }


        }

        [RelayCommand]
        async Task Watching()
        {
            var part = App.realmApp.CurrentUser.Id;

            if (signrealm == null)
            {


                var syncConfig = new FlexibleSyncConfiguration(part);

                signrealm = await Realm.GetInstanceAsync(syncConfig);
                var newpage = signrealm.All<DisplaySign>().FirstOrDefault(t => t.OwnerId == App.realmApp.CurrentUser.Id);
                if (newpage == null)
                {
                    Console.WriteLine("realm showing null");
                    var transaction = signrealm.BeginWrite();
                    try
                    {
                        var newsign = new DisplaySign
                        {
                            Displaytext = "UBER",
                            Displaytext2 = "",
                            Id = App.realmApp.CurrentUser.Id,
                            OwnerId = App.realmApp.CurrentUser.Id
                        };
                        DisplayText = newsign.Displaytext;

                        // Perform a write op...
                        signrealm.Add(newsign);
                        // Do other work that needs to be included in
                        // this transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Something went wrong; roll back the transaction
                        transaction.Dispose();
                    }




                }

                newpage.PropertyChanged += async (sender, eventArgs) =>  //changed from no async
                {
                    // pulls current value

                    Debug.WriteLine("Pull value =" + newpage.Displaytext);
                    NameofPage = newpage.Pagename;
                    backcolor = newpage.Backgroundcolor;
                    backcolor2 = newpage.Backgroundcolor2;
                    displaytext = newpage.Displaytext;
                    displaytext2 = newpage.Displaytext2;
                    ridername = newpage.Ridername;
                    textcolor = newpage.Textcolor;
                    textcolor2 = newpage.Textcolor2;
                    devicename = newpage.Devicename2;

                    DisplayText = newpage.Displaytext;
                };
                DisplayText = newpage.Displaytext;
                Console.WriteLine("Display Text = " + DisplayText);
                Console.WriteLine("displaytext = " + displaytext);
                // Later, when you no longer wish to receive notifications
                //watch.Dispose();
                

            }

        }

        

        private async Task ChangePage(string page)
        {

            await Shell.Current.GoToAsync("//" + page);
        }


    }
}
