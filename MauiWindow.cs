using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSignage;

class MauiWindow : Window
{
    public MauiWindow() : base() { }

    public MauiWindow(Page page) : base(page) { }


    protected override void OnCreated()
    {
        base.OnCreated();



    }
    protected override void OnDestroying()
    {
        var user = App.realmApp.CurrentUser;
        user.LogOutAsync();
        base.OnDestroying();
    }
}
