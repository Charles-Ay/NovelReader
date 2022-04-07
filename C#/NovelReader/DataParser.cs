using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NovelReader.WebRetriever;

namespace NovelReader
{
    public class DataParser
    {
        public string workingDir { get; private set; }
        public string bookDir { get; private set; }
        private static SQLManager.SQLManager SQLManager = new SQLManager.SQLManager();
        public bool dev = false;

        public DataParser()
        {
            workingDir = GetWorkAndBookDir();
            bookDir = Path.Combine(workingDir, "Books");
        }

        /// <summary>
        /// Class used to get data from website
        /// </summary>
        /// <param name="name"></param>
        /// <param name="first">first chapter to download</param>
        public int Fetch(Novel.Novel novel, int first)
        {
            Scrapper retriever = new Scrapper();
            Search search = new Search();
            List<Novel.Novel> novels;
            if (dev)
            {
                SQLManager.SQLInsertChaptersWithLinks(novel);
                novels = SQLManager.SQLGetNovelChapters(novel.name, novel.source, novel.totalChapters);
            }
            else novels = search.GetNovelChapters(ref novel, ref first);

            return retriever.Scrape(novels, workingDir);
        }

        /// <summary>
        /// Get the current directory and book dir
        /// </summary>
        /// <returns></returns>
        private string GetWorkAndBookDir()
        {
            string dir = Directory.GetCurrentDirectory();

            var files = Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                if (file.Contains("books") || file.Contains("Books"))
                {
                    return dir;
                }
            }
            string newdir = Path.Combine(dir, "Books");
            Directory.CreateDirectory(newdir);
            return dir;
        }
    }
}