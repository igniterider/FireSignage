namespace FireSignage.Viewmodels;

public partial class BaseViewModel : ObservableObject
{


    internal virtual void OnAppearing() { }

    internal virtual void OnDisappearing() { }

    [ObservableProperty]
    [AlsoNotifyChangeFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    string title;

    public bool IsNotBusy => !IsBusy;


}
