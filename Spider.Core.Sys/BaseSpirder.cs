using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Spider.Core.Sys
{
    public class BaseSpirder : ISpider
    { 
        public IEnumerable<string> GenerateUrlList()
        {
            if (GenerateUrlListEvent != null)
                return GenerateUrlListEvent();
            return new List<string>();
        }

        public void GetContentByUrl(string url, Encoding enCode, Action<string> nextAction)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            var ia = req.BeginGetResponse((asy) =>
            {
                var response = req.EndGetResponse(asy) as HttpWebResponse;
                var stream = response.GetResponseStream();
                string content = string.Empty;
                using (StreamReader sr = new StreamReader(stream, enCode))
                {
                    content = sr.ReadToEnd();
                }
                if (nextAction != null)
                {
                    nextAction(content);
                }
            }, req); 
        }

        public Func<IEnumerable<string>> GenerateUrlListEvent
        {
            get;
            set;
        } 
    }
}
