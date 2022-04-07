using System;
using System.Collections.Generic;
using System.IO;

namespace NovelReader.TextLogger
{
    public class Logger
    {
        /// <summary>
        /// Contains the supported sources
        /// </summary>
        public static Dictionary<string, string> htmlSupportedWebsites = new Dictionary<string, string>()
        {
            {"freewebnovel", "FreeWebNovel"}, {"noveltrench", "NovelTrench"}
        };

        private static string LogDir = Path.Combine(Directory.GetCurrentDirectory(), $@"Logs");
        private static string NAME = Path.Combine(LogDir, $"Logs_{DateTime.Now.ToFileTime()}.log");

        private Logger()
        {
            if (!Directory.Exists(LogDir)) Directory.CreateDirectory(LogDir);
        }

        /// <summary>
        /// Write text to log file
        /// </summary>
        /// <param name="text"></param>
        public static void writeToLog(string text)
        {
            if (!File.Exists(NAME)) File.Create(NAME).Dispose();
            TextWriter writer = new StreamWriter(NAME, true);
            writer.WriteLine(text);
            writer.Close();
        }

        /// <summary>
        /// Get the line where error was thrown
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static int GetLineNumber(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
        }
    }

    /// <summary>
    /// Class for spining loading icon
    /// </summary>
    public class ConsoleSpiner
    {
        int counter;
        public ConsoleSpiner()
        {
            counter = 0;
        }
        public void Turn()
        {
            counter++;
            switch (counter % 4)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}