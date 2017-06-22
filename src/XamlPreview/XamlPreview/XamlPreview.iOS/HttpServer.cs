using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;

[assembly: Xamarin.Forms.Dependency(typeof(XamlPreview.iOS.HttpServer))]
namespace XamlPreview.iOS
{
    public class HttpServer : XamlPreview.IHttpServer
    {
        HttpListener listener = new HttpListener();

        public void PrefixesAdd(string url)
        {
            listener.Prefixes.Add(url);
        }
        public void Start()
        {
            listener.Start();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    var sr = new System.IO.StreamReader(context.Request.InputStream);
                    var text = sr.ReadToEnd();
                    OnRecv?.Invoke(text);

                    HttpListenerResponse res = context.Response;
                    res.StatusCode = 200;
                    byte[] content = System.Text.Encoding.UTF8.GetBytes("OK");
                    res.OutputStream.Write(content, 0, content.Length);
                    res.Close();
                }
            });
        }
        public event Action<string> OnRecv;

        public string GetUrl()
        {
            var ip = NetworkInterface.GetAllNetworkInterfaces()
                  .Where(ni => ni.Name.Equals("en0"))
                  .First().GetIPProperties().UnicastAddresses
                  .Where(add => add.Address.AddressFamily == AddressFamily.InterNetwork)
                  .First().Address.ToString();
            return string.Format("http://{0}:8080/", ip);
        }
    }
}