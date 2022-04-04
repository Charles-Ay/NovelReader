using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Novel;
using SQLManager;

namespace NovelReader
{
    class DataParser
    {
        private Novel.Novel novel;
        private static string workingDir;
        private static SQLManager.SQLManager SQLManager = new SQLManager.SQLManager();

        public void DataParse()
        {
            workingDir = GetWorkAndBookDir();
            string bookDir = Path.Combine(workingDir, "Books");
        }

        /// <summary>
        /// Class used to get data from website
        /// </summary>
        /// <param name="name"></param>
        public int Fetch(Novel.Novel novel)
        {
            SQLManager.InsertChaptersWithLinks(novel);
            List<Novel.Novel> novels = SQLManager.GetNovelChapters(novel.name, novel.source);
            return doPython(novels);
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

        /// <summary>
        /// run the python script for getting website data
        /// </summary>
        private int doPython(List<Novel.Novel> novels)
        {
            string getter = Path.Combine(workingDir, "TextGetter.exe");
            string inputfile = Path.Combine(workingDir, "input.txt");
            int amount = 0;

            foreach (Novel.Novel novel in novels)
            {
                string text = novel.initalLink;

                if(!File.Exists(inputfile))File.Create(inputfile).Dispose();
                TextWriter writer = new StreamWriter(inputfile, false);
                writer.WriteLine(text);

                writer.Close();
                Process.Start(getter);
                ++amount;
            }
            return amount;
        }
    }
}