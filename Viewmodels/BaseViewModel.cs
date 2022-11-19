using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;

namespace FireSignage.Viewmodels;

public partial class BaseViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;




    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy;


}
