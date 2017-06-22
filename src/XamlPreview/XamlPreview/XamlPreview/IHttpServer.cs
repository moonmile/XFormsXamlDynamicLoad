using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlPreview
{
    public interface IHttpServer
    {
        void PrefixesAdd(string url);
        void Start();
        event Action<string> OnRecv;

        string GetUrl();
    }
}
