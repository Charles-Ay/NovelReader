using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace NovelReader
{
    class DataParser
    {
        public static Novel novel;
        public void Fetch(string name)
        {
            string mainDir = GetWorkDir();
            string bookDir = Path.Combine(mainDir, "Books");

            //get from novel site
            doPython(mainDir);

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

        private void doPython(string path)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = path + @"\TextGetter.py";
            start.UseShellExecute = true;
            //start.Arguments = args;//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
    }
}
