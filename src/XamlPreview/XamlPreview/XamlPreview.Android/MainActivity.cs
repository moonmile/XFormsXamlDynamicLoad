using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using Android.Net.Wifi;

namespace XamlPreview.Droid
{
    [Activity(Label = "XamlPreview", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            WifiManager manager = (WifiManager)GetSystemService(Service.WifiService);
            int ip = manager.ConnectionInfo.IpAddress;
            // string ipaddress = Android.Text.Format.Formatter.FormatIpAddress(ip);
            string ipaddress = string.Format("{0}.{1}.{2}.{3}",
                0xFF & ip ,
                (0xFF00 & ip) >> 8,
                (0xFF0000 & ip) >> 16,
                (0xFF000000 & ip) >> 24 );
            MainActivity.IPAddress = ipaddress;
        }
        public static string IPAddress = "";
    }
}

