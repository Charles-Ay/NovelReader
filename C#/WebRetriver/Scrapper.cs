using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebRetriever
{
    public class Scrapper
    {
        public int Scrape(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            HtmlAgilityPack.HtmlDocument html = new HtmlDocument();
            html.OptionFixNestedTags = true;

            var response = httpRequest.GetResponse();
            if(((HttpWebResponse)response).StatusDescription == "OK")
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    html.LoadHtml(reader.ReadToEnd());
                }
            }

            if (html.DocumentNode != null)
            {
                foreach (HtmlNode node in html.DocumentNode.SelectNodes("//div[@class='" + "tx" + "']"))
                {
                    string value = node.InnerText;
                    // etc...
                }
            }
                return 1;
        }
    }
}
