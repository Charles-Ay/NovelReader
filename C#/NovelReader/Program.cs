using System;
using System.Collections.Generic;
using NovelReader.WebRetriever;

namespace NovelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //NOTE: DONT USE WUXIA WORLD DUE TO SLOW AND EXPENSIVE REQUESTS
            Front userInter = new Front();
            userInter.Init();
            Console.WriteLine("Press Any Button To Exit...");
            Console.ReadLine();
            System.Environment.Exit(0);
            
            //Novel.Novel novel = new Novel.Novel(name, maxChapter, link, source);
            //int retrivedAmt = parser.Fetch(novel);
            //Console.WriteLine($"{retrivedAmt}/{maxChapter} chapters of {novel.name} have been retrived");


            //USE 4 Testing later
            //DataParser da = new DataParser();
            //Novel.Novel novel = new Novel.Novel("Overgeared", 1601, "https://freewebnovel.com/overgeared-novel/chapter-1.html", "FreeWebNovel");
            //int retrivedAmt = da.Fetch(novel);
            //Console.WriteLine($"{retrivedAmt} of {novel.name} have been retrived");
        }
    }
}