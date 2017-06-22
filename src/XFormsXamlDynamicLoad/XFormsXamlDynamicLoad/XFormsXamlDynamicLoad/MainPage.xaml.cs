using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFormsXamlDynamicLoad.Helpers;

namespace XFormsXamlDynamicLoad
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var xaml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ContentPage xmlns=""http://xamarin.com/schemas/2014/forms""
             xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"">
    <StackLayout>
	    <Label Text=""Hello Sub Page"" />
        <Button Text=""click me!"" Clicked=""Button_Clicked"" />
        <Label Text=""{Binding Count, StringFormat='{0} clicked'}"" />
    </StackLayout>
</ContentPage>
";
            /// XAML を動的にロードする
            this.Navigation.PushAsync(new SubPage().LoadFromXaml(xaml));
        }
    }


    public class SubPageViewModel : ObservableObject
    {
        private int count = 0;
        public int Count
        {
            get { return count; }
            set { SetProperty(ref count, value, nameof(Count)); }
        }
    }
    public class SubPage : ContentPage
    {
        public SubPage()
        {
            vm = new SubPageViewModel();
            this.BindingContext = vm;

        }
        SubPageViewModel vm;


        private void Button_Clicked(object sender, EventArgs e)
        {
            vm.Count++;
        }
    }
}
