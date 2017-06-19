using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFormsXamlDynamicLoad
{
    /// <summary>
    /// LoadFromXaml拡張メソッドを作る
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
            return;
        }
    }
}
