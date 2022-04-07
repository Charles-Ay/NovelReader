using HtmlAgilityPack;
using NovelReader.TextLogger;
using System;
using System.Text;
using System.Web;

namespace NovelReader.WebRetriever
{
    public static class Sources
    {
        public static bool FreeWebNovelParse(HtmlDocument html, out string novelText)
        {
            novelText = "";
            if (html.DocumentNode != null)
            {
                try
                {
                    foreach (HtmlNode node in html.DocumentNode.SelectNodes("//h1[@class='tit']"))
                    {
                        novelText = node.InnerText;
                    }

                    foreach (HtmlNode node in html.DocumentNode.SelectNodes("//div[@class='txt ']"))
                    {
                        novelText = node.InnerText;
                    }
                }
                catch (Exception e)
                {
                    Logger.writeToLog($"HTML ERROR - Line:{Logger.GetLineNumber(e)} -- {e.Message}");
                    return false;
                }
            }
            else return false;
            return true;
        }

        public static bool NovelTrenchParse(HtmlDocument html, out string novelText)
        {
            novelText = "";
            if (html.DocumentNode != null)
            {
                try
                {
                    foreach (HtmlNode node in html.DocumentNode.SelectNodes("//div[@class='text-left']"))
                    {
                        novelText = HttpUtility.HtmlDecode(node.InnerText);
                    }
                }
                catch (Exception e)
                {
                    Logger.writeToLog($"HTML ERROR - Line:{Logger.GetLineNumber(e)} -- {e.Message}");
                    return false;
                }
            }
            else return false;
            return true;
        }
    }
}
