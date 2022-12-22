using CommunityToolkit.Mvvm.Input;
using FireSignage.Views.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSignage.Views;
using FireSignage.Views.Dashboards;
using FireSignage.Views.SignDisplay;
using FireSignage.Views.Testing;

namespace FireSignage.Viewmodels
{
    public partial class MainPageViewModel: BaseViewModel
    {
        
        
        public MainPageViewModel()
        {
            Title = "Home";
            
        }

        [RelayCommand]
        async Task Navigate(string page)
        {
            
            Console.WriteLine(page);
            
            


            await Shell.Current.GoToAsync(page);

        }
       
    }
}
