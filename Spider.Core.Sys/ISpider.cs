using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spider.Core.Sys
{
    public interface ISpider
    {
        Func<IEnumerable<string>> GenerateUrlListEvent { get; set; }
        IEnumerable<string> GenerateUrlList();

        void GetContentByUrl(string url, Encoding enCode, Action<string> nextAction);


    }
}
