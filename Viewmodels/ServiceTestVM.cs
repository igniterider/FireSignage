using CommunityToolkit.Mvvm.Input;
using FireSignage.Services;
using System.Threading.Tasks;

namespace FireSignage.Viewmodels
{
    public partial class ServiceTestVM : BaseViewModel
    {
        private RealmService realmservice;
        public DisplaySign GetText { get; set; }

        private async Task SetRealm()
        {
            await realmservice.GetDisplayRealm();
        }

        [RelayCommand]
        public async Task GetData()
        {
            await SetRealm();
            var getdisplay = realmservice.alldataRealm.All<DisplaySign>().FirstOrDefault(t => t.OwnerId == App.realmApp.CurrentUser.Id); ;
            var tdisplay = getdisplay.Displaytext;
            textDisplay = tdisplay;
            Console.WriteLine(tdisplay);
            Console.WriteLine(textDisplay);
            GetText = realmservice.alldataRealm.All<DisplaySign>().FirstOrDefault(e => e.Devicename2 == "test");
        }

      
        public string textDisplay;

        public ServiceTestVM()
        {
            realmservice = new RealmService();
           
        }

    }
}
