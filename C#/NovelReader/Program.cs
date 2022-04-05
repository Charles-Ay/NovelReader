using System;
using TextLogger;

namespace NovelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            DataParser parser = new DataParser();
            string name="", link="", source="";
            int maxChapter = 0;
            try
            {
                Console.WriteLine("Please note that currently supported sites are: WuxiaWorld, FreeWebNovel");
                Console.Write("Enter novel name: ");
                name = Console.ReadLine();
                Console.Write("Enter last chapter to download: ");
                maxChapter = Int32.Parse(Console.ReadLine());
                Console.Write("Enter enter novel begining link: ");
                link = Console.ReadLine();

                source = getSupportedSource(link);
                if(source == "")
                {
                    Console.Write("Unsupported source detected please enter source name: ");
                    source = Console.ReadLine();
                }

                //name = "Overgeared";
                //link = "https://freewebnovel.com/overgeared-novel/chapter-1.html";
                //source = "FreeWebNovel";
                //maxChapter = 100;

                Novel.Novel novel = new Novel.Novel(name, maxChapter, link, source);
                int retrivedAmt = parser.Fetch(novel);
                Console.WriteLine($"{retrivedAmt} of {novel.name} have been retrived");
            }
            catch (ArithmeticException e)
            {
                Logger.writeToLog($"ERROR WITH INPUT: {Logger.GetLineNumber(e)} -- {e.Message}");
                Console.WriteLine("INPUT ERROR CHECK LOGS");
                Console.WriteLine("Press any button to continue....");
                Console.ReadLine();
            }


            //USE 4 Testing later
            //DataParser da = new DataParser();
            //Novel.Novel novel = new Novel.Novel("Overgeared", 1601, "https://freewebnovel.com/overgeared-novel/chapter-1.html", "FreeWebNovel");
            //int retrivedAmt = da.Fetch(novel);
            //Console.WriteLine($"{retrivedAmt} of {novel.name} have been retrived");
        }

        private static string getSupportedSource(string url)
        {
            if (url.Contains("freewebnovel")) return "FreeWebNovel";
            else if (url.Contains("wuxiaworld")) return "WuxiaWorld";
            else return "";
        }
    }
}