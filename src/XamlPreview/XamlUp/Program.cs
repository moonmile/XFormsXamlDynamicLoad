using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamlUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = new Program();
            var t = prog.go(args);
            // t.Start();
            t.Wait();
        }

        async Task go (string[] args)
        {
            var url = args[0];
            var path = args[1];

            var fs = System.IO.File.OpenRead(path);
            var sr = new System.IO.StreamReader(fs);
            var xaml = sr.ReadToEnd();
            var hc = new HttpClient();

            var cont = new StringContent(xaml);
            var res = await hc.PostAsync(url, cont);
            var result = await res.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}
