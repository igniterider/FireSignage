using CommunityToolkit.Mvvm.Input;
using FireSignage.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FireSignage.Viewmodels
{
    public partial class ServiceTestVM : BaseViewModel
    {
        private RealmService realmservice;
        public IEnumerable<DisplaySign> GetDisplay { get; set; }
        public DisplaySign GetDisplayText { get;  }


        [ObservableProperty]
        public string textDisplay;



        [RelayCommand]
        public Task GetData()
        {
            realmservice.GetDisplayRealm();
            var getdisplay = realmservice.alldataRealm.All<DisplaySign>().FirstOrDefault(t => t.OwnerId == App.realmApp.CurrentUser.Id);
            var tdisplay = getdisplay.Displaytext;
            textDisplay = tdisplay;
            Console.WriteLine(tdisplay);
            Console.WriteLine(textDisplay);
            GetDisplay = realmservice.DisplaySigns;
            return Task.CompletedTask;
            
           
        }



        
        

        public ServiceTestVM(RealmService realms)
        {
            realmservice = realms;
        }

    }
}
