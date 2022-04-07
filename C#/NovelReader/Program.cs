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
        }
    }
}