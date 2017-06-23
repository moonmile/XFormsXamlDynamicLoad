using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XamlPreviewUp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ファイルのドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if ( files != null )
            {
                var file = files[0];

                if ( file.ToLower().EndsWith(".xaml"))
                {
                    xamlPath = file;
                    btn1.Content = System.IO.Path.GetFileName(xamlPath);
                    var url = string.Format("http://{0}:{1}/api/xaml", textIp.Text, 8080);
                    upload(url, xamlPath);
                }
                if (file.ToLower().EndsWith(".json"))
                {
                    jsonPath = file;
                    var url = string.Format("http://{0}:{1}/api/json", textIp.Text, 8080);
                    upload(url, jsonPath);
                }
            }
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (xamlPath != "")
            {
                var url = string.Format("http://{0}:{1}/api/xaml", textIp.Text, 8080);
                upload(url, xamlPath);
            }
            if (jsonPath != "")
            {
                var url = string.Format("http://{0}:{1}/api/json", textIp.Text, 8080);
                upload(url, jsonPath);
            }
        }

        private string xamlPath = "";
        private string jsonPath = "";       // デザイン時のデータバインディング用

        async void upload(string url, string path)
        {
            try
            {
                var fs = System.IO.File.OpenRead(path);
                var sr = new System.IO.StreamReader(fs);
                var xaml = sr.ReadToEnd();
                sr.Close();
                var hc = new HttpClient();
                var cont = new StringContent(xaml);
                var res = await hc.PostAsync(url, cont);
                var result = await res.Content.ReadAsStringAsync();
            } catch { }
        }
    }
}
