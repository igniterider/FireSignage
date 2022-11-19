using CommunityToolkit.Mvvm.Input;
using FireSignage.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FireSignage.Viewmodels
{
    public partial class ServiceTestVM : BaseViewModel
    {
        private RealmService realmservice;
        //public IEnumerable<DisplaySign> GetDisplay { get;}
        public DisplaySign GetDisplayText { get; }

        public ICommand CallGetDataCommand { get; }
        public string textDisplay;



        [RelayCommand]
        public async Task GetData()
        {
            await realmservice.GetDisplayRealm();
            var getdisplay = realmservice.alldataRealm.All<DisplaySign>().FirstOrDefault(t => t.OwnerId == App.realmApp.CurrentUser.Id);
            var tdisplay = getdisplay.Displaytext;
            textDisplay = tdisplay;
            Console.WriteLine(tdisplay);
            Console.WriteLine(textDisplay);

            
           
        }

      
       

        public ServiceTestVM()
        {
            realmservice = new RealmService();
            CallGetDataCommand = new AsyncCommand(GetData);
            GetDisplayText = realmservice.alldataRealm.All<DisplaySign>().FirstOrDefault(e => e.Displaytext != null);
        }

    }
}
