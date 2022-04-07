using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NovelReader.TextLogger;
using System.Text.RegularExpressions;
using System.Text;

namespace NovelReader.WebRetriever
{

    public class Search
    {
        public List<SearchType> results = new List<SearchType>();
        /// <summary>
        /// number of results fetched
        /// </summary>
        int numres;
        public bool SearchNovel(int startChapter, string novelname)
        {
            Console.WriteLine($"Searching for {novelname}...");
            //bool result = SearchFreeWebNovel(startChapter, novelname);
            bool result = SearchNovelTrench(startChapter, novelname);
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
                    Logger.writeToLog("LOG TEST FOR NULL REF");
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


        private bool SearchNovelTrench(int startChapter, string novelname)
        {
            string srchqry = novelname.Replace(" ", "+");
            var url = $"https://noveltrench.com/?s={srchqry}&post_type=wp-manga&op=&author=&artist=&release=&adult=";

            var html = Request(url);

            if (html.DocumentNode != null)
            {
                List<string> name = new List<string>();
                List<string> link = new List<string>();
                List<string> sources = new List<string>();

                //get total amount of results

                string tmpString = html.DocumentNode.SelectSingleNode("//h1[@class='h4']").InnerText;
                tmpString = Regex.Match(tmpString, @"\d+").Value;
                numres = Int32.Parse(tmpString);



                //get next page
                string nextPage = html.DocumentNode.SelectSingleNode("//div[@class='nav-previous float-left']").InnerText;

                while (nextPage != "DEAD")
                {
                    foreach (HtmlNode node in html.DocumentNode.SelectNodes("//h3[@class='h4']"))
                    {
                        //Console.WriteLine(node.InnerText);
                        name.Add(node.InnerText);
                        var newNodes = node.SelectNodes("a");
                        foreach (var innerNode in newNodes)
                        {
                            string tmp = innerNode.GetAttributeValue("href", string.Empty);
                            if (tmp != string.Empty)
                            {
                                tmp += $"chapter-{startChapter}";
                                link.Add(tmp);
                            }
                            else
                            {
                                //throw some error or return false
                            }
                        }
                        string output;
                        Logger.htmlSupportedWebsites.TryGetValue("noveltrench", out output);
                        sources.Add(output);
                    }
                    var tmpNode = html.DocumentNode.SelectSingleNode("//div[@class='nav-previous float-left']");

                    if (tmpNode != null)
                    {
                        tmpNode = tmpNode.SelectSingleNode("a");
                        nextPage = tmpNode.GetAttributeValue("href", string.Empty);

                        //links for the page are weird, need to do some magic
                        int index = nextPage.IndexOf("wp-manga") + "wp-manga".Length;
                        if (index >= 0) nextPage = nextPage.Substring(0, index);
                        nextPage = nextPage.Replace("&#038;", "&");
                        html = Request(nextPage);
                        tmpNode = tmpNode.SelectSingleNode("a");
                    }
                    else nextPage = "DEAD";
                }

                if (name.Count != numres || link.Count != numres || sources.Count != numres)
                {
                    //throw error
                    throw new InvalidOperationException();
                }
                for (int i = 0; i < numres; ++i)
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

        private HtmlDocument Request(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

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
            return html;
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


