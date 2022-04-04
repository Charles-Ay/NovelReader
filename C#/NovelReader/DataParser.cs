using System.Diagnostics;
using System.IO;
using Novel;

namespace NovelReader
{
    class DataParser
    {
        public static Novel.Novel novel;

        public string workingDir;

        public void Fetch(string name)
        {
            workingDir = GetWorkDir();
            string bookDir = Path.Combine(workingDir, "Books");

            //get from novel site
            doPython();

        }

        private string GetWorkDir()
        {
            string dir = Directory.GetCurrentDirectory();
            //while (!Directory.Exists("./books"))
            //{
            //   
            //    foreach (var file in files)
            //    {
            //        if (file.Contains("books") || file.Contains("Books"))
            //        {
            //            return dir;

            //        }
            //    }
            //    dir = Directory.GetParent(dir).ToString();

            //}

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

        private void doPython()
        {
            
            Process.Start(Path.Combine(workingDir, "TextGetter.exe"));
        }
    }
}
