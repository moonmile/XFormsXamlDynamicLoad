using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamlPreview
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        IHttpServer sv = null;


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (sv != null)
                return;
            sv = DependencyService.Get<IHttpServer>();
            sv.OnRecv += Sv_OnRecv;
            sv.PrefixesAdd("http://*:8080/");
            sv.Start();

            text1.Text = sv.GetUrl();

        }

        dynamic jsonVM;

        private void Sv_OnRecv(string url, string text)
        {
            if (url == "/api/xaml")
            {

                ContentPage page = new SubPage();
                try
                {
                    page.LoadFromXaml(text);
                }
                catch
                {
                    page = new ErrorPage();
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopToRootAsync();
                    // page.BindingContext = jsonVM;
                    Navigation.PushAsync(page);
                });
            }
            if ( url == "/api/json")
            {
                jsonVM = Newtonsoft.Json.Linq.JObject.Parse(text);
            }
        }
    }

    /// <summary>
    /// ダミーのContentPage
    /// </summary>
    public class SubPage : ContentPage {
        public SubPage() { }
    }

    /// <summary>
    /// 自前で LoadFromXaml を作成する
    /// </summary>
    static class ContentPageExtensions
    {
        public static TXaml LoadFromXaml<TXaml>(this TXaml view, string xaml)
        {
            Load(view, xaml);
            return view;
        }
        private static void Load(object view, string xaml)
        {
            var t = Type.GetType("Xamarin.Forms.Xaml.XamlLoader, Xamarin.Forms.Xaml");
            var mi = t.GetRuntimeMethod("Load", new Type[] { typeof(object), typeof(string) });
            var obj = mi.Invoke(null, new object[] { view, xaml });
        }
    }
}
