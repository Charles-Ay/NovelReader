using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using NovelReader.TextLogger;
using HtmlAgilityPack;
using System.Diagnostics;
using static NovelReader.WebRetriever.Sources;

namespace NovelReader.WebRetriever
{
    public class Scrapper
    {
        protected string novelText;
        public int Scrape(List<Novel.Novel> novels, string workingDir)
        {
            //check if link is a valid html source
            if (novels[0].source == "WuxiaWorld") return doPython(novels, workingDir);
            else
            {
                bool returnedValue = false;
                int amount = 0;
                if (!Directory.Exists(Path.Combine(workingDir, "books"))) Directory.CreateDirectory(Path.Combine(workingDir, "books"));
                workingDir = Path.Combine(workingDir, "books");

                ConsoleSpiner spin = new ConsoleSpiner();

                foreach (Novel.Novel novel in novels)
                {
                    Console.WriteLine($"Retriving - {novel.name} chapter {novel.totalChapters}...");

                    var html = GetSite(novel.initalLink);
                    if (novel.initalLink.Contains("freewebnovel")) returnedValue = FreeWebNovelParse(html, out novelText);
                    else if (novel.initalLink.Contains("noveltrench")) returnedValue = NovelTrenchParse(html, out novelText);

                    if (novelText == "")
                    {
                        //throw some internal error
                    }
                    if (returnedValue == false)
                    {
                        throw new InvalidOperationException();
                    }

                    //note that total chapters becomes current chapter
                    string fileName = Path.Combine(workingDir, $"{novel.name} - Chapter {novel.totalChapters}.txt");
                    if (!File.Exists(fileName)) File.Create(fileName).Dispose();
                    TextWriter writer = new StreamWriter(fileName, true);
                    writer.WriteLine(novelText);
                    writer.Close();

                    //Thread.Sleep(System.TimeSpan.FromSeconds(5));
                    ++amount;
                }
                return amount;
            }
        }

        private HtmlAgilityPack.HtmlDocument GetSite(string url)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            HtmlAgilityPack.HtmlDocument html = new HtmlDocument();
            html.OptionFixNestedTags = true;

            var response = httpRequest.GetResponse();
            if (((HttpWebResponse)response).StatusDescription == "OK")
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




        /// <summary>
        /// run the python script for getting website data
        /// </summary>
        public int doPython(List<Novel.Novel> novels, string workingDir)
        {
            string getter = Path.Combine(workingDir, "TextGetter.exe");
            string inputfile = Path.Combine(workingDir, "input.txt");
            int amount = 0;
            ConsoleSpiner spin = new ConsoleSpiner();

            //note that total chapters becomes current chapter
            foreach (Novel.Novel novel in novels)
            {
                string text = novel.initalLink;

                if (!File.Exists(inputfile)) File.Create(inputfile).Dispose();
                TextWriter writer = new StreamWriter(inputfile, false);
                writer.WriteLine(text);

                writer.Close();
                var curprocess = new Process();
                curprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                curprocess.StartInfo.FileName = getter;
                curprocess.Start();
                Console.WriteLine($"Retriving - {novel.name} chapter {novel.totalChapters}...");
                while (!curprocess.HasExited)
                {
                    spin.Turn();
                }
                //Thread.Sleep(System.TimeSpan.FromSeconds(5));
                ++amount;
            }
            return amount;
        }
    }
}
