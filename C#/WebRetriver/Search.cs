using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NovelReader.TextLogger;

namespace NovelReader.WebRetriever
{

    public class Search
    {
        public List<SearchType> results = new List<SearchType>();
        int numres;
        public bool SearchNovel(int startChapter, string novelname)
        {
            Console.WriteLine($"Searching for {novelname}...");
            bool result = SearchFreeWebNovel(startChapter, novelname);
            return result;
        }

        private bool SearchFreeWebNovel(int startChapter, string novelname)
        {
            var url = "https://freewebnovel.com/search/";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/x-www-form-urlencoded";

            var data = $"searchkey={novelname}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            HtmlAgilityPack.HtmlDocument html = new HtmlDocument();
            html.OptionFixNestedTags = true;

            var response = httpRequest.GetResponse();
            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    html.LoadHtml(reader.ReadToEnd());
                }
            }

            string webtext;
            if (html.DocumentNode != null)
            {
                List<string> name = new List<string>();
                List<string> link = new List<string>();
                List<string> sources = new List<string>();


                //get total amount of results
                try
                {
                    numres = Int32.Parse(html.DocumentNode.SelectSingleNode("//em[@class='num']").InnerText);
                }
                catch(NullReferenceException e)
                {
                    Logger.writeToLog("LOG TEST FRO NULLREF");
                }



                foreach (HtmlNode node in html.DocumentNode.SelectNodes("//h3[@class='tit']"))
                {
                    if(node.InnerText != " Genres" && node.InnerText != " Search Tips")
                    {
                        name.Add(node.InnerText);
                        var newNodes = node.SelectNodes("a");
                        foreach(var innerNode in newNodes)
                        {
                            string tmp = innerNode.GetAttributeValue("href", string.Empty);
                            if(tmp != string.Empty)
                            {
                                tmp = tmp.Replace(".html", "");
                                tmp = tmp.Remove(0,1);
                                tmp = $"https://freewebnovel.com/{tmp}/chapter-{startChapter}.html";
                                link.Add(tmp);
                            }
                            else
                            {
                                //throw some error or return false
                            }
                        }
                        string output;
                        Logger.htmlSupportedWebsites.TryGetValue("freewebnovel", out output);
                        sources.Add(output);
                    }
                }

                if (name.Count != numres || link.Count != numres || sources.Count != numres)
                {
                    //throw error
                }
                for(int i = 0; i < numres; ++i)
                {
                    results.Add(new SearchType(name[i], link[i], sources[i]));
                }
            }
            else
            {
                //ask for new input
            }
            return true;
        }
    }

    public class SearchType
    {
        public SearchType(string name, string link, string source)
        {
            this.name = name;
            this.link = link;
            this.source = source;
        }
        public string name { get; private set; }
        public string link { get; private set; }
        public string source { get; private set; }
    }
}
