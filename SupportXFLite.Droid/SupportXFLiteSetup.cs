using System;
using Android.App;
using Android.OS;
using Rg.Plugins.Popup;

namespace SupportXFLite.Droid
{
    public static class SupportXFLiteSetup
    {
        public static Activity Activity;

        public static void Initialize(Activity _Activity, Bundle bundle)
        {
            Activity = _Activity;
            Popup.Init(_Activity, bundle);
        }
    }
}