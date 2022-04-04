using System.Diagnostics;
using System.IO;
using Novel;

namespace NovelReader
{
    class DataParser
    {
        private static Novel.Novel novel;
        private string workingDir;

        /// <summary>
        /// Class used to get data from website
        /// </summary>
        /// <param name="name"></param>
        public void Fetch(string name)
        {
            workingDir = GetWorkAndBookDir();
            string bookDir = Path.Combine(workingDir, "Books");

            //get from novel site
            doPython();

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
        private void doPython()
        {
            Process.Start(Path.Combine(workingDir, "TextGetter.exe"));
        }
    }
}