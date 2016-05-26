using Spider.Core.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Spider.Zufang.Sys
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpider ispider = new BaseSpirder();
            ispider.GenerateUrlListEvent += () =>
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < 500; i++)
                {
                    temp.Add(string.Format("http://cq.58.com/chuzu/pn{0}/", i));
                }
                return temp;
            };

            foreach (var item in ispider.GenerateUrlList())
            {
                ispider.GetContentByUrl(item, Encoding.UTF8, (content) =>
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(content); 
                    var tbodyNode = doc.DocumentNode.ChildNodes[3].ChildNodes[3].SelectSingleNode("//*[@id='infolist']/table");
                    foreach (HtmlNode itemNode in tbodyNode.ChildNodes)
                    {
                        Console.WriteLine(itemNode.InnerText.Replace("\r\n","").Trim()); 
                    }
                });
            }
            Console.ReadKey();
        }
    }
}
