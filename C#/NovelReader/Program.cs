﻿using System;

namespace NovelReader
{
    class Program
    {
        static void Main(string[] args)
        {

            //CHANGE LINK https://freewebnovel.com/overgeared-novel/chapter-1.html
            DataParser da = new DataParser();
            Novel.Novel novel = new Novel.Novel("Overgeared", 1601, "https://www.wuxiaworld.com/novel/overgeared/og-chapter-1", "WuxiaWorld");
            int retrivedAmt = da.Fetch(novel);
            Console.WriteLine($"{retrivedAmt} of {novel.name} have been retrived");
        }
    }
}