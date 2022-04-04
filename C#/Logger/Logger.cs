using System;
using System.IO;
using System.Text;

namespace TextLogger
{
    public class Logger
    {
        private static FileStream file;
        private static string NAME = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        public static Logger TextLogger = new Logger();

        private Logger()
        {
            if (File.Exists(NAME))
            {
                File.Delete(NAME);
            }
        }

        public static void write(string text)
        {
            using (StreamWriter writer = File.CreateText(NAME))
            {
                writer.WriteLine(text);
            }
        }
    }
}
