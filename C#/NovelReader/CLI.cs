using System;
using System.Collections.Generic;
using NovelReader.TextLogger;
namespace NovelReader.UI
{
    class CLI
    {
        /// <summary>
        /// Show help settings
        /// </summary>
        private void Help()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Useage: NovelReader [OPTIONS] [ARGS]");
            Console.WriteLine("Optional: [Source], [Dev_Mode]");
            Console.WriteLine("Dev: There are two [Deb_Mode]: -d and -d2. -d uses CLI while -d2 does not");
            Console.Write("Sources: ");

            string sites = "";
            foreach(var site in Logger.htmlSupportedWebsites)
            {
                sites += site.Key + ", ";
            }
            sites = sites.Remove(sites.Length - 1, 1);
            Console.Write(sites);

            Console.WriteLine();
            Console.WriteLine("Example input: NovelReader \"Martial World\" 1-10");
            Console.WriteLine("Example input(with optional params): -d \"Martial World\" 8 freewebnovel");

            Console.ForegroundColor = ConsoleColor.White;
        }

        public int GetInput(ref string[] args, ref FrontEnd front, DataParser parser)
        {
            if (args.Length == 1 && args[0] == "-h") Help();
            if (args[0] == "-d") parser.dev = true;

            if (args.Length >= 2 && !parser.dev)
            {
                if(args.Length == 2)
                {
                    string name = args[0];
                    var pair = GetMinMax(args[1]);
                    int min = pair.Key;
                    int max = pair.Value;
                    return front.InitCLI(ref parser, ref name, ref min, ref max);
                }
                else if (args.Length == 3)
                {
                    string name = args[0];
                    var pair = GetMinMax(args[1]);
                    int min = pair.Key;
                    int max = pair.Value;
                    return front.InitCLI(ref parser, ref name, ref min, ref max, args[2]);
                }
            }
            else if(args.Length >= 3 && parser.dev)
            {
                if (args.Length == 3)
                {
                    string name = args[1];
                    var pair = GetMinMax(args[2]);
                    int min = pair.Key;
                    int max = pair.Value;
                    return front .InitCLI(ref parser, ref name, ref min, ref max);
                }
                else if (args.Length == 4)
                {
                    string name = args[1];
                    var pair = GetMinMax(args[2]);
                    int min = pair.Key;
                    int max = pair.Value;
                    return front.InitCLI(ref parser, ref name, ref min, ref max, args[3]);
                }
            }
            return 0;
        }

        private KeyValuePair<int, int> GetMinMax(string pairval)
        {
            if (!pairval.Contains("-"))
            {
                try
                {
                    return new KeyValuePair<int, int>(Int32.Parse(pairval), Int32.Parse(pairval));
                }
                catch (Exception e)
                {
                    Logger.writeToLog($"Input error - {e.Message} at line: {Logger.GetLineNumber(e)}, NOT A WHOLE NUMBER");
                    Console.WriteLine("Invalid Input, please try again(refer to logs for further details)");
                    System.Environment.Exit(1);
                }
            }

            var vals = pairval.Split('-');
            string min = vals[0];
            string max = vals[1];

            try
            {
                return new KeyValuePair<int, int>(Int32.Parse(min), Int32.Parse(max));
            }
            catch(Exception e)
            {
                Logger.writeToLog($"Input error - {e.Message} at line: {Logger.GetLineNumber(e)}, NOT A WHOLE NUMBER");
                Console.WriteLine("Invalid Input, please try again(refer to logs for further details)");
                System.Environment.Exit(1);
            }
            return new KeyValuePair<int, int>(Int32.Parse(min), Int32.Parse(max)); ;
        }
    }
}
