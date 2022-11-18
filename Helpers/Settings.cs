using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireSignage.Helpers
{
    public static class Settings
    {
        public static bool IsDeviceASign
        {
            get => Preferences.Get(nameof(IsDeviceASign), false);
            set => Preferences.Set(nameof(IsDeviceASign), value);
        }

        public static bool IsDeviceRegistered
        {
            get => Preferences.Get(nameof(IsDeviceRegistered), false);
            set => Preferences.Set(nameof(IsDeviceRegistered), value);
        }

        public static string UserId
        {
            get => Preferences.Get(nameof(UserId), null);
            set => Preferences.Set(nameof(UserId), value);
        }

        public static string CurrentSignText
        {
            get => Preferences.Get(nameof(CurrentSignText), null);
            set => Preferences.Set(nameof(CurrentSignText), value);
        }

        public static string CurrentSignText2
        {
            get => Preferences.Get(nameof(CurrentSignText2), null);
            set => Preferences.Set(nameof(CurrentSignText2), value);
        }

        

    }
}
