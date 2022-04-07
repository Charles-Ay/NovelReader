using System;
using NovelReader.UI;

namespace NovelReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //NOTE: DONT USE WUXIA WORLD/doPython DUE TO SLOW AND EXPENSIVE REQUESTS~Seleniumm Web Driver
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Novel Reader - v1");
            Console.ForegroundColor = ConsoleColor.White;

            DataParser parser = new DataParser();
            CLI cLI = new CLI();
            FrontEnd userInter = new FrontEnd();
            int returned = 0;

            if (args[0] == "-d2") parser.dev = true;

            if (parser.dev) 
            {
                returned = userInter.Init(ref parser);
            }
            else returned = cLI.GetInput(ref args, ref userInter, parser);

            Console.WriteLine($"{returned} files downloaded to: {DataParser.workingDir}");
        }
    }
}