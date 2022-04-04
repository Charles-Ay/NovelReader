using System;
using System.IO;

namespace TextLogger
{
    public class Logger
    {
        private static string NAME = Path.Combine(Directory.GetCurrentDirectory(), "Logs.txt");
        private static Logger TextLogger = new Logger();

        private Logger()
        {
            if (File.Exists(NAME))
            {
                File.Delete(NAME);
            }
        }

        public static void writeToLog(string text)
        {
            if (!File.Exists(NAME)) File.Create(NAME).Dispose();
            TextWriter writer = new StreamWriter(NAME, true);
            writer.WriteLine(text);
            writer.Close();
        }

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
            switch (counter % 8)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("/"); break;
                case 2: Console.Write("-"); break;
                case 3: Console.Write("-"); break;
                case 4: Console.Write("\\"); break;
                case 5: Console.Write("\\"); break;
                case 6: Console.Write("|"); break;
                case 7: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}