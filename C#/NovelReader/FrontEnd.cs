using NovelReader.TextLogger;
using System;
using System.Collections.Generic;
using NovelReader.WebRetriever;

namespace NovelReader.UI
{
    public class FrontEnd
    {
        public int Init(ref DataParser parser)
        {
            string name = "";
            int maxChapter = 0, first = 0;
            Console.Write("Enter novel name: ");
            name = Console.ReadLine();
        FIRST_CHAPTER:
            Console.Write("Enter what chapter to first download: ");
            try
            {
                first = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Logger.writeToLog($"Input error - {e.Message} at line: {Logger.GetLineNumber(e)}, NOT A WHOLE NUMBER");
                Console.WriteLine("Invalid Input, please try again(refer to logs for further details)");
                goto FIRST_CHAPTER;
            }

        MAX_CHAPTER:
            Console.Write("Enter how many chapters to download: ");
            try
            {
                maxChapter = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Logger.writeToLog($"Input error - {e.Message} at line: {Logger.GetLineNumber(e)}, NOT A WHOLE NUMBER");
                Console.WriteLine("Invalid Input, please try again(refer to logs for further details)");
                goto MAX_CHAPTER;
            }
            var sRetType = search(ref name, ref first);
            if (sRetType == null) return 0;
            return parser.Fetch(new Novel.Novel(sRetType.name, maxChapter, sRetType.link, sRetType.source), first);
        }

        public int InitCLI(ref DataParser parser, ref string name, ref int first, ref int max, string source= "")
        {
            var sRetType = search(ref name, ref first, source);
            if (sRetType == null) return 0;
            return parser.Fetch(new Novel.Novel(sRetType.name, max, sRetType.link, sRetType.source), first);
        }

        //returs link we are using
        public SearchType search(ref string novelname, ref int startingChapter, string source="")
        {
            Search search = new Search();
            search.SearchNovel(startingChapter, novelname, source);
            Console.WriteLine();
            Console.WriteLine($"Search returned {search.results.Count} result(s):");
            for(int i = 0;i < search.results.Count; ++i)
            {
                Console.WriteLine($"Enter {i + 1} to choose: {search.results[i].name}");
            }
            if (search.results.Count == 0) return null;
        SEARCH_INPUT:
            int input = 0;
            try
            {
                input = Int32.Parse(Console.ReadLine());
            }
            catch(Exception e)
            {
                Logger.writeToLog($"Search input error - {e.Message} at line: {Logger.GetLineNumber(e)}, NOT A WHOLE NUMBER");
                Console.WriteLine("Invalid Input, please try again(refer to logs for further details)");
                goto SEARCH_INPUT;
            }

            if (input <= 0 || input > search.results.Count)
            {
                Logger.writeToLog($"Search input error. Enter a valid number");
                Console.WriteLine("Invalid Input, please try again(refer to logs for further details)");
                goto SEARCH_INPUT;
            }
            return search.results[input - 1];
        }
    }
}
