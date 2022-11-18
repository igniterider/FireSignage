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

        public static string EpisodeId
        {
            get => Preferences.Get(nameof(EpisodeId), null);
            set => Preferences.Set(nameof(EpisodeId), value);
        }

        public static string EpisodeTitle
        {
            get => Preferences.Get(nameof(EpisodeTitle), null);
            set => Preferences.Set(nameof(EpisodeTitle), value);
        }

        public static string EpisodeDescription
        {
            get => Preferences.Get(nameof(EpisodeDescription), null);
            set => Preferences.Set(nameof(EpisodeDescription), value);
        }



    }
}
